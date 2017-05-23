using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;
using JHDock;
using Microsoft.Win32;


namespace JHDock
{
    public partial class BaseForm : CCSkinMain
    {
        public BaseForm()
        {
            InitializeComponent();
            
        }
        public void SetTh(CCSkinMain skin)
        {
            this.XTheme = skin;
        }

        private void OrayForm_Load(object sender, EventArgs e)
        {
        }

        private void BaseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //RegistryKey reg1 = Registry.CurrentUser;
            //RegistryKey reg2 = reg1.CreateSubKey("SoftWare\\MySoft");
            //reg2.SetValue("color", this.Location.X);
        }

    }
}
