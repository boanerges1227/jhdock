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
    public partial class FrmShowDockWindows : PerPixelAlphaForm
    {
        #region fields
        DockWindow doc = null;
        #endregion
        /// <summary>
        /// 构造
        /// </summary>
        public FrmShowDockWindows(DockWindow dock)
            : base()
        {
            InitializeComponent();
            this.doc = dock;
            #region 各种注事件册的
            //this.MouseEnter += new EventHandler(MouseEnterEvent);
            #endregion
            PaintBackGround();
        }
        /// <summary>
        /// 绘制背景
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private void PaintBackGround(int X = 0, int Y = 0, bool isStop = false)
        {
            Graphics gr = null;
            Bitmap FaceBitmap = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            gr = Graphics.FromImage(FaceBitmap);
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            gr.DrawImage((Image)Properties.Resources.隐藏单点菜单, 0, 0, 10, 768);
            this.SetBitmap(FaceBitmap);
        }
        //private void MouseEnterEvent(object sender, EventArgs e)
        //{
        //    this.doc.Show();
        //    this.Hide();
        //}
    }
}
