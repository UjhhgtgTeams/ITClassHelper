using System.Runtime.InteropServices;

namespace ITClassHelper
{
    internal class RoomHook
    {
        [DllImport("LibStHook.dll", CharSet = CharSet.Unicode)]
        public static extern void SetFakeImagePath(string x);
    }
}
