using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace JHDock
{
    class ManipulationAppBar
    {


        /// <summary>
        /// 安装工具栏
        /// </summary>
        /// <returns>是否成功</returns>
        public Boolean AppbarNew(Size size,IntPtr Handle)
        {
            CallbackMessageID = RegisterCallbackMessage();
            if (CallbackMessageID == 0)
                throw new Exception("CallbackMessageID is 0");

            if (IsAppbarMode)
                return true;
            m_PrevSize =size;
            //单点登录主窗体宽度
            m_PrevSize.Width = 100;
            m_PrevLocation = new Point(0,0);
            // 准备消息数据结构
            WinAPI.APPBARDATA msgData = new WinAPI.APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.hWnd = Handle;
            msgData.uCallbackMessage = CallbackMessageID;

            // 安装新的应用程序栏工具栏
            UInt32 retVal = WinAPI.SHAppBarMessage((UInt32)WinAPI.AppBarMessages.New, ref msgData);
            IsAppbarMode = (retVal != 0);
            return IsAppbarMode;
        }
        public void SizeAppBar(IntPtr Handle, ref Point point, ref Size size)
        {
            WinAPI.RECT rt = new WinAPI.RECT();
            rt.top = 0;
            rt.bottom = SystemInformation.PrimaryMonitorSize.Height;
            rt.right = m_PrevSize.Width;
            AppbarQueryPos(ref rt, Handle);
            rt.right = 100;
            AppbarSetPos(ref rt, Handle);
            point = new Point(rt.left, rt.top);
            size = new Size(rt.right - rt.left, rt.bottom - rt.top);
        }
        private void AppbarQueryPos(ref WinAPI.RECT appRect, IntPtr Handle)
        {
            // prepare data structure of message
            WinAPI.APPBARDATA msgData = new WinAPI.APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.hWnd = Handle;
            msgData.uEdge = 0;
            msgData.rc = appRect;

            // 查询工具栏的坐标
            WinAPI.SHAppBarMessage((UInt32)WinAPI.AppBarMessages.QueryPos, ref msgData);
            appRect = msgData.rc;
        }

        private void AppbarSetPos(ref WinAPI.RECT appRect, IntPtr Handle)
        {
            // 准备工具栏的数据
            WinAPI.APPBARDATA msgData = new WinAPI.APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.hWnd = Handle;
            msgData.uEdge = 0;
            msgData.rc = appRect;

            // 设置工具栏的坐标
            WinAPI.SHAppBarMessage((UInt32)WinAPI.AppBarMessages.SetPos, ref msgData);
            appRect = msgData.rc;
        }
        /// <summary>
        /// 卸载工具栏
        /// </summary>
        /// <returns>是否成功</returns>
        public Boolean AppbarRemove(IntPtr Handle,ref Size size,ref Point Location)
        {
            // prepare data structure of message
            WinAPI.APPBARDATA msgData = new WinAPI.APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.hWnd = Handle;

            // 卸载工具栏
            UInt32 retVal = WinAPI.SHAppBarMessage((UInt32)WinAPI.AppBarMessages.Remove, ref msgData);

            IsAppbarMode = false;
            size = m_PrevSize;
            Location = m_PrevLocation;
            return (retVal != 0) ? true : false;
        }
        private UInt32 CallbackMessageID = 0;

        // are we in dock mode?
        private Boolean IsAppbarMode = false;

        // save the floating size and location
        private Size m_PrevSize;
        private Point m_PrevLocation;
        private UInt32 RegisterCallbackMessage()
        {
            String uniqueMessageString = Guid.NewGuid().ToString();
            return ShellApi.RegisterWindowMessage(uniqueMessageString);
        }

    }
}
