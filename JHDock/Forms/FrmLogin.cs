using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using JHDockInterface;
using JHDockCA;
using System.Configuration;
using System.Diagnostics;
using System.Xml;

namespace JHDock
{
    public partial class frmLogin :Form
    {
        #region 全局变量声明开始
        Point mouseOff; //鼠标移动位置变量
        bool leftFlag; //标签是否为左键
        bool isCAValidate = false;
        InterfaceCA objCA = null;
        bool isHaveCA = false;
        bool isStop = false;
        bool isFirstReadCA = true;
        string strCAName = "";
        string IsShowbtnCA = "";
        #endregion 全局变量声明结束
        /// <summary>
        /// 构造
        /// </summary>
        public frmLogin()
        {
            InitializeComponent();
            #region 界面事件声明开始

            this.txtName.MouseEnter += new EventHandler(txtName_MouseEnter);
            this.txtName.GotFocus += new EventHandler(txtName_GotFocus);
            this.txtName.LostFocus += new EventHandler(txtName_LostFocus);
            this.txtName.KeyPress += new KeyPressEventHandler(txtName_KeyPress);

            this.txtPassword.MouseEnter += new EventHandler(txtPassword_MouseEnter);
            this.txtPassword.GotFocus += new EventHandler(txtPassword_GotFocus);
            this.txtPassword.LostFocus += new EventHandler(txtPassword_LostFocus);
            this.txtPassword.KeyPress += new KeyPressEventHandler(txtPassword_KeyPress);

            this.MouseDown += new MouseEventHandler(frmLogin_MouseDown);
            this.MouseMove += new MouseEventHandler(frmLogin_MouseMove);
            this.MouseUp += new MouseEventHandler(frmLogin_MouseUp);

            this.btnOK.MouseMove += new MouseEventHandler(btnOK_MouseDown);
            this.btnOK.MouseLeave += new EventHandler(btnOK_MouseLeave);
            this.btnOK.Click += new EventHandler(btnOK_Click);

            this.btnCancel.MouseMove += new MouseEventHandler(btnCancel_MouseDown);
            this.btnCancel.MouseLeave += new EventHandler(btnCancel_MouseLeave);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);

            this.btnCA.MouseMove += new MouseEventHandler(btnCA_MouseDown);
            this.btnCA.MouseLeave += new EventHandler(btnCA_MouseLeave);
            this.btnCA.Click += new EventHandler(btnCA_Click);

            this.linklabUpdatePad.Click += new EventHandler(Linklab_Click);

            #endregion 界面事件声明结束

