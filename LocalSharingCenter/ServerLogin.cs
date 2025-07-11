using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LocalSharingCenterServer.Protocol;
namespace LocalSharingCenterServer
{
    public partial class ServerLogin : Form
    {
        private const string FILEPATH = "admins.json";
        public ServerLogin()
        {
            InitializeComponent();
            InterfaceHelper.Register(Controls);
        }
        public static bool UserExists(Dictionary<string, string> users, string userName, string password)
        {
            SHA256 hash = SHA256.Create();
            byte[] hashedPassword = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            return users.ContainsKey(userName) && users[userName] == Convert.ToBase64String(hashedPassword);
        }
        public static bool IsServerActive()
        {
            const int PORT = 4444;
            const int TIMEOUT = 500;
            const string BROADCAST_ADDRESS = "192.168.7.255";
            using (UdpClient udpClient = new UdpClient() { EnableBroadcast = true })
            {
                byte[] msg = Encoding.UTF8.GetBytes(options.RequestServer.ToString());
                IPEndPoint broadcast = new IPEndPoint(IPAddress.Parse(BROADCAST_ADDRESS), PORT);
                IPEndPoint recieve = new IPEndPoint(IPAddress.Any, PORT);

                udpClient.Send(msg, msg.Length, broadcast);
                udpClient.Client.ReceiveTimeout = TIMEOUT;

                try
                {

                    byte[] response = udpClient.Receive(ref recieve);
                    return Encoding.UTF8.GetString(response) == options.ServerExists.ToString();
                }
                catch (SocketException ex) when (ex.SocketErrorCode == SocketError.TimedOut)
                {
                    return false;
                }
            }

        }
        private void WelcomeLabel_Click(object sender, EventArgs e)
        {

        }

        private void Menu_Load(object sender, EventArgs e)
        {

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(FILEPATH))
            {
                await InterfaceHelper.WriteMessage("No json file found", "LogBox");
                DialogResult = DialogResult.No;
                return;
            }
            Dictionary<string, string> users = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(FILEPATH));
            if (!UserExists(users, UserNameBox.Text, PasswordBox.Text))
            {
                await InterfaceHelper.WriteMessage("No admin user found", "LogBox");
                
            }
            else if (IsServerActive())
            {
                await InterfaceHelper.WriteMessage("A server is already active", "LogBox");
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
            

        }

        private void LogBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
