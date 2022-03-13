using System;

namespace ITClassHelper
{
    internal class Shared
    {
        public static RoomType roomType = RoomType.Mythware;
        public static string roomPath;
        public static readonly string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ITClassHelper";

        public enum RoomType
        {
            Mythware = 1,
            RedSpider = 2
        }
    }
}
