using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using JHDock.Properties;
using System.Threading;

namespace JHDock
{
    public partial class FrmMainTile : Form
    {
        bool listMenuFlag = false;
        KeyboardHook k_hook;
        MouseHook m_hook;
        public FrmMainTile()
        {
            InitializeComponent();
            k_hook = new KeyboardHook();
            k_hook.KeyDownEvent += new KeyEventHandler(hook_KeyDown);//钩住键按下 
            k_hook.Start();//安装键盘钩子

            m_hook = new MouseHook();
            m_hook.OnMouseActivity += new MouseEventHandler(hook_OnMouseActivity);
            m_hook.Start();

            pbClose.MouseMove += new MouseEventHandler(pb_MouseMove);
            pbMin.MouseMove += new MouseEventHandler(pb_MouseMove);
            picSet.MouseMove += new MouseEventHandler(pb_MouseMove);
            listBoxMenu.MouseMove += new MouseEventHandler(pb_MouseMove);
            picSet.MouseLeave += new System.EventHandler(pb_MouseLeaveButton);
            listBoxMenu.MouseLeave += new System.EventHandler(pb_MouseLeaveButton);
            pbClose.MouseLeave += new System.EventHandler(pb_MouseLeaveButton);
            pbMin.MouseLeave += new System.EventHandler(pb_MouseLeaveButton);
            this.Icon=Resources.sso图标;
            initListBoxMenu();
        }

        private void initListBoxMenu()
        {
            List<ListBoxItem> items = new List<ListBoxItem>();
            ListBoxItem item1 = new ListBoxItem("切换用户","1");
            ListBoxItem item2 = new ListBoxItem("修改密码", "2");
            ListBoxItem item3 = new ListBoxItem("帮助", "3");
            
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);

            listBoxMenu.DataSource = items;
            listBoxMenu.DisplayMember = "Name";
            listBoxMenu.ValueMember = "Value";
           
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            XmlService xmlService = new XmlService();            
            List<IDockItem> pList = xmlService.findAllXmlData();
            lbName.Text = PublicVariableModel.userInfoModelList.UserInfoList[0].Name;
            PictureBox pb = null;
            Label lb = null;
            int i = 0;
            //
            int row = 0;
            //
            int col = 0;
            //行数
            int rowNum = 10;
            //列数
            int colNum;
            //大图标宽度
            int bigWidth = 100;
            //大图标高度
            int bigHeight = 100;
            //小图标宽度
            int smallWidth = 80;
            //小图标高度
            int smallHeight = 80;
            //行间距
            int rowSpacing = 38;
            //列间距
            int colSpacing = 29;
            //字体大小
            int fontSize = 12;
            // 字和图标间距
            int wordSpacing = 10;
            // 图标数量
            int iconNum = pList.Count;
            // 一行最少的图标数量
            int minRowNum = 5;
            // 一行最多的图标数量
            int maxRowNum = 10;
            // 最大的行数
            int maxColNum = 3;

            rowNum = GetRowNum(iconNum, minRowNum, maxRowNum, maxColNum);
            colNum = (iconNum - 1) / rowNum + 1;

            if (colNum > maxColNum)
            {
                ResizePicturePanel(maxRowNum, maxColNum, bigWidth, colSpacing, bigHeight + fontSize + wordSpacing, rowSpacing);
            }

            ToolTip tt = new ToolTip();
            foreach (IDockItem item in pList)
            {
                row = i % rowNum;
                col = i / rowNum;
                pb = new PictureBox();
                pb.BackColor = Color.White;
                pb.Size = new Size(bigWidth, bigHeight);//大小
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Location = new Point(row * (bigWidth + colSpacing), col * (bigHeight + rowSpacing + fontSize + wordSpacing));
                pb.BorderStyle = BorderStyle.None;
                pb.Image = Properties.Resources.user1;
                if (File.Exists(item.IconPath))
                {
                    pb.Image = Image.FromFile(item.IconPath);
                }
                pb.Tag = item;
                pb.Click += new System.EventHandler(this.pictureBox_Click);

                pb.MouseMove += new MouseEventHandler(pb_MouseMove);
                pb.MouseEnter += new System.EventHandler(pb_MouseEnter);
                pb.MouseLeave += new System.EventHandler(pb_MouseLeave);
                this.pbSystemLogos.Controls.Add(pb);
                tt.SetToolTip(pb, item.Argument);


                lb = new Label();
                lb.Text = item.Name;
                lb.Size = new System.Drawing.Size(bigWidth, fontSize);
                lb.TextAlign = ContentAlignment.MiddleCenter;
                lb.AutoSize = false;
                lb.Location = new Point(row * (bigWidth + colSpacing), col * (bigHeight + rowSpacing + fontSize + wordSpacing) + bigHeight + wordSpacing);
                this.pbSystemLogos.Controls.Add(lb);
                i++;

            }
            pbSystemLogos.Left = (this.Width - pbSystemLogos.Width) / 2;
        
            
        }

        private void ResizePicturePanel(int maxRowNum, int maxColNum, int width, int colSpacing, int height, int rowSpacing)
        {
            pbSystemLogos.AutoSize = false;
            pbSystemLogos.Size = new Size(maxRowNum * (width + colSpacing), maxColNum * height + (maxColNum - 1) * rowSpacing);
            pbSystemLogos.AutoScroll = true;
            
        }
        /// <summary>
        /// 获得每行数量
        /// </summary>
        /// <param name="iconNum">图标数量</param>
        /// <param name="minRowNum">每行最少数量</param>
        /// <param name="maxRowNum">每行最大数量</param>
        /// <param name="maxColNum">最多行数</param>
        /// <returns></returns>
        private int GetRowNum(int iconNum, int minRowNum, int maxRowNum, int maxColNum)
        {
            if (iconNum <= minRowNum * maxColNum)
                return minRowNum;
            if (iconNum >= maxRowNum * maxColNum)
                return maxRowNum;
            return (iconNum - 1) / maxColNum + 1;
        }

