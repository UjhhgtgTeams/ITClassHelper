using System.Runtime.InteropServices;

namespace ITClassHelper
{
    internal class RoomHook
    {
        [DllImport("LibStHook.dll", CharSet = CharSet.Unicode)]
        public static extern void SetFakeImagePath(string x);

        [DllImport("LibStHook.dll")]
        public static extern void SetNoBlackScreen(bool x);

        [DllImport("LibStHook.dll")]
        public static extern void SetNoTopMostWindow(bool x);

        [DllImport("LibStHook.dll")]
        public static extern void SetEnableTerminate(bool x);

        [DllImport("LibStHook.dll")]
        public static extern void SetUnhookKeyboard(bool x);
    }
}
