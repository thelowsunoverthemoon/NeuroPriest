using System;
using System.Runtime.InteropServices;

namespace NeuroPriest.Shared
{
    internal static class WinWrapper
    {
        public const int VK_CTRL = 0x11;
        public const long KF_UP = 0x80000000;
        public const int WH_KEYBOARD = 2;

        internal enum ArrowKeys
        {
            VK_RIGHT = 0x27,
            VK_LEFT = 0x25,
            VK_UP = 0x26,
            VK_DOWN = 0x28
        }

        public delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

        // investiage setlasterror meaning?
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(
            int hookType,
            HookProc lpfn,
            IntPtr hMod,
            uint dwThreadId
        );

        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentThreadId();

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(
            IntPtr hhk,
            int nCode,
            IntPtr wParam,
            IntPtr lParam
        );

        [DllImport("user32.dll")]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateEventA(
            IntPtr lpEventAttributes,
            bool bManualReset,
            bool bInitialState,
            string lpName
        );

        [DllImport("kernel32.dll")]
        public static extern uint SignalObjectAndWait(
            IntPtr hObjectToSignal,
            IntPtr hObjectToWaitOn,
            uint dwMilliseconds,
            bool bAlertable
        );

        [DllImport("kernel32.dll")]
        public static extern bool SetEvent(IntPtr hEvent);

        [DllImport("kernel32.dll")]
        public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
    }
}
