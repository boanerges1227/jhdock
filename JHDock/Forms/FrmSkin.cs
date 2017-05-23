using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin.SkinControl;
using CCWin;
using System.Reflection;
using Microsoft.Win32;

namespace JHDock
{
    public partial class FrmSkin : BaseForm
    {
        private static FrmSkin instance;
        public static FrmSkin Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FrmSkin();
                }
                return instance;
            }
        }
        private FrmSkin()
        {
            InitializeComponent();
            this.btnMac.Click += new System.EventHandler(this.btn_Click);
            this.btnDevExpress.Click += new System.EventHandler(this.btn_Click);
            this.btnVS.Click += new System.EventHandler(this.btn_Click);
            this.btnNone.Click += new System.EventHandler(this.btn_Click);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            SkinButton btn = sender as SkinButton;
            string themeName = btn.Name.Substring(3);
            SetTheme(themeName);            
            //this.SetTh(new CCWin.Skin_Mac());
            RegistryKey reg1 = Registry.CurrentUser;
            RegistryKey reg2 = reg1.CreateSubKey("SoftWare\\MySoft");
            reg2.SetValue("3", themeName);
        }

        public static void SetTheme(string themeName)
        {
            CCSkinMain skinTheme = new CCSkinMain();
            switch (themeName)
            {
                case "Mac":
                    skinTheme = new Skin_Mac() { };
                    break;
                case "DevExpress":
                    skinTheme = new Skin_DevExpress() { };
                    break;
                case "VS":
                    skinTheme = new Skin_VS() { };
                    break;
                case "Metro":
                    skinTheme = new Skin_Metro() { };
                    break;
                case "Color":
                    skinTheme = new Skin_Color() { };
                    break;
                case "None":
                    skinTheme = new CCSkinMain() { };
                    break;
            }
            //if (themeName != "None")
            //{
            //    string skinName = string.Format("Skin_{0}", themeName);
            //    Assembly assembly = Assembly.GetExecutingAssembly();

            //    object obj = assembly.CreateInstance(skinName);
            //    skinTheme = obj as CCSkinMain;
            //}
            FrmMainQQ.GetInstance().SetTh(skinTheme);
            FrmUpdatePassWord.Instance.SetTh(skinTheme);
            FrmUserInfo.Instance.SetTh(skinTheme);
            FrmSkin.Instance.SetTh(skinTheme);
            SystemParamModel.SkinType = themeName;
        }

        private void FrmSkin_Load(object sender, EventArgs e)
        {
            //this.XTheme = new CCWin.CCSkinMain();
        }
    }
}
