using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using JHDockInterface;

namespace JHDock
{
    /// <summary>
    /// 修改密码页面 2014-1-8 陈宝栋创建
    /// </summary>
    public partial class FrmUpdatePassWord : BaseForm
    {
        private static FrmUpdatePassWord instance;
        public static FrmUpdatePassWord Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new FrmUpdatePassWord();
                }
                return instance;
            }
        }
        private bool flag = false;
        Point mouseOff; //鼠标移动位置变量
        private FrmUpdatePassWord()
        {
            InitializeComponent();
            this.txtUserName.Text = PublicVariableModel.userLoginName;
            //窗体以及控件事件注册
            //this.BackColor = Color.FromArgb(255, 31, 105, 156);
            //this.BackColor = Color.FromArgb(255, 13, 13, 13);
            this.btnOk.MouseMove += new MouseEventHandler(btnOk_MouseMove);
            this.txtOldPassWord.KeyPress += new KeyPressEventHandler(txtName_KeyPress);
            this.txtNewPassWord.KeyPress += new KeyPressEventHandler(txtNewName_KeyPress);
            this.txtNewPassWords.KeyPress += new KeyPressEventHandler(txtNewNames_KeyPress);
            this.KeyPress += new KeyPressEventHandler(Form_KeyPress);
            this.btnOk.MouseLeave += new EventHandler(btnOk_MouseLeave);
            this.btnCancel.MouseMove += new MouseEventHandler(btnCancel_MouseMove);
            this.btnCancel.MouseLeave += new EventHandler(btnCancel_MouseLeave);
            this.MouseDown += new MouseEventHandler(FromUpdatePassWord_MouseDown);
            this.MouseUp += new MouseEventHandler(FromUpdatePassWord_MouseUp);
            this.MouseMove += new MouseEventHandler(FromUpdatePassWord_MouseMove);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.btnOk.Click += new EventHandler(btnOk_Click);
            this.txtUserName.GotFocus += new EventHandler(TxtUserName_GotFocus);
            this.txtUserName.LostFocus += new EventHandler(TxtUserName_LostFocus);
            this.txtOldPassWord.GotFocus += new EventHandler(TxtOldPassWord_GotFocus);
            this.txtOldPassWord.LostFocus += new EventHandler(TxtOldPassWord_LostFocus);
            this.txtNewPassWord.GotFocus += new EventHandler(TxtNewPassWord_GotFocus);
            this.txtNewPassWord.LostFocus += new EventHandler(TxtNewPassWord_LostFocus);
            this.txtNewPassWords.GotFocus += new EventHandler(TxtNewPassWords_GotFocus);
            this.txtNewPassWords.LostFocus += new EventHandler(TxtNewPassWords_LostFocus);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            this.Region = new System.Drawing.Region(GetRoundedRectPath(rect, 25));
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

        #region 修改密码事件
        private void btnOk_MouseMove(object sender, EventArgs e)
        {
            this.btnOk.Image = (Image)JHDock.Properties.Resources.修改密码点击态;
        }
        public void btnOk_MouseLeave(object sender, EventArgs e)
        {
            this.btnOk.Image = (Image)JHDock.Properties.Resources.修改密码常态;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            UpdatePassWord();
        }
        #endregion
        #region 关闭按钮事件
        private void btnCancel_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnCancel.Image = (Image)JHDock.Properties.Resources.关闭点击态;
        }
        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            this.btnCancel.Image = (Image)JHDock.Properties.Resources.关闭常态;
        }
        #endregion

        private void UpdatePassWord()
        {
            if (this.txtUserName.Text == "")
            {
                MessageBox.Show("用户名不能为空！");
                return;
            }
            else if (this.txtOldPassWord.Text == "")
            {
                MessageBox.Show("原密码不能为空！");
                return;
            }
            else if (this.txtNewPassWord.Text == "")
            {
                MessageBox.Show("请输入要修改的密码！");
                return;
            }
            else if (this.txtNewPassWords.Text == "")
            {
                MessageBox.Show("请输入确认密码！");
                return;
            }
            else if (this.txtOldPassWord.Text == this.txtNewPassWord.Text)
            {
                MessageBox.Show("原密码不能和新密码一致！");
                return;
            }
            else if (this.txtNewPassWord.Text != this.txtNewPassWords.Text)
            {
                MessageBox.Show("两次输入的密码不一致！");
            }

            else
            {
                UpdatePassWordModel updatePassWordModel = new UpdatePassWordModel();
                //updatePassWordModel.from_Sys = "EMR";
                updatePassWordModel.JHIPMsgVersion = "1.0.1";
                updatePassWordModel.code = this.txtUserName.Text;
                updatePassWordModel.passWord = this.txtOldPassWord.Text;
                updatePassWordModel.newPassWord = this.txtNewPassWords.Text;
                DataSet dataBackXml = null;
                string strUserLoginInfo = "";
                string strBackXml = "";
                string isUpdateSeuccess = "";
                //WebServiceUserInfo.JhipWebsvcClient jhipWeb = new WebServiceUserInfo.JhipWebsvcClient();
                JHIPSSO.SsoWebsvcClient jhipWeb = new JHIPSSO.SsoWebsvcClient();
                strUserLoginInfo = XmlSerializerHelper.Serializer<UpdatePassWordModel>(updatePassWordModel);
                strUserLoginInfo = strUserLoginInfo.Remove(0, "<?xml version=\"1.0\" encoding=\"utf-8\" ?> ".Length - 2);
                //strUserLoginInfo = strUserLoginInfo.Replace("JHIPMsgVersion=\"1.0.1\"", "JHIPMsgVersion='1.0.1'");
                strBackXml = jhipWeb.ModifyPassword(strUserLoginInfo);
                dataBackXml = XmlSerializerHelper.ConvertXMLToDataSet(strBackXml);
                isUpdateSeuccess = dataBackXml.Tables[0].Rows[0]["RESULT_SYSTEM_NO"].ToString().Trim();
                if (isUpdateSeuccess == "true")
                {
                    DialogResult result = MessageBox.Show("修改密码成功！", "友情提示");
                    this.Close();
                    #region 修改修改密码按钮位置以前的代码
                    //if (result == DialogResult.OK)
                    //{
                    //bool IsPass = true;
                    //IAuthentication userCheck = AuthenticationFactory.Create(false);
                    //string strResult = "";
                    //this.Close();
                    //IsPass = userCheck.VerifyUser(this.txtUserName.Text.Trim(), this.txtNewPassWord.Text.Trim(), out strResult);
                    //if (IsPass && string.IsNullOrEmpty(strResult))
                    //{
                    //    this.Close();
                    //    fromLogin.Close();
                    //    fromLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
                    //}
                    //else
                    //{
                    //    this.Close();
                    //    MessageBox.Show("登录失败,服务器正忙,请稍候再试！");
                    //}
                    //}
                    //else
                    //{
                    //    this.Close();
                    //}
                    #endregion
                }
                else
                {
                    MessageBox.Show("修改密码失败,服务器正忙,请稍候再试！");
                }

            }
        }

        /// <summary>
        /// 在帐号框中回车时，焦点到密码框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                UpdatePassWord();
            }
        }
        /// <summary>
        /// 在帐号框中回车时，焦点到密码框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtNewName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                UpdatePassWord();
            }
        }
        void txtNewNames_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                UpdatePassWord();
            }
        }
        /// <summary>
        /// 在窗体中回车时，焦点到密码框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                UpdatePassWord();
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
            path.CloseFigure();
            return path;
        }
        //窗体关闭事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserName_MouseDown(object sender, MouseEventArgs e)
        {
            WinAPI.HideCaret(((TextBox)sender).Handle);
        }
    }
}
