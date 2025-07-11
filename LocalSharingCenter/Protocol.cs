using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalSharingCenter
{
    /// <summary>
    /// A static class for protocol related functions.
    /// </summary>
    public static class Protocol
    {
        public const int SERVER_CHECK_PORT = 4444;
        public const int UDP_PORT = 6001; 
        public const int TCP_PORT = 6002; 
        public const int FILE_PORT = 6003;


        /// <summary>
        /// Enums of different protocol fields.
        /// </summary>
        public enum Connection { RequestServer, ServerExists, PointRequest, PointResponse, ServerKey, ClientKey, ClientAesKey, CheckUser, RegisterUser};

        public enum Commands { List, Get};

        public enum FileTransferFields { FILE_BEGIN, FILE_PIECE, FILE_END, NO_FILE}

        public enum Status { ok, no};

        /// <summary>
        /// The server's public key used for signature authentication.
        /// </summary>
        public static string ServerPublicKey = @"<RSAKeyValue><Modulus>tQDSUw8TPOiXXWtjxk8JV4FZ66mH5uz17whkUlcXI2knH+MILttMDXa+ZR0POOrTVqYMKrX2Mh7o8uBGtVs6SAz0bTpVdNnN89/20Xy7pl0o9MaoiBht5F4s8oukXc5P/8Ku2b/4kcOg5aTcYB57V8w1N7xcLXqKqO3f8PNvKrj+HdlpKBB+Iljgy61IUV0gkR9d8GvOgXouhzA2pJwsiodd6jc67eIE9XTZMfGnOGo60CpksnA03CkmiVQMbqAQt833oRm2u09KeT5Zz8eckeMvcf519EsdXcRPXLPaKvuKpt6eLFdeUb2C0U1PKqudVVJx9Tj2itTg2X+/CGBmtQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        /// <summary>
        /// Returns a list of IPEndPoints to which the broadcast packet should be sent.
        /// This avoids relying on the operating system to choose a network interface, which might result in the packet being sent via a virtual interface and never leaving the computer.
        /// </summary>
        /// <param name="port">The port to use for the broadcast.</param>
        /// <returns>A list of IPEndPoints representing the broadcast addresses for active IPv4 network interfaces.</returns>
        public static List<IPEndPoint> SubnetsBroadcast(int port)
        {
            List<IPEndPoint> points = new List<IPEndPoint>();
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus == OperationalStatus.Up && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    var ipProperties = ni.GetIPProperties();
                    foreach (var ua in ipProperties.UnicastAddresses)
                    {
                        if (ua.Address.AddressFamily == AddressFamily.InterNetwork && ua.IPv4Mask != null)
                        {
                            byte[] ipBytes = ua.Address.GetAddressBytes();
                            byte[] maskBytes = ua.IPv4Mask.GetAddressBytes();
                            byte[] broadcast = new byte[4];
                            for (int i = 0; i < 4; i++)
                            {
                                broadcast[i] = (byte)(ipBytes[i] | (~maskBytes[i] & 0xFF));
                            }
                            points.Add(new IPEndPoint(new IPAddress(broadcast), port));
                        }
                    }
                }
            }
            return points;
        }
    }
}
