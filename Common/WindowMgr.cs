using System;
using System.Runtime.InteropServices;

namespace ITClassHelper
{
    internal class WindowMgr
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static IntPtr GetStudentWindow() => FindWindow(null, "屏幕演播室窗口");

        public static int[] GetWindowInfo(IntPtr hWnd)
        {
            RECT fx = new RECT();
            GetWindowRect(hWnd, ref fx);
            int width = fx.Right - fx.Left; int height = fx.Bottom - fx.Top;
            int x = fx.Left; int y = fx.Top;
            return new int[] { width, height, x, y };
        }
    }
}
