using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace JHDock
{
    public partial class FrmMessageBox : Form
    {
        public bool isYes = false;
        string strArgumentsl = "";
        string strSysNol = "";
        string processNamel = "";
        string strMessagel = "";
        IDockItem item = null;
        public FrmMessageBox(string strMessage, string strBtnMessage1, string strBtnMessage2, string strArguments, string strSysNo, string processName,IDockItem item)
        {
            InitializeComponent();
            this.item = item;
            strMessagel=strMessage;
            this.lblContext.Text = strMessagel;
            this.btnYes.Text = strBtnMessage1;
            this.btnNo.Text = strBtnMessage2;
            this.strArgumentsl = strArguments;
            this.strSysNol = strSysNo;
            oFDmanualFind.Filter = "文件类型(*.exe)|*.exe";
            this.processNamel = processName;
            //绑定计算机硬盘列表
            string[] drives = Directory.GetLogicalDrives();
            this.cobDiskDirectory.Items.AddRange(drives);
            this.cobDiskDirectory.Items.Add("全盘查找");
            if (this.cobDiskDirectory.Text == "" || this.cobDiskDirectory.Text == null)
            {
                this.btnNo.Enabled = false;
            }
        }
        /// <summary>
        /// 手动查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYes_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            oFDmanualFind.FileName = this.processNamel;
            if (oFDmanualFind.ShowDialog() == DialogResult.OK)
            {
                FormatXml.UpdateLoc_Run_Program(this.strSysNol, oFDmanualFind.FileName);
                //PublicVariableModel.exeFilePath = oFDmanualFind.FileName;
                this.item.LocTarget = oFDmanualFind.FileName;
                startInfo.FileName = oFDmanualFind.FileName; //启动的应用程序名称       
                startInfo.Arguments = this.strArgumentsl;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                Process.Start(startInfo);
                this.Close();
            }

        }
        private void cobDiskDirectory_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cobDiskDirectory.Text != "" || this.cobDiskDirectory.Text != null)
            {
                this.btnNo.Enabled = true;
            }
        }
        /// <summary>
        /// 自动查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNo_Click(object sender, EventArgs e)
        {
            try
            {
                this.lblContext.Text = "";
                this.progressBar.Visible = true;
                string strFileName = strFileName = ConvertFileAndStr.GetFullFileName(this.cobDiskDirectory.Text,this.processNamel,this.progressBar);
                if (!File.Exists(strFileName))
                {
                    MessageBox.Show("此计算机中没有找到该程序！");
                    this.lblContext.Visible=true;
                    return;
                }
                ProcessStartInfo startInfo = new ProcessStartInfo();
                FormatXml.UpdateLoc_Run_Program(this.strSysNol, strFileName);
                this.item.LocTarget = strFileName;
                startInfo.FileName = strFileName; //启动的应用程序名称       
                startInfo.Arguments = this.strArgumentsl;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                Process.Start(startInfo);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

    }
}
