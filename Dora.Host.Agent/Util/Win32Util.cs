using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dora.Agent.Util
{
    public class Win32Util
    {
        static IntPtr HWND_BROADCAST = new IntPtr(0xffff);
        public static void CloseScreen()
        {
            Win32Api.SendMessage(HWND_BROADCAST, Win32Api.WM_SYSCOMMAND, Win32Api.SC_MONITORPOWER, 2); //关闭显示器;
        }
        public static void OpenScreen()
        {
            Win32Api.SendMessage(HWND_BROADCAST, Win32Api.WM_SYSCOMMAND, Win32Api.SC_MONITORPOWER, -1); //打开显示器;
        }
    }

    /// <summary>
    /// 1. DllImport
    /// 2. static extern
    /// </summary>
    class Win32Api
    {
        public const uint WM_SYSCOMMAND = 0x0112;
        public const uint SC_MONITORPOWER = 0xF170;

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, uint wParam, int lParam);
    }
}
