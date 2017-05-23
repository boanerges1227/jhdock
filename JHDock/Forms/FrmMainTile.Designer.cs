namespace JHDock
{
    partial class FrmMainTile
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainTile));
            this.pbMin = new System.Windows.Forms.PictureBox();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.lbName = new System.Windows.Forms.Label();
            this.pbSystemLogos = new System.Windows.Forms.Panel();
            this.picUser = new System.Windows.Forms.PictureBox();
            this.picLeftBottom = new System.Windows.Forms.PictureBox();
            this.picRightBottom = new System.Windows.Forms.PictureBox();
            this.picMessage = new System.Windows.Forms.PictureBox();
            this.picSet = new System.Windows.Forms.PictureBox();
            this.listBoxMenu = new System.Windows.Forms.ListBox();
            this.lbMessage = new System.Windows.Forms.Label();
            this.lbSet = new System.Windows.Forms.Label();
            this.timerLockScreen = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLeftBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRightBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSet)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMin
            // 
            this.pbMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMin.Image = global::JHDock.Properties.Resources.最小化常态;
            this.pbMin.Location = new System.Drawing.Point(1306, 0);
            this.pbMin.Name = "pbMin";
            this.pbMin.Size = new System.Drawing.Size(30, 26);
            this.pbMin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbMin.TabIndex = 3;
            this.pbMin.TabStop = false;
            this.pbMin.Click += new System.EventHandler(this.pbMin_Click);
            // 
            // pbClose
            // 
            this.pbClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbClose.Image = ((System.Drawing.Image)(resources.GetObject("pbClose.Image")));
            this.pbClose.Location = new System.Drawing.Point(1336, 0);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(30, 26);
            this.pbClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbClose.TabIndex = 4;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            // 
            // lbName
            // 
            this.lbName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbName.AutoSize = true;
            this.lbName.BackColor = System.Drawing.Color.Transparent;
            this.lbName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.ForeColor = System.Drawing.Color.Black;
            this.lbName.Location = new System.Drawing.Point(1057, 29);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(41, 12);
            this.lbName.TabIndex = 8;
            this.lbName.Text = "lbName";
            this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbSystemLogos
            // 
            this.pbSystemLogos.AutoSize = true;
            this.pbSystemLogos.BackColor = System.Drawing.Color.Transparent;
            this.pbSystemLogos.Location = new System.Drawing.Point(554, 180);
            this.pbSystemLogos.Name = "pbSystemLogos";
            this.pbSystemLogos.Size = new System.Drawing.Size(243, 75);
            this.pbSystemLogos.TabIndex = 13;
            // 
            // picUser
            // 
            this.picUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picUser.BackColor = System.Drawing.Color.Transparent;
            this.picUser.Image = ((System.Drawing.Image)(resources.GetObject("picUser.Image")));
            this.picUser.Location = new System.Drawing.Point(1030, 24);
            this.picUser.Name = "picUser";
            this.picUser.Size = new System.Drawing.Size(22, 22);
            this.picUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picUser.TabIndex = 14;
            this.picUser.TabStop = false;
            // 
            // picLeftBottom
            // 
            this.picLeftBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picLeftBottom.Image = ((System.Drawing.Image)(resources.GetObject("picLeftBottom.Image")));
            this.picLeftBottom.Location = new System.Drawing.Point(24, 667);
            this.picLeftBottom.Name = "picLeftBottom";
            this.picLeftBottom.Size = new System.Drawing.Size(192, 22);
            this.picLeftBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLeftBottom.TabIndex = 15;
            this.picLeftBottom.TabStop = false;
            // 
            // picRightBottom
            // 
            this.picRightBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picRightBottom.Image = ((System.Drawing.Image)(resources.GetObject("picRightBottom.Image")));
            this.picRightBottom.Location = new System.Drawing.Point(1177, 667);
            this.picRightBottom.Name = "picRightBottom";
            this.picRightBottom.Size = new System.Drawing.Size(165, 27);
            this.picRightBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picRightBottom.TabIndex = 16;
            this.picRightBottom.TabStop = false;
            // 
            // picMessage
            // 
            this.picMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picMessage.BackColor = System.Drawing.Color.Transparent;
            this.picMessage.Image = ((System.Drawing.Image)(resources.GetObject("picMessage.Image")));
            this.picMessage.Location = new System.Drawing.Point(1120, 24);
            this.picMessage.Name = "picMessage";
            this.picMessage.Size = new System.Drawing.Size(22, 22);
            this.picMessage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picMessage.TabIndex = 17;
            this.picMessage.TabStop = false;
            // 
            // picSet
            // 
            this.picSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picSet.BackColor = System.Drawing.Color.Transparent;
            this.picSet.Image = ((System.Drawing.Image)(resources.GetObject("picSet.Image")));
            this.picSet.Location = new System.Drawing.Point(1210, 24);
            this.picSet.Name = "picSet";
            this.picSet.Size = new System.Drawing.Size(22, 22);
            this.picSet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picSet.TabIndex = 18;
            this.picSet.TabStop = false;
            // 
            // listBoxMenu
            // 
            this.listBoxMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxMenu.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxMenu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxMenu.FormattingEnabled = true;
            this.listBoxMenu.ItemHeight = 12;
            this.listBoxMenu.Location = new System.Drawing.Point(1210, 47);
            this.listBoxMenu.Name = "listBoxMenu";
            this.listBoxMenu.Size = new System.Drawing.Size(53, 36);
            this.listBoxMenu.TabIndex = 19;
            this.listBoxMenu.Visible = false;
            this.listBoxMenu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBoxMenu_MouseClick);
            // 
            // lbMessage
            // 
            this.lbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMessage.AutoSize = true;
            this.lbMessage.BackColor = System.Drawing.Color.Transparent;
            this.lbMessage.Location = new System.Drawing.Point(1147, 29);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(29, 12);
            this.lbMessage.TabIndex = 21;
            this.lbMessage.Text = "消息";
            this.lbMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbSet
            // 
            this.lbSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSet.AutoSize = true;
            this.lbSet.BackColor = System.Drawing.Color.Transparent;
            this.lbSet.Location = new System.Drawing.Point(1237, 29);
            this.lbSet.Name = "lbSet";
            this.lbSet.Size = new System.Drawing.Size(29, 12);
            this.lbSet.TabIndex = 22;
            this.lbSet.Text = "设置";
            this.lbSet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerLockScreen
            // 
            this.timerLockScreen.Enabled = true;
            this.timerLockScreen.Interval = 1000;
            this.timerLockScreen.Tick += new System.EventHandler(this.timerLockScreen_Tick);
            // 
            // FrmMainTile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::JHDock.Properties.Resources.背景图;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1366, 728);
            this.Controls.Add(this.lbSet);
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.listBoxMenu);
            this.Controls.Add(this.picSet);
            this.Controls.Add(this.picMessage);
            this.Controls.Add(this.picRightBottom);
            this.Controls.Add(this.picLeftBottom);
            this.Controls.Add(this.picUser);
            this.Controls.Add(this.pbSystemLogos);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.pbClose);
            this.Controls.Add(this.pbMin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMainTile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "黄石中心医院单点登录平台";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmMain_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pbMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLeftBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRightBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMin;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Panel pbSystemLogos;
        private System.Windows.Forms.PictureBox picUser;
        private System.Windows.Forms.PictureBox picLeftBottom;
        private System.Windows.Forms.PictureBox picRightBottom;
        private System.Windows.Forms.PictureBox picMessage;
        private System.Windows.Forms.PictureBox picSet;
        private System.Windows.Forms.ListBox listBoxMenu;
        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.Label lbSet;
        private System.Windows.Forms.Timer timerLockScreen;
    }
}