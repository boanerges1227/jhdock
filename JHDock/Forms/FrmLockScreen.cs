using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JHDock
{
    public partial class FrmLockScreen : Form
    {
        public FrmLockScreen()
        {
            InitializeComponent();
        }
        public string UserName { get; set; }
        private void btnOK_Click(object sender, EventArgs e)
        {           
            if (FormatXml.LogIn(this.UserName, this.tbxPassword.Text.Trim()))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("密码错误，请重新输入");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        
    }
}
