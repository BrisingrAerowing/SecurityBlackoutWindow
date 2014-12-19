using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;

namespace SecurityBlackoutWindow
{
    public static class BlackoutWindow
    {

        public static void Blackout(IntPtr hWnd, Action action)
        {
            Blackout(hWnd, () =>
            {
                action();
                return (object)null;
            });
        }

        public static T Blackout<T>(IntPtr hParentWnd, Func<T> func)
        {
            NativeMethods.WNDCLASSEX wndClass = new NativeMethods.WNDCLASSEX();
            
            wndClass.cbSize = (UInt32)Marshal.SizeOf(typeof(NativeMethods.WNDCLASSEX));
            wndClass.style = 0;
            wndClass.lpfnWndProc = NativeMethods.DefWindowProc;
            wndClass.cbClsExtra = 0;
            wndClass.cbWinExtra = 0;
            wndClass.hInstance = Process.GetCurrentProcess().Handle;
            wndClass.hIcon = IntPtr.Zero;
            wndClass.hIconSm = IntPtr.Zero;
            wndClass.hbrBackground = NativeMethods.GetSysColorBrush(NativeMethods.COLOR_DESKTOP);
            wndClass.lpszMenuName = null;
            wndClass.lpszClassName = "Blackout";

            IntPtr hWndClassEx = Marshal.AllocHGlobal((int)wndClass.cbSize);
            Marshal.StructureToPtr(wndClass, hWndClassEx, false);
            
            IntPtr hStopWnd;
            NativeMethods.RECT rectParent;
            
            if(NativeMethods.RegisterClassEx(hWndClassEx) == IntPtr.Zero)
            {
                throw new Win32Exception();
            }

            if (hParentWnd == IntPtr.Zero) hParentWnd = NativeMethods.GetDesktopWindow();

            NativeMethods.GetWindowRect(hParentWnd, out rectParent);

            int nWidth = rectParent.right - rectParent.left;
            int nHeight = rectParent.bottom - rectParent.top;

            hStopWnd = NativeMethods.CreateWindowEx(NativeMethods.WS_EX_LAYERED,
                                                    wndClass.lpszClassName,
                                                    null,
                                                    NativeMethods.WS_VISIBLE | NativeMethods.WS_POPUP,
                                                    rectParent.left,
                                                    rectParent.top,
                                                    nWidth,
                                                    nHeight,
                                                    hParentWnd,
                                                    IntPtr.Zero,
                                                    wndClass.hInstance,
                                                    IntPtr.Zero);

            if (hStopWnd == IntPtr.Zero) throw new Win32Exception();

            NativeMethods.SetLayeredWindowAttributes(hStopWnd, 0, 255, NativeMethods.LWA_ALPHA);

            Thread.Sleep(500);

            NativeMethods.SetLayeredWindowAttributes(hStopWnd, 0, 196, NativeMethods.LWA_ALPHA);

            var res = func();

            NativeMethods.DestroyWindow(hStopWnd);

            NativeMethods.UnregisterClass(wndClass.lpszClassName, wndClass.hInstance);

            try
            {
                Marshal.FreeHGlobal(hWndClassEx);
            }
            catch
            {

            }

            return res;
            
        }

    }
}
