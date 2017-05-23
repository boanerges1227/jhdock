using System.Windows.Forms;
namespace JHDock
{
    partial class DockWindow
    {
        //Required by the Windows Form Designer
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.XDockContextMenu = new System.Windows.Forms.ContextMenu();
            //2014-1-9 陈宝栋修改
            this.mnuSkins = new System.Windows.Forms.MenuItem();
            this.selectUserInfo = new System.Windows.Forms.MenuItem();
            this.mnuAbout = new System.Windows.Forms.MenuItem();
            //
            //XDockContextMenu
            //
            this.XDockContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuSkins,
            this.selectUserInfo,
            this.mnuAbout
            });
            //
            //mnuSkins
            //
            this.mnuSkins.Index = 0;
            this.mnuSkins.Text = "修改密码";

            this.selectUserInfo.Index = 1;
            this.selectUserInfo.Text = "查看员工信息";
            //
            //mnuAbout
            //
            this.mnuAbout.Index = 2;
            this.mnuAbout.Text = "    关闭 ";
            //
            ////DockWindow
            ////
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.AllowDrop = true;
            //this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            //2013-12-06 庞少军 修改成靠右
            m_winWidth = 100;
            m_winHeight = Screen.PrimaryScreen.WorkingArea.Height;
            m_winLeft = 0;
            m_winTop = 0;

            this.ClientSize = new System.Drawing.Size(m_winWidth, m_winHeight);
            this.Left = m_winLeft;
            this.Top = m_winTop;

            this.Name = "DockWindow";
            this.Text = "DockWindow";
            this.TopMost = true;
            this.ShowInTaskbar = false;
        }

        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        internal System.Windows.Forms.MenuItem mnuAbout
        {
            get { return withEventsField_mnuAbout; }
            set
            {
                if (withEventsField_mnuAbout != null)
                {
                    withEventsField_mnuAbout.Click -= mnuAbout_Click;
                }
                withEventsField_mnuAbout = value;
                if (withEventsField_mnuAbout != null)
                {
                    withEventsField_mnuAbout.Click += mnuAbout_Click;
                }
            }
        }
        #endregion

        internal System.Windows.Forms.ContextMenu XDockContextMenu;
        internal System.Windows.Forms.ContextMenuStrip XDockContextMenuStrip;
        internal System.Windows.Forms.MenuItem mnuSkins;
        internal System.Windows.Forms.MenuItem selectUserInfo;
        private System.Windows.Forms.MenuItem withEventsField_mnuAbout;
    }
}