        private void pbMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //
        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void createPicBoxList()
        {
            
        }
        private void pbxBottom_Paint(object sender, PaintEventArgs e)
        {
            foreach (Control C in this.Controls)
            {

                if (C is Label)
                {

                    Label L = (Label)C;

                    L.Visible = false;

                    //e.Graphics.DrawString(L.Text, L.Font, new SolidBrush(L.ForeColor), L.Left - pbxBottom.Left, L.Top - pbxBottom.Top);

                }
                //if (C is PictureBox&&C.Name=="picB")
                //{
                //    PictureBox p = (PictureBox)C;
                //    p.Visible = false;
                //    e.Graphics.DrawImage(picB.Image, new Point(p.Left - pbxBottom.Left, p.Top - pbxBottom.Top));
                //}

            }
        }
        private void pictureBox_Click(object sender, System.EventArgs e)
        {
            if (sender != null)
            {
                
                IDockItem item = ((Control)sender).Tag as IDockItem;
                if (!FormatXml.isHaveNoSysSub(item.ID))
                {
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
                                    strLocTarget = item.Target.ToString();
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
            }
        }
        private void pb_MouseMove(object sender, MouseEventArgs e)
        {            
            ((Control)sender).Cursor = Cursors.Hand;
            Control c = sender as Control;
            if (c.Name == "pbClose")
            {
                ((PictureBox)c).Image = Resources.单点关闭点击态;
            }
            if (c.Name == "pbMin")
            {
                ((PictureBox)c).Image = Resources.最小化点击态;
            }
            if (c.Name == "picSet")
            {
                listBoxMenu.Visible = true;
                listMenuFlag = true;
            }
            if (c.Name == "listBoxMenu")
            {
                listBoxMenu.Visible = true;
                listMenuFlag = true;
            }
        }
        private void pb_MouseEnter(object sender, System.EventArgs e)
        {
            Control pic = sender as Control;
            Size size = new Size(pic.Width + 4, pic.Height + 4);
            pic.Size = size;
            
        }
        private void pb_MouseLeave(object sender, System.EventArgs e)
        {
            Control pic = sender as Control;
            Size size = new Size(pic.Width - 4, pic.Height - 4);
            pic.Size = size;
        }
        private void pb_MouseLeaveButton(object sender, System.EventArgs e)
        {
            Control pic = sender as Control;
            if (pic.Name == "pbClose")
            {
                ((PictureBox)pic).Image = Resources.单点关闭常态;
            }
            if (pic.Name == "pbMin")
            {
                ((PictureBox)pic).Image = Resources.最小化常态;
            }
            if (pic.Name == "picSet")
            {
                listMenuFlag = false;
            }
            if (pic.Name == "listBoxMenu")
            {
                listMenuFlag = false;
            }
        }
        private void pbChangeUser_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void FrmMain_Paint(object sender, PaintEventArgs e)
        {
            foreach (Control C in this.Controls)
            {

                if (C.Name == "picBLogo" || C.Name == "picLeftBottom" || C.Name == "picRightBottom")
                {

                    PictureBox pic = (PictureBox)C;

                    pic.Visible = false;

                    e.Graphics.DrawImage(pic.Image, new Point(pic.Left, pic.Top));
                }
            }
        }
     
        private int lockScreenCount = 0;
        private int hookCount = 0;
        private void timerLockScreen_Tick(object sender, EventArgs e)
        {
            if (!listMenuFlag)
            {
                listBoxMenu.Visible = false;
            }
            lockScreenCount++;
            hookCount++;
            if (hookCount > 600)
            {
                hookCount = 0;
                m_hook.Stop();
                m_hook.Start();
                k_hook.Stop();
                k_hook.Start();
                
            }
            if (lockScreenCount == int.Parse(PublicVariableModel.lockScreenTime))
            {
                LockScreen();
            }
        }
        /// <summary>
        /// 锁屏
        /// </summary>
        private void LockScreen()
        {
            FrmLockScreen frmLockScreen = new FrmLockScreen();
            frmLockScreen.UserName = PublicVariableModel.userName;
            DialogResult result = frmLockScreen.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                lockScreenCountClear();                
            }
            
        }       
        private void lockScreenCountClear()
        {
            lockScreenCount = 0;
        }
        private void hook_KeyDown(object sender, KeyEventArgs e)
        {
            lockScreenCountClear();
        }
        private void hook_OnMouseActivity(object sender, MouseEventArgs e)
        {
            lockScreenCountClear();
        }

        private void listBoxMenu_MouseClick(object sender, MouseEventArgs e)
        {
            int index = listBoxMenu.IndexFromPoint(e.X, e.Y);
            listBoxMenu.SelectedIndex = index;
            if (index == 0)
            {
                Application.Restart();
            }
            if (index == 1)
            {
                FrmUpdatePassWord frmUpdatePassWord = FrmUpdatePassWord.Instance;
                frmUpdatePassWord.Show();

            }
            //if (index != -1)
            //{
            //    MessageBox.Show(listBoxMenu.SelectedItem.ToString());
            //}
        }

        
    }
}
