using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Xml;

namespace JHDock
{
    public partial class FrmMessageClose : Form
    {
        private DockWindow dock = null;
        private NotifyIcon noTiIc = null;
        private string strIsCloseOrMin = ConfigurationManager.ConnectionStrings["IsCloseOrMin"].ConnectionString;
        public FrmMessageClose(DockWindow dockWindow, NotifyIcon noTiIc)
        {
            InitializeComponent();
            this.dock = dockWindow;
            this.noTiIc = noTiIc;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
           this.dock.Hide();
           this.noTiIc.Visible = true;
           if (this.cbxNoPrompt.Checked==true)
           {
               XmlDocument xDoc = new System.Xml.XmlDocument();
               xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
               System.Xml.XmlNode xNode;
               System.Xml.XmlElement xElem1;
               xNode = xDoc.SelectSingleNode("//connectionStrings");
               xElem1 = (System.Xml.XmlElement)xNode.SelectSingleNode("//add[@name='IsCloseOrMin']");
               if (xElem1 != null) xElem1.SetAttribute("connectionString", "Yes");
               xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");
           }
           this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.dock.Close();
            if (this.cbxNoPrompt.Checked == true)
            {
                XmlDocument xDoc = new System.Xml.XmlDocument();
                xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
                System.Xml.XmlNode xNode;
                System.Xml.XmlElement xElem1;
                xNode = xDoc.SelectSingleNode("//connectionStrings");
                xElem1 = (System.Xml.XmlElement)xNode.SelectSingleNode("//add[@name='IsCloseOrMin']");
                if (xElem1 != null) xElem1.SetAttribute("connectionString", "No");
                xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");
            }
            this.Close();
        }
    }
}
