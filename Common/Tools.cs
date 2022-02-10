using System.Diagnostics;

namespace ITClassHelper
{
    internal class Tools
    {
        public static void ExecuteProcess(string fileName, string arguments, bool noHide = false)
        {
            Process process = new Process();
            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments
            };
            if (noHide != true)
                processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo = processInfo;
            process.Start();
        }
    }
}
