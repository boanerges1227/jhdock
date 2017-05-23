using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JHDock
{
    /// <summary>
    /// 窗体之间的公用变量类 2014-1-15 陈宝栋创建
    /// </summary>
    public class PublicVariableModel
    {
        public static string userName { get; set; }
        public static string userLoginName { get; set; }
        public static string passWord { get; set; }
        //SessionID20160418李斌增加
        public static string SessionID { get; set; }
        public static string isUserCA { get; set; }
        public static string exeFilePath { get; set; }
        public static List<IDockItem> pList { get; set; }
        public static string IsUpdateImage { get; set; }
        public static DataSet dsUserInfo { get; set; }
        public static bool isWindows8 { get; set; }
        public static IntPtr pHanle { get; set; }
        public static int isOnePageOrOthers { get; set; }
        public static int pageCount { get; set; }
        //每一页加载的图标个数
        public static int IcoinCount { get; set; }
        public static FrmSubSystem frmSub { get; set; }
        public static bool isMouseMoveSubFrm { get; set; }
        public static bool isCurrHand { get; set; }
        public static bool isDockMouseLeave { get; set; }
        public static bool isSubSystemMouseMove { get; set; }
        public static string strLocTatget { set; get; }
        public static UserInfoModelList userInfoModelList { set; get; }
        //锁屏时间
        public static string lockScreenTime { set; get; }
        
    }
}
