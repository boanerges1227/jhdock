namespace JHDock
{
    partial class FrmSkin
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
            instance = null;
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnMac = new CCWin.SkinControl.SkinButton();
            this.btnDevExpress = new CCWin.SkinControl.SkinButton();
            this.btnVS = new CCWin.SkinControl.SkinButton();
            this.btnNone = new CCWin.SkinControl.SkinButton();
            this.SuspendLayout();
            // 
            // btnMac
            // 
            this.btnMac.BackColor = System.Drawing.Color.Transparent;
            this.btnMac.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnMac.DownBack = null;
            this.btnMac.Location = new System.Drawing.Point(7, 57);
            this.btnMac.MouseBack = null;
            this.btnMac.Name = "btnMac";
            this.btnMac.NormlBack = null;
            this.btnMac.Size = new System.Drawing.Size(111, 78);
            this.btnMac.TabIndex = 0;
            this.btnMac.Text = "苹果风格";
            this.btnMac.UseVisualStyleBackColor = true;
            // 
            // btnDevExpress
            // 
            this.btnDevExpress.BackColor = System.Drawing.Color.Transparent;
            this.btnDevExpress.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnDevExpress.DownBack = null;
            this.btnDevExpress.Location = new System.Drawing.Point(124, 57);
            this.btnDevExpress.MouseBack = null;
            this.btnDevExpress.Name = "btnDevExpress";
            this.btnDevExpress.NormlBack = null;
            this.btnDevExpress.Size = new System.Drawing.Size(111, 78);
            this.btnDevExpress.TabIndex = 1;
            this.btnDevExpress.Text = "DevExpress风格";
            this.btnDevExpress.UseVisualStyleBackColor = true;
            // 
            // btnVS
            // 
            this.btnVS.BackColor = System.Drawing.Color.Transparent;
            this.btnVS.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnVS.DownBack = null;
            this.btnVS.Location = new System.Drawing.Point(7, 143);
            this.btnVS.MouseBack = null;
            this.btnVS.Name = "btnVS";
            this.btnVS.NormlBack = null;
            this.btnVS.Size = new System.Drawing.Size(111, 78);
            this.btnVS.TabIndex = 4;
            this.btnVS.Text = "微软风格";
            this.btnVS.UseVisualStyleBackColor = true;
            // 
            // btnNone
            // 
            this.btnNone.BackColor = System.Drawing.Color.Transparent;
            this.btnNone.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnNone.DownBack = null;
            this.btnNone.Location = new System.Drawing.Point(124, 143);
            this.btnNone.MouseBack = null;
            this.btnNone.Name = "btnNone";
            this.btnNone.NormlBack = null;
            this.btnNone.Size = new System.Drawing.Size(111, 78);
            this.btnNone.TabIndex = 5;
            this.btnNone.Text = "默认";
            this.btnNone.UseVisualStyleBackColor = true;
            // 
            // FrmSkin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 228);
            this.Controls.Add(this.btnNone);
            this.Controls.Add(this.btnVS);
            this.Controls.Add(this.btnDevExpress);
            this.Controls.Add(this.btnMac);
            this.Name = "FrmSkin";
            this.Text = "";
            this.Load += new System.EventHandler(this.FrmSkin_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinButton btnMac;
        private CCWin.SkinControl.SkinButton btnDevExpress;
        private CCWin.SkinControl.SkinButton btnVS;
        private CCWin.SkinControl.SkinButton btnNone;
    }
}