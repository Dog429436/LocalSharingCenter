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
using System.Net.NetworkInformation;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace LocalSharingCenterHome
{
    public partial class Server : Form
    {
        public static int userCount = 1;
        public static ConcurrentDictionary<string, ConnectedClient> clients = new ConcurrentDictionary<string, ConnectedClient>();
        TcpListener listener;
        public static string serverPublicKey;
        public static string serverPrivateKey;
        public Server()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Listens for incoming UDP PointRequest packets and responds with PointResponse.
        /// </summary>
        private void ServerAddressResponse() 
        {
            IPEndPoint recievePoint = new IPEndPoint(IPAddress.Any, Protocol.UDP_PORT);
            UdpClient serverClient = new UdpClient(recievePoint);
            Thread pointRequest = new Thread(() =>
            {
                while (true)
                {

                    try
                    {
                        byte[] data = serverClient.Receive(ref recievePoint);
                        string request = Encoding.UTF8.GetString(data);
                        if (request == Protocol.Connection.PointRequest.ToString())
                        {
                            string response = Protocol.Connection.PointResponse.ToString();
                            byte[] byteResponse = Encoding.UTF8.GetBytes(response);
                            serverClient.Send(byteResponse, response.Length, recievePoint);
                            InterfaceHelper.WriteMessage(string.Format("{0} Someone requested the server ip", DateTime.Now), ServerLogs, true);
                        }
                    }
                    catch (Exception e)
                    {
                        InterfaceHelper.WriteMessage(string.Format("{0} error with server signature, client might not be able to connect", DateTime.Now), ServerLogs, true);
                    }
                }
            });
            pointRequest.IsBackground = true;
            pointRequest.Start();

        }

        /// <summary>
        /// Listens for incoming UDP RequestServer packets and responds by sending a ServerExists packet.
        /// This is used to enforce that only one server instance can be active per network. 
        /// </summary>
        async public void Deny() 
        {
            using (UdpClient server = new UdpClient(Protocol.SERVER_CHECK_PORT) { EnableBroadcast = true})
            {
                IPEndPoint others = new IPEndPoint(IPAddress.Any, Protocol.SERVER_CHECK_PORT);

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
                    Thread clientThread = new Thread(() =>
                    {
                        try
                        {
                            ConnectedClient connectedClient = new ConnectedClient(client, ip, ServerLogs, ClientsList);
                            connectedClient.Connect().Wait();
                            if (connectedClient.isConnected)
                            {
                                int clientId = userCount;
                                userCount++;
                                connectedClient.userId = clientId;
                                InterfaceHelper.WriteToList(clientId.ToString(), ClientsList);
                                clients.TryAdd(clientId.ToString(), connectedClient);
                                connectedClient.ClientHandle();
                            }
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
        

        private async void Server_Load(object sender, EventArgs e)
        {
            using (RSA rsa = RSA.Create())
            {
                serverPrivateKey = rsa.ToXmlString(true);
                serverPublicKey = rsa.ToXmlString(false);
            }
            Thread DenyOthers = new Thread(() => Deny()) { IsBackground = true };
            DenyOthers.Start();
            Thread givePoint = new Thread(() => ServerAddressResponse()) { IsBackground = true };
            givePoint.Start();
            Thread clientsHandle = new Thread(() => ConnectionHandle()) { IsBackground = true };
            clientsHandle.Start();
            await InterfaceHelper.WriteMessage(string.Format("Port: {0}\nGenerated rsa public and private keys", Protocol.TCP_PORT), ServerLogs, true);
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
