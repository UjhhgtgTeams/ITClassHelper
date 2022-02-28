using System;
using System.Windows.Forms;
using static ITClassHelper.WndMgr;

namespace ITClassHelper
{
    public partial class FormCastControl : Form
    {
        public FormCastControl() => InitializeComponent();

        private void HideCastButton_Click(object sender, EventArgs e)
        {
            HideCast();
            Hide();
        }

        private void ShowCastButton_Click(object sender, EventArgs e) => ShowCast();

        private void HideControllerButton_Click(object sender, EventArgs e) => Hide();

        private void ShowCast()
        {
            IntPtr studentWindow = GetStudentWindow();
            MoveWindow(studentWindow, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, true);
            Hide();
        }

        private void HideCast()
        {
            IntPtr studentWindow = GetStudentWindow();
            MoveWindow(studentWindow, Size.Width, Size.Height, 0, 0, true);
        }

        private void MinimizeCastButton_Click(object sender, EventArgs e) => Hide();
    }
}
