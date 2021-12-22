using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ITClassHelper
{
    public partial class CastController : Form
    {
        int ScreenX;
        int ScreenY;

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public CastController()
        {
            InitializeComponent();
            ScreenX = Screen.PrimaryScreen.Bounds.Width;
            ScreenY = Screen.PrimaryScreen.Bounds.Height;
        }

        private void CastController_Load(object sender, EventArgs e)
        {}

        private void HideCastButton_Click(object sender, EventArgs e)
        { HideCast(); }

        private void ShowCastButton_Click(object sender, EventArgs e)
        { ShowCast(); }

        private void ShowCast()
        {
            IntPtr studentWindow = FindWindow(null, "屏幕演播室窗口");
            MoveWindow(studentWindow, 0, 0, ScreenX, ScreenY, true);
            Hide();
        }

        private static void HideCast()
        {
            IntPtr studentWindow = FindWindow(null, "屏幕演播室窗口");
            MoveWindow(studentWindow, 446, 114, 0, 0, true);
        }

        private void HideTimeCastButton_Click(object sender, EventArgs e)
        {
            HideCast();
            Thread.Sleep(5000);
            ShowCast();
        }

        private void CloseAppButton_Click(object sender, EventArgs e)
        {
            CloseApp();
        }

        private void CloseApp()
        {
            string ntsdPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ntsd.exe";
            ExecuteProcess(ntsdPath, "-c q -pn StudentMain.exe");
            new Thread(CleanupNtsd).Start();
        }

        private void ExecuteProcess(string process, string arguments)
        {
            Process ExeProcess = new Process();
            ProcessStartInfo ExeProcessInfo;
            ExeProcessInfo = new ProcessStartInfo
            {
                FileName = process,
                Arguments = arguments,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            ExeProcess.StartInfo = ExeProcessInfo;
            ExeProcess.Start();
        }

        private void CleanupNtsd()
        {
            Thread.Sleep(1000);
            ExecuteProcess("taskkill", "/f /im ntsd.exe");
        }
    }
}
