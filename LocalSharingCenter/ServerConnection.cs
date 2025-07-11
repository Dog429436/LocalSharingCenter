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

namespace LocalSharingCenter
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
        /// Sends a PointRequest packet to the server and authenticates it by verifying the server's signature using its public RSA key.
        /// If the server is successfully authenticated its IP address is returned in order to connect.
        /// </summary>
        /// <returns>The server's IP address as a string if authentication succeeds, otherwise, returns "/"</returns>
        private string requestServerAuthentication()
        {
            UdpClient senderSocket = new UdpClient() { EnableBroadcast = true }; 
            int num = rnd.Next(); 
            byte[] bitMsg = Encoding.ASCII.GetBytes(Protocol.Connection.PointRequest.ToString() + "|" + num); 
            IPEndPoint receiveEndPoint = new IPEndPoint(IPAddress.Any, 0);
            List<IPEndPoint> addresses = Protocol.SubnetsBroadcast(Protocol.UDP_PORT);
            foreach (IPEndPoint address in addresses)
            {
                senderSocket.Send(bitMsg, bitMsg.Length, address);
            }
            senderSocket.Client.ReceiveTimeout = 500;
            try
            {
                byte[] receivedData = senderSocket.Receive(ref receiveEndPoint); 
                string response = Encoding.UTF8.GetString(receivedData); 
                string[] parts = response.Split('|'); 
                if (parts.Length == 2 && parts[0] == Protocol.Connection.PointResponse.ToString())
                {
                    byte[] signature = Convert.FromBase64String(parts[1]);
                    byte[] numBytes = Encoding.UTF8.GetBytes(num.ToString());
                    using (RSA rsa = RSA.Create())
                    {
                        rsa.FromXmlString(Protocol.ServerPublicKey);
                        bool answer = rsa.VerifyData(numBytes, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                        if (answer)
                        {
                            return receiveEndPoint.Address.ToString();
                        }

                    }

                }

            }
            catch (Exception) { } 
            finally
            {
                senderSocket.Close(); 
            }
            return "/";

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
            string serverIp = requestServerAuthentication();
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
       /// <param name="DownloadButton">The button used to download selected files.</param>
       /// <param name="FileList">The list box displaying the available files on the server.</param>
       /// <param name="FileBox">The rich text box showing logs of process messages.</param>
       /// <param name="FileBar">The progress bar indicating the progress of file transfer.</param>
        public void ResponseHandle(Button DownloadButton, ListBox FileList, RichTextBox FileBox, ProgressBar FileBar) 
        {
            List<byte[]> parts = new List<byte[]>();
            string fileName = "";
            int currentFileNum = 0;
            int numOfFiles = 0;
            long totalBytes = 0;
            long currentBytes = 0;
            bool isRecievingFiles = false;
            FileBar.Minimum = 0;
            FileBar.Maximum = 100;
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
                            if (FileList.InvokeRequired)
                            {
                                FileList.Invoke(new Action(() =>
                                {
                                    FileList.Items.Clear();
                                    FileList.Items.AddRange(files);
                                }));
                            }
                            else
                            {
                                FileList.Items.Clear();
                                FileList.Items.AddRange(files);
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
                                await InterfaceHelper.WriteMessage("No target folder found, creating...", FileBox, true);
                                Directory.CreateDirectory(FileTransfer.FOLDER);
                            }
                            parts.Clear();
                            isRecievingFiles = true;
                            await InterfaceHelper.WriteMessage(string.Format("File transfer started: ({0}/{1})", currentFileNum, numOfFiles), FileBox, true);

                        }
                        else if (response.StartsWith(Protocol.FileTransferFields.FILE_PIECE.ToString()))
                        {
                            if (isRecievingFiles)
                            {
                                string[] encodedData = response.Split('|');
                                currentBytes = long.Parse(encodedData[2]);
                                int percent = (int)((currentBytes * 100) / totalBytes);
                                FileBar.Invoke(new Action(() =>
                                {
                                    FileBar.Value = percent;
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
                                await InterfaceHelper.WriteMessage(string.Format("File transfer ended, recieved file: ({0}/{1})", currentFileNum, numOfFiles), FileBox, true);
                                if (currentFileNum == numOfFiles)
                                {
                                    await InterfaceHelper.WriteMessage(string.Format("Successfully recieved all of the files: ({0}/{1})", currentFileNum, numOfFiles), FileBox, true);
                                    currentFileNum = 1;
                                    numOfFiles = 0;
                                }
                                else
                                {
                                    currentFileNum++;
                                }
                                if (DownloadButton.InvokeRequired)
                                {
                                    DownloadButton.Invoke(new Action(() => DownloadButton.Enabled = true));
                                }
                                else
                                {
                                    DownloadButton.Invoke(new Action(() => DownloadButton.Enabled = true));
                                }
                            }
                        }
                        else if (response.StartsWith(Protocol.FileTransferFields.NO_FILE.ToString()))
                        {
                            await InterfaceHelper.WriteMessage(string.Format("Sorry, the server couldn't find the requested file"), FileBox, true);
                            if (DownloadButton.InvokeRequired)
                            {
                                DownloadButton.Invoke(new Action(() => DownloadButton.Enabled = true));
                            }
                            else
                            {
                                DownloadButton.Invoke(new Action(() => DownloadButton.Enabled = true));
                            }
                        }
                        else if (fileFields.Length == 3 && fileFields[0] == Protocol.Status.ok.ToString())
                        {
                            currentFileNum = int.Parse(fileFields[1]) + 1;
                            filesCount = int.Parse(fileFields[2]);
                            await InterfaceHelper.WriteMessage(string.Format("{0} successfully received a file: ({1}/{2})", "server", currentFileNum, fileFields[2]), FileBox, true);
                            if (currentFileNum == filesCount)
                            {
                                await InterfaceHelper.WriteMessage(string.Format("{0} successfully received all of the files: ({1}/{2})", "server", currentFileNum, filesCount), FileBox, true);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        if (DownloadButton.InvokeRequired)
                        {
                            DownloadButton.Invoke(new Action(() => DownloadButton.Enabled = true));
                        }
                        else
                        {
                            DownloadButton.Invoke(new Action(() => DownloadButton.Enabled = true));
                        }
                        await InterfaceHelper.WriteMessage("Server connection lost, you can return to menu and try again", FileBox, true);
                        CloseTcpConnection();
                    }
                }
            });
            clientThread.IsBackground = true;
            clientThread.Start();
        }
        
        /// <summary>
        /// Handles the client login attempt.
        /// </summary>
        /// <param name="username">The client's username.</param>
        /// <param name="password">The client's password.</param>
        /// <param name="messages">The rich text box displaying the logs.</param>
        /// <returns>A Task that returns true or false based on a successful login attempt.</returns>
        public async Task<bool> TryLogin(string username, string password, RichTextBox messages)
        {
            try
            {
                string checkUser = Protocol.Connection.CheckUser.ToString() + "|" + username + "|" + password;
                string request = AesHelper.EncryptToAesMessageString(checkUser, this.clientAesKey, this.clientAesVector);
                this.writer.WriteLine(request);
                string response = this.reader.ReadLine();
                if (response == null)
                {
                    await InterfaceHelper.WriteMessage("Server connection lost, you can try to connect again", messages, true);
                    CloseTcpConnection();
                    return false;
                }
                string message = AesHelper.DecryptAesMessageString(response, this.clientAesKey, this.clientAesVector);
                if (message == Protocol.Status.no.ToString())
                {
                    await InterfaceHelper.WriteMessage("Login failed", messages, true);
                    return false;
                }
                await InterfaceHelper.WriteMessage("Login successfull", messages, true);
                return true;
            }
            catch (Exception ex)
            {
                await InterfaceHelper.WriteMessage("Server connection lost, you can try to connect again", messages, true);
                this.CloseTcpConnection();
            }
            return false;
        }

        /// <summary>
        /// Handles the client signup attempt and validates the password's strength.
        /// </summary>
        /// <param name="username">The client's username.</param>
        /// <param name="password">The client's password.</param>
        /// <param name="signupMessages">The rich text box displaying the logs.</param>
        /// <returns>A task that represents the signup attempt.</returns>
        public async Task TrySignUp(string username, string password, RichTextBox signupMessages)
        {
            string specialChars = "!@#$%^&*()-_=+[]{};:'\"\\|,.<>/?`~";
            int uppercaseCharCount = 0;
            int numberCount = 0;
            int specialCharsCount = 0;
            bool hasWhiteSpaces = false;
            bool securedPassword = true;
            if (password.Length < 8)
            {
                await InterfaceHelper.WriteMessage("Password is too short", signupMessages, true);
                securedPassword = false;
            }
            for (int i = 0; i < password.Length; i++)
            {
                if ((int)password[i] >= 65 && (int)password[i] <= 90)
                {
                    uppercaseCharCount++;
                }
                if ((int)password[i] >= 48 && (int)password[i] <= 57)
                {
                    numberCount++;
                }
                for (int j = 0; j < specialChars.Length; j++) 
                {
                    if (password[i] == specialChars[j])
                    {
                        specialCharsCount++;
                    }
                }
                if (password[i] == ' ') 
                {
                    hasWhiteSpaces = true;
                }
            }
            if (hasWhiteSpaces)
            {
                await InterfaceHelper.WriteMessage("Password has a whitespace", signupMessages, true);
                securedPassword = false;
            }
            if (uppercaseCharCount < 1)
            {
                await InterfaceHelper.WriteMessage("Password is missing an uppercase character", signupMessages, true);
                securedPassword = false;
            }
            if (numberCount < 1)
            {
                await InterfaceHelper.WriteMessage("Password is missing a digit", signupMessages, true); 
                securedPassword = false;
            }
            if (specialCharsCount < 1) 
            {
                await InterfaceHelper.WriteMessage("Password is missing a special character", signupMessages, true); 
                securedPassword = false;
            }
            if (securedPassword) 
            {
                string registerFields = Protocol.Connection.RegisterUser.ToString() + '|' + username + '|' + password;
                string clientRegister = AesHelper.EncryptToAesMessageString(registerFields, this.clientAesKey, this.clientAesVector);
                try
                {
                    this.writer.WriteLine(clientRegister);
                    string response = this.reader.ReadLine();
                    if (response == null)
                    {
                        await InterfaceHelper.WriteMessage("Server connection lost, you can try to connect again", signupMessages, true);
                        CloseTcpConnection();
                        return;
                    }
                    string message = AesHelper.DecryptAesMessageString(response, this.clientAesKey, this.clientAesVector);
                    if (message == Protocol.Status.no.ToString())
                    {
                        await InterfaceHelper.WriteMessage("Username already used", signupMessages, true);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    await InterfaceHelper.WriteMessage("Server connection lost, you can try to connect again", signupMessages, true);
                    CloseTcpConnection();
                    return;
                }
                await InterfaceHelper.WriteMessage("You registered successfully you may log in", signupMessages, true);
            }
            
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