            //窗体初使化
            Init();
            //陈宝栋修改 2014-02-26
            //初始化服务器图片
            string strMessage = FormatXml.InitPic();
            if (strMessage != "true")
            {
                MessageBox.Show(strMessage);
            }
            //如果插入key,则将客户端证书序列号直接作为用户名
            //this.txtName.Text = CAUser.CertID;
            isHaveCA = HaveCAMethod();
            IsShowbtnCA = ConfigurationManager.ConnectionStrings["IsShowbtnCA"].ConnectionString;
            if (IsShowbtnCA == "1")
            {
                this.btnCA.Visible = true;
            }
            //荆门二院的登录窗体是个性化的图片
            if (ConfigurationManager.ConnectionStrings["logoName"].ConnectionString == "JMEYY_LOGO")
            {
                this.pictureBox1.BackgroundImage = Properties.Resources.JMEYY_LOGO;
                this.pictureBox4.Visible = Convert.ToBoolean(ConfigurationManager.ConnectionStrings["leftFlag"].ConnectionString);
                this.pictureBox2.Visible = Convert.ToBoolean(ConfigurationManager.ConnectionStrings["rightFlag"].ConnectionString);
            }
            //黄石的登录窗体是个性化的图片
            if (ConfigurationManager.ConnectionStrings["logoName"].ConnectionString == "HBHS_LOGO")
            {
                this.pictureBox1.BackgroundImage = Properties.Resources.logo;
                this.pictureBox4.BackgroundImage = Properties.Resources.文字部分;
                this.pictureBox2.Visible = true;
            }
            //滁州的登录窗体是个性化的图片
            if (ConfigurationManager.ConnectionStrings["logoName"].ConnectionString == "CZEYY_LOGO")
            {
                this.pictureBox1.BackgroundImage = Properties.Resources.CZEYY_LOGO;
                this.pictureBox4.Visible = Convert.ToBoolean(ConfigurationManager.ConnectionStrings["leftFlag"].ConnectionString);
                this.pictureBox2.Visible = Convert.ToBoolean(ConfigurationManager.ConnectionStrings["rightFlag"].ConnectionString);
            }
            //和田地区人民的登录窗体是个性化的图片
            if (ConfigurationManager.ConnectionStrings["logoName"].ConnectionString == "HTRMYY_LOGO")
            {
                this.pictureBox1.BackgroundImage = Properties.Resources.HTRMYY;
                this.pictureBox4.Visible = Convert.ToBoolean(ConfigurationManager.ConnectionStrings["leftFlag"].ConnectionString);
                this.pictureBox2.Visible = Convert.ToBoolean(ConfigurationManager.ConnectionStrings["rightFlag"].ConnectionString);
            }
            //琼海人民的登录窗体是个性化的图片
            if (ConfigurationManager.ConnectionStrings["logoName"].ConnectionString == "QHRMYY_LOGO")
            {
                this.pictureBox1.BackgroundImage = Properties.Resources.QHRMYY_LOGO;
                this.pictureBox4.Visible = Convert.ToBoolean(ConfigurationManager.ConnectionStrings["leftFlag"].ConnectionString);
                this.pictureBox2.Visible = Convert.ToBoolean(ConfigurationManager.ConnectionStrings["rightFlag"].ConnectionString);
            }
        }

        #region 修改密码事件
        private void Linklab_Click(object sender, EventArgs e)
        {
            FrmUpdatePassWord updatePassword = FrmUpdatePassWord.Instance;
            updatePassword.Show();
        }
        #endregion

        #region 绘制窗体开始

