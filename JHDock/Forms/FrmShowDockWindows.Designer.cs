using System.Windows.Forms;
namespace JHDock
{
    partial class FrmShowDockWindows
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
            //
            //XDockContextMenu
            //
            this.XDockContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuSkins,
            this.selectUserInfo,
            });
            //
            //mnuSkins
            //
            this.mnuSkins.Index = 0;
            this.mnuSkins.Text = "修改密码";

            this.selectUserInfo.Index = 1;
            this.selectUserInfo.Text = "查看员工信息";

            this.Name = "DockWindow";
            this.Text = "DockWindow";
            this.TopMost = true;
            this.ShowInTaskbar = false;
        }

        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        #endregion

        internal System.Windows.Forms.ContextMenu XDockContextMenu;
        internal System.Windows.Forms.ContextMenuStrip XDockContextMenuStrip;
        internal System.Windows.Forms.MenuItem mnuSkins;
        internal System.Windows.Forms.MenuItem selectUserInfo;
        private System.Windows.Forms.MenuItem withEventsField_mnuAbout;
    }
}