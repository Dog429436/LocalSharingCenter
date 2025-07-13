using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LocalSharingCenterHome
{
    /// <summary>
    /// A class for client side socket operations
    /// </summary>
    public class ServerConnection
    {
        private TcpClient tcpClient; 
        private NetworkStream stream; 
        private StreamReader reader; 
        private StreamWriter writer; 
        private string serverRsaPublicKey;
        private byte[] clientAesKey; 
        private byte[] clientAesVector;
        private int filesCount = 0;
        private bool isConnected = false;
        private static Random rnd = new Random();

        public ServerConnection()
        {
            
        }

        public bool IsConnected()
        {
            return this.isConnected;
        }

        /// <summary>
        /// Sends a PointRequest packet to the server.
        /// If a server is active, he will respond and its IP address is returned in order to connect.
        /// </summary>
        /// <returns>The server's IP address as a string if there is one active, otherwise, returns "/"</returns>
        private string requestServerIp()
        {
            using (UdpClient senderSocket = new UdpClient() { EnableBroadcast = true })
            {
                byte[] bitMsg = Encoding.ASCII.GetBytes(Protocol.Connection.PointRequest.ToString());
                IPEndPoint receiveEndPoint = new IPEndPoint(IPAddress.Any, Protocol.UDP_PORT);
                List<IPEndPoint> addresses = Protocol.SubnetsBroadcast(Protocol.UDP_PORT);
                senderSocket.Client.ReceiveTimeout = 500;
                foreach (IPEndPoint address in addresses)
                {
                    try
                    {
                        senderSocket.Send(bitMsg, bitMsg.Length, address);
                        byte[] receivedData = senderSocket.Receive(ref receiveEndPoint);
                        string response = Encoding.UTF8.GetString(receivedData);
                        if (response == Protocol.Connection.PointResponse.ToString())
                        {
                            return receiveEndPoint.Address.ToString();

                        }

                    }
                    catch (Exception) { }
                }
                return "/";
            }
        }

        /// <summary>
        /// Handles the server connection by discovering and authenticating the server, receiving its RSA public key and sending AES parameters encrypted with the public key.
        /// </summary>
        /// <returns>The Task that represents the asynchronous connection operation.</returns>
        public async Task ConnectToServer(RichTextBox MenuLogs)
        {
            if ( this.IsConnected())
            {
                return;
            }

            #region Server discovery
            string serverIp = requestServerIp();
            if (serverIp == "/")
            {
                await InterfaceHelper.WriteMessage("There isnt a server active ", MenuLogs, true);
                return;
            }
            await InterfaceHelper.WriteMessage("Server ip acquired", MenuLogs, true);
            #endregion

            #region Client tcpclient creation
            this.tcpClient = new TcpClient(serverIp, Protocol.TCP_PORT);
            this.stream = tcpClient.GetStream();
            this.reader = new StreamReader(stream, Encoding.UTF8);
            this.writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
            #endregion

            #region Rsa key exchange
            try
            {
                string data = this.reader.ReadLine();
                if (data == null)
                {
                    await InterfaceHelper.WriteMessage("Connection failed, server unresponsive", MenuLogs, true);
                    this.CloseTcpConnection();
                    return;
                }
                string[] parts = data.Split('|');
                if (parts[0] != Protocol.Connection.ServerKey.ToString())
                {
                    await InterfaceHelper.WriteMessage("Rsa keys error", MenuLogs, true);
                    this.CloseTcpConnection();
                    return;
                }
                this.serverRsaPublicKey = parts[1];
                await InterfaceHelper.WriteMessage("Recieved server rsa public key", MenuLogs, true);
            }
            catch (Exception ex)
            {
                await InterfaceHelper.WriteMessage("Connection failed, server unresponsive", MenuLogs, true);
                this.CloseTcpConnection();
                return;
            }
            #endregion

            #region Sending Aes key
            using (Aes aes = Aes.Create())
            {
                this.clientAesKey = aes.Key;
                this.clientAesVector = aes.IV;
            }
            string aesKey = Convert.ToBase64String(this.clientAesKey);
            string aesVector = Convert.ToBase64String(this.clientAesVector);
            string clientKeyFields = aesKey + "|" + aesVector;
            byte[] encryptedAesFields;
            string aesMessage = "";
            string clientMessage = "";
            using (RSA rsaAes = RSA.Create())
            {
                rsaAes.FromXmlString(this.serverRsaPublicKey);
                encryptedAesFields = rsaAes.Encrypt(Encoding.UTF8.GetBytes(clientKeyFields), RSAEncryptionPadding.Pkcs1);
                aesMessage = Convert.ToBase64String(encryptedAesFields);
                clientMessage = Protocol.Connection.ClientAesKey.ToString() + "|" + aesMessage;
            }
            try
            {
                this.writer.WriteLine(clientMessage);
                await InterfaceHelper.WriteMessage("Server was sent aes key", MenuLogs, true);
                string response = this.reader.ReadLine();
                if (response == null)
                {
                    await InterfaceHelper.WriteMessage("Connection failed, server unresponsive", MenuLogs, true);
                    this.CloseTcpConnection();
                    return;
                }
                string message = AesHelper.DecryptAesMessageString(response, this.clientAesKey, this.clientAesVector);
                if (message == Protocol.Status.ok.ToString())
                {
                    await InterfaceHelper.WriteMessage("Successfully connected to server", MenuLogs, true);
                    this.isConnected = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                await InterfaceHelper.WriteMessage("Connection failed, server unresponsive", MenuLogs, true);
                this.CloseTcpConnection();
            }
            #endregion

            return;
        }
        

       /// <summary>
       /// Handles the server's responses, including the custom file transfer protocol that I implemented.
       /// </summary>
       /// <param name="downloadButton">The button used to download selected files.</param>
       /// <param name="fileList">The list box displaying the available files on the server.</param>
       /// <param name="fileBox">The rich text box showing logs of process messages.</param>
       /// <param name="fileBar">The progress bar indicating the progress of file transfer.</param>
        public void ResponseHandle(Button downloadButton, ListBox fileList, RichTextBox fileBox, ProgressBar fileBar) 
        {
            List<byte[]> parts = new List<byte[]>();
            string fileName = "";
            int currentFileNum = 0;
            int numOfFiles = 0;
            long totalBytes = 0;
            long currentBytes = 0;
            bool isRecievingFiles = false;
            fileBar.Minimum = 0;
            fileBar.Maximum = 100;
            Thread clientThread = new Thread(async () =>
            {
                while (this.isConnected)
                {
                    try
                    {
                        string encryptedResponse = this.reader.ReadLine();
                        if (encryptedResponse == null)
                        {
                            throw new Exception();
                        }

                        string response = AesHelper.DecryptAesMessageString(encryptedResponse, clientAesKey, this.clientAesVector);
                        string[] fileFields = response.Split('|');
                        if (response.StartsWith("FILES:"))
                        {
                            string[] files = response.Substring(6).Split('|');
                            if (fileList.InvokeRequired)
                            {
                                fileList.Invoke(new Action(() =>
                                {
                                    fileList.Items.Clear();
                                    fileList.Items.AddRange(files);
                                }));
                            }
                            else
                            {
                                fileList.Items.Clear();
                                fileList.Items.AddRange(files);
                            }
                        }
                        else if (response.StartsWith(Protocol.FileTransferFields.FILE_BEGIN.ToString()))
                        {
                            string[] fields = response.Split('|');
                            fileName = fields[1];
                            currentFileNum = int.Parse(fields[2]) + 1;
                            numOfFiles = int.Parse(fields[3]);
                            totalBytes = long.Parse(fields[4]);
                            currentBytes = 0;
                            fileName = Path.GetFileName(fileName);
                            if (!Directory.Exists(FileTransfer.FOLDER))
                            {
                                await InterfaceHelper.WriteMessage("No target folder found, creating...", fileBox, true);
                                Directory.CreateDirectory(FileTransfer.FOLDER);
                            }
                            parts.Clear();
                            isRecievingFiles = true;
                            await InterfaceHelper.WriteMessage(string.Format("File transfer started: ({0}/{1})", currentFileNum, numOfFiles), fileBox, true);

                        }
                        else if (response.StartsWith(Protocol.FileTransferFields.FILE_PIECE.ToString()))
                        {
                            if (isRecievingFiles)
                            {
                                string[] encodedData = response.Split('|');
                                currentBytes = long.Parse(encodedData[2]);
                                int percent = (int)((currentBytes * 100) / totalBytes);
                                fileBar.Invoke(new Action(() =>
                                {
                                    fileBar.Value = percent;
                                }));
                                byte[] encryptedData = Convert.FromBase64String(encodedData[1]);
                                byte[] data = AesHelper.DecryptAesMessageBytes(encryptedData, this.clientAesKey, this.clientAesVector);
                                parts.Add(data);

                            }
                        }
                        else if (response.StartsWith(Protocol.FileTransferFields.FILE_END.ToString()))
                        {
                            if (isRecievingFiles && fileName != "")
                            {
                                string fullPath = Path.Combine(FileTransfer.FOLDER, fileName);
                                using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                                {
                                    foreach (byte[] part in parts)
                                    {
                                        fs.Write(part, 0, part.Length);
                                    }
                                }
                                isRecievingFiles = false;
                                fileName = "";
                                parts.Clear();
                                this.writer.WriteLine(AesHelper.EncryptToAesMessageString(string.Format("{0}|{1}", Protocol.Status.ok.ToString(), currentFileNum - 1), this.clientAesKey, this.clientAesVector));
                                await InterfaceHelper.WriteMessage(string.Format("File transfer ended, recieved file: ({0}/{1})", currentFileNum, numOfFiles), fileBox, true);
                                if (currentFileNum == numOfFiles)
                                {
                                    await InterfaceHelper.WriteMessage(string.Format("Successfully recieved all of the files: ({0}/{1})", currentFileNum, numOfFiles), fileBox, true);
                                    currentFileNum = 1;
                                    numOfFiles = 0;
                                }
                                else
                                {
                                    currentFileNum++;
                                }
                                if (downloadButton.InvokeRequired)
                                {
                                    downloadButton.Invoke(new Action(() => downloadButton.Enabled = true));
                                }
                                else
                                {
                                    downloadButton.Invoke(new Action(() => downloadButton.Enabled = true));
                                }
                            }
                        }
                        else if (response.StartsWith(Protocol.FileTransferFields.NO_FILE.ToString()))
                        {
                            await InterfaceHelper.WriteMessage(string.Format("Sorry, the server couldn't find the requested file"), fileBox, true);
                            if (downloadButton.InvokeRequired)
                            {
                                downloadButton.Invoke(new Action(() => downloadButton.Enabled = true));
                            }
                            else
                            {
                                downloadButton.Invoke(new Action(() => downloadButton.Enabled = true));
                            }
                        }
                        else if (fileFields.Length == 3 && fileFields[0] == Protocol.Status.ok.ToString())
                        {
                            currentFileNum = int.Parse(fileFields[1]) + 1;
                            filesCount = int.Parse(fileFields[2]);
                            await InterfaceHelper.WriteMessage(string.Format("{0} successfully received a file: ({1}/{2})", "server", currentFileNum, fileFields[2]), fileBox, true);
                            if (currentFileNum == filesCount)
                            {
                                await InterfaceHelper.WriteMessage(string.Format("{0} successfully received all of the files: ({1}/{2})", "server", currentFileNum, filesCount), fileBox, true);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        if (downloadButton.InvokeRequired)
                        {
                            downloadButton.Invoke(new Action(() => downloadButton.Enabled = true));
                        }
                        else
                        {
                            downloadButton.Invoke(new Action(() => downloadButton.Enabled = true));
                        }
                        await InterfaceHelper.WriteMessage("Server connection lost, you can return to menu and try again", fileBox, true);
                        CloseTcpConnection();
                    }
                }
            });
            clientThread.IsBackground = true;
            clientThread.Start();
        }
        
        /// <summary>
        /// Lists the available files on the server.
        /// </summary>
        /// <param name="FileBox">The list box displaying the file names.</param>
        /// <returns>A task representing the asynchronous list file operation.</returns>
        public async Task ListFilesButton(RichTextBox FileBox)
        {
            try
            {
                string command = AesHelper.EncryptToAesMessageString(Protocol.Commands.List.ToString(), clientAesKey, this.clientAesVector);
                this.writer.WriteLine(command);
            }
            catch (Exception ex)
            {
                CloseTcpConnection();
                string str = "Server connection lost, you can return to menu and try again";
                await InterfaceHelper.WriteMessage(str, FileBox, true);
            }
        }

        /// <summary>
        /// Requests to download the selected files.
        /// </summary>
        /// <param name="message">The list of files to download, each file name is separated by '|'.</param>
        /// <param name="FileBox">The rich text box displaying the logs.</param>
        /// <returns>A task representing the asynchronous download request operation.</returns>
        public async Task DownloadFilesButton(string message, RichTextBox FileBox)
        {
            try
            {
                string command = AesHelper.EncryptToAesMessageString(message, clientAesKey, clientAesVector);
                this.writer.WriteLine(command);
                string str = "Files request sent";
                await InterfaceHelper.WriteMessage(str, FileBox, true);
            }
            catch (Exception ex)
            {
                CloseTcpConnection();
                string str = "Server connection lost, you can return to menu and try again\n";
                await InterfaceHelper.WriteMessage(str, FileBox, true);
            }
        }

        /// <summary>
        /// Uploads the selected files to the server.
        /// </summary>
        /// <param name="selectedFiles">An array of selected file names.</param>
        /// <param name="FileBox">The rich text box displaying the logs.</param>
        /// <param name="FileBar">The progress bar indicating the progress of file transfer.</param>
        /// <returns>A task representing the asynchronous file upload operation.</returns>
        public async Task UploadFilesButton(string[] selectedFiles, RichTextBox FileBox, ProgressBar FileBar)
        {
            this.filesCount = selectedFiles.Length;
            await Task.Run(async () =>
            {
                await FileTransfer.SendFiles(selectedFiles, "client", "server", this.clientAesKey, clientAesVector, null, this.writer, FileBox, FileBar);
            });
        }

        
        /// <summary>
        /// Closes the TCP connection and disposes of the related stream, reader, and writer objects.
        /// </summary>
        public void CloseTcpConnection()
        {
            this.reader.Close();
            this.writer.Close();
            this.stream.Close();
            this.tcpClient.Close();
            this.isConnected = false;
        }
        
    }
}
