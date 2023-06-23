using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ITClassHelper
{
    internal class HotKey
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
}
