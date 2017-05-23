using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JHDock
{
    /// <summary>
    /// 窗体属性的实体类
    /// 2014-03-20 陈宝栋创建 
    /// </summary>
    public class WindowsInfo
    {
        private IntPtr m_Handle;
        /// <summary> 
        /// 句柄 
        /// </summary> 
        public IntPtr Handle { get { return m_Handle; } set { m_Handle = value; } }
        private string m_Title;
        /// <summary> 
        /// 标题 
        /// </summary> 
        public string Title { get { return m_Title; } set { m_Title = value; } }
        private bool m_IsMinimzed;
        /// <summary> 
        /// 是否最小 
        /// </summary> 
        public bool IsMinimzed { get { return m_IsMinimzed; } set { m_IsMinimzed = value; } }
        private bool m_IsMaximized;
        /// <summary> 
        /// 是否最大      
        /// </summary> 
        public bool IsMaximized { get { return m_IsMaximized; } set { m_IsMaximized = value; } }

        public WindowsInfo()
        {
            m_Handle = IntPtr.Zero;
            m_Title = "";
            m_IsMinimzed = false;
            m_IsMaximized = false;
        }

        public WindowsInfo(IntPtr p_Handle, string p_Title, bool p_IsMinimized, bool p_IsMaximized)
        {
            this.m_Handle = p_Handle;
            this.m_Title = p_Title;
            this.m_IsMinimzed = p_IsMinimized;
            this.m_IsMaximized = p_IsMaximized;
        }
    }
   /// <summary> 
   /// 获取全部打开的窗体 
   /// 2014-03-20 陈宝栋创建 
   /// </summary> 
   public class GetWindows
   {
       
       //private static IList<WindowsInfo> _WindowsList = new List<WindowsInfo>();
       private static IntPtr _Statusbar;
       public delegate bool EnumWindowsProc(IntPtr p_Handle, int p_Param);
       private static bool isZoomed = false;
       private static bool NetEnumWindows(IntPtr p_Handle, int p_Param)
       {

           if (!WinAPI.IsWindowVisible(p_Handle))
               return true;
           StringBuilder _TitleString = new StringBuilder(256);
           WinAPI.GetWindowText(p_Handle, _TitleString, 256);
           if (string.IsNullOrEmpty(_TitleString.ToString())) 
           {
               return true;
           }         
           if (_TitleString.Length != 0 || (_TitleString.Length == 0) || p_Handle != _Statusbar)
           {
               if (WinAPI.IsZoomed(p_Handle))
               {
                   isZoomed = true;
                   return false;
               }
               //_WindowsList.Add(new WindowsInfo(p_Handle, _TitleString.ToString(), WinAPI.IsIconic(p_Handle), WinAPI.IsZoomed(p_Handle)));
           }
           return true;
       }


       public static bool Load()
       {
           isZoomed = false;
           //获取任务栏的句柄
           _Statusbar = WinAPI.FindWindow("Shell_TrayWnd", "");
           EnumWindowsProc _EunmWindows = new EnumWindowsProc(NetEnumWindows);
           WinAPI.EnumWindows(_EunmWindows, 0);
           return isZoomed;
       }
    }

}
