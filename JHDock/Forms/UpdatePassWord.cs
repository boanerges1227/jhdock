using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace JHDock
{
    public partial class UpdatePassWord : Form
    {
        private bool flag = false;
        Point mouseOff; //鼠标移动位置变量
        public UpdatePassWord()
        {
            InitializeComponent();
            //窗体以及控件事件注册
            this.BackColor = Color.FromArgb(255, 31, 105, 156);
            this.btnOk.MouseMove += new MouseEventHandler(btnOk_MouseMove);
            this.btnOk.MouseLeave += new EventHandler(btnOk_MouseLeave);
            this.btnCancel.MouseMove += new MouseEventHandler(btnCancel_MouseMove);
            this.btnCancel.MouseLeave += new EventHandler(btnCancel_MouseLeave);
            this.MouseDown += new MouseEventHandler(FromUpdatePassWord_MouseDown);
            this.MouseUp += new MouseEventHandler(FromUpdatePassWord_MouseUp);
            this.MouseMove += new MouseEventHandler(FromUpdatePassWord_MouseMove);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.txtUserName.GotFocus += new EventHandler(TxtUserName_GotFocus);
            this.txtUserName.LostFocus += new EventHandler(TxtUserName_LostFocus);
            this.txtOldPassWord.GotFocus += new EventHandler(TxtOldPassWord_GotFocus);
            this.txtOldPassWord.LostFocus += new EventHandler(TxtOldPassWord_LostFocus);
            this.txtNewPassWord.GotFocus += new EventHandler(TxtNewPassWord_GotFocus);
            this.txtNewPassWord.LostFocus += new EventHandler(TxtNewPassWord_LostFocus);
            this.txtNewPassWords.GotFocus += new EventHandler(TxtNewPassWords_GotFocus);
            this.txtNewPassWords.LostFocus += new EventHandler(TxtNewPassWords_LostFocus);
            Rectangle rect = new Rectangle(0,0,this.Width,this.Height);
            this.Region=new System.Drawing.Region (GetRoundedRectPath(rect, 25));
        }
        #region 文本框获取焦点事件
        private void TxtUserName_GotFocus(object sender, EventArgs e)
        {
            this.picbUserName.Image = (Image)JHDock.Properties.Resources.输入框触发;
            this.txtUserName.SelectAll();
        }
        private void TxtUserName_LostFocus(object sender, EventArgs e)
        {
            this.picbUserName.Image = (Image)JHDock.Properties.Resources.输入框默认_2;
        }
        private void TxtOldPassWord_GotFocus(object sender, EventArgs e)
        {
            this.picbOldPassWord.Image = (Image)JHDock.Properties.Resources.输入框触发;
        }
        private void TxtOldPassWord_LostFocus(object sender, EventArgs e)
        {
            this.picbOldPassWord.Image = (Image)JHDock.Properties.Resources.输入框默认_2;
        }
        private void TxtNewPassWord_GotFocus(object sender, EventArgs e)
        {
            this.picNewPassWord.Image = (Image)JHDock.Properties.Resources.输入框触发;
        }
        private void TxtNewPassWord_LostFocus(object sender, EventArgs e)
        {
            this.picNewPassWord.Image = (Image)JHDock.Properties.Resources.输入框默认_2;
        }
        private void TxtNewPassWords_GotFocus(object sender, EventArgs e)
        {
            this.picbPassWords.Image = (Image)JHDock.Properties.Resources.输入框触发;
        }
        private void TxtNewPassWords_LostFocus(object sender, EventArgs e)
        {
            this.picbPassWords.Image = (Image)JHDock.Properties.Resources.输入框默认_2;
        }
        #endregion

        #region 窗体移动事件
        private void FromUpdatePassWord_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                flag = true;
                mouseOff = new Point(-e.X, -e.Y);
            }
        }
        private void FromUpdatePassWord_MouseUp(object sender, MouseEventArgs e)
        {
            if (flag)
            {
                flag = false;
            }
        }
        private void FromUpdatePassWord_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);
                Location = mouseSet;
            }
        }
        #endregion

        #region 按钮鼠标掠过时更换图片
        private void btnCancel_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnCancel.Image = (Image)JHDock.Properties.Resources.取消点击态;
        }
        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            this.btnCancel.Image = (Image)JHDock.Properties.Resources.取消常态;
        }
        private void btnOk_MouseMove(object sender, EventArgs e)
        {
            this.btnOk.Image = (Image)JHDock.Properties.Resources.登录点击态;
        }
        public void btnOk_MouseLeave(object sender, EventArgs e)
        {
            this.btnOk.Image = (Image)JHDock.Properties.Resources.登录常态;
        }
        #endregion

        /// <summary>
        /// 修改密码事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.txtUserName.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("用户名不能为空！");
                return;
            }
            else if (this.txtOldPassWord.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("原密码不能为空！");
                return;
            }
            else if (this.txtNewPassWord.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("请输入要修改的密码！");
                return;
            }
            else if (this.txtNewPassWords.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("请输入确认密码！");
                return;
            }
            else if (this.txtNewPassWord.Text != this.txtNewPassWords.Text)
            {
                System.Windows.Forms.MessageBox.Show("两次输入的密码不一致！");
            }

            else
            {
                DataSet dtUserLoginInfo = new DataSet();
                //WebServiceUserInfo.JhipWebsvcClient jhipWeb = new WebServiceUserInfo.JhipWebsvcClient();
                JHIPSSO.SsoWebsvcClient jhipWeb = new JHIPSSO.SsoWebsvcClient();
                dtUserLoginInfo.Tables[0].TableName = "REQUEST";
                dtUserLoginInfo.Tables[0].Rows[0]["JHIPMsgVersion"] = "1.0.1";
                dtUserLoginInfo.Tables[0].Rows[1]["FROM_SYS"] = "EMR";
                dtUserLoginInfo.Tables[0].Rows[2]["CODE"] = this.txtUserName.Text;
                dtUserLoginInfo.Tables[0].Rows[3]["PASSWORD"] = this.txtOldPassWord.Text;
                dtUserLoginInfo.Tables[0].Rows[4]["NEW_PASSWORD"] = this.txtNewPassWords.Text;
                string strUserLoginInfo = XmlSerializerHelper.ConvertDataSetToXML(dtUserLoginInfo);
            }

        }
        /// <summary>
        /// 画窗体圆角
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
            //   左上角    
            path.AddArc(arcRect, 185, 90);
            //   右上角    
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);
            //   右下角    
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 356, 90);
            //   左下角    
            arcRect.X = rect.Left;
            arcRect.Width += 2;
            arcRect.Height += 2;
            path.AddArc(arcRect, 90, 90);

            //上横线
            // path.CloseFigure();
            return path;
        }
        //窗体关闭事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
