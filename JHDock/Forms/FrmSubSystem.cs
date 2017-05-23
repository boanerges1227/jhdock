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

namespace JHDock
{
    //Inherits System.Windows.Forms.Form
    //
    /// <summary>
    /// 创建人：陈宝栋
    /// 创建时间：2014-05-12
    /// 功能：子窗体窗体类
    /// </summary>
    public partial class FrmSubSystem : PerPixelAlphaForm
    {
        #region fields
        private int m_winWidth = 0;
        private int m_winHeight = 0;
        private int m_winLeft = 0;
        private int m_winTop = 0;
        public System.Drawing.Bitmap FaceBitmap;
        private JHDock.Settings m_Settings;
        private JHDock.DockManager m_DockManager;
        bool isStop = false;
        string tip = "";
        int X = 0;
        int Y = 0;
        string sysNO = "";
        #endregion
        /// <summary>
        /// 构造
        /// </summary>
        public FrmSubSystem(int X, int Y, string tip, string sysNO)
            : base()
        {
            InitializeComponent();
            this.X = X;
            this.Y = Y;
            this.tip = tip;
            this.sysNO = sysNO;
            #region 各种注事件册的
            this.MouseLeave += new EventHandler(SubSystem_MouseLeave);
            MouseMove += DockWindow_MouseMove;
            MouseUp += DockWindow_MouseUp;
            DragEnter += Dock_DragEnter;
            this.MouseClick += new MouseEventHandler(DockWindow_MouseClick);
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
                #region 动态加载按钮
                foreach (IDockItem item in FormatXml.GetSysSubList(sysNO))
                {
                    AddItem(item.ID, item.IconPath, item.LocTarget, item.Name, item.Target, item.Argument, item.StartIn, item.LockItem);
                }
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
        }
        #region private methods
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="pIconPath"></param>
        /// <param name="Name"></param>
        private void AddItem(string pIconPath, string Name)
        {
            JHDock.DefDockItem pDockItem = new JHDock.DefDockItem();
            var _with1 = pDockItem;
            _with1.IconPath = pIconPath;
            _with1.Name = Name;
            m_DockManager.Add(pDockItem);
        }
        /// <summary>
        /// 子窗体新按钮属性
        /// </summary>
        /// <param name="pIconPath">路径名称</param>
        /// <param name="Name">按钮名称</param>
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
        /// 子窗体新按钮属性
        /// </summary>
        /// <param name="pIconPath">路径名称</param>
        /// <param name="Name">按钮名称</param>
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
        /// 绘制背景
        /// </summary>
        /// <param name="X">横坐标</param>
        /// <param name="Y">纵坐标</param>
        private void PaintBackGround(int X = 0, int Y = 0, bool isStop = false)
        {
            Bitmap pDefSkin = new Bitmap(m_Settings.Style.SkinDockInfo.BackGround.Path);
            Graphics gr = null;
            float pMargin = 0;
            float pHeight = 0;
            float pWidth = 0;
            var _with2 = m_Settings.Style.SkinDockInfo.BackGround;
            pWidth = m_winWidth;
            pHeight = m_winHeight;
            int pX = 0;
            FaceBitmap = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            gr = Graphics.FromImage(FaceBitmap);
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            pMargin = m_Settings.Style.SkinDockInfo.BackGround.LeftMargin + m_Settings.Style.SkinDockInfo.BackGround.OuterLeftMargin;
            int t1Width = Properties.Resources.图标列表的窗体背景7.Width;
            int t2Width = Properties.Resources.图标列表的窗体背景8.Width;
            Font drawFont1 = new Font("宋体", 11, FontStyle.Regular);
            SizeF drawSize1 = gr.MeasureString("微软雅黑", drawFont1);
            int height = (int)Math.Ceiling((float)FormatXml.GetSysSubList(this.sysNO).Count / 6) * (72 + (int)(drawSize1.Height)) + 20;
            int newY = this.Y - (height / 2 - 36);
            Font drawFont = new Font("微软雅黑", 12, FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            SizeF drawSize = gr.MeasureString(tip, drawFont);
            gr.DrawString(tip, drawFont, drawBrush, this.X + (pWidth / 2 - drawSize.Width / 2), newY);
            gr.DrawImage((Image)Properties.Resources.图标列表的窗体背景7, this.X + 10, newY, t1Width, height);
            gr.DrawImage((Image)Properties.Resources.图标列表的窗体背景8, this.X + t1Width + 10, newY, pWidth, height);
            gr.DrawImage((Image)Properties.Resources.图标列表的窗体背景9, this.X + pWidth + t1Width - 10, newY, t2Width, height);
            m_DockManager.Paint1(gr, m_Settings, this.X + 40, newY + 25, X, Y, isStop);
            this.SetBitmap(FaceBitmap);
        }

        #endregion
        #region window events start
        ///// <summary>
        /// 
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
        /// 鼠标右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DockWindow_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.m_DockManager.Onclick(sender, e);
            }
        }
        /// <summary>
        /// 移动鼠标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DockWindow_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (PublicVariableModel.isMouseMoveSubFrm == false)
            {
                PublicVariableModel.isMouseMoveSubFrm = true;
            }
            PublicVariableModel.isSubSystemMouseMove = true;
            PaintBackGround(e.X, e.Y, isStop);
            this.m_DockManager.MouseLeve(sender, e, this);
        }
        /// <summary>
        /// 鼠标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubSystem_MouseLeave(object sender, EventArgs e)
        {
            if (PublicVariableModel.frmSub != null)
            {
                PublicVariableModel.frmSub.Close();
                PublicVariableModel.frmSub = null;
            }
            if (PublicVariableModel.isMouseMoveSubFrm)
            {
                PublicVariableModel.isMouseMoveSubFrm = false;
            }
            PublicVariableModel.isSubSystemMouseMove = false;

        }
        private void DockWindow_MouseClick(object sender, MouseEventArgs e)
        {
            OperatingSystem os = Environment.OSVersion;
            int isXt = os.Version.Major;
            int height_Screen = Screen.PrimaryScreen.WorkingArea.Height;
        }

        #endregion window events end
    }
}
