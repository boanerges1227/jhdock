using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace JHDock
{
    public class PerPixelAlphaForm : Form
    {
        //Dim CurrBitmap As Bitmap
        /// <summary>
        /// 构造
        /// </summary>
        public PerPixelAlphaForm()
        {
            //添加win7和xp系统的判断（解决功能提示信息在win7上显示不全的问题） 陈宝栋修改 2014-04-14
            this.FormBorderStyle = FormBorderStyle.None;
            OperatingSystem os = Environment.OSVersion;
            int isXt = os.Version.Major;
            //if (isXt == 5)
            //{
            //不可调整大小工具窗口边框

            //}
            //else if (isXt == 6)
            //{
            //    ////无边框
            //    this.FormBorderStyle = FormBorderStyle.None;
            //}
            ////可拖放
            //this.AllowDrop = true;
            //FormBorderStyle = FormBorderStyle.FixedToolWindow;
            // Register a unique message as our callback message
            //CallbackMessageID = RegisterCallbackMessage();
            //if (CallbackMessageID == 0)
            //    throw new Exception("RegisterCallbackMessage failed");
        }
        #region 图标移动
        //#region Enums
        //public enum AppBarMessages
        //{
        //    /// <summary>
        //    /// 注册一个新的自定义的appbar，并指定系统应使用发送通知消息到自定义的appbar消息标识符
        //    /// </summary>
        //    New = 0x00000000,
        //    /// <summary>
        //    ///注销一个appbar，从系统的内部列表中删除了
        //    /// </summary>
        //    Remove = 0x00000001,
        //    /// <summary>
        //    /// 请求一个appbar的大小和屏幕位置
        //    /// </summary>
        //    QueryPos = 0x00000002,
        //    /// <summary>
        //    /// 设置一个appbar的大小和屏幕位置
        //    /// </summary>
        //    SetPos = 0x00000003,
        //    /// <summary>
        //    /// Retrieves the autohide and always-on-top states of the 
        //    /// Microsoft?Windows?taskbar. 
        //    /// </summary>
        //    GetState = 0x00000004,
        //    /// <summary>
        //    /// Retrieves the bounding rectangle of the Windows taskbar. 
        //    /// </summary>
        //    GetTaskBarPos = 0x00000005,
        //    /// <summary>
        //    /// 通知一个appbar已经激活该系统
        //    /// </summary>
        //    Activate = 0x00000006,
        //    /// <summary>
        //    /// 获取与屏幕的特定边缘相关的自动隐藏自定义的appbar的句柄
        //    /// </summary>
        //    GetAutoHideBar = 0x00000007,
        //    /// <summary>
        //    /// 注册或取消注册一个自定义的appbar自动隐藏在屏幕的边缘
        //    /// </summary>
        //    SetAutoHideBar = 0x00000008,
        //    /// <summary>
        //    /// 通知系统，当一个appbar的位置发生了变化
        //    /// </summary>
        //    WindowPosChanged = 0x00000009,
        //    /// <summary>
        //    /// 设置自定义的appbar的自动隐藏的状态或者总是在最上层的属性
        //    /// </summary>
        //    SetState = 0x0000000a
        //}


        //public enum AppBarNotifications
        //{
        //    /// <summary>
        //    /// Notifies an appbar that the taskbar's autohide or 
        //    /// always-on-top state has changed梩hat is, the user has selected 
        //    /// or cleared the "Always on top" or "Auto hide" check box on the
        //    /// taskbar's property sheet. 
        //    /// </summary>
        //    StateChange = 0x00000000,
        //    /// <summary>
        //    /// Notifies an appbar when an event has occurred that may affect 
        //    /// the appbar's size and position. Events include changes in the
        //    /// taskbar's size, position, and visibility state, as well as the
        //    /// addition, removal, or resizing of another appbar on the same 
        //    /// side of the screen.
        //    /// </summary>
        //    PosChanged = 0x00000001,
        //    /// <summary>
        //    /// 通知当一个全屏应用程序打开或关闭一个appbar。此通知是在一个由ABM_NEW消息设置一个应用程序定义的消息的形式发送
        //    /// </summary>
        //    FullScreenApp = 0x00000002,
        //    /// <summary>
        //    /// Notifies an appbar that the user has selected the Cascade, 
        //    /// Tile Horizontally, or Tile Vertically command from the 
        //    /// taskbar's shortcut menu.
        //    /// </summary>
        //    WindowArrange = 0x00000003
        //}

        //[Flags]
        //public enum AppBarStates
        //{
        //    AutoHide = 0x00000001,
        //    AlwaysOnTop = 0x00000002,
        //    AlwaysUnder = 0x00000003
        //}


        //public enum AppBarEdges
        //{
        //    Left = 0,
        //    Top = 1,
        //    Right = 2,
        //    Bottom = 3,
        //    Float = 4
        //}

        //// Window Messages        
        //public enum WM
        //{
        //    ACTIVATE = 0x0006,
        //    WINDOWPOSCHANGED = 0x0047,
        //    NCHITTEST = 0x0084
        //}

        //public enum MousePositionCodes
        //{
        //    HTERROR = (-2),
        //    HTTRANSPARENT = (-1),
        //    HTNOWHERE = 0,
        //    HTCLIENT = 1,
        //    HTCAPTION = 2,
        //    HTSYSMENU = 3,
        //    HTGROWBOX = 4,
        //    HTSIZE = HTGROWBOX,
        //    HTMENU = 5,
        //    HTHSCROLL = 6,
        //    HTVSCROLL = 7,
        //    HTMINBUTTON = 8,
        //    HTMAXBUTTON = 9,
        //    HTLEFT = 10,
        //    HTRIGHT = 11,
        //    HTTOP = 12,
        //    HTTOPLEFT = 13,
        //    HTTOPRIGHT = 14,
        //    HTBOTTOM = 15,
        //    HTBOTTOMLEFT = 16,
        //    HTBOTTOMRIGHT = 17,
        //    HTBORDER = 18,
        //    HTREDUCE = HTMINBUTTON,
        //    HTZOOM = HTMAXBUTTON,
        //    HTSIZEFIRST = HTLEFT,
        //    HTSIZELAST = HTBOTTOMRIGHT,
        //    HTOBJECT = 19,
        //    HTCLOSE = 20,
        //    HTHELP = 21
        //}

        //#endregion Enums

        //#region AppBar Functions
        ///// <summary>
        ///// 安装工具栏
        ///// </summary>
        ///// <returns>是否成功</returns>
        //private Boolean AppbarNew()
        //{
        //    if (CallbackMessageID == 0)
        //        throw new Exception("CallbackMessageID is 0");

        //    if (IsAppbarMode)
        //        return true;
        //    m_PrevSize = Size;
        //    //单点登录主窗体宽度
        //    m_PrevSize.Width = 100;
        //    m_PrevLocation = this.Location;

        //    // 准备消息数据结构
        //    ShellApi.APPBARDATA msgData = new ShellApi.APPBARDATA();
        //    msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
        //    msgData.hWnd = this.Handle;
        //    msgData.uCallbackMessage = CallbackMessageID;

        //    // 安装新的应用程序栏工具栏
        //    UInt32 retVal = ShellApi.SHAppBarMessage((UInt32)AppBarMessages.New, ref msgData);
        //    IsAppbarMode = (retVal != 0);
        //    //SizeAppBar();
        //    return IsAppbarMode;
        //}
        ///// <summary>
        ///// 卸载工具栏
        ///// </summary>
        ///// <returns>是否成功</returns>
        //public Boolean AppbarRemove()
        //{
        //    // prepare data structure of message
        //    ShellApi.APPBARDATA msgData = new ShellApi.APPBARDATA();
        //    msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
        //    msgData.hWnd = this.Handle;

        //    // 卸载工具栏
        //    UInt32 retVal = ShellApi.SHAppBarMessage((UInt32)AppBarMessages.Remove, ref msgData);

        //    IsAppbarMode = false;

        //    Size = m_PrevSize;
        //    Location = m_PrevLocation;

        //    return (retVal != 0) ? true : false;
        //}

        //private void AppbarQueryPos(ref ShellApi.RECT appRect)
        //{
        //    // prepare data structure of message
        //    ShellApi.APPBARDATA msgData = new ShellApi.APPBARDATA();
        //    msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
        //    msgData.hWnd = this.Handle;
        //    msgData.uEdge = (UInt32)m_Edge;
        //    msgData.rc = appRect;

        //    // 查询工具栏的坐标
        //    ShellApi.SHAppBarMessage((UInt32)AppBarMessages.QueryPos, ref msgData);
        //    appRect = msgData.rc;
        //}

        //private void AppbarSetPos(ref ShellApi.RECT appRect)
        //{
        //    // 准备工具栏的数据
        //    ShellApi.APPBARDATA msgData = new ShellApi.APPBARDATA();
        //    msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
        //    msgData.hWnd = this.Handle;
        //    msgData.uEdge = (UInt32)m_Edge;
        //    msgData.rc = appRect;

        //    // 设置工具栏的坐标
        //    ShellApi.SHAppBarMessage((UInt32)AppBarMessages.SetPos, ref msgData);
        //    appRect = msgData.rc;
        //}

        //private void AppbarActivate()
        //{
        //    // prepare data structure of message
        //    ShellApi.APPBARDATA msgData = new ShellApi.APPBARDATA();
        //    msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
        //    msgData.hWnd = this.Handle;

        //    // send activate to the system
        //    ShellApi.SHAppBarMessage((UInt32)AppBarMessages.Activate, ref msgData);
        //}

        //private void AppbarWindowPosChanged()
        //{
        //    // prepare data structure of message
        //    ShellApi.APPBARDATA msgData = new ShellApi.APPBARDATA();
        //    msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
        //    msgData.hWnd = this.Handle;

        //    // send windowposchanged to the system 
        //    ShellApi.SHAppBarMessage((UInt32)AppBarMessages.WindowPosChanged, ref msgData);
        //}

        //private Boolean AppbarSetAutoHideBar()
        //{
        //    // prepare data structure of message
        //    ShellApi.APPBARDATA msgData = new ShellApi.APPBARDATA();
        //    msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
        //    msgData.hWnd = this.Handle;
        //    msgData.uEdge = (UInt32)m_Edge;
        //    msgData.lParam = (int)AppBarStates.AlwaysOnTop;

        //    // set auto hide
        //    UInt32 retVal = ShellApi.SHAppBarMessage((UInt32)AppBarMessages.SetAutoHideBar, ref msgData);
        //    return (retVal != 0) ? true : false;
        //}

        //private IntPtr AppbarGetAutoHideBar(AppBarEdges edge)
        //{
        //    // prepare data structure of message
        //    ShellApi.APPBARDATA msgData = new ShellApi.APPBARDATA();
        //    msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
        //    msgData.uEdge = (UInt32)edge;

        //    // get auto hide
        //    IntPtr retVal = (IntPtr)ShellApi.SHAppBarMessage((UInt32)AppBarMessages.GetAutoHideBar, ref msgData);
        //    return retVal;
        //}
        ///// <summary>
        ///// 获取系统工具栏的状态
        ///// </summary>
        ///// <returns>返回状态</returns>
        //private AppBarStates AppbarGetTaskbarState()
        //{
        //    // prepare data structure of message
        //    ShellApi.APPBARDATA msgData = new ShellApi.APPBARDATA();
        //    msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);

        //    // get taskbar state
        //    UInt32 retVal = ShellApi.SHAppBarMessage((UInt32)AppBarMessages.GetState, ref msgData);
        //    return (AppBarStates)retVal;
        //}

        //private void AppbarSetTaskbarState(AppBarStates state)
        //{
        //    // 准备消息的数据结构
        //    ShellApi.APPBARDATA msgData = new ShellApi.APPBARDATA();
        //    msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
        //    msgData.lParam = (Int32)state;
        //    msgData.hWnd = this.Handle;
        //    // 设置任务栏状态
        //    ShellApi.SHAppBarMessage((UInt32)AppBarMessages.SetState, ref msgData);
        //}

        //private void AppbarGetTaskbarPos(out ShellApi.RECT taskRect)
        //{
        //    // 准备消息的数据结构
        //    ShellApi.APPBARDATA msgData = new ShellApi.APPBARDATA();
        //    msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);

        //    // 获取任务栏的位置
        //    ShellApi.SHAppBarMessage((UInt32)AppBarMessages.GetTaskBarPos, ref msgData);
        //    taskRect = msgData.rc;
        //}

        //#endregion AppBar Functions

        //#region Private Variables

        //// saves the current edge
        //private AppBarEdges m_Edge = AppBarEdges.Float;

        //// saves the callback message id
        //private UInt32 CallbackMessageID = 0;

        //// are we in dock mode?
        //private Boolean IsAppbarMode = false;

        //// save the floating size and location
        //private Size m_PrevSize;
        //private Point m_PrevLocation;

        //#endregion Private Variables
        //private UInt32 RegisterCallbackMessage()
        //{
        //    String uniqueMessageString = Guid.NewGuid().ToString();
        //    return ShellApi.RegisterWindowMessage(uniqueMessageString);
        //}

        //private void SizeAppBar()
        //{
        //    ShellApi.RECT rt = new ShellApi.RECT();

        //    if ((m_Edge == AppBarEdges.Left) || (m_Edge == AppBarEdges.Right))
        //    {
        //        rt.top = 0;
        //        rt.bottom = SystemInformation.PrimaryMonitorSize.Height;
        //        if (m_Edge == AppBarEdges.Left)
        //        {
        //            rt.right = m_PrevSize.Width;
        //        }
        //        else
        //        {
        //            rt.right = SystemInformation.PrimaryMonitorSize.Width;
        //            rt.left = rt.right - m_PrevSize.Width;
        //        }
        //    }
        //    else
        //    {
        //        rt.left = 0;
        //        rt.right = SystemInformation.PrimaryMonitorSize.Width;
        //        if (m_Edge == AppBarEdges.Top)
        //        {
        //            rt.bottom = m_PrevSize.Height;
        //        }
        //        else
        //        {
        //            rt.bottom = SystemInformation.PrimaryMonitorSize.Height;
        //            rt.top = rt.bottom - m_PrevSize.Height;
        //        }
        //    }

        //    AppbarQueryPos(ref rt);

        //    switch (m_Edge)
        //    {
        //        case AppBarEdges.Left:
        //            rt.right = rt.left + m_PrevSize.Width;
        //            break;
        //        case AppBarEdges.Right:
        //            rt.left = rt.right - m_PrevSize.Width;
        //            break;
        //        case AppBarEdges.Top:
        //            rt.bottom = rt.top + m_PrevSize.Height;
        //            break;
        //        case AppBarEdges.Bottom:
        //            rt.top = rt.bottom - m_PrevSize.Height;
        //            break;
        //    }

        //    AppbarSetPos(ref rt);
        //    Location = new Point(rt.left, rt.top);
        //    Size = new Size(rt.right - rt.left, rt.bottom - rt.top);
        //}


        //#region Message Handlers

        ////void OnAppbarNotification(ref Message msg)
        ////{
        ////    AppBarStates state;
        ////    AppBarNotifications msgType = (AppBarNotifications)(Int32)msg.WParam;

        ////    switch (msgType)
        ////    {
        ////        case AppBarNotifications.PosChanged:
        ////            SizeAppBar();
        ////            break;

        ////        case AppBarNotifications.StateChange:
        ////            state = AppbarGetTaskbarState();
        ////            if ((state & AppBarStates.AlwaysOnTop) != 0)
        ////            {
        ////                TopMost = true;
        ////                BringToFront();
        ////            }
        ////            else
        ////            {
        ////                TopMost = false;
        ////                SendToBack();
        ////            }
        ////            break;

        ////        case AppBarNotifications.FullScreenApp:
        ////            if ((int)msg.LParam != 0)
        ////            {
        ////                TopMost = false;
        ////                SendToBack();
        ////            }
        ////            else
        ////            {
        ////                state = AppbarGetTaskbarState();
        ////                if ((state & AppBarStates.AlwaysOnTop) != 0)
        ////                {
        ////                    TopMost = true;
        ////                    BringToFront();
        ////                }
        ////                else
        ////                {
        ////                    TopMost = false;
        ////                    SendToBack();
        ////                }
        ////            }

        ////            break;

        ////        case AppBarNotifications.WindowArrange:
        ////            if ((int)msg.LParam != 0)    // before
        ////                Visible = false;
        ////            else                        // after
        ////                Visible = true;

        ////            break;
        ////    }
        ////}

        //void OnNcHitTest(ref Message msg)
        //{
        //    DefWndProc(ref msg);
        //    if ((m_Edge == AppBarEdges.Top) && ((int)msg.Result == (int)MousePositionCodes.HTBOTTOM))
        //        0.ToString();
        //    else if ((m_Edge == AppBarEdges.Bottom) && ((int)msg.Result == (int)MousePositionCodes.HTTOP))
        //        0.ToString();
        //    else if ((m_Edge == AppBarEdges.Left) && ((int)msg.Result == (int)MousePositionCodes.HTRIGHT))
        //        0.ToString();
        //    else if ((m_Edge == AppBarEdges.Right) && ((int)msg.Result == (int)MousePositionCodes.HTLEFT))
        //        0.ToString();
        //    else if ((int)msg.Result == (int)MousePositionCodes.HTCLOSE)
        //        0.ToString();
        //    else
        //    {
        //        msg.Result = (IntPtr)MousePositionCodes.HTCLIENT;
        //        return;
        //    }
        //    base.WndProc(ref msg);
        //}


        //#endregion Message Handlers

        //#region Window Procedure

        ////protected override void WndProc(ref Message msg)
        ////{
        ////    if (IsAppbarMode)
        ////    {
        ////        if (msg.Msg == CallbackMessageID)
        ////        {
        ////            OnAppbarNotification(ref msg);
        ////        }
        ////        else if (msg.Msg == (int)WM.ACTIVATE)
        ////        {
        ////            AppbarActivate();
        ////        }
        ////        else if (msg.Msg == (int)WM.WINDOWPOSCHANGED)
        ////        {
        ////            AppbarWindowPosChanged();
        ////        }
        ////        else if (msg.Msg == (int)WM.NCHITTEST)
        ////        {
        ////            OnNcHitTest(ref msg);
        ////            return;
        ////        }
        ////    }

        ////    base.WndProc(ref msg);
        ////}

        //#endregion Window Procedure

        //protected override void OnClosing(CancelEventArgs e)
        //{
        //    AppbarRemove();
        //    base.OnClosing(e);
        //}

        ////protected override void OnSizeChanged(EventArgs e)
        ////{
        ////    if (IsAppbarMode)
        ////    {
        ////        if (m_Edge == AppBarEdges.Top || m_Edge == AppBarEdges.Bottom)
        ////            m_PrevSize.Height = Size.Height;
        ////        else
        ////            m_PrevSize.Width = Size.Width;

        ////        SizeAppBar();
        ////    }

        ////    base.OnSizeChanged(e);
        ////}


        //public AppBarEdges Edge
        //{
        //    get
        //    {
        //        return m_Edge;
        //    }
        //    set
        //    {
        //        m_Edge = value;
        //        if (value == AppBarEdges.Float)
        //        {
        //            AppbarRemove();
        //        }
        //        else
        //        {
        //            AppbarNew();
        //        }

        //        if (IsAppbarMode)
        //        {
        //            SizeAppBar();
        //        }

        //    }
        //}
        #endregion
        #region 绘制窗体方法
        /// <summary>
        /// 设置图像
        /// </summary>
        /// <param name="bitmap"></param>
        public void SetBitmap(Bitmap bitmap)
        {
            SetBitmap(bitmap, 255);
        }
        /// <summary>
        /// 设置图像
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="opacity"></param>
        public void SetBitmap(Bitmap bitmap, byte opacity)
        {
            //如果图像象素非32位格式，则报错
            if (!(bitmap.PixelFormat == PixelFormat.Format32bppArgb))
            {
                throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");
            }
            //CurrBitmap = bitmap
            IntPtr screenDc = WinAPI.GetDC(IntPtr.Zero);
            IntPtr memDc = WinAPI.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;
            try
            {
                //2013-12-13 庞少军 这里有可能会内存溢出，还未解决
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBitmap = WinAPI.SelectObject(memDc, hBitmap);
                Size size = new Size(bitmap.Width, bitmap.Height);
                Point pointSource = new Point(0, 0);
                Point topPos = new Point(Left, Top);
                WinAPI.BLENDFUNCTION blend = new WinAPI.BLENDFUNCTION();
                blend.BlendOp = Win32.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = opacity;
                blend.AlphaFormat = Win32.AC_SRC_ALPHA;
                WinAPI.UpdateLayeredWindow(Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, Win32.ULW_ALPHA);
            }
            finally
            {
                WinAPI.ReleaseDC(IntPtr.Zero, screenDc);
                if (!(hBitmap.Equals(IntPtr.Zero)))
                {
                    WinAPI.SelectObject(memDc, oldBitmap);
                    WinAPI.DeleteObject(hBitmap);
                }
                WinAPI.DeleteDC(memDc);
            }
        }
        /// <summary>
        /// 重写
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                //置扩展窗口样式值的按钮组合位置：基类的值或524288
                cp.ExStyle = cp.ExStyle | (524288);
                return cp;
            }
        }
        #endregion
    }
    class Win32
    {

        //public enum Bool
        //{
        //    False = 0,
        //    True
        //}

        //<StructLayout(LayoutKind.Sequential)> _
        public struct Point
        {
            public Int32 x;

            public Int32 y;
            public Point(Int32 x, Int32 y)
            {
                this.x = x;
                this.y = y;
            }
        }

        //<StructLayout(LayoutKind.Sequential)> _
        public struct Size
        {
            public Int32 cx;

            public Int32 cy;
            public Size(Int32 cx, Int32 cy)
            {
                this.cx = cx;
                this.cy = cy;
            }
        }

        //<StructLayout(LayoutKind.SequentialPack = 1)> _
        public struct ARGB
        {
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
        }

        //<StructLayout(LayoutKind.Sequential, , 1)> _
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }
        public const Int32 ULW_COLORKEY = 1;
        public const Int32 ULW_ALPHA = 2;
        public const Int32 ULW_OPAQUE = 4;
        public const byte AC_SRC_OVER = 0;

        public const byte AC_SRC_ALPHA = 1;

    }
    
}
