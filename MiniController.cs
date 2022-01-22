using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ITClassHelper
{
    public partial class MiniController : Form
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public MiniController()
        {
            InitializeComponent();
        }

        private void MiniController_Load(object sender, EventArgs e) { }

        private void HideCastButton_Click(object sender, EventArgs e) => HideCast();

        private void ShowCastButton_Click(object sender, EventArgs e) => ShowCast();

        private void HideControllerButton_Click(object sender, EventArgs e) => Hide();

        private void ShowCast()
        {
            IntPtr studentWindow = FindWindow(null, "屏幕演播室窗口");
            MoveWindow(studentWindow, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, true);
            Hide();
        }

        private void HideCast()
        {
            IntPtr studentWindow = FindWindow(null, "屏幕演播室窗口");
            MoveWindow(studentWindow, Size.Width, Size.Height, 0, 0, true);
        }

        private void HideTimeCastButton_Click(object sender, EventArgs e) => Hide();
    }
}
