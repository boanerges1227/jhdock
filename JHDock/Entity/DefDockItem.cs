using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Data;
using System.Configuration;

namespace JHDock
{
    /// <summary>
    /// 系统图标类
    /// </summary>
    public class DefDockItem
     : IDockItem
    {
        private Bitmap m_Icon;
        private string m_IconPath;
        private string m_Name;
        private string m_Target;
        private string m_LocTarget;
        private string m_StartIn;
        private string m_Argument;
        private System.Windows.Forms.FormWindowState m_WindowdType;
        private int m_X;
        private int m_Y;
        private int m_Width;
        private int m_Height;
        private bool m_LockItem;
        private int m_CentreX;
        private int m_CentreY;
        private string m_SubID;
        //2013-12-06 庞少军 修改成靠右
        private int m_RightX;
        public int RightX
        {
            get { return m_RightX; }
            set { m_RightX = value; }
        }

        //2013-12-30 毕兰 添加ID标识属性
        private string m_ID;
        public string ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        public string Argument
        {
            get { return m_Argument; }
            set { m_Argument = value; }
        }

        public int Height
        {
            get { return m_Height; }
            set { m_Height = value; }
        }

        public System.Drawing.Bitmap Icon
        {
            get { return m_Icon; }
            set { m_Icon = value; }
        }

        public string IconPath
        {
            get { return m_IconPath; }
            set { m_IconPath = value; }
        }
        public bool LockItem
        {
            get { return m_LockItem; }
            set { m_LockItem = value; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        public string SubID
        {
            get { return m_SubID; }
            set { m_SubID = value; }
        }
        /// <summary>
        /// 窗体图标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Onclick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //2013-12-23 毕兰 打开应用程序
            if (sender != null)
            {
                IDockItem item = sender as IDockItem;
                if (!FormatXml.isHaveNoSysSub(item.ID))
                {
                    try
                    {
                        if (item.StartIn.Trim().ToLower().Contains("info"))
                        {
                            return;
                        }
                        if (item != null && !string.IsNullOrEmpty(item.Target))
                        {

                            //2104-1-17 陈宝栋修改
                            string strKey = ConvertEx.GetIv(16);
                            //加密串传参用户编码
                            //string strEncryptPwd = ConvertEx.AESEncrypt(PublicVariableModel.userName, "GOODWILLCIS-JHIP", strKey);
                            //加密串传参登录用户名-王静然20151026
                            string strEncryptPwd = ConvertEx.AESEncrypt(PublicVariableModel.userLoginName, "GOODWILLCIS-JHIP", strKey);
                            if (item.StartIn.Trim().ToUpper() == "BS")
                            {
                                string strPostData = @"userCode=" + strEncryptPwd + "&Iv=" + strKey;
                                string strLocTatget = "";
                                if (PublicVariableModel.strLocTatget != null && PublicVariableModel.strLocTatget != "")
                                {
                                    strLocTatget = PublicVariableModel.strLocTatget;
                                }
                                else
                                {
                                    strLocTatget = item.LocTarget.ToString();
                                }
                                if (item.Target.Contains('%') && (strLocTatget == "" || strLocTatget == null))
                                {
                                    FrmBsMessageBox frmBSMessage = new FrmBsMessageBox(item.Target, strPostData, item.ID, item.Name);
                                    frmBSMessage.ShowDialog();
                                }
                                else if (item.Target.Contains('%') && (strLocTatget != "" || strLocTatget != null))
                                {
                                    HttpGood.OpenNewIe(strLocTatget, strPostData, item.Name);
                                }
                                else
                                {
                                    //注释掉原来的启动IE 方式
                                    //HttpGood.OpenNewIe(@item.Target, strPostData, item.Name);
                                    HttpGood.OpenNewIe(@item.Target + "?" + @strPostData, strPostData, item.Name);
                                    //HttpGood.OpenNewIe("http://172.16.80.110:7001/defaultroot/Logon!logon.action?", strPostData, item.Name);
                                }
                                //if (item.Name != "HIS")
                                //{
                                //    IntPtr pHanle = WinAPI.GetForegroundWindow();
                                //    WinAPI.ShowWindow(pHanle, 3);//把窗口最大化
                                //}
                            }
                            else
                            {
                                //windows桌面的路径
                                //string strDeskTopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                                string[] strDirs = item.Target.Split('\\');
                                string strTarget = item.Target;
                                //string strexe = strDirs[strDirs.Length - 1];//ConsoleApplication3.exe A|
                                //在桌面要打开的程序路径
                                //string strDeskTopPath = strDeskTopDir+"\\"+strDirs[strDirs.Length - 1];
                                ProcessStartInfo startInfo = new ProcessStartInfo();
                                //if (PublicVariableModel.exeFilePath != "" && PublicVariableModel.exeFilePath != null)
                                //{
                                //    item.LocTarget = PublicVariableModel.exeFilePath;
                                //    MessageBox.Show(item.Target+"  "+item.LocTarget);
                                //}
                                if (item.Target.Substring(item.Target.Length - 1, 1) == "|")
                                {
                                    item.Target = item.Target.Substring(0, item.Target.Length - 2).Trim();
                                }
                                if (File.Exists(item.Target))
                                {
                                    if (strTarget.Contains('|'))
                                    {
                                        string strParam = strTarget.Substring(strTarget.Length - 2, 2);
                                        startInfo.Arguments = strParam + strEncryptPwd + "|" + strKey;
                                    }
                                    else
                                    {
                                        startInfo.Arguments = strEncryptPwd + "|" + strKey;
                                    }
                                    startInfo.FileName = item.Target; //启动的应用程序名称  
                                    startInfo.WorkingDirectory = Path.GetDirectoryName(item.Target);

                                    #region 如果是门诊并且bat文件路径存在就执行bat文件进行手动更新文件
                                    if (item.Target.Contains("autoLogin.exe"))
                                    {
                                        //File.get
                                        //单点登录更新门诊.bat
                                        string batPath = System.IO.Path.GetDirectoryName(item.Target) + @"\单点登录更新门诊.bat";
                                        if (File.Exists(batPath))
                                        {
                                            Process pro_bat = new Process();
                                            FileInfo file = new FileInfo(batPath);
                                            //pro_bat.StartInfo.WorkingDirectory = file.Directory.FullName;
                                            pro_bat.StartInfo.FileName = batPath;
                                            pro_bat.StartInfo.CreateNoWindow = false;
                                            pro_bat.Start();
                                        }
                                    }
                                    #endregion

                                    #region 记录启动的应用程序名称，用于测试，王静然，20151014
                                    //StreamWriter log = new StreamWriter(@"E:\wangjingran\ceshi.txt", true);
                                    //log.WriteLine("默认工作目录:" + startInfo.WorkingDirectory);
                                    //log.WriteLine("参数：" + startInfo.Arguments);
                                    //log.Close();
                                    #endregion

                                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                                    Process.Start(startInfo);
                                }
                                //else if (File.Exists(strDeskTopPath))
                                //{
                                //    startInfo.FileName = strDeskTopPath; //启动的应用程序名称       
                                //    startInfo.Arguments = strEncryptPwd + "|" + strKey;
                                //    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                                //    Process.Start(startInfo);
                                //}
                                else if (File.Exists(item.LocTarget))
                                {
                                    startInfo.FileName = item.LocTarget; //启动的应用程序名称                                    
                                    startInfo.Arguments = strEncryptPwd + "|" + strKey;
                                    startInfo.WorkingDirectory = Path.GetDirectoryName(item.LocTarget);
                                    #region 如果是门诊并且bat文件路径存在就执行bat文件进行手动更新文件
                                    if (item.Target.Contains("autoLogin.exe"))
                                    {
                                        //File.get
                                        //单点登录更新门诊.bat
                                        string batPath = System.IO.Path.GetDirectoryName(item.Target) + @"\单点登录更新门诊.bat";
                                        if (File.Exists(batPath))
                                        {
                                            Process pro_bat = new Process();
                                            FileInfo file = new FileInfo(batPath);
                                            //pro_bat.StartInfo.WorkingDirectory = file.Directory.FullName;
                                            pro_bat.StartInfo.FileName = batPath;
                                            pro_bat.StartInfo.CreateNoWindow = false;
                                            pro_bat.Start();
                                        }
                                    }
                                    #endregion
                                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                                    Process.Start(startInfo);
                                }
                                else
                                {
                                    FrmMessageBox frmMessage = new FrmMessageBox("应用程序不存在，请确认配置信息", "手动查找", "自动查找", strEncryptPwd + "|" + strKey, item.ID, strDirs[strDirs.Length - 1], item);
                                    frmMessage.ShowDialog();
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("应用程序路径配置信息", "提示");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("打开程序时错误：" + ex.Message, "提示");
                    }
                }
            }
        }

        public void Paint(Graphics DstGraphics, Settings DockSettings, string tip = "")
        {
            if (System.IO.File.Exists(m_IconPath))
            {
                //陈宝栋修改非空判断挪至到里边 2014-1-22
                if (m_Icon == null)
                {
                    m_Icon = new Bitmap(m_IconPath);
                }
                DstGraphics.DrawImage(m_Icon, m_X, m_Y, m_Width, m_Height);
            }
            //指定图片指定文件不存在加载默认图片 陈宝栋 修改 2014-1-22
            else
            {
                DstGraphics.DrawImage((Bitmap)Properties.Resources.JHIP_LOGO, m_X, m_Y, m_Width, m_Height);
            }
            if (tip != "")
            {
                //Font drawFont = new Font("Arial", 10);
                Font drawFont = new Font("微软雅黑", 11, FontStyle.Regular);
                SolidBrush drawBrush = new SolidBrush(Color.White);
                float temp = 0;
                SizeF drawSize = DstGraphics.MeasureString(tip, drawFont);
                if (drawSize.Width >= 120)
                {
                    string tip1 = tip.Substring(0, tip.Length / 2);
                    string tip2 = tip.Substring(tip.Length / 2, tip.Length / 2);
                    drawSize = DstGraphics.MeasureString(tip1, drawFont);
                    temp = (m_Width - drawSize.Width) / 2;
                    DstGraphics.DrawString(tip1, drawFont, drawBrush, m_X + temp, m_Y + m_Height);
                    drawSize = DstGraphics.MeasureString(tip2, drawFont);
                    temp = (m_Width - drawSize.Width) / 2;
                    DstGraphics.DrawString(tip2, drawFont, drawBrush, m_X + temp, m_Y + m_Height + drawSize.Height - 5);
                }
                else
                {
                    drawSize = DstGraphics.MeasureString(tip, drawFont);
                    temp = (m_Width - drawSize.Width) / 2;
                    DstGraphics.DrawString(tip, drawFont, drawBrush, m_X + temp, m_Y + m_Height + 2);
                }
            }
        }

        public string StartIn
        {
            get { return m_StartIn; }
            set { m_StartIn = value; }
        }

        public string Target
        {
            get { return m_Target; }
            set { m_Target = value; }
        }

        public string LocTarget
        {
            get { return m_LocTarget; }
            set { m_LocTarget = value; }
        }

        public int Width
        {
            get { return m_Width; }
            set { m_Width = value; }
        }

        public System.Windows.Forms.FormWindowState WindowdType
        {
            get { return m_WindowdType; }
            set { m_WindowdType = value; }
        }

        public int X
        {
            get { return m_X; }
            set { m_X = value; }
        }

        public int Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }

        public int CentreX
        {
            get { return m_CentreX; }
        }

        public int CentreY
        {
            get { return m_CentreY; }
        }

        public void InitilizeCentre(int X, int Y)
        {
            m_CentreX = X;
            m_CentreY = Y;
        }

    }
}
