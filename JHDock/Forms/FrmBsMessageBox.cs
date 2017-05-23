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
    public partial class FrmBsMessageBox : Form
    {
        private string strCobvalues = "";
        private string postData = "";
        private string itemId = "";
        private string itemName = "";
        public FrmBsMessageBox(string cobValues, string strPostData,string itemId,string itemName)
        {
            InitializeComponent();
            this.strCobvalues = cobValues;
            this.postData = strPostData;
            this.itemId = itemId;
            this.itemName = itemName;
            string[] cobValueArr = cobValues.Split('%');
            this.cobServerIP.Items.AddRange(cobValueArr);
        }

        private void btnBSOk_Click(object sender, EventArgs e)
        {
            FormatXml.UpdateLoc_Run_Program(this.itemId, this.cobServerIP.Text);
            HttpGood.OpenNewIe(this.cobServerIP.Text, this.postData, this.itemName);
            PublicVariableModel.strLocTatget = this.cobServerIP.Text;
            this.Close();
        }
    }
}
