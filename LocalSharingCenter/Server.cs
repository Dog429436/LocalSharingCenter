using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Text.Json;
using System.Net.NetworkInformation;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace LocalSharingCenter
{
    public partial class Server : Form
    {
        const int PORT = 4444;
        const int MAXTRIESBEFOREBLOCK = 5;
        const int ALLOWEDDELAY = 10;
        public static int userCount = 0;
        public static ConcurrentDictionary<string, ConnectedClient> clients = new ConcurrentDictionary<string, ConnectedClient>();
        public static ConcurrentDictionary<string, (DateTime, int)> ips = new ConcurrentDictionary<string, (DateTime, int)>();
        TcpListener listener;
        public static string serverPublicKey;
        public static string serverPrivateKey;
        Random rnd = new Random();
        static string adminPassword;
        public Server()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Listens for incoming UDP PointRequest packets and responds by signing the received data using the server's private RSA key.
        /// This is used for server authentication so the client can connect to a trusted server.
        /// </summary>
        private void ServerSignatureAuthentication() 
        {
            IPEndPoint recievePoint = new IPEndPoint(IPAddress.Any, Protocol.UDP_PORT);
            UdpClient serverClient = new UdpClient(recievePoint);
            string serverRsaPrivateKey = AesHelper.DecryptServerRsaPrivateKey(adminPassword);
            Thread signRequest = new Thread(() =>
            {
                while (true)
                {

                    try
                    {
                        byte[] data = serverClient.Receive(ref recievePoint);
                        string request = Encoding.UTF8.GetString(data);
                        string[] parts = request.Split('|');
                        if (parts.Length == 2 && parts[0] == Protocol.Connection.PointRequest.ToString())
                        {
                            byte[] num = Encoding.UTF8.GetBytes(parts[1]);
                            using (RSA rsa = RSA.Create())
                            {
                                rsa.FromXmlString(serverRsaPrivateKey);
                                byte[] signature = rsa.SignData(num, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                                string encodedResponse = Protocol.Connection.PointResponse.ToString() + "|" + Convert.ToBase64String(signature);
                                byte[] response = Encoding.UTF8.GetBytes(encodedResponse);
                                serverClient.Send(response, response.Length, recievePoint);
                            }
                            InterfaceHelper.WriteMessage(string.Format("{0} Someone requested the server ip", DateTime.Now), ServerLogs, true);
                        }
                    }
                    catch (Exception e)
                    {
                        InterfaceHelper.WriteMessage(string.Format("{0} error with server signature, client might not be able to connect", DateTime.Now), ServerLogs, true);
                    }
                }
            });
            signRequest.IsBackground = true;
            signRequest.Start();

        }

        /// <summary>
        /// Listens for incoming UDP RequestServer packets and responds by sending a ServerExists packet.
        /// This is used to enforce that only one server instance can be active per network. 
        /// </summary>
        async public void Deny() 
        {
            using (UdpClient server = new UdpClient(PORT))
            {
                IPEndPoint others = new IPEndPoint(IPAddress.Any, PORT);

                while (true)
                {
                    try
                    {
                        byte[] bits = server.Receive(ref others);
                        if (Encoding.UTF8.GetString(bits) == Protocol.Connection.RequestServer.ToString())
                        {
                            byte[] denyPacket = Encoding.UTF8.GetBytes(Protocol.Connection.ServerExists.ToString());
                            server.Send(denyPacket, denyPacket.Length, others);
                            await InterfaceHelper.WriteMessage(string.Format("{0} Someone was denied\n", DateTime.Now), ServerLogs, true);
                        }

                    }
                    catch (SocketException ex) when (ex.SocketErrorCode == SocketError.TimedOut)
                    {
                        
                    }
                    Thread.Sleep(10);
                }
            }

        }

        /// <summary>
        /// Handles incoming TCP connections from clients by accepting them and initializing ConnectedClient objects.
        /// Implements basic anti-DDoS protection through rate limiting and a ban system to block abusive IPs.
        /// </summary>
        private void ConnectionHandle() 
        {
            try
            {
                TcpListener server = new TcpListener(IPAddress.Any, Protocol.TCP_PORT);
                server.Start();
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    string ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                    DateTime now = DateTime.Now;
                    DateTime LastTry;
                    if (ips.ContainsKey(ip))
                    {
                        LastTry = ips[ip].Item1;
                        if ((now - LastTry).TotalSeconds < ALLOWEDDELAY)
                        {
                            if (ips[ip].Item2 >= MAXTRIESBEFOREBLOCK)
                            {
                                client.Close();
                                continue;
                            }
                            ips[ip] = (now, ips[ip].Item2 + 1);
                        }
                        else
                        {
                            ips[ip] = (now, 1);
                        }
                    }
                    else
                    {
                        ips.TryAdd(ip, (now, 1));
                    }
                    Thread clientThread = new Thread(() =>
                    {
                        try
                        {
                            ConnectedClient connectedClient = new ConnectedClient(client, ip, ServerLogs, ClientsList);
                            connectedClient.Connect().Wait();
                            bool notLoggedIn = true;
                            while (notLoggedIn)
                            {
                                string clientMessage = connectedClient.reader.ReadLine();
                                string message = AesHelper.DecryptAesMessageString(clientMessage, connectedClient.clientAesKey, connectedClient.clientAesVector);
                                string[] fields = message.Split('|');
                                string response = "";
                                if (fields.Length == 3 && fields[0] == Protocol.Connection.CheckUser.ToString())
                                {
                                    string username = fields[1];
                                    string password = fields[2];
                                    SQLiteDataBase db = new SQLiteDataBase();
                                    if (db.UserExists(username, password, false) && !clients.ContainsKey(username))
                                    {
                                        int userId = Interlocked.Increment(ref userCount);
                                        connectedClient.userId = userId;
                                        connectedClient.username = username;
                                        response = AesHelper.EncryptToAesMessageString(Protocol.Status.ok.ToString(), connectedClient.clientAesKey, connectedClient.clientAesVector);
                                        connectedClient.writer.WriteLine(response);
                                        InterfaceHelper.WriteMessage(string.Format("User: {0} successfully logged in", connectedClient.userId), ServerLogs, true);
                                        InterfaceHelper.WriteToList(connectedClient.username, ClientsList);
                                        notLoggedIn = false;
                                        clients.TryAdd(username, connectedClient);
                                    }
                                    else
                                    {
                                        response = AesHelper.EncryptToAesMessageString(Protocol.Status.no.ToString(), connectedClient.clientAesKey, connectedClient.clientAesVector);
                                    }
                                }
                                else if (fields.Length == 3 && fields[0] == Protocol.Connection.RegisterUser.ToString())
                                {
                                    string username = fields[1];
                                    string password = fields[2];
                                    byte[] hashedPassword;
                                    string salt = rnd.Next().ToString();
                                    using (SHA256 hash = SHA256.Create()) 
                                    {
                                        hashedPassword = hash.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                                    }
                                    SQLiteDataBase db = new SQLiteDataBase();
                                    if (db.UsernameExists(username))
                                    {
                                        InterfaceHelper.WriteMessage("A client tried to registed with an already exist username", ServerLogs, true); 
                                        response = AesHelper.EncryptToAesMessageString(Protocol.Status.no.ToString(), connectedClient.clientAesKey, connectedClient.clientAesVector);
                                    }
                                    else
                                    {
                                        db.WriteUser(username, Convert.ToBase64String(hashedPassword), salt);
                                        InterfaceHelper.WriteMessage("A client registered successfully", ServerLogs, true); 
                                        response = AesHelper.EncryptToAesMessageString(Protocol.Status.ok.ToString(), connectedClient.clientAesKey, connectedClient.clientAesVector);

                                    }
                                }
                                connectedClient.writer.WriteLine(response);
                            }
                            connectedClient.ClientHandle();
                        }
                        catch (Exception e)
                        {

                        }

                    });
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
            }
            catch
            {
                InterfaceHelper.WriteMessage("A client error occured", ServerLogs, true);
            }
        }
        

        private void Server_Load(object sender, EventArgs e)
        {
            ServerLogin.BringToFront();
        }
        private void UserNameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PasswordBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void PasswordLabel_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the adminlogin process by searching the SQLite database for the existing user.
        /// If login is sucessfull, initializes the server and generates new RSA keys.
        /// </summary>
        private async void LoginButton_Click(object sender, EventArgs e)
        {
            string username = UserNameBox.Text;
            string password = PasswordBox.Text;
            SQLiteDataBase db = new SQLiteDataBase();
            if (db.UserExists(username, password, true))
            {
                adminPassword = password;
                ServerLogin.SendToBack();
                using (RSA rsa = RSA.Create())
                {
                    serverPrivateKey = rsa.ToXmlString(true);
                    serverPublicKey = rsa.ToXmlString(false);
                }
                Thread DenyOthers = new Thread(() => Deny()) { IsBackground = true };
                DenyOthers.Start();
                Thread givePoint = new Thread(() => ServerSignatureAuthentication()) { IsBackground = true };
                givePoint.Start();
                Thread clientsHandle = new Thread(() => ConnectionHandle()) { IsBackground = true };
                clientsHandle.Start();
                await InterfaceHelper.WriteMessage(string.Format("Port: {0}\nGenerated rsa public and private keys", Protocol.TCP_PORT), ServerLogs, true);


            }
            else
            {
                await InterfaceHelper.WriteMessage("No admin user found", LogBox, true);
            }
        }
        private void LogBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void WelcomeLabel_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ServerLogs_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the server shutdown.
        /// </summary>
        private async void ServerShutdownButton_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ConnectedClient client in clients.Values)
                {
                    client.CloseConnection();
                }
                await InterfaceHelper.WriteMessage("Server closed connections", ServerLogs, true);
            }
            catch (Exception ex)
            {
                await InterfaceHelper.WriteMessage("A client has a connection error", ServerLogs, true);
            }
        }

        private void ServerLabel_Click(object sender, EventArgs e)
        {

        }

        private void ConnectedUsersLabel_Click(object sender, EventArgs e)
        {

        }

        private void ClientsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
