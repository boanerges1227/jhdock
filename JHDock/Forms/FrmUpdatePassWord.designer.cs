namespace JHDock
{
    partial class FrmUpdatePassWord
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
            this.label1 = new System.Windows.Forms.Label();
            this.labNewPassWord = new System.Windows.Forms.Label();
            this.labOldPassWord = new System.Windows.Forms.Label();
            this.labUserName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.PictureBox();
            this.picbUserName = new System.Windows.Forms.PictureBox();
            this.txtOldPassWord = new System.Windows.Forms.TextBox();
            this.picbOldPassWord = new System.Windows.Forms.PictureBox();
            this.txtNewPassWord = new System.Windows.Forms.TextBox();
            this.picNewPassWord = new System.Windows.Forms.PictureBox();
            this.txtNewPassWords = new System.Windows.Forms.TextBox();
            this.picbPassWords = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbOldPassWord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNewPassWord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbPassWords)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(48, 238);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "确认新密码";
            // 
            // labNewPassWord
            // 
            this.labNewPassWord.AutoSize = true;
            this.labNewPassWord.ForeColor = System.Drawing.Color.White;
            this.labNewPassWord.Location = new System.Drawing.Point(71, 189);
            this.labNewPassWord.Name = "labNewPassWord";
            this.labNewPassWord.Size = new System.Drawing.Size(41, 12);
            this.labNewPassWord.TabIndex = 21;
            this.labNewPassWord.Text = "新密码";
            // 
            // labOldPassWord
            // 
            this.labOldPassWord.AutoSize = true;
            this.labOldPassWord.ForeColor = System.Drawing.Color.White;
            this.labOldPassWord.Location = new System.Drawing.Point(71, 142);
            this.labOldPassWord.Name = "labOldPassWord";
            this.labOldPassWord.Size = new System.Drawing.Size(41, 12);
            this.labOldPassWord.TabIndex = 20;
            this.labOldPassWord.Text = "原密码";
            // 
            // labUserName
            // 
            this.labUserName.AutoSize = true;
            this.labUserName.ForeColor = System.Drawing.Color.White;
            this.labUserName.Location = new System.Drawing.Point(72, 98);
            this.labUserName.Name = "labUserName";
            this.labUserName.Size = new System.Drawing.Size(41, 12);
            this.labUserName.TabIndex = 19;
            this.labUserName.Text = "用户名";
            // 
            // txtUserName
            // 
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserName.Location = new System.Drawing.Point(132, 98);
            this.txtUserName.Multiline = true;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(185, 14);
            this.txtUserName.TabIndex = 16;
            this.txtUserName.TabStop = false;
            this.txtUserName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtUserName_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(36, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 24;
            this.label2.Text = "修改密码";
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.Transparent;
            this.btnOk.Image = global::JHDock.Properties.Resources.确定默认;
            this.btnOk.Location = new System.Drawing.Point(102, 281);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(82, 27);
            this.btnOk.TabIndex = 25;
            this.btnOk.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.Image = global::JHDock.Properties.Resources.取消默认;
            this.btnCancel.Location = new System.Drawing.Point(259, 281);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 27);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.TabStop = false;
            // 
            // picbUserName
            // 
            this.picbUserName.BackColor = System.Drawing.Color.Transparent;
            this.picbUserName.Image = global::JHDock.Properties.Resources.输入框默认_2;
            this.picbUserName.Location = new System.Drawing.Point(117, 89);
            this.picbUserName.Name = "picbUserName";
            this.picbUserName.Size = new System.Drawing.Size(223, 32);
            this.picbUserName.TabIndex = 27;
            this.picbUserName.TabStop = false;
            // 
            // txtOldPassWord
            // 
            this.txtOldPassWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOldPassWord.Location = new System.Drawing.Point(132, 142);
            this.txtOldPassWord.Multiline = true;
            this.txtOldPassWord.Name = "txtOldPassWord";
            this.txtOldPassWord.PasswordChar = '*';
            this.txtOldPassWord.Size = new System.Drawing.Size(185, 14);
            this.txtOldPassWord.TabIndex = 0;
            // 
            // picbOldPassWord
            // 
            this.picbOldPassWord.BackColor = System.Drawing.Color.Transparent;
            this.picbOldPassWord.Image = global::JHDock.Properties.Resources.输入框默认_2;
            this.picbOldPassWord.Location = new System.Drawing.Point(117, 133);
            this.picbOldPassWord.Name = "picbOldPassWord";
            this.picbOldPassWord.Size = new System.Drawing.Size(223, 32);
            this.picbOldPassWord.TabIndex = 29;
            this.picbOldPassWord.TabStop = false;
            // 
            // txtNewPassWord
            // 
            this.txtNewPassWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNewPassWord.Location = new System.Drawing.Point(132, 189);
            this.txtNewPassWord.Multiline = true;
            this.txtNewPassWord.Name = "txtNewPassWord";
            this.txtNewPassWord.PasswordChar = '*';
            this.txtNewPassWord.Size = new System.Drawing.Size(185, 14);
            this.txtNewPassWord.TabIndex = 1;
            // 
            // picNewPassWord
            // 
            this.picNewPassWord.BackColor = System.Drawing.Color.Transparent;
            this.picNewPassWord.Image = global::JHDock.Properties.Resources.输入框默认_2;
            this.picNewPassWord.Location = new System.Drawing.Point(117, 180);
            this.picNewPassWord.Name = "picNewPassWord";
            this.picNewPassWord.Size = new System.Drawing.Size(223, 32);
            this.picNewPassWord.TabIndex = 31;
            this.picNewPassWord.TabStop = false;
            // 
            // txtNewPassWords
            // 
            this.txtNewPassWords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNewPassWords.Location = new System.Drawing.Point(132, 236);
            this.txtNewPassWords.Multiline = true;
            this.txtNewPassWords.Name = "txtNewPassWords";
            this.txtNewPassWords.PasswordChar = '*';
            this.txtNewPassWords.Size = new System.Drawing.Size(185, 14);
            this.txtNewPassWords.TabIndex = 2;
            // 
            // picbPassWords
            // 
            this.picbPassWords.BackColor = System.Drawing.Color.Transparent;
            this.picbPassWords.Image = global::JHDock.Properties.Resources.输入框默认_2;
            this.picbPassWords.Location = new System.Drawing.Point(117, 227);
            this.picbPassWords.Name = "picbPassWords";
            this.picbPassWords.Size = new System.Drawing.Size(223, 32);
            this.picbPassWords.TabIndex = 33;
            this.picbPassWords.TabStop = false;
            // 
            // FrmUpdatePassWord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 321);
            this.Controls.Add(this.txtNewPassWords);
            this.Controls.Add(this.picbPassWords);
            this.Controls.Add(this.txtNewPassWord);
            this.Controls.Add(this.picNewPassWord);
            this.Controls.Add(this.txtOldPassWord);
            this.Controls.Add(this.picbOldPassWord);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labNewPassWord);
            this.Controls.Add(this.labOldPassWord);
            this.Controls.Add(this.labUserName);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.picbUserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmUpdatePassWord";
            this.Opacity = 0.8D;
            this.Text = "";
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbOldPassWord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNewPassWord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbPassWords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labNewPassWord;
        private System.Windows.Forms.Label labOldPassWord;
        private System.Windows.Forms.Label labUserName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox btnOk;
        private System.Windows.Forms.PictureBox btnCancel;
        private System.Windows.Forms.PictureBox picbUserName;
        private System.Windows.Forms.TextBox txtOldPassWord;
        private System.Windows.Forms.PictureBox picbOldPassWord;
        private System.Windows.Forms.TextBox txtNewPassWord;
        private System.Windows.Forms.PictureBox picNewPassWord;
        private System.Windows.Forms.TextBox txtNewPassWords;
        private System.Windows.Forms.PictureBox picbPassWords;

    }
}