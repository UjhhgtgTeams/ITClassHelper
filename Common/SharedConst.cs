using System;

namespace ITClassHelper
{
    internal class SharedConst
    {
        public static RoomType roomType = RoomType.None;
        public static string roomPath;
        public static readonly string programVersion = "3.3.5";
        public static readonly string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ITClassHelper";
        public static readonly string programSite = @"https://gitee.com/ujhhgtg/ITClassHelper/raw/master/";
        public static readonly string disableAttackFilePath = appDataPath + @"\disableAttack.txt";
        public static readonly string redSpiderBackupPath = appDataPath + @"\REDAgent.exe";
        public static readonly string removeCtrlPath = appDataPath + @"\RemoveCtrl.exe";
        public static readonly string localUpdateConfigPath = appDataPath + @"\LocalUpdateConfig.csv";
        public static readonly string cloudUpdateConfigPath = appDataPath + @"\CloudUpdateConfig.csv";

        public enum RoomType
        {
            None = 0,
            Mythware = 1,
            RedSpider = 2
        }
    }
}
