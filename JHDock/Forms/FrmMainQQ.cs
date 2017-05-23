using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JHDock.Properties;
using Microsoft.Win32;
using CCWin;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;


namespace JHDock
{
    public partial class FrmMainQQ : BaseForm
    {
        private static FrmMainQQ instance;
        public static FrmMainQQ GetInstance()
        {
            if (instance == null)
            {
                instance = new FrmMainQQ();
            }
            return instance;
        }
        string themeName;
        NotifyIcon noTiIc = null;
        //是否首次启动标识
        bool startFlag = true;
        private FrmMainQQ()
        {
            InitializeComponent();
            InitNotifyIcon();
            this.moduleListBox1.Initialize(new XmlService().findAllXmlData());
            timerGetSession.Start();
            this.autoDocker1.Initialize(this);

            KeyboardHook k_hook = new KeyboardHook();
            k_hook.KeyDownEvent += new KeyEventHandler(hook_KeyDown);//钩住键按下 
            k_hook.Start();//安装键盘钩子

            MouseHook m_hook = new MouseHook();
            m_hook.OnMouseActivity += new MouseEventHandler(hook_OnMouseActivity);
            m_hook.Start();
        }
        private void InitNotifyIcon()
        {
            noTiIc = new NotifyIcon();
            noTiIc.Icon = Resources.sso图标;
            noTiIc.Text = "当前用户为：" + PublicVariableModel.userInfoModelList.UserInfoList[0].Name;
            noTiIc.MouseClick += new MouseEventHandler(noTiIcMouseClick);
            this.ShowInTaskbar = false;
            this.noTiIc.Visible = true;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            labelName.Text = PublicVariableModel.userInfoModelList.UserInfoList[0].Name;
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("SoftWare\\MySoft");
            int x = Convert.ToInt32(reg.GetValue("1"));
            int y = Convert.ToInt32(reg.GetValue("2"));
            this.Location = new Point(x, y);//可以转换成 Left 、Top 见 2.
            object theme = reg.GetValue("3");
            if (theme == null)
            {
                themeName = "None";
                reg.SetValue("3", themeName);
            }
            else {
                themeName = theme.ToString();
            }
            FrmSkin.SetTheme(themeName);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmUserInfo frmUserInfo = FrmUserInfo.Instance;
            frmUserInfo.Show();
        }


        private void noTiIcMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true;
                switch (autoDocker1.dockHideType)
                {
                    case DockHideType.Left:
                        this.Location = new Point(this.Location.X + this.Width, this.Location.Y);
                        break;
                    case DockHideType.Right:
                        this.Location = new Point(this.Location.X - this.Width, this.Location.Y);
                        break;
                    case DockHideType.Top:
                        this.Location = new Point(this.Location.X, this.Location.Y + this.Height);
                        break; 
                }
            }

        }


        private void timerGetSession_Tick(object sender, EventArgs e)
        {
            JHIPSSO.SsoWebsvcClient client = new JHIPSSO.SsoWebsvcClient();
            string requestStr = string.Format("<REQUEST JHIPMsgVersion=\"1.0.1\"><JHSESSIONID>{0}</JHSESSIONID></REQUEST>", PublicVariableModel.SessionID);
            string responseStr = client.getNewSessionID(requestStr);
            DataSet ds = XmlSerializerHelper.ConvertXMLToDataSet(responseStr);
            PublicVariableModel.SessionID = ds.Tables[1].Rows[0]["JHSessionID"].ToString();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RegistryKey reg1 = Registry.CurrentUser;
            RegistryKey reg2 = reg1.CreateSubKey("SoftWare\\MySoft");
            reg2.SetValue("1", this.Location.X);
            reg2.SetValue("2", this.Location.Y);

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FrmSkin frmSkin = FrmSkin.Instance;
            frmSkin.Show();
        }
        private int lockScreenCount = 0;
        private void timerLockScreen_Tick(object sender, EventArgs e)
        {
            lockScreenCount++;
            if (lockScreenCount == int.Parse(PublicVariableModel.lockScreenTime))
            {
                LockScreen();
            }
        }
        /// <summary>
        /// 锁屏
        /// </summary>
        private void LockScreen()
        {
            FrmLockScreen frmLockScreen = new FrmLockScreen();
            frmLockScreen.UserName = PublicVariableModel.userName;
            DialogResult result = frmLockScreen.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                lockScreenCountClear();
            }
        }       
        private void lockScreenCountClear()
        {
            lockScreenCount = 0;
        }
        private void hook_KeyDown(object sender, KeyEventArgs e)
        {
            lockScreenCountClear();
        }
        private void hook_OnMouseActivity(object sender, MouseEventArgs e)
        {
            lockScreenCountClear();
        }
    }
}
