using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace ITClassHelper
{
    internal class Network
    {
        public readonly static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        public static bool socketBound = false;

        [DllImport("Iphlpapi.dll")]
        public static extern int SendARP(int dest, int host, ref long mac, ref int length);

        [DllImport("Ws2_32.dll")]
        public static extern int inet_addr(string ip);

        public static string GetIPAddress(string hostName = "")
        {
            if (hostName == "")
                hostName = Dns.GetHostName();
            string resultAddress = null;
            try
            {
                foreach (IPAddress curCheckIP in Dns.GetHostEntry(hostName).AddressList)
                {
                    if (curCheckIP.AddressFamily == AddressFamily.InterNetwork)
                    {
                        resultAddress = curCheckIP.ToString();
                    }
                }
            }
            catch (SocketException) { resultAddress = null; }
            return resultAddress;
        }

        public static bool GetPortInUse(int port)
        {
            IPGlobalProperties ipProp = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] endPoints = ipProp.GetActiveTcpListeners();
            foreach (IPEndPoint endPoint in endPoints)
            {
                if (endPoint.Port == port)
                    return true;
            }
            return false;
        }

        public static string GetMacByIP(string ip)
        {
            int ldest = inet_addr(ip);
            string mac = "";
            try
            {
                long macinfo = 0;
                int len = 6;
                int result = SendARP(ldest, 0, ref macinfo, ref len);
                mac = Convert.ToString(macinfo, 16);
            }
            catch { }
            mac = "000000000000" + mac;
            mac = mac.Substring(mac.Length - 12);
            return mac.Substring(10, 2).ToUpper() + "-" + mac.Substring(8, 2).ToUpper() + "-"
                + mac.Substring(6, 2).ToUpper() + "-" + mac.Substring(4, 2).ToUpper() + "-"
                + mac.Substring(2, 2).ToUpper() + "-" + mac.Substring(0, 2).ToUpper();
        }

        public static string GetHostName(string ip)
        {
            string hostName = null;
            try
            {
                hostName = Dns.GetHostEntry(ip).HostName;
            }
            catch (SocketException) { hostName = null; }
            return hostName;
        }
    }
}