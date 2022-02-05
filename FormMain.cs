using Microsoft.VisualBasic;
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

namespace ITClassHelper
{
    public partial class FormMain : Form
    {
        readonly FormCastControl castControl = new FormCastControl();
        readonly FormDeviceManage deviceManage = new FormDeviceManage();
        static readonly string ProgramVersion = "3.1.0-d";
        static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ITClassHelper";
        static readonly string ncPath = path + @"\nc.exe";
        static readonly string ntsdPath = path + @"\ntsd.exe";
        static readonly string killerPath = path + @"\ComputerKiller.py";
        static readonly string disableAttackFilePath = path + @"\disableAttack.txt";
        static string roomPath;
        static EndPoint serverPoint = null;
        static bool firstTimeHide = true;

        public FormMain()
        {
            if (Process.GetProcessesByName("ITClassHelper").Length > 1)
            {
                foreach (Process programProc in Process.GetProcessesByName("ITClassHelper"))
                {
                    if (programProc.Id != Process.GetCurrentProcess().Id)
                    {
                        string[] procArgs = ProcessMgr.ConvertCommandLineArgs(ProcessMgr.GetCommandLineArgs(programProc));
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
            if (ProgramVersion.Contains("-d"))
                MessageBox.Show("这是一个实验性版本，尚不稳定，请小心操作！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(path);

            if (File.Exists(disableAttackFilePath))
            {
                DisableAttackButton.Enabled = false;
                DeviceManageButton.Enabled = false;
            }

            if (!File.Exists(ntsdPath))
            {
                byte[] RescNtsd = Properties.Resources.Ntsd;
                FileStream ntsdFsObj = new FileStream(ntsdPath, FileMode.CreateNew);
                ntsdFsObj.Write(RescNtsd, 0, RescNtsd.Length);
                ntsdFsObj.Close();
            }

            byte[] RescNetCat = Properties.Resources.NetCat;
            FileStream netCatFsObj = new FileStream(ncPath, FileMode.Create);
            netCatFsObj.Write(RescNetCat, 0, RescNetCat.Length);
            netCatFsObj.Close();

            byte[] RescKiller = Properties.Resources.ComputerKiller;
            FileStream killerFsObj = new FileStream(killerPath, FileMode.Create);
            killerFsObj.Write(RescKiller, 0, RescKiller.Length);
            killerFsObj.Close();

            try
            {
                if (Network.binded == false) Network.socket.Bind(new IPEndPoint(IPAddress.Parse(Network.GetIPAddress()), 6666));
                Network.binded = true;
            }
            catch (SocketException ex)
            {
                MessageBox.Show($"本机 IP 地址绑定失败！将无法使用[简易内网聊天]功能！\n错因：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            new Thread(LoopThread) { IsBackground = true }.Start();
            new Thread(MessageRecieve) { IsBackground = true }.Start();
        }

        private void LoopThread()
        {
            while (true)
            {
                IntPtr studentWindow = HandleMgr.GetStudentWindow();
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
                    TopMost = false;
                }
                if (MousePosition == new Point(0, 0))
                {
                    HandleMgr.MoveWindow(studentWindow, castControl.Size.Width, castControl.Size.Height, 1000, 500, true);
                    castControl.Show();
                }
                if (GetStudentProcs().Length == 0)
                {
                    RoomStatusLabel.Text = "未在运行";
                    RoomStatusLabel.ForeColor = Color.Green;
                }
                else
                {
                    RoomStatusLabel.Text = "正在运行";
                    RoomStatusLabel.ForeColor = Color.Red;
                }
                GetRoomPath("quiet");
                Thread.Sleep(1500);
            }
        }

        private void MessageRecieve()
        {
            while (true)
            {
                if (serverPoint != null)
                {
                    byte[] buffer = new byte[10240];
                    int length = Network.socket.ReceiveFrom(buffer, ref serverPoint);
                    string message = Encoding.UTF8.GetString(buffer, 0, length);
                    string fullMessage = $"收到来自 {serverPoint} 的消息：";
                    new Thread(x => MessageBox.Show(fullMessage, "消息", MessageBoxButtons.OK, MessageBoxIcon.None)).Start();
                }
            }
        }

        private void PauseRoomButton_Click(object sender, EventArgs e) => PauseRoom();

        private void PauseRoom()
        {
            if (GetStudentProcs().Length > 0)
                ProcessMgr.SuspendProcess(GetStudentProcs()[0].Id);
        }

        private void CloseRoomButton_Click(object sender, EventArgs e) => CloseRoom();

        private void CloseRoom()
        {
            if (GetStudentProcs().Length > 0)
                ProcessMgr.TerminateProcess(GetStudentProcs()[0].Id);
            if (GetStudentProcs().Length > 0)
            {
                Tools.ExecuteProcess(ntsdPath, "-c q -pn StudentMain.exe");
                new Thread(x =>
                {
                    Thread.Sleep(1000);
                    foreach (Process ntsdProc in Process.GetProcessesByName("ntsd"))
                        ntsdProc.Kill();
                }).Start();
            }
        }

        private Process[] GetStudentProcs()
        {
            return Process.GetProcessesByName("StudentMain");
        }

        private void RecoverRoomButton_Click(object sender, EventArgs e) => RecoverRoom();

        private void RecoverRoom()
        {
            if (GetStudentProcs().Length > 0)
                ProcessMgr.ResumeProcess(GetStudentProcs()[0].Id);
            else
            {
                if (File.Exists(roomPath))
                    Tools.ExecuteProcess(roomPath, "", true);
                else
                    GetRoomPath("manual");
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
            Tools.ExecuteProcess(@".\ITCHLauncher.exe", "-upd", true);
            Process.GetCurrentProcess().Kill(); ;
        }

        private void GetRoomPathButton_Click(object sender, EventArgs e) => GetRoomPath("manual");

        private void GetRoomPath(string getMethod)
        {
            if (roomPath != null)
            {
                if (getMethod != "quiet")
                    MessageBox.Show("已获取过了教室程序路径！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (GetStudentProcs().Length != 0)
                {
                    roomPath = GetStudentProcs()[0].MainModule.FileName;
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
            RegistryKey pswdKey = null;
            try
            { pswdKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\TopDomain\e-Learning Class Standard\1.00"); }
            catch
            {
                if (MessageBox.Show("无法从默认位置获取到教室密码！\n按[确定]在下一个窗口手动输入由'SOFTWARE\\'开头的路径；\n按[取消]放弃读取。", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    return;
                string inputSubKey = Interaction.InputBox("请手动输入由'SOFTWARE\\'开头的路径：", "信息");
                try
                { pswdKey = Registry.LocalMachine.OpenSubKey(inputSubKey); }
                catch { MessageBox.Show("无法从此位置获取到教室密码！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            string fullPswd = (string)pswdKey.GetValue("UninstallPasswd");
            MessageBox.Show($"密码为：{fullPswd.Substring(6)}", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetPswdButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("设置密码完成后将自动重启教室！\n按[确定]继续设置；\n按[取消]放弃设置。", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                return;
            string keyDir = @"HKEY_LOCAL_MACHINE\SOFTWARE\TopDomain\e-Learning Class Standard\1.00";
            string pswdText = Interaction.InputBox("请输入要设置的密码：", "信息");
            Registry.SetValue(keyDir, "UninstallPasswd", $@"Passwd{pswdText}");
            new Thread(x =>
            {
                CloseRoom();
                Thread.Sleep(1000);
                RecoverRoom();
            }) { IsBackground = true }.Start();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            if (firstTimeHide == true)
            {
                MessageBox.Show("机房助手已自动隐藏到后台，按下 Alt+H 即可显示！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                firstTimeHide = false;
            }
        }

        private void ExitProgramButton_Click(object sender, EventArgs e)
        {
            Network.socket.Close();
            Process.GetCurrentProcess().Kill(); ;
        }

        private void DeviceManageButton_Click(object sender, EventArgs e)
        {
            deviceManage.ShowDialog();
        }

        private void ChatButton_Click(object sender, EventArgs e)
        {
            string chatType = Interaction.InputBox("请选择聊天模式：\n1：发送    2：接收", "信息");
            if (chatType == "1")
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
            else if (chatType == "2")
            {
                string serverIP = Interaction.InputBox("请输入发送者的 IP 地址：", "信息");
                serverPoint = new IPEndPoint(IPAddress.Parse(serverIP), 6666);
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