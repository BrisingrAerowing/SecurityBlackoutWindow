using System;
using System.Runtime.InteropServices;

namespace SecurityBlackoutWindow
{
    internal static class NativeMethods
    {

        internal const Int32 LWA_ALPHA = 0x00000002;
        internal const UInt32 WS_POPUP = 0x80000000;
        internal const UInt32 WS_VISIBLE = 0x10000000;
        internal const UInt32 WS_EX_LAYERED = 0x00080000;
        internal const Int32 COLOR_DESKTOP = 1;

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate IntPtr WNDPROC(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        internal struct WNDCLASSEX
        {
            public UInt32 cbSize;
            public UInt32 style;
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public WNDPROC lpfnWndProc;
            public int cbClsExtra;
            public int cbWinExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            public string lpszMenuName;
            public string lpszClassName;
            public IntPtr hIconSm;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public Int32 left;
            public Int32 top;
            public Int32 right;
            public Int32 bottom;
        }

        [DllImport("User32.dll")]
        internal static extern IntPtr CreateWindowEx(UInt32 dwExStyle, string lpClassName, string lpWindowName, UInt32 dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        [DllImport("User32.dll")]
        internal static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("User32.dll")]
        internal static extern bool SetLayeredWindowAttributes(IntPtr hWnd, UInt32 crKey, byte bAlpha, int dwFlags);

        [DllImport("User32.dll")]
        internal static extern IntPtr DefWindowProc(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        internal static extern IntPtr RegisterClassEx(IntPtr wndClassEx);

        [DllImport("User32.dll")]
        internal static extern IntPtr GetDesktopWindow();

        [DllImport("User32.dll")]
        internal static extern IntPtr GetSysColorBrush(Int32 nIndex);

        [DllImport("User32.dll")]
        internal static extern bool GetWindowRect(IntPtr hWnd, [Out]out RECT lpRect);

        [DllImport("User32.dll")]
        internal static extern bool UnregisterClass(string lpClassName, IntPtr hInstance);



    }
}
