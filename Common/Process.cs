using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;

namespace ITClassHelper
{
    internal class Process
    {
        public static readonly string ntsdPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ITClassHelper" + @"\ntsd.exe";

        [Flags]
        public enum ProcessAccess : uint
        {
            Terminate = 0x1,
            CreateThread = 0x2,
            SetSessionId = 0x4,
            VmOperation = 0x8,
            VmRead = 0x10,
            VmWrite = 0x20,
            DupHandle = 0x40,
            CreateProcess = 0x80,
            SetQuota = 0x100,
            SetInformation = 0x200,
            QueryInformation = 0x400,
            SetPort = 0x800,
            SuspendResume = 0x800,
            QueryLimitedInformation = 0x1000,
            Synchronize = 0x100000
        }

        [DllImport("ntdll.dll")]
        private static extern uint NtResumeProcess([In] IntPtr processHandle);

        [DllImport("ntdll.dll")]
        private static extern uint NtSuspendProcess([In] IntPtr processHandle);

        [DllImport("ntdll.dll")]
        private static extern uint NtTerminateProcess([In] IntPtr processHandle);

        [DllImport("user32.dll")]
        public static extern bool EndTask(IntPtr hWnd, bool fShutDown, bool fForce);

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(
            ProcessAccess desiredAccess,
            bool inheritHandle,
            int processId
        );

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle([In] IntPtr handle);

        [DllImport("shell32.dll")]
        private static extern IntPtr CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, out int pNumArgs);

        public static void NtTerminateProcess(int processId)
        {
            IntPtr hProc = IntPtr.Zero;
            try
            {
                hProc = OpenProcess(ProcessAccess.Terminate, false, processId);
                if (hProc != IntPtr.Zero)
                    NtTerminateProcess(hProc);
            }
            finally
            {
                if (hProc != IntPtr.Zero)
                    CloseHandle(hProc);
            }
        }

        public static void NtSuspendProcess(int processId)
        {
            IntPtr hProc = IntPtr.Zero;
            try
            {
                hProc = OpenProcess(ProcessAccess.SuspendResume, false, processId);
                if (hProc != IntPtr.Zero)
                    NtSuspendProcess(hProc);
            }
            finally
            {
                if (hProc != IntPtr.Zero)
                    CloseHandle(hProc);
            }
        }

        public static void NtResumeProcess(int processId)
        {
            IntPtr hProc = IntPtr.Zero;
            try
            {
                hProc = OpenProcess(ProcessAccess.SuspendResume, false, processId);
                if (hProc != IntPtr.Zero)
                    NtResumeProcess(hProc);
            }
            finally
            {
                if (hProc != IntPtr.Zero)
                    CloseHandle(hProc);
            }
        }

        public static void Run(string fileName, string arguments, bool hide = true, bool waitForExit = false)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments
            };
            if (hide == true)
            {
                processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            process.StartInfo = processInfo;
            process.Start();
            if (waitForExit == true)
            {
                process.WaitForExit();
            }
        }

        public static string[] GetProcessArgs(int procId)
        {
            string argsString;
            using (ManagementObjectSearcher mos = new ManagementObjectSearcher("Select CommandLine From Win32_Process Where ProcessId = " + procId))
            {
                using (ManagementObjectCollection moc = mos.Get())
                {
                    ManagementBaseObject @object = moc.Cast<ManagementBaseObject>().SingleOrDefault();
                    argsString = @object?["CommandLine"]?.ToString() ?? "";
                }
            }
            IntPtr argsRaw = CommandLineToArgvW(argsString, out var argc);
            if (argsRaw == IntPtr.Zero)
                return null;
            try
            {
                string[] argsArray = new string[argc];
                for (var i = 0; i < argsArray.Length; i++)
                {
                    IntPtr p = Marshal.ReadIntPtr(argsRaw, i * IntPtr.Size);
                    argsArray[i] = Marshal.PtrToStringUni(p);
                }
                return argsArray;
            }
            finally
            {
                Marshal.FreeHGlobal(argsRaw);
            }
        }

        public static void KillProcs(string procName)
        {
            if (GetProcsByName(procName).Length > 0)
            {
                foreach (System.Diagnostics.Process proc in GetProcsByName(procName))
                    proc.Kill();
            }
            if (GetProcsByName(procName).Length > 0)
            {
                foreach (System.Diagnostics.Process proc in GetProcsByName(procName))
                    NtTerminateProcess(proc.Id);
            }
            if (GetProcsByName(procName).Length > 0)
            {
                foreach (System.Diagnostics.Process proc in GetProcsByName(procName))
                    Run(ntsdPath, $"-c q -p {proc.Id}");
                new Thread(x =>
                {
                    Thread.Sleep(1500);
                    foreach (System.Diagnostics.Process ntsdProc in GetProcsByName("ntsd"))
                        ntsdProc.Kill();
                }).Start();
            }
            if (GetProcsByName(procName).Length > 0)
            {
                foreach (System.Diagnostics.Process proc in GetProcsByName(procName))
                {
                    try { EndTask(proc.Handle, true, true); }
                    catch { }
                }
            }
        }

        public static System.Diagnostics.Process[] GetProcsByName(string procName)
        {
            return System.Diagnostics.Process.GetProcessesByName(procName);
        }

        public static void KillChildProcs(int parentProcId)
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID = " + parentProcId);
            ManagementObjectCollection moc = mos.Get();
            foreach (ManagementObject mo in moc.Cast<ManagementObject>())
            {
                KillProcs(System.Diagnostics.Process.GetProcessById(Convert.ToInt32(mo["ProcessID"])).ProcessName);
            }
        }
    }
}
