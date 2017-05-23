using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace JHDock
{
    ///陈宝栋 2014-1-8创建
    /// <summary>
    /// WindowsAPI的公共类
    /// </summary>
    public class WinAPI
    {
        public enum Bool
        {
            False = 0,
            True
        }
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }
        [DllImport("JHMKAESCBC.dll")]
        public static extern Bool DecryptAESCBC(StringBuilder aValue, StringBuilder aKey, StringBuilder aIV, out StringBuilder aDestValue);
        [DllImport("user32.dll")]
        public static extern Bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern Bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern Bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")]
        public static extern uint SetBkColor(IntPtr hdc, int crColor);
        /// <summary>
        /// 该函数枚举所有屏幕上的顶层窗口，并将窗口句柄传送给应用程序定义的回调函数。
        /// 回调函数返回FALSE将停止枚举，否则EnumWindows函数继续到所有顶层窗口枚举完为止
        /// </summary>
        /// <param name="ewp">指向一个应用程序定义的回调函数指针</param>
        /// <param name="lParam">指定一个传递给回调函数的应用程序定义值</param>
        /// <returns>如果函数成功，返回值为非零；如果函数失败，返回值为零</returns>
        [DllImport("user32.dll")]
        public static extern int EnumWindows(GetWindows.EnumWindowsProc ewp, int lParam);
        /// <summary>
        /// 该函数获得给定窗口的可视状态
        /// </summary>
        /// <param name="hWnd">指定窗体句柄</param>
        /// <returns>返回窗体是否可视</returns>
        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32", EntryPoint = "mouse_event")]
        private static extern int mouse_event(
                                              int dwFlags,// 下表中标志之一或它们的组合 
                                              int dx,
                                              int dy, //指定x，y方向的绝对位置或相对位置 
                                              int cButtons,//没有使用 
                                              int dwExtraInfo//没有使用 
                                                ); 

        [DllImport("IpHlpApi.dll")]
        public extern static uint GetIfTable(byte[] pIfTable, ref uint pdwSize, bool bOrder);
        /// <summary>
        /// 获取当前活动窗体的句柄
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("User32")]
        public extern static int GetWindow(int hWnd, int wCmd);
        [DllImport("User32")]
        public extern static int GetWindowLongA(int hWnd, int wIndx);
        [DllImport("user32.dll")]
        public static extern bool GetWindowText(int hWnd, StringBuilder title, int maxBufSize);
        [DllImport("user32", CharSet = CharSet.Auto)]
        public extern static int GetWindowTextLength(IntPtr hWnd);
        [DllImport("user32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("user32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, uint lParam);
        [DllImport("user32.dll ")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll ")]
        public static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("User32.dll")]
        public static extern bool PtInRect(ref Rectangle r, Point p);
        ///<summary>
        /// 向指定窗口发送字符串
        ///</summary>
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);
        [DllImport("user32.dll ", EntryPoint = "FindWindow ")]
        public static extern int FindWindow(string lpClassName, int lpWindowName);
        [DllImport("user32.DLL")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll ", EntryPoint = "FindWindowEx ")]
        public static extern int FindWindowEx(int hWnd1, int hWnd2, string lpsz1, int lpsz2);
        [DllImport("user32.dll")]
        public static extern bool IsZoomed(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();
        [DllImport("user32", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32")]
        public static extern System.IntPtr GetWindowDC(System.IntPtr hwnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetActiveWindow();
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint dwProcessId);
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);
        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
        [DllImport("kernel32.dll")]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint dwFreeType);
        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr handle);
        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);
        // 函数功能：该函数改变指定窗口的位置和尺寸。对于顶层窗口，位置和尺寸是相对于屏幕的左上角的：对于子窗口，位置和尺寸是相对于父窗口客户区的左上角坐标的
        //hWnd：窗口句柄,X：指定窗口的新位置的左边,Y：指定窗口的新位置的顶部边界,nWidth：指定窗口的新的宽度,nHaight：指定窗口的新的高度。
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);
        // 获得窗口矩形
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hWnd, out Rectangle lpRect);
        // 获得客户区矩形
        [DllImport("user32.dll")]
        public static extern int GetClientRect(IntPtr hWnd, out Rectangle lpRect);
        ///<summary>
        /// 可将最小化窗口还原
        ///</summary>
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        // 注意，运行时知道如何列集一个矩形
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(int hwnd, ref Rectangle rc);
        ///<summary>
        /// 设置指定窗口为当前活动窗口
        ///</summary>
        ///<param name="hWnd">窗口句柄</param>
        [DllImport("User32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [Flags()]
        public enum KEYEVENTF
        {
            EXTENDEDKEY = 0x0001,
            KEYUP = 0x0002,
            UNICODE = 0x0004,
            SCANCODE = 0x0008,
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT
        {
            [FieldOffset(0)]
            public Int32 type;//0-MOUSEINPUT;1-KEYBDINPUT;2-HARDWAREINPUT
            [FieldOffset(4)]
            public KEYBDINPUT ki;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(4)]
            public HARDWAREINPUT hi;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public Int32 dx;
            public Int32 dy;
            public Int32 mouseData;
            public Int32 dwFlags;
            public Int32 time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public Int16 wVk;
            public Int16 wScan;
            public Int32 dwFlags;
            public Int32 time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            public Int32 uMsg;
            public Int16 wParamL;
            public Int16 wParamH;
        }

        [DllImport("user32.dll")]
        public static extern UInt32 SendInput(UInt32 nInputs, ref INPUT pInputs, int cbSize);

        [DllImport("user32.dll")]
        public static extern UInt32 SendInput(UInt32 nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll", EntryPoint = "GetMessageExtraInfo", SetLastError = true)]
        public static extern IntPtr GetMessageExtraInfo();

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);//设置鼠标位置
        //隐藏可编辑控件的光标
        [DllImport("user32", EntryPoint = "HideCaret")]
        public static extern bool HideCaret(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool MessageBeep(uint type);
        [DllImport("Shell32.dll")]
        public static extern int ExtractIconEx(string libName, int iconIndex, IntPtr[] largeIcon, IntPtr[] smallIcon, int nIcons);
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx,
        int cy, uint uFlags);
        [DllImport("User32.dll", EntryPoint = "SetWindowPos", SetLastError = true)]
        public static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndWinInsertAfter, int x, int y, int cx, int cy, int nFlags);

        #region 关于windows任务栏的API
        #region struct
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public Int32 left;
            public Int32 top;
            public Int32 right;
            public Int32 bottom;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public UInt32 cbSize;
            public IntPtr hWnd;
            public UInt32 uCallbackMessage;
            public UInt32 uEdge;
            public RECT rc;
            public Int32 lParam;
        }
        #endregion
        #region Enums
        public enum AppBarMessages
        {
            /// <summary>
            /// 注册一个新的自定义的appbar，并指定系统应使用发送通知消息到自定义的appbar消息标识符
            /// </summary>
            New = 0x00000000,
            /// <summary>
            ///注销一个appbar，从系统的内部列表中删除了
            /// </summary>
            Remove = 0x00000001,
            /// <summary>
            /// 请求一个appbar的大小和屏幕位置
            /// </summary>
            QueryPos = 0x00000002,
            /// <summary>
            /// 设置一个appbar的大小和屏幕位置
            /// </summary>
            SetPos = 0x00000003,
            /// <summary>
            /// Retrieves the autohide and always-on-top states of the 
            /// Microsoft?Windows?taskbar. 
            /// </summary>
            GetState = 0x00000004,
            /// <summary>
            /// Retrieves the bounding rectangle of the Windows taskbar. 
            /// </summary>
            GetTaskBarPos = 0x00000005,
            /// <summary>
            /// 通知一个appbar已经激活该系统
            /// </summary>
            Activate = 0x00000006,
            /// <summary>
            /// 获取与屏幕的特定边缘相关的自动隐藏自定义的appbar的句柄
            /// </summary>
            GetAutoHideBar = 0x00000007,
            /// <summary>
            /// 注册或取消注册一个自定义的appbar自动隐藏在屏幕的边缘
            /// </summary>
            SetAutoHideBar = 0x00000008,
            /// <summary>
            /// 通知系统，当一个appbar的位置发生了变化
            /// </summary>
            WindowPosChanged = 0x00000009,
            /// <summary>
            /// 设置自定义的appbar的自动隐藏的状态或者总是在最上层的属性
            /// </summary>
            SetState = 0x0000000a
        }


        public enum AppBarNotifications
        {
            /// <summary>
            /// Notifies an appbar that the taskbar's autohide or 
            /// always-on-top state has changed梩hat is, the user has selected 
            /// or cleared the "Always on top" or "Auto hide" check box on the
            /// taskbar's property sheet. 
            /// </summary>
            StateChange = 0x00000000,
            /// <summary>
            /// Notifies an appbar when an event has occurred that may affect 
            /// the appbar's size and position. Events include changes in the
            /// taskbar's size, position, and visibility state, as well as the
            /// addition, removal, or resizing of another appbar on the same 
            /// side of the screen.
            /// </summary>
            PosChanged = 0x00000001,
            /// <summary>
            /// 通知当一个全屏应用程序打开或关闭一个appbar。此通知是在一个由ABM_NEW消息设置一个应用程序定义的消息的形式发送
            /// </summary>
            FullScreenApp = 0x00000002,
            /// <summary>
            /// Notifies an appbar that the user has selected the Cascade, 
            /// Tile Horizontally, or Tile Vertically command from the 
            /// taskbar's shortcut menu.
            /// </summary>
            WindowArrange = 0x00000003
        }

        [Flags]
        public enum AppBarStates
        {
            AutoHide = 0x00000001,
            AlwaysOnTop = 0x00000002,
            AlwaysUnder = 0x00000003
        }


        public enum AppBarEdges
        {
            Left = 0,
            Top = 1,
            Right = 2,
            Bottom = 3,
            Float = 4
        }

        // Window Messages        
        public enum WM
        {
            ACTIVATE = 0x0006,
            WINDOWPOSCHANGED = 0x0047,
            NCHITTEST = 0x0084
        }

        public enum MousePositionCodes
        {
            HTERROR = (-2),
            HTTRANSPARENT = (-1),
            HTNOWHERE = 0,
            HTCLIENT = 1,
            HTCAPTION = 2,
            HTSYSMENU = 3,
            HTGROWBOX = 4,
            HTSIZE = HTGROWBOX,
            HTMENU = 5,
            HTHSCROLL = 6,
            HTVSCROLL = 7,
            HTMINBUTTON = 8,
            HTMAXBUTTON = 9,
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17,
            HTBORDER = 18,
            HTREDUCE = HTMINBUTTON,
            HTZOOM = HTMAXBUTTON,
            HTSIZEFIRST = HTLEFT,
            HTSIZELAST = HTBOTTOMRIGHT,
            HTOBJECT = 19,
            HTCLOSE = 20,
            HTHELP = 21
        }

        #endregion Enums
        [DllImport("shell32.dll")]
        public static extern UInt32 SHAppBarMessage(UInt32 dwMessage,ref APPBARDATA pData);
        #endregion

    }
}
