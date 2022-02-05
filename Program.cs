using System;
using System.Windows.Forms;

namespace ITClassHelper
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length == 2)
            {
                if (args[0] == "-rs")
                    RevShell.Reverse(args[1]);
            }
            else
                Application.Run(new FormMain());
        }
    }
}