        /// <summary>
        /// 初使化
        /// </summary>
        void Init()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackgroundImage = Properties.Resources.底图;
            this.Width = 685;
            this.Height = 237;
            this.Region = null;
            //绘制窗体
            GetRoundedRectPath();
        }
        /// <summary>
        /// 绘制窗体
        /// </summary>
        private void GetRoundedRectPath()
        {
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            Rectangle arcRect = new Rectangle(rect.Location, new Size(25, 25));
            GraphicsPath path = new GraphicsPath();
            // 左上角
            path.AddArc(arcRect, 180, 90);
            // 右上角
            arcRect.X = rect.Right - 25;
            path.AddArc(arcRect, 270, 90);
            // 右下角
            arcRect.Y = rect.Bottom - 25;
            path.AddArc(arcRect, 0, 90);
            //左下斜线
            path.AddLine(103, 236, 0, 146);
            path.CloseFigure();
            this.Region = new Region(path);
        }

        #endregion 绘制窗体结束

        #region 文本框事件

        void txtName_MouseEnter(object sender, EventArgs e)
        {
            this.txtName.SelectAll();
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
                this.txtPassword.Focus();
                this.txtPassword.SelectAll();
            }
        }
        /// <summary>
        /// 当帐号框获得焦点时，更换背景图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtName_GotFocus(object sender, EventArgs e)
        {
            this.picName.Image = (Image)JHDock.Properties.Resources.user1触发;
            this.picNameTxt.Image = (Image)JHDock.Properties.Resources.输入框触发;
            this.txtName.SelectAll();
        }
        /// <summary>
        /// 当帐号框失去焦点时，更换背景图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtName_LostFocus(object sender, EventArgs e)
        {
            this.picName.Image = (Image)JHDock.Properties.Resources.user1;
            this.picNameTxt.Image = (Image)JHDock.Properties.Resources.输入框默认_2;
        }

        void txtPassword_MouseEnter(object sender, EventArgs e)
        {
            this.txtPassword.SelectAll();
        }
        /// <summary>
        /// 当焦点在密码框时，触发回车键，实现点击确认按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnOK.Image = (Image)JHDock.Properties.Resources.登录常态;
                btnOK_Click(sender, e);
            }
        }
        /// <summary>
        /// 密码框获得焦点时，更换背景图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtPassword_GotFocus(object sender, EventArgs e)
        {
            this.picPassword.Image = (Image)JHDock.Properties.Resources.锁2触发;
            this.picPasswordTxt.Image = (Image)JHDock.Properties.Resources.输入框触发;
            this.txtPassword.SelectAll();
        }
        /// <summary>
        /// 密码框推动焦点时，更换背景图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtPassword_LostFocus(object sender, EventArgs e)
        {
            this.picPassword.Image = (Image)JHDock.Properties.Resources.锁2;
            this.picPasswordTxt.Image = (Image)JHDock.Properties.Resources.输入框默认_2;
        }

        #endregion

        #region 窗体移动

        void frmLogin_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false; 
            }
        }

        void frmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y); //设置移动后的位置
                Location = mouseSet;
            }
        }

        void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true; //点击左键按下时标注为true; 
            }
        }

        #endregion

        #region 取消按钮事件
        /// <summary>
        /// 点击取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 鼠标离开取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            PictureBox btn = sender as PictureBox;
            btn.BackgroundImage = (Image)JHDock.Properties.Resources.取消常态;
        }
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCancel_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox btn = sender as PictureBox;
            btn.BackgroundImage = (Image)JHDock.Properties.Resources.取消点击态;
        }

        #endregion

        #region 确定按钮事件
        /// <summary>
        /// 点击确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnOK_Click(object sender, EventArgs e)
        {
            //陈
            //FormatXml formatXml = new FormatXml();
            //bool isSaveXmlsuccess=formatXml.SaveXml(this.txtName.Text, this.txtPassword.Text);
            //毕：分开数据库验证与CA验证
            IAuthentication userCheck = AuthenticationFactory.Create(false);
            string result = "";
            bool IsPass = true;

           
            try
            {
                if (this.txtPassword.Text == "")
                {
                    MessageBox.Show("密码不能为空！");
                    return;
                }
                //增加CA验证功能
                if (isHaveCA && IsShowbtnCA == "0")
                {

                    if (objCA == null)
                    {
                        GetCA();
                    }
                    isCAValidate = objCA.ReadKey();
                    if (isCAValidate)
                    {
                        if (objCA.UserLogin(2, this.txtPassword.Text))
                        {
                            if (!IsPass)
                            {
                                MessageBox.Show(result);
                                return;
                            }
                            //PublicVariableModel.userLoginName = this.txtName.Text.Trim();
                            DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                    }
                    else
                    {
                        IsPass = userCheck.VerifyUser(this.txtName.Text.Trim(), this.txtPassword.Text.Trim(), isCAValidate, out result);
                        if (!IsPass)
                        {
                            MessageBox.Show(result, "提示");
                        }
                        else
                        {
                            if (result == "")
                            {
                                //PublicVariableModel.userLoginName = this.txtName.Text.Trim();
                                DialogResult = System.Windows.Forms.DialogResult.OK;
                            }
                            else
                            {
                                MessageBox.Show(result);
                            }
                        }
                    }
                }
                else
                {
                    //IsPass = userCheck.VerifyUser(this.txtName.Text.Trim(), this.txtPassword.Text.Trim(), isHaveCA, out result);
                    IsPass = userCheck.VerifyUser(this.txtName.Text.Trim(), this.txtPassword.Text.Trim(), false, out result);
                    if (!IsPass)
                    {
                        MessageBox.Show(result, "提示");
                    }
                    else
                    {
                        if (result == "")
                        {
                            PublicVariableModel.userLoginName = this.txtName.Text.Trim();
                            DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("登录时错误：" + ex.Message, "提示");
            }
        }
       
        /// <summary>
        /// 鼠标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnOK_MouseLeave(object sender, EventArgs e)
        {
            PictureBox btn = sender as PictureBox;
            btn.Image = (Image)JHDock.Properties.Resources.登录常态;
        }
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnOK_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox btn = sender as PictureBox;
            btn.Image = (Image)JHDock.Properties.Resources.登录点击态;
        }

        #endregion

        #region CA登录按钮事件
        public void btnCA_Click(object sender, EventArgs e)
        {
            GetKeyInfo(sender, e);
        }

        void btnCA_MouseLeave(object sender, EventArgs e)
        {
            PictureBox btn = sender as PictureBox;
            btn.BackgroundImage = (Image)JHDock.Properties.Resources.CA登录常态;
        }
        void btnCA_MouseDown(object sender, EventArgs e)
        {
            PictureBox btn = sender as PictureBox;
            btn.BackgroundImage = (Image)JHDock.Properties.Resources.CA登录点击态;
        }
        #endregion

        #region timer1事件
        /// <summary>
        /// 实时检测是否插入key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (isHaveCA)
                {
                    if (objCA == null)
                    {
                        GetCA();
                    }
                    if (isFirstReadCA)
                    {
                        isCAValidate = objCA.ReadKey();
                        //如果插入key,则将客户端证书序列号直接作为用户名
                        if (isCAValidate)
                        {
                            //this.txtName.Text = CAUser.CertID;
                            IAuthentication userCheck = AuthenticationFactory.Create(false);
                            string result = "";
                            bool IsPass = userCheck.VerifyUser(CAUser.CertID, this.txtPassword.Text.Trim(), isCAValidate, out result);
                            this.txtName.Text = PublicVariableModel.userLoginName;
                            isFirstReadCA = false;
                            if (result != "")
                            {
                                MessageBox.Show(result);
                            }
                            this.txtName.Enabled = false;
                        }
                    }
                    //if (this.txtName.Text == "" || this.txtName.Text != CAUser.CertID)
                    //{
                    //    isFirstReadCA = true;
                    //}

                }
            }
            catch (Exception ex)
            {
                if (!isStop)
                {
                    isStop = true;
                    isHaveCA = false;
                    MessageBox.Show("检测CA异常，请检查是否安装CA客户端！");
                }
            }
        }
        #endregion      

        #region private methods
        /// <summary>
        /// 判断启用哪个CA
        /// </summary>
        /// <returns></returns>
        private InterfaceCA GetCA()
        {
            strCAName = ConfigurationManager.ConnectionStrings["CAName"].ConnectionString;
            switch (strCAName)
            {
                case "GDCA": objCA = new GDCA();
                    break;
                case "HBCA": objCA = new HBCA();
                    break;
            }
            return objCA;
        }
        /// <summary>
        /// 验证帐号 
        /// </summary>
        private void CheckLogin()
        {
            string name = this.txtName.Text;
            string pass = this.txtPassword.Text;
        }
        /// <summary>
        /// 判断是否启用CA
        /// 返回值：true-启用 false-关闭
        /// </summary>
        private bool HaveCAMethod()
        {
            string strHaveCA = PublicVariableModel.isUserCA;
            if (strHaveCA.ToUpper() == "TRUE")
                return true;
            else
                return false;
        }    
        #endregion

        #region 湖北CA methods

        #region GetKeyInfo备份 151215测试内存报错前备份
        //public void GetKeyInfo(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        objCA = GetCA();
        //        isCAValidate = objCA.ReadKey();
        //        //如果插入key,则将客户端证书序列号直接作为用户名
        //        if (isCAValidate)
        //        {
        //            string result = "";
        //            string webstr = "";

        //            HBCA hbca = objCA as HBCA;
        //            CAUser causer = hbca.getUserInfo();
        //            //this.txtName.Text = CAUser.CertID;
        //            IAuthentication userCheck = AuthenticationFactory.Create(false);
        //            //userCheck.VerifyUser("9603", "", true, out  result);
        //            //DockWindow ds = new DockWindow();
        //            //ds.ShowDialog();
        //            hbca.Dispose();

        //            #region Ensemble接口绑定CA用户
        //            string Name = causer.Name;
        //            string certId = causer.Identification;
        //            string UserId = causer.UserCert;
        //            DateTime ValidateDate = causer.TICKET_VALIDATE;
        //            byte[] csbt = Convert.FromBase64String(causer.ImageB64);
        //            SendCAInfo.SendCAInfoSoapClient client = new SendCAInfo.SendCAInfoSoapClient();
        //            webstr = client.SendCAInfo(Name, certId, UserId, ValidateDate.ToString(), csbt);
        //            #endregion

        //            if (webstr == "OK")
        //            {
        //                //传参：序列号
        //                //bool IsPass = userCheck.VerifyUser(causer.Identification, this.txtPassword.Text.Trim(), isCAValidate, out result);
        //                //传参：工号
        //                bool IsPass = userCheck.VerifyUser(causer.UserCert, this.txtPassword.Text.Trim(), isCAValidate, out result);
        //                //this.txtName.Text = PublicVariableModel.userLoginName;   

        //                if (result != "")
        //                {
        //                    MessageBox.Show(result);
        //                    return;
        //                }
        //                else
        //                {
        //                    this.Size = new System.Drawing.Size(-90, -90);

        //                    if (!IsPass)
        //                    {
        //                        MessageBox.Show(result, "提示");
        //                    }
        //                    else
        //                    {
        //                        if (result == "")
        //                        {
        //                            //PublicVariableModel.userLoginName = this.txtName.Text.Trim();
        //                            DialogResult = System.Windows.Forms.DialogResult.OK;
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show(result);
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("Ensemble写入数据接口有误，请查找。");
        //            }
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!isStop)
        //        {
        //            isStop = true;
        //            isHaveCA = false;
        //            MessageBox.Show("检测CA异常，请检查是否安装CA客户端！");
        //        }
        //    }

        //}
        #endregion

        public void GetKeyInfo(object sender, EventArgs e)
        {
            try
            {
                objCA = GetCA();
                isCAValidate = objCA.ReadKey();
                //如果插入key,则将客户端证书序列号直接作为用户名
                if (isCAValidate)
                {
                    string result = "";
                    string webstr = "";

                    HBCA hbca = objCA as HBCA;
                    CAUser causer = hbca.getUserInfo();
                    //this.txtName.Text = CAUser.CertID;
                    IAuthentication userCheck = AuthenticationFactory.Create(false);

                    #region Ensemble接口绑定CA用户
                    string Name = causer.Name;
                    string certId = causer.Identification;
                    string UserId = causer.UserCert;
                    DateTime ValidateDate = causer.TICKET_VALIDATE;
                    byte[] csbt = Convert.FromBase64String(causer.ImageB64);
                    //SendCAInfo.SendCAInfoSoapClient client = new SendCAInfo.SendCAInfoSoapClient();
                    JHIPSSO.SsoWebsvcClient client = new JHIPSSO.SsoWebsvcClient();
                    //webstr = client.SendCAInfo(Name, certId, UserId, ValidateDate.ToString(), csbt);
                    webstr = "OK";
                    #endregion

                    if (webstr == "OK")
                    {
                        //传参：序列号
                        //bool IsPass = userCheck.VerifyUser(causer.Identification, this.txtPassword.Text.Trim(), isCAValidate, out result);
                        //传参：工号
                        bool IsPass = userCheck.VerifyUser(causer.UserCert, this.txtPassword.Text.Trim(), isCAValidate, out result);
                        //this.txtName.Text = PublicVariableModel.userLoginName;   

                        if (result != "")
                        {
                            MessageBox.Show(result);
                            return;
                        }
                        else
                        {
                            this.Size = new System.Drawing.Size(-90, -90);

                            if (!IsPass)
                            {
                                MessageBox.Show(result, "提示");
                            }
                            else
                            {
                                if (result == "")
                                {
                                    //PublicVariableModel.userLoginName = this.txtName.Text.Trim();
                                    DialogResult = System.Windows.Forms.DialogResult.OK;
                                }
                                else
                                {
                                    MessageBox.Show(result);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ensemble写入数据接口有误，请查找。");
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                if (!isStop)
                {
                    isStop = true;
                    isHaveCA = false;
                    MessageBox.Show("检测CA异常，请检查是否安装CA客户端！");
                }
            }
        }        
        #endregion
    }
}
