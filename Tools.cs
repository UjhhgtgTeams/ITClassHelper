using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace ITClassHelper
{
    internal class Tools
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("Iphlpapi.dll")]
        public static extern int SendARP(int dest, int host, ref long mac, ref int length);

        [DllImport("Ws2_32.dll")]
        public static extern int inet_addr(string ip);

        public static void ExecuteProcess(string fileName, string arguments, bool noHide = false)
        {
            Process process = new Process();
            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments
            };
            if (noHide != true) processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo = processInfo;
            process.Start();
        }

        public static string GetIPAddress()
        {
            string mainAddress = "ERROR";
            foreach (IPAddress curCheckIP in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (curCheckIP.AddressFamily.ToString() == "InterNetwork")
                {
                    mainAddress = curCheckIP.ToString();
                }
            }
            return mainAddress;
        }
    }
}
