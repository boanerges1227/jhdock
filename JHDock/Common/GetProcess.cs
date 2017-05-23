using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace JHDock
{
    class GetProcess
    {

        #region 查找所有应用程序标题

        private const int GW_HWNDFIRST = 0;
        private const int GW_HWNDNEXT = 2;
        private const int GWL_STYLE = (-16);
        private const int WS_VISIBLE = 268435456;
        private const int WS_BORDER = 8388608;

        #region AIP声明
        //已经封装WinAPI类 2014-1-9 陈宝栋
        //[DllImport("IpHlpApi.dll")]
        //extern static public uint GetIfTable(byte[] pIfTable, ref uint pdwSize, bool bOrder);

        //[DllImport("User32")]
        //private extern static int GetWindow(int hWnd, int wCmd);

        //[DllImport("User32")]
        //private extern static int GetWindowLongA(int hWnd, int wIndx);

        //[DllImport("user32.dll")]
        //private static extern bool GetWindowText(int hWnd, StringBuilder title, int maxBufSize);

        //[DllImport("user32", CharSet = CharSet.Auto)]
        //private extern static int GetWindowTextLength(IntPtr hWnd);
        #endregion
        /// <summary>
        /// 查找所有应用程序标题
        /// </summary>
        /// <returns>应用程序标题范型</returns>
        public static List<string> FindAllApps(int Handle)
        {
            List<string> Apps = new List<string>();

            int hwCurr;
            hwCurr =WinAPI.GetWindow(Handle, GW_HWNDFIRST);

            while (hwCurr > 0)
            {
                int IsTask = (WS_VISIBLE | WS_BORDER);
                int lngStyle = WinAPI.GetWindowLongA(hwCurr, GWL_STYLE);
                bool TaskWindow = ((lngStyle & IsTask) == IsTask);
                if (TaskWindow)
                {
                    int length = WinAPI.GetWindowTextLength(new IntPtr(hwCurr));
                    StringBuilder sb = new StringBuilder(2 * length + 1);
                    WinAPI.GetWindowText(hwCurr, sb, sb.Capacity);
                    string strTitle = sb.ToString();
                    if (!string.IsNullOrEmpty(strTitle))
                    {
                        Apps.Add(strTitle);
                    }
                }
                hwCurr = WinAPI.GetWindow(hwCurr, GW_HWNDNEXT);
            }

            return Apps;
        }
        #endregion

        //调用
        //private void btReApp_Click(object sender, EventArgs e)
        //{
        //    LvApp.Items.Clear();
        //    List<string> Apps = SystemInfo.FindAllApps((int)this.Handle);
        //    foreach (string app in Apps)
        //    {
        //        ListViewItem item = new ListViewItem(app);
        //        LvApp.Items.Add(item);
        //    }
        //}
    }
}
