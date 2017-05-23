using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESBasic.ObjectManagement.Managers;
using CCWin.SkinControl;
using System.Diagnostics;
using System.IO;
using System.Configuration;

namespace JHDock
{
    public partial class ModuleListBox : UserControl
    {
        public ModuleListBox()
        {
            InitializeComponent();
        }
        public void Initialize(List<IDockItem>  pList)
        {
            string sysModelName = ConfigurationManager.ConnectionStrings["SysModelName"].ConnectionString;
            ChatListItem chatItem = new ChatListItem(sysModelName, true);
            chatItem.IsTwinkleHide = true;
            //chatItem.Text = "功能模块";
            chatItem.TwinkleSubItemNumber = 0;
            foreach (IDockItem item in pList)
            {
                ChatListSubItem sItem = new ChatListSubItem();
                sItem.DisplayName = item.Name;
                sItem.HeadImage = Properties.Resources.user1;
                if (File.Exists(item.IconPath))
                {
                    sItem.HeadImage = Image.FromFile(item.IconPath);
                }
                sItem.NicName = item.Argument;
                sItem.PersonalMsg = "";
                sItem.PlatformTypes = PlatformType.PC;
                sItem.Tag = item;
                chatItem.SubItems.Add(sItem);
            }
            this.chatListBox.Items.Add(chatItem);
        }

        private void chatListBox_DoubleClickSubItem(object sender, ChatListEventArgs e, MouseEventArgs es)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="es"></param>
        private void chatListBox_ClickSubItem(object sender, ChatListClickEventArgs e, MouseEventArgs es)
        {
            if (sender != null)
            {
                ChatListSubItem subItem = e.SelectSubItem;
                IDockItem item = subItem.Tag as IDockItem;
                try
                {
                    if (item.StartIn.Trim().ToLower().Contains("info"))
                    {
                        return;
                    }
                    if (item != null && !string.IsNullOrEmpty(item.Target.ToString()))
                    {
                        string SessionID = PublicVariableModel.SessionID;
                        if (item.StartIn.Trim().ToUpper() == "BS")
                        {
                            string strPostData = @"sessionID=" + SessionID;
                            string strLocTarget = "";
                            if (PublicVariableModel.strLocTatget != null && PublicVariableModel.strLocTatget != "")
                            {
                                strLocTarget = PublicVariableModel.strLocTatget;
                            }
                            else
                            {
                                strLocTarget = item.LocTarget.ToString();
                            }
                            if (item.Target.Contains('%') && (strLocTarget == "" || strLocTarget == null))
                            {
                                FrmBsMessageBox frmBSMessage = new FrmBsMessageBox(item.Target, strPostData, item.ID, item.Name);
                                frmBSMessage.ShowDialog();
                            }
                            else if (item.Target.Contains('%') && (strLocTarget != "" || strLocTarget != null))
                            {
                                HttpGood.OpenNewIe(strLocTarget, strPostData, item.Name);
                            }
                            else
                            {
                                HttpGood.OpenNewIe(@item.Target + "?" + @strPostData, strPostData, item.Name);
                            }
                        }
                        else
                        {
                            string[] strDirs = item.Target.Split('\\');
                            string strTarget = item.Target;
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            if (item.Target.Substring(item.Target.Length - 1, 1) == "|")
                            {
                                item.Target = item.Target.Substring(0, item.Target.Length - 2).Trim();
                            }
                            if (File.Exists(item.Target))
                            {
                                if (strTarget.Contains('|'))
                                {
                                    string strParam = strTarget.Substring(strTarget.Length - 2, 2);
                                    startInfo.Arguments = strParam + SessionID;
                                }
                                else
                                {
                                    startInfo.Arguments = SessionID;
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
                            else if (File.Exists(item.LocTarget))
                            {
                                startInfo.FileName = item.LocTarget; //启动的应用程序名称
                                startInfo.Arguments = SessionID;
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
                                FrmMessageBox frmMessage = new FrmMessageBox("应用程序不存在，请确认配置信息", "手动查找", "自动查找", SessionID, item.ID, strDirs[strDirs.Length - 1], item);
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
                }
            }

            ///MessageBox.Show("DisplayName="+item.DisplayName+",NicName="+item.NicName+",PersonMsg="+item.PersonalMsg+",Tag="+item.Tag);
        }
    }
}
