﻿using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static ITClassHelper.ProcMgr;

namespace ITClassHelper
{
    public partial class FormMain : Form
    {
        readonly FormCastControl castControl = new FormCastControl();
        readonly FormDeviceManage deviceManage = new FormDeviceManage();
        static readonly string ProgramVersion = "3.2.5-d";
        static readonly string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ITClassHelper";
        static readonly string ntsdPath = appDataPath + @"\ntsd.exe";
        static readonly string disableAttackFilePath = appDataPath + @"\disableAttack.txt";
        static readonly string killerPath = @".\ComputerKiller.py";
        static string roomPath;
        static bool firstTimeHide = true;
        static bool killRedSpider = false;

        public FormMain()
        {
            if (GetProcs("ITClassHelper").Length > 1)
            {
                foreach (Process programProc in GetProcs("ITClassHelper"))
                {
                    int procId = programProc.Id;
                    if (procId != Process.GetCurrentProcess().Id)
                    {
                        string[] procArgs = GetProcessArgs(procId);
                        if (procArgs[1] != "-rs")
                        {
                            MessageBox.Show("机房助手已在运行！点击[确认]退出当前进程！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Process.GetCurrentProcess().Kill();
                        }
                    }
                }
            }
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            ProgramAboutLabel.Text = ProgramAboutLabel.Text.Replace("X.Y.Z", ProgramVersion);
            HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.Alt, Keys.H);
            castControl.Show();
            castControl.Hide();
            if (ProgramVersion.Contains("-d"))
                MessageBox.Show("这是一个实验性版本，尚不稳定，请小心操作！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(appDataPath);

            if (File.Exists(disableAttackFilePath))
            {
                DisableAttackButton.Enabled = false;
                DeviceManageButton.Enabled = false;
            }

            if (!File.Exists(ntsdPath))
            {
                byte[] RescNtsd = Properties.Resources.Ntsd;
                using (FileStream ntsdFsObj = new FileStream(ntsdPath, FileMode.CreateNew))
                {
                    ntsdFsObj.Write(RescNtsd, 0, RescNtsd.Length);
                }
            }

            byte[] RescKiller = Properties.Resources.ComputerKiller;
            using (FileStream killerFsObj = new FileStream(killerPath, FileMode.Create))
            {
                killerFsObj.Write(RescKiller, 0, RescKiller.Length);
            }

            if (Network.GetPortIsUsed(6666) != true)
            {
                try
                {
                    if (Network.socketBound == false)
                        Network.socket.Bind(new IPEndPoint(IPAddress.Parse(Network.GetIPAddress(Dns.GetHostName())), 6666));
                    Network.socketBound = true;
                }
                catch (SocketException ex)
                {
                    MessageBox.Show($"本机 IP 地址绑定失败！将无法使用[简易内网聊天]功能！\n错因：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ChatButton.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show($"本机聊天端口被占用！将无法使用[简易内网聊天]功能！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ChatButton.Enabled = false;
            }

            new Thread(LoopThread) { IsBackground = true }.Start();
            new Thread(MessageRecieve) { IsBackground = true }.Start();
            new Thread(RedSpiderTerminator) { IsBackground = true }.Start();
        }

        private void LoopThread()
        {
            while (true)
            {
                IntPtr studentWindow = WndMgr.GetStudentWindow();
                if (studentWindow != IntPtr.Zero)
                {
                    if (Visible == true)
                    {
                        TopMost = true;
                        Activate();
                    }
                }
                else
                {
                    if (TopMost == true)
                        TopMost = false;
                }
                if (MousePosition == new Point(0, 0))
                {
                    WndMgr.MoveWindow(studentWindow, castControl.Size.Width, castControl.Size.Height, 1000, 500, true);
                    castControl.Show();
                }
                if (GetProcs("StudentMain").Length == 0 && GetProcs("REDAgent").Length == 0)
                {
                    RoomStatusLabel.Text = "未在运行";
                    RoomStatusLabel.ForeColor = Color.Green;
                }
                else
                {
                    RoomStatusLabel.Text = "正在运行";
                    RoomStatusLabel.ForeColor = Color.Red;
                }
                GetMythwarePath("quiet");
                Thread.Sleep(1500);
            }
        }

        private void MessageRecieve()
        {
            while (true)
            {
                EndPoint serverPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] buffer = new byte[10240];
                int length = Network.socket.ReceiveFrom(buffer, ref serverPoint);
                string message = Encoding.UTF8.GetString(buffer, 0, length);
                string formattedMessage = $"收到来自 {serverPoint} 的消息：\n{message}";
                new Thread(x => MessageBox.Show(formattedMessage, "消息", MessageBoxButtons.OK, MessageBoxIcon.None)).Start();
            }
        }

        private void SuspendRoomButton_Click(object sender, EventArgs e) => SuspendRoom();

        private void SuspendRoom()
        {
            if (GetProcs("StudentMain").Length > 0)
                NtSuspendProcess(GetProcs("StudentMain")[0].Id);
            if (GetProcs("REDAgent").Length > 0)
            {
                // killRedSpider = false;
                NtSuspendProcess(GetProcs("REDAgent")[0].Id);
            }
        }

        private void CloseRoomButton_Click(object sender, EventArgs e)
        {
            KillProcs("StudentMain");
            // killRedSpider = true;
        }

        private void KillProcs(string procName)
        {
            if (GetProcs(procName).Length > 0)
            {
                foreach (Process proc in GetProcs(procName))
                    proc.Kill();
            }
            if (GetProcs(procName).Length > 0)
            {
                foreach (Process proc in GetProcs(procName))
                    NtTerminateProcess(proc.Id);
            }
            if (GetProcs(procName).Length > 0)
            {
                foreach (Process proc in GetProcs(procName))
                    Run(ntsdPath, $"-c q -p {proc.Id}");
                new Thread(x =>
                {
                    Thread.Sleep(1500);
                    foreach (Process ntsdProc in GetProcs("ntsd"))
                        ntsdProc.Kill();
                }).Start();
            }
            if (GetProcs(procName).Length > 0)
            {
                foreach (Process proc in GetProcs(procName))
                    EndTask(proc.Handle, true, true);
            }
        }

        private Process[] GetProcs(string procName)
        {
            return Process.GetProcessesByName(procName);
        }

        private void ResumeRoomButton_Click(object sender, EventArgs e) => ResumeRoom();

        private void ResumeRoom()
        {
            // killRedSpider = false;
            if (GetProcs("StudentMain").Length > 0)
                NtResumeProcess(GetProcs("StudentMain")[0].Id);
            if (GetProcs("REDAgent").Length > 0)
                NtResumeProcess(GetProcs("REDAgent")[0].Id);
            else
            {
                if (File.Exists(roomPath))
                    Run(roomPath, "", true);
                else
                    GetMythwarePath("manual");
            }
        }

        private void DisableAttackButton_Click(object sender, EventArgs e)
        {
            DisableAttackButton.Enabled = false;
            DeviceManageButton.Enabled = false;
            File.Create(disableAttackFilePath);
        }

        private void UpdateProgramButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(@".\ITCHLauncher.exe"))
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFile("https://gitee.com/ujhhgtg/ITClassHelper/raw/master/bin/Release/ITCHLauncher.exe", @".\ITCHLauncher.exe");
                }
            }
            Run(@".\ITCHLauncher.exe", "-upd", true);
            Process.GetCurrentProcess().Kill();
        }

        private void GetMythwarePathButton_Click(object sender, EventArgs e) => GetMythwarePath("manual");

        private void GetMythwarePath(string getMethod)
        {
            if (roomPath != null)
            {
                if (getMethod != "quiet")
                    MessageBox.Show("已获取过了教室程序路径！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (GetProcs("StudentMain").Length != 0)
                {
                    roomPath = GetProcs("StudentMain")[0].MainModule.FileName;
                    if (getMethod != "quiet")
                        MessageBox.Show("已自动获取到教室程序路径！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string defaultRoomPath = @"C:\Program Files\Mythware\e-Learning Class\StudentMain.exe";
                    if (File.Exists(defaultRoomPath))
                    {
                        roomPath = defaultRoomPath;
                        if (getMethod != "quiet")
                            MessageBox.Show("已自动获取到教室程序路径！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (getMethod == "manual")
                        {
                            MessageBox.Show("无法自动找到教室程序路径！请在下一界面手动选择！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            OpenFileDialog fileDialog = new OpenFileDialog
                            {
                                Multiselect = false,
                                Title = "选择教室程序",
                                Filter = "教室程序(*.exe)|*.exe"
                            };
                            if (fileDialog.ShowDialog() == DialogResult.OK)
                            {
                                roomPath = fileDialog.FileName;
                            }
                        }
                    }
                }
            }
        }

        private void GetPswdButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("友情提示：可在任何时候使用超级密码 mythware_super_password！\n按[确定]继续获取；\n按[取消]将其复制到剪贴板并放弃获取。", "信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
            {
                Clipboard.SetDataObject("mythware_super_password");
                return;
            }
            RegistryKey pswdKey;
            try
            { pswdKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\TopDomain\e-Learning Class Standard\1.00"); }
            catch
            {
                MessageBox.Show("无法获取到教室密码！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string fullPswd = (string)pswdKey.GetValue("UninstallPasswd");
            MessageBox.Show($"密码为：{fullPswd.Substring(6)}", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetPswdButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("设置密码完成后将自动重启教室！\n按[确定]继续设置；\n按[取消]放弃设置。", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                return;
            if (MessageBox.Show("友情提示：可在任何时候使用超级密码 mythware_super_password！\n按[确定]继续设置；\n按[取消]将其复制到剪贴板并放弃设置。", "信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
            {
                Clipboard.SetDataObject("mythware_super_password");
                return;
            }
            string keyDir = @"HKEY_LOCAL_MACHINE\SOFTWARE\TopDomain\e-Learning Class Standard\1.00";
            string pswdText = Interaction.InputBox("请输入要设置的密码：", "信息");
            Registry.SetValue(keyDir, "UninstallPasswd", $@"Passwd{pswdText}");
            new Thread(x =>
            {
                KillProcs("StudentMain");
                Thread.Sleep(1000);
                ResumeRoom();
            })
            { IsBackground = true }.Start();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            if (firstTimeHide == true)
            {
                MessageBox.Show("机房助手已隐藏到后台，按下 Alt+H 即可显示！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                firstTimeHide = false;
            }
        }

        private void ExitProgramButton_Click(object sender, EventArgs e)
        {
            Network.socket.Close();
            HotKey.UnregisterHotKey(Handle, 100);
            Process.GetCurrentProcess().Kill();
        }

        private void DeviceManageButton_Click(object sender, EventArgs e) => deviceManage.Show();

        private void ChatButton_Click(object sender, EventArgs e)
        {
            string clientIP = Interaction.InputBox("请输入接收者的 IP 地址：", "信息");
            EndPoint clientPoint = new IPEndPoint(IPAddress.Parse(clientIP), 6666);
            byte[] bytes = Encoding.UTF8.GetBytes(Interaction.InputBox("请输入要发送的消息：", "信息"));
            try
            { Network.socket.SendTo(bytes, clientPoint); }
            catch (SocketException ex)
            {
                MessageBox.Show($"发送消息失败！\n错因：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("发送消息成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RemoveKeyboardHookButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("去除挂钩时将自动重启教室！\n按[确定]继续去除；\n按[取消]放弃去除。", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                return;
            GetMythwarePath("manual");
            string masterHelperPath = roomPath.Replace(@"StudentMain.exe", "MasterHelper.exe");
            KillProcs("StudentMain");
            KillProcs("MasterHelper");
            File.Delete(masterHelperPath);
            File.Create(masterHelperPath);
        }

        private void RedSpiderTerminator()
        {
            while (true)
            {
                if (killRedSpider == true)
                    KillProcs("REDAgent");
                Thread.Sleep(1000);
            }
        }

        protected override void WndProc(ref Message msg)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg.Msg)
            {
                case WM_HOTKEY:
                    switch (msg.WParam.ToInt32())
                    {
                        case 100:
                            Visible = !Visible;
                            break;
                    }
                    break;
            }
            base.WndProc(ref msg);
        }
    }
}