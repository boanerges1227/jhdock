namespace JHDock
{
    partial class FrmMessageBox
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
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.lblContext = new System.Windows.Forms.Label();
            this.cobDiskDirectory = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.oFDmanualFind = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(207, 101);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 0;
            this.btnYes.Text = "button2";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.Location = new System.Drawing.Point(207, 63);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 23);
            this.btnNo.TabIndex = 1;
            this.btnNo.Text = "button1";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // lblContext
            // 
            this.lblContext.AutoSize = true;
            this.lblContext.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblContext.Location = new System.Drawing.Point(36, 24);
            this.lblContext.Name = "lblContext";
            this.lblContext.Size = new System.Drawing.Size(67, 14);
            this.lblContext.TabIndex = 2;
            this.lblContext.Text = "提示内容";
            // 
            // cobDiskDirectory
            // 
            this.cobDiskDirectory.FormattingEnabled = true;
            this.cobDiskDirectory.Location = new System.Drawing.Point(12, 64);
            this.cobDiskDirectory.Name = "cobDiskDirectory";
            this.cobDiskDirectory.Size = new System.Drawing.Size(181, 20);
            this.cobDiskDirectory.TabIndex = 3;
            this.cobDiskDirectory.SelectedValueChanged += new System.EventHandler(this.cobDiskDirectory_SelectedValueChanged);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(4, 48);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(286, 76);
            this.progressBar.TabIndex = 4;
            this.progressBar.Visible = false;
            // 
            // oFDmanualFind
            // 
            this.oFDmanualFind.FileName = "openFileDialog1";
            // 
            // FrmMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 136);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.cobDiskDirectory);
            this.Controls.Add(this.lblContext);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "提示";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Label lblContext;
        private System.Windows.Forms.ComboBox cobDiskDirectory;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.OpenFileDialog oFDmanualFind;
    }
}