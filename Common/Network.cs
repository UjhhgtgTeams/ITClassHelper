using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace ITClassHelper
{
    internal class Network
    {
        public readonly static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        public static bool binded = false;

        [DllImport("Iphlpapi.dll")]
        public static extern int SendARP(int dest, int host, ref long mac, ref int length);

        [DllImport("Ws2_32.dll")]
        public static extern int inet_addr(string ip);

        public static string GetIPAddress(bool getCurIP = true, string hostName = "")
        {
            string resultAddress = null, targetHostName;
            if (getCurIP == true)
                targetHostName = Dns.GetHostName();
            else
                targetHostName = hostName;
            try
            {
                foreach (IPAddress curCheckIP in Dns.GetHostEntry(targetHostName).AddressList)
                {
                    if (curCheckIP.AddressFamily.ToString() == "InterNetwork")
                    {
                        resultAddress = curCheckIP.ToString();
                    }
                }
            }
            catch (SocketException) { resultAddress = "ERROR"; }
            return resultAddress;
        }
    }
}
