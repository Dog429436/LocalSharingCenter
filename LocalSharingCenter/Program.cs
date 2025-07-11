using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace LocalSharingCenter
{
    internal static class Program
    {
        
        /// <summary>
        /// Checks whether there is already an active server on the local network.
        /// This is used to enforce that only one server instance can be active per network.
        /// </summary>
        /// <returns>True if a response from an active server is received otherwise, false.</returns>
        public static bool IsServerActive() 
        {
            using (UdpClient client = new UdpClient() { EnableBroadcast = true })
            {
                byte[] msg = Encoding.UTF8.GetBytes(Protocol.Connection.RequestServer.ToString());
                IPEndPoint receive = new IPEndPoint(IPAddress.Any, Protocol.SERVER_CHECK_PORT);
                List<IPEndPoint> addresses = Protocol.SubnetsBroadcast(Protocol.SERVER_CHECK_PORT);
                foreach (IPEndPoint address in addresses)
                {
                    client.Send(msg, msg.Length, address);
                }
                client.Client.ReceiveTimeout = 500;
                try
                {
                    byte[] response = client.Receive(ref receive);
                    return Encoding.UTF8.GetString(response) == Protocol.Connection.ServerExists.ToString();
                }
                catch (SocketException ex)
                {
                    return false;
                }
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DialogResult dlr = new Menu().ShowDialog();
            if (dlr == DialogResult.Yes)
            {
                if (IsServerActive()) 
                {
                    MessageBox.Show("A server is already active, closing....");
                }
                else
                {
                    Application.Run(new Server());
                }
            }
            if (dlr == DialogResult.No)
            {
                Application.Run(new Client());
            }
        }
    }
}
