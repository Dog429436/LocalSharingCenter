using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalSharingCenter
{
    /// <summary>
    /// A class for server side socket operations
    /// </summary>
    public class ConnectedClient
    {
        public TcpClient client;
        public string username;
        public string clientIp;
        public int userId;
        public NetworkStream stream;
        public StreamReader reader;
        public StreamWriter writer;
        public byte[] clientAesKey;
        public byte[] clientAesVector;
        public RichTextBox serverTextBox;
        public ListBox serverListBox;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectedClient"/> class using the specified TcpClient and the client IP.
        /// </summary>
        /// <param name="client">The accepted client TCP connection.</param>
        /// <param name="ip">The client IP address.</param>
        /// <param name="serverTextBox">The RichTextBox used for logging operations.</param>
        /// <param name="serverListBox">The ListBox displaying a list of connected clients.</param>
        public ConnectedClient(TcpClient client, string ip, RichTextBox serverTextBox, ListBox serverListBox)
        {
            #region clientConnection
            this.client = client;
            this.clientIp = ip;
            this.stream = client.GetStream();
            this.reader = new StreamReader(stream);
            this.writer = new StreamWriter(stream) { AutoFlush = true };
            this.serverTextBox = serverTextBox;
            this.serverListBox = serverListBox;
            #endregion
        }

        /// <summary>
        /// Establishes a secure connection between the client and server using RSA and AES encryption.
        /// </summary>
        /// <returns>A task that sends the server's RSA public key and receives the client's AES key and IV encrypted with RSA.</returns>
        public async Task Connect()
        {
            try
            {
                this.writer.WriteLine(Protocol.Connection.ServerKey.ToString() + "|" + Server.serverPublicKey);
                await InterfaceHelper.WriteMessage("A client was sent rsa server public key", this.serverTextBox, true);
                string clientMessage = this.reader.ReadLine();
                string[] parts = clientMessage.Split('|');
                if (parts.Length == 2 && parts[0] == Protocol.Connection.ClientAesKey.ToString())
                {
                    using (RSA serverRsa = RSA.Create())
                    {
                        serverRsa.FromXmlString(Server.serverPrivateKey);
                        byte[] clientAesKey = serverRsa.Decrypt(Convert.FromBase64String(parts[1]), RSAEncryptionPadding.Pkcs1);
                        string client = Encoding.UTF8.GetString(clientAesKey);
                        string[] aesFields = client.Split('|');
                        this.clientAesKey = Convert.FromBase64String(aesFields[0]);
                        this.clientAesVector = Convert.FromBase64String(aesFields[1]);
                    }
                    string response = AesHelper.EncryptToAesMessageString(Protocol.Status.ok.ToString(), this.clientAesKey, this.clientAesVector);
                    this.writer.WriteLine(response);
                    await InterfaceHelper.WriteMessage("A client sent his aes key", this.serverTextBox, true);
                    await InterfaceHelper.WriteMessage("A client successfully connected", this.serverTextBox, true);
                }
            }
            catch (Exception ex)
            {
                await InterfaceHelper.WriteMessage("A client didn't respond, closing connection...", this.serverTextBox, true);
                this.CloseConnection();

            }
        }

        /// <summary>
        /// Sends the list of available files in the shared folder to the client.
        /// If the shared folder does not exist, it is created.
        /// </summary>
        public async void ListFiles()
        {
            if (!Directory.Exists(FileTransfer.FOLDER))
            {
                Directory.CreateDirectory(FileTransfer.FOLDER);
                await InterfaceHelper.WriteMessage("No shared folder found, creating...", this.serverTextBox, true);
            }
            string[] files = Directory.GetFiles(FileTransfer.FOLDER);
            string response = "FILES:" + string.Join("|", files.Select(Path.GetFileName));
            string encryptedRespond = AesHelper.EncryptToAesMessageString(response, this.clientAesKey, this.clientAesVector);
            this.writer.WriteLine(encryptedRespond);
            await InterfaceHelper.WriteMessage(string.Format("{0} requested to list files", this.username), this.serverTextBox, true);
        }

        /// <summary>
        /// Handles the client requests with the appropriate responses:
        /// - List: Calls <see cref="ListFiles"/> to send the list of files.
        /// - Get: Calls <see cref="FileTransfer.SendFiles"/> to send the requested files to the client using chunked transfer with the following headers:
        ///  - FILE_BEGIN: Indicates the start of a new file.
        ///  - FILE_PIECE: Represents a chunk of a file.
        ///  - FILE_END: Indicates the end of a file.
        /// </summary>
        public async void ClientHandle()
        {
            List<byte[]> parts = new List<byte[]>();
            string fileName = "";
            int currentFileNum = 0;
            int numOfFiles = 0;
            long totalBytes = 0;
            long currentBytes = 0;
            bool isRecievingFiles = false;
            while (true)
            {
                try
                {
                    string clientMessage = this.reader.ReadLine();
                    if (clientMessage == null)
                    {
                        break;
                    }
                    string message = AesHelper.DecryptAesMessageString(clientMessage, this.clientAesKey, this.clientAesVector);
                    string[] fields = message.Split('|');
                    if (fields[0] == Protocol.Commands.List.ToString())
                    {
                        this.ListFiles();
                    }
                    else if (fields[0] == Protocol.Commands.Get.ToString())
                    {
                        string[] fileNames = fields.Skip(1).ToArray();
                        await FileTransfer.SendFiles(fileNames, this.username, this.username, this.clientAesKey, this.clientAesVector, this.reader, this.writer, this.serverTextBox, null);
                    }
                    else if (message.StartsWith(Protocol.FileTransferFields.FILE_BEGIN.ToString()))
                    {
                        string[] fileFields = message.Split('|');
                        fileName = fileFields[1];
                        currentFileNum = int.Parse(fileFields[2]) + 1;
                        numOfFiles = int.Parse(fileFields[3]);
                        totalBytes = long.Parse(fileFields[4]);
                        fileName = Path.GetFileName(fileName);
                        if (!Directory.Exists(FileTransfer.FOLDER))
                        {
                            await InterfaceHelper.WriteMessage("No target folder found, creating...", this.serverTextBox, true);
                            Directory.CreateDirectory(FileTransfer.FOLDER);
                        }
                        parts.Clear();
                        isRecievingFiles = true;
                        await InterfaceHelper.WriteMessage(string.Format("File transfer started: ({0}/{1})", currentFileNum, numOfFiles), this.serverTextBox, true);

                    }
                    else if (message.StartsWith(Protocol.FileTransferFields.FILE_PIECE.ToString()))
                    {
                        if (isRecievingFiles)
                        {
                            string[] encodedData = message.Split('|');
                            byte[] encryptedData = Convert.FromBase64String(encodedData[1]);
                            currentBytes = long.Parse(encodedData[2]);
                            byte[] data = AesHelper.DecryptAesMessageBytes(encryptedData, this.clientAesKey, this.clientAesVector);
                            parts.Add(data);

                        }
                    }
                    else if (message.StartsWith(Protocol.FileTransferFields.FILE_END.ToString()))
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
                            this.writer.WriteLine(AesHelper.EncryptToAesMessageString(string.Format("{0}|{1}|{2}", Protocol.Status.ok.ToString(), currentFileNum - 1, numOfFiles), this.clientAesKey, this.clientAesVector));
                            await InterfaceHelper.WriteMessage(string.Format("File transfer ended, recieved file: ({0}/{1})", currentFileNum, numOfFiles), this.serverTextBox, true);
                            if (currentFileNum == numOfFiles)
                            {
                                await InterfaceHelper.WriteMessage(string.Format("Successfully recieved all of the files: ({0}/{1})", currentFileNum, numOfFiles), this.serverTextBox, true);
                                currentFileNum = 1;
                                numOfFiles = 0;
                            }
                            else
                            {
                                currentFileNum++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    await InterfaceHelper.WriteMessage("Client connection lost", this.serverTextBox, true);
                    break;
                }
            }
            this.CloseConnection();
        }

        /// <summary>
        /// Closes the StreamReader, StreamWriter, NetworkStream, TcpClient associated with this connection,
        /// and removes the client from the list of active clients.
        /// </summary>
        public void CloseConnection()
        {
            this.reader.Close();
            this.writer.Close();
            this.stream.Close();
            this.client.Close();
            Server.clients.TryRemove(this.username, out _);
            InterfaceHelper.RemoveFromList(this.username, this.serverListBox);
        }
    }
}
