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
            IntPtr castWindow = GetCastWindow();
            SetWindowPos(castWindow, WndPos.NoTopMost, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, (uint)SWP.SWP_SHOWWINDOW);
            Hide();
        }

        private void HideCast()
        {
            IntPtr castWindow = GetCastWindow();
            SetWindowPos(castWindow, WndPos.NoTopMost, Size.Width, Size.Height, 0, 0, (uint)SWP.SWP_SHOWWINDOW);

        }

        private void MinimizeCastButton_Click(object sender, EventArgs e) => Hide();

        private void SetCastTitleBarButton_Click(object sender, EventArgs e)
        {
            IntPtr castWindow = GetCastWindow();
            if (castWindow != IntPtr.Zero)
            {
                if (SetCastTitleBarButton.Text == "显示\n标题栏")
                {
                    ShowWindowTitleBar(castWindow);
                    SetCastTitleBarButton.Text = "隐藏\n标题栏";
                }
                else
                {
                    HideWindowTitleBar(castWindow);
                    SetCastTitleBarButton.Text = "显示\n标题栏";
                }
            }
        }
    }
}
