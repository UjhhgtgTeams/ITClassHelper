using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;

namespace ITClassHelper
{
    internal class ProcMgr
    {
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

        public static void TerminateProcess(int processId)
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

        public static void SuspendProcess(int processId)
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

        public static void ResumeProcess(int processId)
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

        public static string[] GetProcessArgs(Process proc)
        {
            string args;
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                "SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + proc.Id))
            using (ManagementObjectCollection objects = searcher.Get())
            {
                ManagementBaseObject @object = objects.Cast<ManagementBaseObject>().SingleOrDefault();
                args = @object?["CommandLine"]?.ToString() ?? "";
            }
            IntPtr argv = CommandLineToArgvW(args, out var argc);
            if (argv == IntPtr.Zero)
            {
                return null;
            }
            try
            {
                string[] argsArray = new string[argc];
                for (var i = 0; i < argsArray.Length; i++)
                {
                    IntPtr p = Marshal.ReadIntPtr(argv, i * IntPtr.Size);
                    argsArray[i] = Marshal.PtrToStringUni(p);
                }
                return argsArray;
            }
            finally
            {
                Marshal.FreeHGlobal(argv);
            }
        }

        public static void KillProcessTree(int procId)
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + procId);
            ManagementObjectCollection moc = mos.Get();
            foreach (ManagementObject mo in moc)
            {
                TerminateProcess(Convert.ToInt32(mo["ProcessID"]));
            }
        }
    }
}
