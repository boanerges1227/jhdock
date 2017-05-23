namespace JHDock
{
    partial class FrmBsMessageBox
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
            this.cobServerIP = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBSOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cobServerIP
            // 
            this.cobServerIP.FormattingEnabled = true;
            this.cobServerIP.Location = new System.Drawing.Point(164, 36);
            this.cobServerIP.Name = "cobServerIP";
            this.cobServerIP.Size = new System.Drawing.Size(148, 20);
            this.cobServerIP.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "请选择要连接的服务器IP：";
            // 
            // btnBSOk
            // 
            this.btnBSOk.Location = new System.Drawing.Point(329, 33);
            this.btnBSOk.Name = "btnBSOk";
            this.btnBSOk.Size = new System.Drawing.Size(75, 23);
            this.btnBSOk.TabIndex = 2;
            this.btnBSOk.Text = "确定";
            this.btnBSOk.UseVisualStyleBackColor = true;
            this.btnBSOk.Click += new System.EventHandler(this.btnBSOk_Click);
            // 
            // FrmBsMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 76);
            this.Controls.Add(this.btnBSOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cobServerIP);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBsMessageBox";
            this.Text = "提示";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cobServerIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBSOk;
    }
}