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

namespace LocalSharingCenterHome
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
        /// Returns an IP address used to bind a UDP socket to a non virtual interface
        /// </summary>
        /// <returns>IPAddress object of a non virtual interface</returns>
        public static IPAddress GetClientAddress()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus == OperationalStatus.Up && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback && !ni.Description.Contains("Virtual") && !ni.Description.Contains("VMware") && !ni.Description.Contains("Hyper-V"))
                {
                    
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            return ip.Address;
                        }
                    }
                }
            }
            return IPAddress.Loopback;
        }

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
                if (ni.OperationalStatus == OperationalStatus.Up && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback && !ni.Description.Contains("Virtual") && !ni.Description.Contains("virtual") && !ni.Description.Contains("VMware") && !ni.Description.Contains("Hyper-V"))
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
