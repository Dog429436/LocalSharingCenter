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
            using (UdpClient client = new UdpClient(new IPEndPoint(Protocol.GetClientAddress(), 0)) { EnableBroadcast = true })
            {
                byte[] msg = Encoding.UTF8.GetBytes(Protocol.Connection.RequestServer.ToString());
                IPEndPoint receive = new IPEndPoint(IPAddress.Any, Protocol.SERVER_CHECK_PORT);
                List<IPEndPoint> addresses = Protocol.SubnetsBroadcast(Protocol.SERVER_CHECK_PORT);
                client.Client.ReceiveTimeout = 500;
                foreach (IPEndPoint address in addresses)
                {
                    try
                    {
                        client.Send(msg, msg.Length, address);
                        byte[] response = client.Receive(ref receive);
                        if (Encoding.UTF8.GetString(response) == Protocol.Connection.ServerExists.ToString())
                        {
                            return true;
                        }
                    }
                    catch (SocketException ex)
                    {

                    }
                }
                return false;
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
