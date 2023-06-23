using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static ITClassHelper.Process;
using static ITClassHelper.SharedConst;
using static ITClassHelper.Text;
using static ITClassHelper.Window;

namespace ITClassHelper
{
    public partial class FormMain : Form
    {
        readonly FormCastControl castControl = new FormCastControl();
        readonly FormDeviceManage deviceManage = new FormDeviceManage();
        static bool firstTimeHide = true;

        public FormMain(string[] args)
        {
            if (GetProcs("ITClassHelper").Length > 1)
            {
                foreach (System.Diagnostics.Process programProc in GetProcs("ITClassHelper"))
                {
                    int procId = programProc.Id;
                    if (procId != System.Diagnostics.Process.GetCurrentProcess().Id)
                    {
                        MessageBox.Show("机房助手已在运行！点击[确认]退出当前进程！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                    }
                }
            }
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            ProgramAboutLabel.Text = ProgramAboutLabel.Text.Replace("X.Y.Z", programVersion);
            HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.Alt, Keys.H);
            castControl.Show();
            castControl.Hide();
            if (programVersion.Contains("-d"))
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

            byte[] RescLocalUpdateInfo = Properties.Resources.LocalUpdateInfo;
            using (FileStream localUpdateInfoFsObj = new FileStream(localUpdateConfigPath, FileMode.Create))
            {
                localUpdateInfoFsObj.Write(RescLocalUpdateInfo, 0, RescLocalUpdateInfo.Length);
            }

            byte[] RescRemoveCtrl = Properties.Resources.RemoveCtrl;
            using (FileStream removeCtrlFsObj = new FileStream(removeCtrlPath, FileMode.Create))
            {
                removeCtrlFsObj.Write(RescRemoveCtrl, 0, RescRemoveCtrl.Length);
            }

            if (Network.GetPortInUse(6666) != true)
            {
                try
                {
                    if (Network.socketBound == false)
                        Network.socket.Bind(new IPEndPoint(IPAddress.Parse(Network.GetIPAddress()), 6666));
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
                MessageBox.Show($"本机聊天端口(6666)被占用！将无法使用[简易内网聊天]功能！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ChatButton.Enabled = false;
            }

            File.Delete(cloudUpdateConfigPath);
            bool checkStatus = true;
            using (WebClient wc = new WebClient())
            {
                try { wc.DownloadFile($@"{programSite}/Resources/UpdateConfig.csv", cloudUpdateConfigPath); }
                catch (WebException) { UpdateProgramButton.Text = "更新检查失败"; checkStatus = false; }
            }
            if (checkStatus == true)
            {
                using (StreamReader localConfigSr = File.OpenText(localUpdateConfigPath))
                {
                    using (StreamReader cloudConfigSr = File.OpenText(cloudUpdateConfigPath))
                    {
                        string cloudVersion = ConvertTextToList(cloudConfigSr.ReadToEnd(), ',')[0];
                        string localVersion = ConvertTextToList(localConfigSr.ReadToEnd(), ',')[0];
                        string cloudStatus = ConvertTextToList(cloudConfigSr.ReadToEnd(), ',')[1];
                        string localStatus = ConvertTextToList(localConfigSr.ReadToEnd(), ',')[1];

                        if (cloudVersion != localVersion)
                        {
                            if (cloudStatus != "stable")
                                UpdateProgramButton.Text = $"发现实验性新版本 {cloudVersion}";
                            else
                                UpdateProgramButton.Text = $"发现新版本 {cloudVersion}";
                        }
                        else
                            UpdateProgramButton.Text = "暂无新版本";
                    }
                }
            }

            new Thread(BackgroundThread) { IsBackground = true }.Start();
            new Thread(ReceiveMessage) { IsBackground = true }.Start();
        }

        private void BackgroundThread()
        {
            while (true)
            {
                SetRoomPath(false);
                IntPtr castWindow = GetCastWindow();
                if (castWindow != IntPtr.Zero)
                {
                    int[] castWndInfo = GetWindowInfo(castWindow);
                    //MoveWindow(castWindow, castWndInfo[2], castWndInfo[3], castWndInfo[0], castWndInfo[1], true);
                    SetWindowPos(castWindow, WndPos.NoTopMost, castWndInfo[2], castWndInfo[3], castWndInfo[0], castWndInfo[1], (uint)SetWindowPosFlags.SWP_NOSIZE);
                    if (Visible == true)
                        TopMost = true;
                }
                else
                {
                    if (TopMost == true)
                        TopMost = false;
                }
                if (MousePosition == new Point(0, 0))
                {
                    MoveWindow(castWindow, castControl.Size.Width, castControl.Size.Height, 1000, 500, true);
                    castControl.Show();
                }
                if (GetProcs("StudentMain").Length > 0 || GetProcs("REDAgent").Length > 0)
                {
                    RoomStatusLabel.Text = "正在运行";
                    RoomStatusLabel.ForeColor = Color.Red;
                }
                else
                {
                    RoomStatusLabel.Text = "未在运行";
                    RoomStatusLabel.ForeColor = Color.Green;
                }
                Thread.Sleep(1000);
            }
        }

        private void SuspendRoomButton_Click(object sender, EventArgs e) => SuspendRoom();

        private void SuspendRoom()
        {
            if (GetProcs("StudentMain").Length > 0)
                NtSuspendProcess(GetProcs("StudentMain")[0].Id);
            if (GetProcs("REDAgent").Length > 0)
                NtSuspendProcess(GetProcs("REDAgent")[0].Id);
        }

        private void KillRoomButton_Click(object sender, EventArgs e) => KillRoom();

        private void KillRoom()
        {
            if (roomType == RoomType.Mythware)
                KillProcs("StudentMain");
            else
            {
                KillProcs("REDAgent");
                try { File.Move(roomPath, redSpiderBackupPath); }
                catch (IOException) { }
                catch (UnauthorizedAccessException) { }
                catch (ArgumentNullException) { }
            }
        }

        private void ResumeRoomButton_Click(object sender, EventArgs e) => ResumeRoom();

        private void ResumeRoom()
        {
            if (roomType == RoomType.Mythware)
                if (GetProcs("StudentMain").Length > 0)
                    NtResumeProcess(GetProcs("StudentMain")[0].Id);
                else
                {
                    if (GetProcs("REDAgent").Length > 0)
                        NtResumeProcess(GetProcs("REDAgent")[0].Id);
                    else
                    {
                        if (File.Exists(roomPath))
                            Run(roomPath, "", true);
                        else
                        {
                            try { File.Move(redSpiderBackupPath, roomPath); }
                            catch (IOException) { }
                            catch (UnauthorizedAccessException) { }
                        }
                    }
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
                    wc.DownloadFile($@"{programSite}bin/Release/ITCHLauncher.exe", @".\ITCHLauncher.exe");
                }
            }
            Run(@".\ITCHLauncher.exe", "", true);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void SetRoomPathButton_Click(object sender, EventArgs e) => SetRoomPath();

        private void SetRoomPath(bool passive = true)
        {
            bool gotRoomPath = false;
            if (roomPath != null)
            {
                if (passive == false) MessageBox.Show("已获取过了教室程序路径！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gotRoomPath = true;
            }
            else
            {
                if (GetProcs("StudentMain").Length > 0 || GetProcs("REDAgent").Length > 0)
                {
                    if (GetProcs("StudentMain").Length > 0)
                        roomPath = GetProcs("StudentMain")[0].MainModule.FileName;
                    else
                        roomPath = GetProcs("REDAgent")[0].MainModule.FileName;
                    if (passive == false) MessageBox.Show("已自动获取到教室程序路径！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gotRoomPath = true;
                }
                else
                {
                    string defRoomPath = @"C:\Program Files\Mythware\e-Learning Class\StudentMain.exe";
                    if (File.Exists(defRoomPath))
                    {
                        roomPath = defRoomPath;
                        if (passive == false) MessageBox.Show("已自动获取到教室程序路径！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gotRoomPath = true;
                    }
                    else
                    {
                        if (passive == false)
                        {
                            MessageBox.Show("无法自动找到教室程序路径！\n按[确定]在下一界面手动选择；\n按[取消]放弃。", "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            OpenFileDialog fileDialog = new OpenFileDialog
                            {
                                Multiselect = false,
                                Title = "选择教室程序",
                                Filter = "教室程序(*.exe)|*.exe"
                            };
                            if (fileDialog.ShowDialog() == DialogResult.OK)
                            {
                                roomPath = fileDialog.FileName;
                                gotRoomPath = true;
                            }
                        }
                    }
                }
            }
            if (gotRoomPath == true)
            {
                string roomName = roomPath.Substring(roomPath.LastIndexOf(@"\") + 1);
                if (roomName == "StudentMain.exe")
                    roomType = RoomType.Mythware;
                else if (roomName == "REDAgent.exe")
                    roomType = RoomType.RedSpider;
                else
                    if (passive == false)
                {
                    MessageBox.Show("获取到了一个无效的教室路径，教室类型已重置！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    roomType = RoomType.None;
                }
            }
        }

        private void ReceiveMessage()
        {
            while (true)
            {
                EndPoint serverPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] buffer = new byte[10240];
                int length = Network.socket.ReceiveFrom(buffer, ref serverPoint);
                string message = $"收到来自 {serverPoint} 的消息：" + Encoding.UTF8.GetString(buffer, 0, length);
                new Thread(x => MessageBox.Show(message, "消息", MessageBoxButtons.OK, MessageBoxIcon.None)).Start();
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
            {
                pswdKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\TopDomain\e-Learning Class Standard\1.00");
                string fullPswd = (string)pswdKey.GetValue("UninstallPasswd");
                MessageBox.Show($"密码为：{fullPswd.Substring(6)}", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("无法获取到教室密码！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
            System.Diagnostics.Process.GetCurrentProcess().Kill();
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
            SetRoomPath();
            string masterHelperPath = roomPath.Replace(@"StudentMain.exe", "MasterHelper.exe");
            KillProcs("StudentMain");
            KillProcs("MasterHelper");
            File.Delete(masterHelperPath);
            File.Create(masterHelperPath);
        }

        private void ProgramSettingsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ProgramSettingsCheckBox.Checked == true)
            {
                UpdateProgramButton.Visible = false;
                DisableAttackButton.Visible = false;
                SetRoomPathButton.Visible = false;
                DeviceManageButton.Visible = false;
                ChatButton.Visible = false;
            }
            else
            {

                UpdateProgramButton.Visible = true;
                DisableAttackButton.Visible = true;
                SetRoomPathButton.Visible = true;
                DeviceManageButton.Visible = true;
                ChatButton.Visible = true;
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