using System;
using System.Windows.Forms;

namespace ITClassHelper
{
    public partial class FormCastControl : Form
    {
        public FormCastControl() => InitializeComponent();

        private void FormController_Load(object sender, EventArgs e) { }

        private void HideCastButton_Click(object sender, EventArgs e) => HideCast();

        private void ShowCastButton_Click(object sender, EventArgs e) => ShowCast();

        private void HideControllerButton_Click(object sender, EventArgs e) => Hide();

        private void ShowCast()
        {
            IntPtr studentWindow = WindowMgr.GetStudentWindow();
            WindowMgr.MoveWindow(studentWindow, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, true);
            Hide();
        }

        private void HideCast()
        {
            IntPtr studentWindow = WindowMgr.GetStudentWindow();
            WindowMgr.MoveWindow(studentWindow, Size.Width, Size.Height, 0, 0, true);
        }

        private void HideTimeCastButton_Click(object sender, EventArgs e) => Hide();
    }
}
