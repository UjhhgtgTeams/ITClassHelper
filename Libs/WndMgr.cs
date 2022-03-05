using System;
using System.Runtime.InteropServices;

namespace ITClassHelper
{
    internal class WndMgr
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref Rect lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static IntPtr GetStudentWindow() => FindWindow(null, "屏幕演播室窗口");

        public static int[] GetWindowInfo(IntPtr hWnd)
        {
            Rect rect = new Rect();
            GetWindowRect(hWnd, ref rect);
            int width = rect.Right - rect.Left; int height = rect.Bottom - rect.Top;
            int x = rect.Left; int y = rect.Top;
            return new int[] { width, height, x, y };
        }
    }
}
