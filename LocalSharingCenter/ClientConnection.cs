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
    public class ServerConnection : Connection
    {
        private const int UDP_PORT = 6001; //פורט לתקשורת udp
        private const int TCP_PORT = 6002; //פורט לתקשורת tcp
        private string serverRsaPublicKey; //מפתח ציבורי של השרת
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

        #region requestServerAuthentication() 
        /// <summary>
        /// טענת כניסה: קריאה לפעלה, טענת יציאה: החזרת כתובת ip של server מאומת
        /// </summary>
        /// <returns></returns>
        private string requestServerAuthentication()
        {
            UdpClient senderSocket = new UdpClient() { EnableBroadcast = true }; //יצירת אובייקט לתקשורת udp
            IPEndPoint allPoints = new IPEndPoint(IPAddress.Broadcast, UDP_PORT); //הגדרת נדוקת שליחה
            int num = rnd.Next(); //יצירת מספר רנדומלי
            byte[] bitMsg = Encoding.ASCII.GetBytes(Protocol.connection.PointRequest.ToString() + "|" + num); //המרת שדה הבקשה לקבלת כתובת והמספר למערך בתים
            IPEndPoint receiveEndPoint = new IPEndPoint(IPAddress.Any, 0); //הגדרת נקודת התגובה
            senderSocket.Send(bitMsg, bitMsg.Length, allPoints); //שידור הבקשה לרשת המקומית
            senderSocket.Client.ReceiveTimeout = 500; //הגדרת זמן המתנה לתשובה
            try
            {
                byte[] receivedData = senderSocket.Receive(ref receiveEndPoint); //קבלת תשובת השרת
                string response = Encoding.UTF8.GetString(receivedData); //המרת תשובת השרת למערך בתים
                string[] parts = response.Split('|'); //המרת התשובה למערך מחזורות כך שכל מחרוזת היא שדה אחר בתשובה
                if (parts.Length == 2 && parts[0] == Protocol.connection.PointResponse.ToString())
                {
                    byte[] signature = Convert.FromBase64String(parts[1]);
                    byte[] numBytes = Encoding.UTF8.GetBytes(num.ToString());
                    using (RSA rsa = RSA.Create())
                    {
                        rsa.FromXmlString(Protocol.Server_public_key);
                        bool answer = rsa.VerifyData(numBytes, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                        if (answer)
                        {
                            return receiveEndPoint.Address.ToString();
                        }

                    }

                }

            }
            catch (Exception) { } //תפיסת חריגות למניעת הפסקת התוכנה
            finally
            {
                senderSocket.Close(); //סגירת האובייקט
            }
            return "/";

        }
        #endregion

        #region ConnectToServer()
        /// <summary>
        /// טענת כניסה: קריאה לפעולה, טענת יציאה: התחברות לשרת קיים תוך העברת מפתחות rsa ומפתח aes
        /// </summary>
        /// <returns></returns>
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
            this.client = new TcpClient(serverIp, TCP_PORT);
            this.stream = client.GetStream();
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
                    this.CloseConnection();
                    return;
                }
                string[] parts = data.Split('|');
                if (parts[0] != Protocol.connection.ServerKey.ToString())
                {
                    await InterfaceHelper.WriteMessage("Rsa keys error", MenuLogs, true);
                    this.CloseConnection();
                    return;
                }
                this.serverRsaPublicKey = parts[1];
                await InterfaceHelper.WriteMessage("Recieved server rsa public key", MenuLogs, true);
            }
            catch (Exception ex)
            {
                await InterfaceHelper.WriteMessage("Connection failed, server unresponsive", MenuLogs, true);
                this.CloseConnection();
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
                encryptedAesFields = rsaAes.Encrypt(Encoding.UTF8.GetBytes(clientKeyFields), RSAEncryptionPadding.OaepSHA1);
                aesMessage = Convert.ToBase64String(encryptedAesFields);
                clientMessage = Protocol.connection.ClientAesKey.ToString() + "|" + aesMessage;
            }
            try
            {
                this.writer.WriteLine(clientMessage);
                await InterfaceHelper.WriteMessage("Server was sent aes key", MenuLogs, true);
                string response = this.reader.ReadLine();
                if (response == null)
                {
                    await InterfaceHelper.WriteMessage("Connection failed, server unresponsive", MenuLogs, true);
                    this.CloseConnection();
                    return;
                }
                string message = AesHelper.DecryptAesMessageString(response, this.clientAesKey, this.clientAesVector);
                if (message == Protocol.status.ok.ToString())
                {
                    await InterfaceHelper.WriteMessage("Successfully connected to server", MenuLogs, true);
                    this.isConnected = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                await InterfaceHelper.WriteMessage("Connection failed, server unresponsive", MenuLogs, true);
                this.CloseConnection();
            }
            #endregion
            return;
        }
        #endregion

        #region ResponseHandle() //טענת כניסה: קריאה לפעולה, טענת יציאה: פתיחת תרד בו מתקבלים תגובות מהשרת
        public void ResponseHandle(Button DownloadButton, ListBox FileList, RichTextBox FileBox) //טענת כניסה: קריאה לפעולה, טענת יציאה: פתיחת תרד בו מתקבלים תגובות מהשרת
        {
            List<byte[]> parts = new List<byte[]>();
            string fileName = "";
            int currentFileNum = 0;
            int numOfFiles = 0;
            bool isRecievingFiles = false;
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
                        else if (response.StartsWith(Protocol.fileTransferFiels.FILE_BEGIN.ToString()))
                        {
                            string header = Protocol.fileTransferFiels.FILE_BEGIN.ToString();
                            string[] fields = response.Substring(header.Length).Split('|');
                            fileName = fields[1];
                            currentFileNum = int.Parse(fields[2]) + 1;
                            numOfFiles = int.Parse(fields[3]);
                            fileName = Path.GetFileName(fileName);
                            if (!Directory.Exists(Protocol.TARGETFOLDER))
                            {
                                await InterfaceHelper.WriteMessage("No target folder found, creating...", FileBox, true);
                                Directory.CreateDirectory(Protocol.TARGETFOLDER);
                            }
                            parts.Clear();
                            isRecievingFiles = true;
                            await InterfaceHelper.WriteMessage(string.Format("File transfer started: ({0}/{1})", currentFileNum, numOfFiles), FileBox, true);

                        }
                        else if (response.StartsWith(Protocol.fileTransferFiels.FILE_PIECE.ToString()))
                        {
                            if (isRecievingFiles)
                            {
                                string header = Protocol.fileTransferFiels.FILE_PIECE.ToString() + "|";
                                string encodedData = response.Substring(header.Length);
                                byte[] encryptedData = Convert.FromBase64String(encodedData);
                                byte[] data = AesHelper.DecryptAesMessageBytes(encryptedData, this.clientAesKey, this.clientAesVector);
                                parts.Add(data);

                            }
                        }
                        else if (response.StartsWith(Protocol.fileTransferFiels.FILE_END.ToString()))
                        {
                            if (isRecievingFiles && fileName != "")
                            {
                                string fullPath = Path.Combine(Protocol.TARGETFOLDER, fileName);
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
                                this.writer.WriteLine(AesHelper.EncryptToAesMessageString(string.Format("{0}|{1}", Protocol.status.ok.ToString(), currentFileNum - 1), this.clientAesKey, this.clientAesVector));
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
                        else if (response.StartsWith(Protocol.fileTransferFiels.NO_FILE.ToString()))
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
                        string[] fileFields = response.Split('|');
                        if (fileFields.Length == 2 && fileFields[0] == Protocol.status.ok.ToString())
                        {
                            await InterfaceHelper.WriteMessage(string.Format("server successfully recieved a file: ({0}/{1})", int.Parse(fileFields[1]) + 1, filesCount), FileBox, true);
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
                        this.CloseConnection();
                    }
                }
            });
            clientThread.IsBackground = true;
            clientThread.Start();
        }
        #endregion 

        public async Task<bool> TryLogin(string username, string password, RichTextBox messages)
        {
            #region התחברות למשתמש קיים
            try
            {
                string checkUser = Protocol.connection.CheckUser.ToString() + "|" + username + "|" + password;
                string request = AesHelper.EncryptToAesMessageString(checkUser, this.clientAesKey, this.clientAesVector);
                this.writer.WriteLine(request);
                string response = this.reader.ReadLine();
                if (response == null)
                {
                    await InterfaceHelper.WriteMessage("Server connection lost, you can try to connect again", messages, true);
                    this.CloseConnection();
                    return false;
                }
                string message = AesHelper.DecryptAesMessageString(response, this.clientAesKey, this.clientAesVector);
                if (message == Protocol.status.no.ToString())
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
                this.CloseConnection();
            }
            return false;
            #endregion
        }

        public async Task TrySignUp(string username, string password, RichTextBox signupMessages)
        {
            #region רישום משתמש חדש
            string specialChars = "!@#$%^&*()-_=+[]{};:'\"\\|,.<>/?`~";
            int uppercaseCharCount = 0;
            int numberCount = 0;
            int specialCharsCount = 0;
            bool hasWhiteSpaces = false;
            bool securedPassword = true;
            if (password.Length < 8) //אם אורך הסיסמה פחות מ 8 תווים
            {
                await InterfaceHelper.WriteMessage("Passowrd is too short", signupMessages, true); //תודפס הודעה
                securedPassword = false;
            }
            for (int i = 0; i < password.Length; i++)
            {
                if ((int)password[i] >= 65 && (int)password[i] <= 90) //אם תו ברשימה הוא אות גדולה
                {
                    uppercaseCharCount++; //יעלה מונה התווים של האותיות הגדולות
                }
                if ((int)password[i] >= 48 && (int)password[i] <= 57) //אם תו הוא ספרה
                {
                    numberCount++; //מונה הספרות יעלה
                }
                for (int j = 0; j < specialChars.Length; j++) //אם תו הוא תו מיוחד
                {
                    if (password[i] == specialChars[j])
                    {
                        specialCharsCount++; //מונה התווים יעלה
                    }
                }
                if (password[i] == ' ') //אם יש רווח
                {
                    hasWhiteSpaces = true;
                }
            }
            if (hasWhiteSpaces)
            {
                await InterfaceHelper.WriteMessage("Password has a whitespace", signupMessages, true); //תודפס הודעה
                securedPassword = false;
            }
            if (uppercaseCharCount < 1) //אם אין לפחות אות גדולה אחת בסיסמה
            {
                await InterfaceHelper.WriteMessage("Passowrd is missing an uppercase character", signupMessages, true); //תודפס הודעה
                securedPassword = false;
            }
            if (numberCount < 1) //אם אין לפחות ספרה אחת בסיסמה
            {
                await InterfaceHelper.WriteMessage("Passowrd is missing a digit", signupMessages, true); //תודפס הודעה
                securedPassword = false;
            }
            if (specialCharsCount < 1) //אם אין לפחות תו מיוחד אחד בסיסמה
            {
                await InterfaceHelper.WriteMessage("Passowrd is missing a special character", signupMessages, true); //תודפס הודעה
                securedPassword = false;
            }
            if (securedPassword) //אם הסיסמה בטוחה
            {
                string registerFields = Protocol.connection.RegisterUser.ToString() + '|' + username + '|' + password;
                string clientRegister = AesHelper.EncryptToAesMessageString(registerFields, this.clientAesKey, this.clientAesVector);
                try
                {
                    this.writer.WriteLine(clientRegister);
                    string response = this.reader.ReadLine();
                    if (response == null)
                    {
                        await InterfaceHelper.WriteMessage("Server connection lost, you can try to connect again", signupMessages, true);
                        this.CloseConnection();
                        return;
                    }
                    string message = AesHelper.DecryptAesMessageString(response, this.clientAesKey, this.clientAesVector);
                    if (message == Protocol.status.no.ToString())
                    {
                        await InterfaceHelper.WriteMessage("Username already used", signupMessages, true);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    await InterfaceHelper.WriteMessage("Server connection lost, you can try to connect again", signupMessages, true);
                    this.CloseConnection();
                    return;
                }
                await InterfaceHelper.WriteMessage("You registered successfully you may log in", signupMessages, true);
            }
            #endregion
        }

        public async Task ListFilesButton(RichTextBox FileBox)
        {
            try
            {
                string command = AesHelper.EncryptToAesMessageString(Protocol.commands.List.ToString(), clientAesKey, this.clientAesVector);
                this.writer.WriteLine(command);
            }
            catch (Exception ex)
            {
                this.CloseConnection();
                string str = "Server connection lost, you can return to menu and try again";
                await InterfaceHelper.WriteMessage(str, FileBox, true);
            }
        }

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
                this.CloseConnection();
                string str = "Server connection lost, you can return to menu and try again\n";
                await InterfaceHelper.WriteMessage(str, FileBox, true);
            }
        }

        public async Task UploadFilesButton(string[] selectedFiles, RichTextBox FileBox)
        {
            this.filesCount = selectedFiles.Length;
            await Protocol.SendFilesClientSide(selectedFiles, "client", this.clientAesKey, clientAesVector, this.reader, this.writer, FileBox);
        }

        #region CloseTcpConnection()
        /// <summary>
        /// //טענת כניסה: קריאה לפעולה, טענת יציאה: סגירת אובייקטים המאפשרים תקשורת
        /// </summary>
        public override void CloseConnection()
        {
            base.CloseConnection();
            this.isConnected = false;
        }
        #endregion
    }
}
