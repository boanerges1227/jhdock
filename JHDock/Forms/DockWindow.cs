using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using JHDock.Properties;
using System.Configuration;

namespace JHDock
{
    //Inherits System.Windows.Forms.Form
    //
    public partial class DockWindow : PerPixelAlphaForm
    {
        #region fields
        //2014-01-06 陈宝栋 移动桌面坐标
        //private MoveDeskIcon moveDeskIcon = new MoveDeskIcon();
        //private Point iconPoint = new Point(112, 0);
        //private MouseHook mouseHook = new MouseHook();
        //2013-12-06 庞少军 修改成靠右
        private string strHosptailName = ConfigurationManager.ConnectionStrings["HosptailName"].ConnectionString;
        private string strIsCloseOrMin = ConfigurationManager.ConnectionStrings["IsCloseOrMin"].ConnectionString;
        private int m_winWidth = 0;
        private int m_winHeight = 0;
        private int m_winLeft = 0;
        private int m_winTop = 0;
        private bool isfirst = true;
        private FrmShowDockWindows frmShowDockWindows = null;
        ////定时器
        //private Timer m_timer = null;
        public System.Drawing.Bitmap FaceBitmap;
        private JHDock.Settings m_Settings;
        private JHDock.DockManager m_DockManager;
        //是否是第一次加载
        private bool isfirstLoad = false;
        bool isStop = false;
        public NotifyIcon noTiIc = null;
        bool isHide = false;
        #endregion
        /// <summary>
        /// 构造
        /// </summary>
        public DockWindow()
            : base()
        {
            InitializeComponent();
            if (frmShowDockWindows == null)
            {
                frmShowDockWindows = new FrmShowDockWindows(this);
            }
            //CreateNewAPPBar();
            noTiIc = new NotifyIcon();
            noTiIc.Icon = Resources.sso图标;
            noTiIc.ContextMenuStrip = new ContextMenuStrip();
            noTiIc.ContextMenuStrip.Items.Add("关  闭");
            noTiIc.Text = "当前用户为：" + PublicVariableModel.userInfoModelList.UserInfoList[0].Name;

             #region 各种注事件册的
            noTiIc.MouseClick += new MouseEventHandler(noTiIcMouseClick);
            noTiIc.ContextMenuStrip.MouseClick += new MouseEventHandler(noTiIcCMSMouseClick);
            noTiIc.MouseMove += new MouseEventHandler(noTiIcMouseMove);
            MouseLeave += DockWindow_MouseLeave;
            MouseMove += DockWindow_MouseMove;
            MouseUp += DockWindow_MouseUp;
            //Leave += DockWindow_Leave;
            DragDrop += Dock_DragDrop;
            DragEnter += Dock_DragEnter;
            this.MouseClick += new MouseEventHandler(DockWindow_MouseClick);
            //2014-1-14 陈宝栋修改
            FormClosed += Dock_Close;
            this.mnuSkins.Click += new EventHandler(mnuSkins_Click);
            this.selectUserInfo.Click += new EventHandler(selectUserInfo_Click);
            Timer time = new Timer();
            time.Tick += new EventHandler(Timer_Tick);
            time.Enabled = true;
            time.Interval = 100;
            Timer time1 = new Timer();
            time1.Tick += new EventHandler(Timer_Tick1);
            time1.Enabled = true;
            time1.Interval = 1000;
            this.KeyDown +=new KeyEventHandler(DockWindow_KeyDown);
            
            #endregion
            #region 数据初使化开始
            try
            {
                #region 绘制背景图片
                string pConfigPath = Application.StartupPath + "\\xDockConfig.ini";
                m_Settings = new JHDock.Settings(pConfigPath);
                m_DockManager = new JHDock.DockManager();
                #endregion
                //初使化按钮个数
                #region 原VB
                //AddItem(Application.StartupPath + "\\Icons\\remote_desktop.png", "Remote Desktop");
                //AddItem(Application.StartupPath + "\\Icons\\Folder.png", "Folder");
                //AddItem(Application.StartupPath + "\\Icons\\Columbia Blue.png", "Columbia Blue");
                //AddItem(Application.StartupPath + "\\Icons\\Control Panel.png", "Control Panel");
                //AddItem(Application.StartupPath + "\\Icons\\Hard Drive.png", "Hard Drive");
                //AddItem(Application.StartupPath + "\\Icons\\Help.png", "Help");
                //AddItem(Application.StartupPath + "\\Icons\\Internet Shortcut.png", "Internet Shortcut");
                //AddItem(Application.StartupPath + "\\Icons\\My Computer.png", "My Computer");
                //AddItem(Application.StartupPath + "\\Icons\\Recycle Bin (full).png", "Recycle Bin");
                #endregion 原
                #region 改C#
                //AddItem(Application.StartupPath + "\\Icons\\01.png", "BIMP");
                //AddItem(Application.StartupPath + "\\Icons\\02.png", "EMR");
                //AddItem(Application.StartupPath + "\\Icons\\03.png", "HIS");
                //AddItem(Application.StartupPath + "\\Icons\\04.png", "HQMS");
                //AddItem(Application.StartupPath + "\\Icons\\05.png", "HPCS");
                //AddItem(Application.StartupPath + "\\Icons\\06.png", "LCP");
                //AddItem(Application.StartupPath + "\\Icons\\07.png", "OIS");
                //AddItem(Application.StartupPath + "\\Icons\\08.png", "RCP");
                #endregion 改C#
                #region 动态加载按钮
                //2013-12-19 毕兰 动态添加按钮
                int i = 0;
                PublicVariableModel.pageCount = 0;
                PublicVariableModel.isOnePageOrOthers = PublicVariableModel.pList.Count;
                int height_Screen = Screen.PrimaryScreen.WorkingArea.Height;
                //int pMaxY = (int)(m_Settings.Icons.ZoomPx * m_Settings.Icons.ZoomWidth) / 2;
                PublicVariableModel.IcoinCount = (int)Math.Ceiling((float)(height_Screen - 200) / 62);
                foreach (IDockItem item in PublicVariableModel.pList)
                {
                    //当图标个数超出一页规定的个数，加载到下一页
                    if (i >= PublicVariableModel.IcoinCount)
                    {
                        break;
                    }
                    //2014-1-6 毕兰 动态加载图标
                    AddItem(item.ID, item.IconPath, item.LocTarget, item.Name, item.Target, item.Argument, item.StartIn, item.LockItem);
                    i++;
                }

                ////2013-12-20 毕兰 从webservice中引用方法，加载动态结点
                //DockWebService.WebService1SoapClient dockService = new DockWebService.WebService1SoapClient();
                //DockIcon[] dockArray = dockService.GetAllIcons();
                //foreach (DockIcon item in dockArray)
                //{
                //    AddItem(Application.StartupPath + item.IconPath, item.Name, item.Target);
                //}

                #endregion 动态加载按钮
            }
            catch (Exception ex2)
            {
                MessageBox.Show("初使化界面数据时错误：" + ex2.Message, "提示");
            }


            #endregion 数据初使化结束


            
            #region 界面绘制开始
            try
            {
                m_DockManager.Initilize(m_Settings, this.Width);
                PaintBackGround();
            }
            catch (Exception ex3)
            {
                MessageBox.Show("绘制界面时错误：" + ex3.Message, "提示");
                return;
            }
            #endregion 界面绘制结束
            //顶入窗体事件
            //SetParent();
            isfirstLoad = true;
           
        }
        #region private methods
        /// <summary>
        /// 图标重绘方法
        /// </summary>
        public void Redraw(int pageCount)
        {
            string pConfigPath = Application.StartupPath + "\\xDockConfig.ini";
            m_Settings = new JHDock.Settings(pConfigPath);
            m_DockManager = new JHDock.DockManager();
            int i = 0;
            foreach (IDockItem item in PublicVariableModel.pList)
            {
                i++;
                if (i > pageCount * PublicVariableModel.IcoinCount && i <= (pageCount + 1) * PublicVariableModel.IcoinCount)
                {
                    AddItem(item.ID, item.IconPath, item.LocTarget, item.Name, item.Target, item.Argument, item.StartIn, item.LockItem);
                }
            }
            m_DockManager.Initilize(m_Settings, this.Width);
            PaintBackGround();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            //bool isZoomed = GetWindows.Load();
            //if (isZoomed)
            //{
            //    if (isfirst)
            //    {
            //        this.TopMost = false;
            //        SetWindowBottom();
            //        RemoveAppBar();
            //        isfirst = false;
            //    }
            //}
            //else
            //{
            //    if (!isfirst || isfirstLoad)
            //    {
            //        CreateNewAPPBar();
            //        this.TopMost = true;
            //        isfirstLoad = false;
            //        isfirst = true;
            //    }
            //}
            if (Cursor.Position.X <= 10 && !isfirstLoad && isHide)
            {
                this.Show();
                frmShowDockWindows.Hide();
                isHide = false;
                this.TopMost = true;
                isfirstLoad = true;
            }
            if (!isStop && !PublicVariableModel.isMouseMoveSubFrm && !isfirstLoad)
            {
                this.Hide();
                frmShowDockWindows.Show();
                isHide = true;
            }
        }
        private void Timer_Tick1(object sender, EventArgs e)
        {
            if (PublicVariableModel.isDockMouseLeave && !PublicVariableModel.isSubSystemMouseMove && PublicVariableModel.frmSub != null)
            {
                if (!isStop)
                {
                    this.Hide();
                }
                PublicVariableModel.frmSub.Close();
                PublicVariableModel.frmSub = null;
                isHide = true;
            }
        }
        /// <summary>
        /// 钉入桌面
        /// </summary>
        public void SetParent()
        {
            OperatingSystem os = Environment.OSVersion;
            int isXt = os.Version.Major;
            IntPtr pWnd = IntPtr.Zero;
            const UInt32 WS_POPUP = 0x80000000;
            const UInt32 WS_CHILD = 0x40000000;
            if (isXt == 5)
            {
                pWnd = WinAPI.FindWindow("Progman", "Program   Manager ");
            }
            else
            {
                pWnd = WinAPI.FindWindow("Progman", null);
                pWnd = WinAPI.FindWindowEx(pWnd, IntPtr.Zero, "SHELLDLL_DefVIew", null);
                pWnd = WinAPI.FindWindowEx(pWnd, IntPtr.Zero, "SysListView32", null);
            }
            int style = WinAPI.GetWindowLong(this.Handle, -16);
            style = (int)((style & ~(WS_POPUP)) | WS_CHILD);
            WinAPI.SetWindowLong(this.Handle, -16, style);
            WinAPI.SetParent(this.Handle, pWnd);
        }
        /// <summary>
        /// 创建任务栏
        /// </summary>
        public void CreateNewAPPBar()
        {
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            ManipulationAppBar createAppbar = new ManipulationAppBar();
            createAppbar.AppbarNew(this.Size,this.Handle);
            Point point = new Point();
            Size size = new Size();
            createAppbar.SizeAppBar(this.Handle, ref point, ref size);
            this.Location = new Point(0, 0);
        }
        /// <summary>
        /// 卸载任务栏
        /// </summary>
        public void RemoveAppBar()
        {
            ManipulationAppBar createAppbar = new ManipulationAppBar();
            Point point = new Point();
            Size size = new Size();
            createAppbar.AppbarRemove(this.Handle, ref size, ref point);
        }
        /// <summary>
        /// 窗体置底
        /// </summary>
        private void SetWindowBottom()
        {
            try
            {
                if (Environment.OSVersion.Version.Major < 6)
                {
                    base.SendToBack();
                    IntPtr hWndNewParent = WinAPI.FindWindow("Progman", null);
                    WinAPI.SetParent(this.Handle, hWndNewParent);
                }
                else
                {
                    WinAPI.SetWindowPos(this.Handle, 1, 0, 0, 0, 0, 0x13);
                }
            }
            catch (ApplicationException exx)
            {
                MessageBox.Show(this, exx.Message, "Pin to Desktop");
            }
        }
        //添加按钮
        private void AddItem(string pIconPath, string Name)
        {
            JHDock.DefDockItem pDockItem = new JHDock.DefDockItem();
            var _with1 = pDockItem;
            _with1.IconPath = pIconPath;
            _with1.Name = Name;
            m_DockManager.Add(pDockItem);
        }
        /// <summary>
        /// 2013-12-23 毕兰 添加新的按钮属性
        /// </summary>
        /// <param name="pIconPath"></param>
        /// <param name="Name"></param>
        private void AddItem(string pIconPath, string Name, string Target)
        {
            JHDock.DefDockItem pDockItem = new JHDock.DefDockItem();
            var _with1 = pDockItem;
            _with1.IconPath = pIconPath;
            _with1.Name = Name;
            _with1.Target = Target;
            m_DockManager.Add(pDockItem);
        }
        /// <summary>
        /// 2013-12-23 毕兰 添加新的按钮属性
        /// </summary>
        /// <param name="pIconPath"></param>
        /// <param name="Name"></param>
        private void AddItem(string Id, string pIconPath, string locTarget, string Name, string Target, string Argument, string StartIn, bool LockItem)
        {
            JHDock.DefDockItem pDockItem = new JHDock.DefDockItem();
            var _with1 = pDockItem;
            _with1.IconPath = pIconPath;
            _with1.Name = Name;
            _with1.Target = Target;
            _with1.Argument = Argument;
            _with1.StartIn = StartIn;
            _with1.LockItem = LockItem;
            _with1.ID = Id;
            _with1.LocTarget = locTarget;
            m_DockManager.Add(pDockItem);
        }
        /// <summary>
        /// 加载皮肤栏目
        /// </summary>
        private void LoadSkins()
        {
            DirectoryInfo pSkinFolder = new DirectoryInfo(Application.StartupPath + "\\Skins");
            DirectoryInfo pDirectory = null;
            MenuItem pSkinMenu = default(MenuItem);
            this.mnuSkins.MenuItems.Clear();

            //循环加载皮肤菜单栏目
            foreach (DirectoryInfo pDirectory_loopVariable in pSkinFolder.GetDirectories())
            {
                pDirectory = pDirectory_loopVariable;
                pSkinMenu = this.mnuSkins.MenuItems.Add(pDirectory.Name, ContextHandler);
                pSkinMenu.Text = pDirectory.Name;

            }


            // Me.XDockContextMenu.MenuItems.Item(
        }
        /// <summary>
        /// 右键皮肤菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ContextHandler(System.Object sender, System.EventArgs e)
        {
            MenuItem pMenuItem = ((System.Windows.Forms.MenuItem)(sender));
            //MenuItem pMenuItem = sender;
            m_Settings.Style.Theme = pMenuItem.Text;
            m_Settings.Save();
            PaintBackGround();
        }
        /// <summary>
        /// 绘制背景
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private void PaintBackGround(int X = 0, int Y = 0, bool isStop = false)
        {

            Bitmap pDefSkin = null;
            string backgroundPath = m_Settings.Style.SkinDockInfo.BackGround.Path;
            //if (strHosptailName != "xy215")
            //{
            //    backgroundPath = backgroundPath.Replace("bg.png", "bg2.png");
            //}
            pDefSkin = new Bitmap(backgroundPath);
            Graphics gr = null;
            float pMargin = 0;
            float pHeight = 0;
            float pWidth = 0;
            var _with2 = m_Settings.Style.SkinDockInfo.BackGround;
            //2013-12-06 庞少军 修改成靠右
            pWidth = m_winWidth;
            pHeight = m_winHeight;
            int pX = 0;
            FaceBitmap = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Convert.ToInt32(pHeight) + 64 * 2);
            gr = Graphics.FromImage(FaceBitmap);
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            pMargin = m_Settings.Style.SkinDockInfo.BackGround.LeftMargin + m_Settings.Style.SkinDockInfo.BackGround.OuterLeftMargin;
            gr.DrawImage(pDefSkin, new Rectangle(pX, 0, System.Convert.ToInt32(pMargin), System.Convert.ToInt32(pHeight)), new Rectangle(0, 0, System.Convert.ToInt32(pMargin), pDefSkin.Height), GraphicsUnit.Pixel);
            gr.DrawImage(pDefSkin, new Rectangle(System.Convert.ToInt32(pMargin) + pX, 0, System.Convert.ToInt32(pWidth - pMargin * 3), System.Convert.ToInt32(pHeight)), new Rectangle(System.Convert.ToInt32(pMargin), 0, System.Convert.ToInt32(pDefSkin.Width - pMargin * 2), pDefSkin.Height), GraphicsUnit.Pixel);
            gr.DrawImage(pDefSkin, new Rectangle(System.Convert.ToInt32(pWidth - pMargin * 2) + pX, 0, System.Convert.ToInt32(pMargin), System.Convert.ToInt32(pHeight)), new Rectangle(System.Convert.ToInt32(pDefSkin.Width - pMargin), 0, System.Convert.ToInt32(pMargin), pDefSkin.Height), GraphicsUnit.Pixel);
            m_DockManager.Paint(gr, m_Settings, X, Y, isStop);
            this.SetBitmap(FaceBitmap);
        }
       
            
        #endregion
        #region window events start
        //private void Dock_SizeChanged(object sender, UserPreferenceChangedEventArgs e)
        //{
        //    //屏幕分辨率改变时，刷新界面
        //    PublicVariableModel.pageCount = 0;
        //    PublicVariableModel.isOnePageOrOthers = PublicVariableModel.pList.Count;
        //    int height_Screen = Screen.PrimaryScreen.WorkingArea.Height;
        //    PublicVariableModel.IcoinCount = (int)Math.Ceiling((float)(height_Screen - 200) / 62);
        //    Redraw(0);
        //}
        private void noTiIcMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                //noTiIc.Visible = false;
                //this.ShowInTaskbar = true;
            }
            if (e.Button == MouseButtons.Right)
            {
                noTiIc.ContextMenuStrip.Show();
            }
        }
        private void noTiIcMouseMove(object sender, MouseEventArgs e)
        {
            
        }
        private void noTiIcCMSMouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
            noTiIc.Visible = false;
        }
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dock_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        /// <summary>
        /// 拖曳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dock_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            string[] file_names = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file_name in file_names)
            {
                MessageBox.Show(file_name);
            }
        }
        /// <summary>
        /// 鼠标右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DockWindow_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

            }

            if (e.Button == MouseButtons.Left)
            {
                this.m_DockManager.Onclick(sender, e);
            }
        }
        /// <summary>
        /// 点击关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuAbout_Click(Object sender, EventArgs e)
        {
            //---
            //2013-12-06 庞少军 修改成靠右
            //debug...
            this.Close();
        }
        /// <summary>
        /// 查看员工信息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectUserInfo_Click(object sender, EventArgs e)
        {
            FrmUserInfo frmUserInfo = FrmUserInfo.Instance;
            frmUserInfo.Show();
        }
        /// <summary>
        /// 点击修改密码按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSkins_Click(System.Object sender, System.EventArgs e)
        {
            //---
            //2013-12-06 庞少军 修改成靠右
            //debug...
            FrmUpdatePassWord updatePassword = FrmUpdatePassWord.Instance;
            updatePassword.Show();

        }
        /// <summary>
        /// 移动鼠标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DockWindow_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            PaintBackGround(e.X, e.Y, isStop);
            OperatingSystem os = Environment.OSVersion;
            int isXt = os.Version.Major;
            int height_Screen = Screen.PrimaryScreen.WorkingArea.Height;
            PublicVariableModel.isDockMouseLeave = false;
            if (isXt == 5)
            {
                int tkXp = 0;
                if (strHosptailName == "xy215")
                {
                    tkXp = 77;
                }
                else
                {
                    tkXp = Resources.锁定图标点击状态.Width - 13;
                }
                if ((e.X >= 0 && e.X <= Resources.人员信息.Width - 13 && e.Y >= height_Screen - 40 && e.Y <= height_Screen - 21) || (e.X >= Resources.人员信息.Width - 2 && e.X <= Resources.人员信息.Width + Resources.关闭.Width + 7 && e.Y >= height_Screen - 40 && e.Y <= height_Screen - 21) || (e.X >= 0 && e.X <= tkXp && e.Y >= height_Screen - 69 && e.Y <= height_Screen - 52))
                {
                    this.Cursor = Cursors.Hand;
                }
                else if (e.X >= 50 && e.X <= 60 && e.Y >= height_Screen - 111 && e.Y <= height_Screen - 94)
                {
                    double page = PublicVariableModel.isOnePageOrOthers / (float)PublicVariableModel.IcoinCount;
                    double totalPage = Math.Ceiling(page);
                    if (PublicVariableModel.isOnePageOrOthers > PublicVariableModel.IcoinCount && PublicVariableModel.pageCount != (totalPage - 1))
                    {
                        this.Cursor = Cursors.Hand;
                    }
                }
                else if (e.X >= 0 && e.X <= 10 && e.Y >= height_Screen - 111 && e.Y <= height_Screen - 94)
                {
                    if (PublicVariableModel.isOnePageOrOthers > PublicVariableModel.IcoinCount && PublicVariableModel.pageCount != 0)
                    {
                        this.Cursor = Cursors.Hand;
                    }
                }
                else
                {
                    this.Cursor = Cursors.Arrow;
                }
            }
            else
            {
                int tkWin7 = 0;
                if (strHosptailName == "xy215")
                {
                    tkWin7 = 77;
                }
                else
                {
                    tkWin7 = Resources.锁定图标点击状态.Width - 13;
                }
                if ((e.X >= 0 && e.X <= Resources.人员信息.Width - 13 && e.Y >= height_Screen - 39 && e.Y <= height_Screen - 22) || (e.X >= Resources.人员信息.Width - 2 && e.X <= Resources.人员信息.Width + Resources.关闭.Width + 10 && e.Y >= height_Screen - 38 && e.Y <= height_Screen - 19) || (e.X >= 0 && e.X <= tkWin7 && e.Y >= height_Screen - 69 && e.Y <= height_Screen - 52))
                {
                    this.Cursor = Cursors.Hand;
                }
                else if (e.X >= 50 && e.X <= 60 && e.Y >= height_Screen - 90 && e.Y <= height_Screen - 75)
                {
                    double page = PublicVariableModel.isOnePageOrOthers / (float)PublicVariableModel.IcoinCount;
                    double totalPage = Math.Ceiling(page);
                    if (PublicVariableModel.isOnePageOrOthers > PublicVariableModel.IcoinCount && PublicVariableModel.pageCount != (totalPage - 1))
                    {
                        this.Cursor = Cursors.Hand;
                    }
                }
                else if (e.X >= 0 && e.X <= 10 && e.Y >= height_Screen - 90 && e.Y <= height_Screen - 75)
                {
                    if (PublicVariableModel.isOnePageOrOthers > PublicVariableModel.IcoinCount && PublicVariableModel.pageCount != 0)
                    {
                        this.Cursor = Cursors.Hand;
                    }
                }
                else
                {
                    this.Cursor = Cursors.Arrow;
                }
            }
            //鼠标掠过时，不隐藏窗体
            isfirstLoad = true;
        }
        /// <summary>
        /// 鼠标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DockWindow_MouseLeave(object sender, System.EventArgs e)
        {
            PaintBackGround(0, 0, isStop);
            if (strHosptailName != "xy215")
            {
                if (!isStop && !PublicVariableModel.isMouseMoveSubFrm)
                {
                    this.Hide();
                    frmShowDockWindows.Show();
                    isHide = true;
                }
                PublicVariableModel.isDockMouseLeave = true;
                isfirstLoad = false;
            }
        }
        private void Dock_Close(object sender, EventArgs e)
        {
            //moveDeskIcon.MoveDeskIconAll();
            //mouseHook.Stop();//释放键盘钩子
        }
        private void DockWindow_MouseClick(object sender, MouseEventArgs e)
        {
            OperatingSystem os = Environment.OSVersion;
            int isXt = os.Version.Major;
            int height_Screen = Screen.PrimaryScreen.WorkingArea.Height;
            int tk = 0;
            if (strHosptailName == "xy215")
            {
                tk = 77;
            }
            else
            {
                tk = Resources.锁定图标点击状态.Width - 13;
            }
            //XP系统下
            if (isXt == 5)
            {
                if (e.X >= Resources.人员信息.Width - 2 && e.X <= Resources.人员信息.Width + Resources.关闭.Width + 7 && e.Y >= height_Screen - 40 && e.Y <= height_Screen - 21)
                {
                    //if (strHosptailName == "xy215")
                    //{
                    //    if (strIsCloseOrMin == "Yes")
                    //    {
                    //        this.Hide();
                    //        noTiIc.Visible = true;
                    //    }
                    //    else if (strIsCloseOrMin == "No")
                    //    {
                    //        this.Close();
                    //    }
                    //    else
                    //    {
                    //        FrmMessageClose frmMessage = new FrmMessageClose(this, noTiIc);
                    //        frmMessage.ShowDialog();
                    //    }
                    //}
                    //else
                    //{
                    if (MessageBox.Show("您确定要退出单点登录程序吗?", "信息", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        //RemoveAppBar();
                        this.Close();
                    }
                    //}
                }
                if (e.X >= 0 && e.X <= Resources.人员信息.Width - 10 && e.Y >= height_Screen - 40 && e.Y <= height_Screen - 21)
                {
                    if (strHosptailName == "xy215")
                    {
                        this.Hide();
                        noTiIc.Visible = true;
                    }
                    else
                    {
                        FrmUserInfo frmUserInfo = FrmUserInfo.Instance;
                        frmUserInfo.Show();
                    }
                }
                if (e.X >= 50 && e.X <= 60 && e.Y >= height_Screen - 111 && e.Y <= height_Screen - 94)
                {
                    double page = PublicVariableModel.isOnePageOrOthers / (float)PublicVariableModel.IcoinCount;
                    double totalPage = Math.Ceiling(page);
                    if (PublicVariableModel.isOnePageOrOthers > PublicVariableModel.IcoinCount && PublicVariableModel.pageCount != (totalPage - 1))
                    {
                        PublicVariableModel.pageCount++;
                        Redraw(PublicVariableModel.pageCount);
                    }
                }
                if (e.X >= 0 && e.X <= 10 && e.Y >= height_Screen - 111 && e.Y <= height_Screen - 94)
                {
                    if (PublicVariableModel.isOnePageOrOthers > PublicVariableModel.IcoinCount && PublicVariableModel.pageCount != 0)
                    {
                        PublicVariableModel.pageCount--;
                        Redraw(PublicVariableModel.pageCount);
                    }
                }
                if (e.X >= 0 && e.X <= tk && e.Y >= height_Screen - 69 && e.Y <= height_Screen - 52)
                {
                    if (strHosptailName == "xy215")
                    {
                        FrmUserInfo frmUserInfo = FrmUserInfo.Instance;
                        frmUserInfo.Show();
                    }
                    else
                    {
                        if (isStop)
                        {
                            m_DockManager.Initilize(m_Settings, this.Width);
                            PaintBackGround(0, 0, isStop);
                            isStop = false;
                        }
                        else
                        {
                            m_DockManager.Initilize(m_Settings, this.Width);
                            PaintBackGround();
                            isStop = true;
                        }
                    }
                }
            }
            //Win7系统下
            else
            {
                if (e.X >= Resources.人员信息.Width - 2 && e.X <= Resources.人员信息.Width + Resources.关闭.Width + 10 && e.Y >= height_Screen - 38 && e.Y <= height_Screen - 19)
                {
                    //if (strHosptailName == "xy215")
                    //{
                    //    if (strIsCloseOrMin == "Yes")
                    //    {
                    //        this.Hide();
                    //        noTiIc.Visible = true;
                    //    }
                    //    else if (strIsCloseOrMin == "No")
                    //    {
                    //        this.Close();
                    //    }
                    //    else
                    //    {
                    //        FrmMessageClose frmMessage = new FrmMessageClose(this, noTiIc);
                    //        frmMessage.ShowDialog();
                    //    }
                    //}
                    //else
                    //{
                    if (MessageBox.Show("您确定要退出单点登录程序吗?", "友情提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        this.Close();
                    }
                    //}
                }
                if (e.X >= 0 && e.X <= Resources.人员信息.Width - 13 && e.Y >= height_Screen - 39 && e.Y <= height_Screen - 22)
                {
                    if (strHosptailName == "xy215")
                    {
                        this.Hide();
                        noTiIc.Visible = true;
                    }
                    else
                    {
                        FrmUserInfo frmUserInfo = FrmUserInfo.Instance;
                        frmUserInfo.Show();
                    }
                }
                if (e.X >= 50 && e.X <= 60 && e.Y >= height_Screen - 90 && e.Y <= height_Screen - 75)
                {
                    double page = PublicVariableModel.isOnePageOrOthers / (float)PublicVariableModel.IcoinCount;
                    double totalPage = Math.Ceiling(page);
                    if (PublicVariableModel.isOnePageOrOthers > PublicVariableModel.IcoinCount && PublicVariableModel.pageCount != (totalPage - 1))
                    {
                        PublicVariableModel.pageCount++;
                        Redraw(PublicVariableModel.pageCount);
                    }
                }
                if (e.X >= 0 && e.X <= 10 && e.Y >= height_Screen - 90 && e.Y <= height_Screen - 75)
                {
                    if (PublicVariableModel.isOnePageOrOthers > PublicVariableModel.IcoinCount && PublicVariableModel.pageCount != 0)
                    {
                        PublicVariableModel.pageCount--;
                        Redraw(PublicVariableModel.pageCount);
                    }
                }
                if (e.X >= 0 && e.X <= tk && e.Y >= height_Screen - 69 && e.Y <= height_Screen - 52)
                {
                    if (strHosptailName == "xy215")
                    {
                        FrmUserInfo frmUserInfo = FrmUserInfo.Instance;
                        frmUserInfo.Show();
                    }
                    else
                    {
                        if (isStop)
                        {
                            m_DockManager.Initilize(m_Settings, this.Width);
                            PaintBackGround(0, 0, isStop);
                            isStop = false;
                        }
                        else
                        {
                            m_DockManager.Initilize(m_Settings, this.Width);
                            PaintBackGround();
                            isStop = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 快捷键Ctrl+B实现左侧栏缩小至任务栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DockWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyValue == 66)
            {
                this.Hide();
                noTiIc.Visible = true;
            }
        }

        #endregion window events end
    }
}
