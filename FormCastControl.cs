using System;
using System.Windows.Forms;
using static ITClassHelper.Window;

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
            IntPtr studentWindow = GetCastWindow();
            MoveWindow(studentWindow, WndPos.NoTopMost, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, SetWindowPosFlags.SWP_NOMOVE);
            Hide();
        }

        private void HideCast()
        {
            IntPtr studentWindow = GetCastWindow();
            MoveWindow(studentWindow, WndPos.NoTopMost, Size.Width, Size.Height, 0, 0, SetWindowPosFlags.SWP_NOMOVE);

        }

        private void MinimizeCastButton_Click(object sender, EventArgs e) => Hide();
    }
}
