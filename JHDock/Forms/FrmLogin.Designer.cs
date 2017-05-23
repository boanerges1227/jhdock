namespace JHDock
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.picNameTxt = new System.Windows.Forms.PictureBox();
            this.picPasswordTxt = new System.Windows.Forms.PictureBox();
            this.picName = new System.Windows.Forms.PictureBox();
            this.picPassword = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.PictureBox();
            this.linklabUpdatePad = new System.Windows.Forms.LinkLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btnCA = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNameTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPasswordTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Location = new System.Drawing.Point(420, 42);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(185, 14);
            this.txtName.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Location = new System.Drawing.Point(420, 85);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(185, 14);
            this.txtPassword.TabIndex = 3;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox2.Location = new System.Drawing.Point(469, 201);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(196, 25);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // picNameTxt
            // 
            this.picNameTxt.BackColor = System.Drawing.Color.Transparent;
            this.picNameTxt.Image = global::JHDock.Properties.Resources.输入框触发;
            this.picNameTxt.Location = new System.Drawing.Point(391, 31);
            this.picNameTxt.Name = "picNameTxt";
            this.picNameTxt.Size = new System.Drawing.Size(223, 32);
            this.picNameTxt.TabIndex = 5;
            this.picNameTxt.TabStop = false;
            // 
            // picPasswordTxt
            // 
            this.picPasswordTxt.BackColor = System.Drawing.Color.Transparent;
            this.picPasswordTxt.Image = global::JHDock.Properties.Resources.输入框默认_2;
            this.picPasswordTxt.Location = new System.Drawing.Point(391, 76);
            this.picPasswordTxt.Name = "picPasswordTxt";
            this.picPasswordTxt.Size = new System.Drawing.Size(223, 32);
            this.picPasswordTxt.TabIndex = 5;
            this.picPasswordTxt.TabStop = false;
            // 
            // picName
            // 
            this.picName.BackColor = System.Drawing.Color.White;
            this.picName.Image = global::JHDock.Properties.Resources.user1;
            this.picName.Location = new System.Drawing.Point(398, 38);
            this.picName.Name = "picName";
            this.picName.Size = new System.Drawing.Size(15, 19);
            this.picName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picName.TabIndex = 5;
            this.picName.TabStop = false;
            // 
            // picPassword
            // 
            this.picPassword.BackColor = System.Drawing.Color.White;
            this.picPassword.Image = global::JHDock.Properties.Resources.锁2;
            this.picPassword.Location = new System.Drawing.Point(398, 80);
            this.picPassword.Name = "picPassword";
            this.picPassword.Size = new System.Drawing.Size(15, 20);
            this.picPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPassword.TabIndex = 5;
            this.picPassword.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BackgroundImage = global::JHDock.Properties.Resources.登录常态;
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOK.Location = new System.Drawing.Point(391, 144);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(69, 27);
            this.btnOK.TabIndex = 6;
            this.btnOK.TabStop = false;
            this.btnOK.Tag = "4";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = global::JHDock.Properties.Resources.取消常态;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancel.Location = new System.Drawing.Point(545, 144);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 27);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.TabStop = false;
            this.btnCancel.Tag = "5";
            // 
            // linklabUpdatePad
            // 
            this.linklabUpdatePad.AutoSize = true;
            this.linklabUpdatePad.BackColor = System.Drawing.Color.Transparent;
            this.linklabUpdatePad.LinkColor = System.Drawing.Color.White;
            this.linklabUpdatePad.Location = new System.Drawing.Point(620, 85);
            this.linklabUpdatePad.Name = "linklabUpdatePad";
            this.linklabUpdatePad.Size = new System.Drawing.Size(53, 12);
            this.linklabUpdatePad.TabIndex = 8;
            this.linklabUpdatePad.TabStop = true;
            this.linklabUpdatePad.Text = "修改密码";
            this.linklabUpdatePad.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImage = global::JHDock.Properties.Resources.嘉和信息logo;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox4.Location = new System.Drawing.Point(160, 144);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(208, 27);
            this.pictureBox4.TabIndex = 12;
            this.pictureBox4.TabStop = false;
            // 
            // btnCA
            // 
            this.btnCA.BackColor = System.Drawing.Color.Transparent;
            this.btnCA.BackgroundImage = global::JHDock.Properties.Resources.CA登录常态;
            this.btnCA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCA.Location = new System.Drawing.Point(469, 144);
            this.btnCA.Name = "btnCA";
            this.btnCA.Size = new System.Drawing.Size(69, 27);
            this.btnCA.TabIndex = 14;
            this.btnCA.TabStop = false;
            this.btnCA.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::JHDock.Properties.Resources.JHIP_LOGO;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.InitialImage = global::JHDock.Properties.Resources.JHIP_LOGO;
            this.pictureBox1.Location = new System.Drawing.Point(51, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(327, 48);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // frmLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::JHDock.Properties.Resources.底图;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(685, 238);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCA);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.linklabUpdatePad);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.picPassword);
            this.Controls.Add(this.picName);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.picPasswordTxt);
            this.Controls.Add(this.picNameTxt);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNameTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPasswordTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox picNameTxt;
        private System.Windows.Forms.PictureBox picPasswordTxt;
        private System.Windows.Forms.PictureBox picName;
        private System.Windows.Forms.PictureBox picPassword;
        private System.Windows.Forms.PictureBox btnOK;
        private System.Windows.Forms.PictureBox btnCancel;
        private System.Windows.Forms.LinkLabel linklabUpdatePad;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox btnCA;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}