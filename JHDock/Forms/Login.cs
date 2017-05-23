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
using JHDock.Factory;

namespace JHDock
{
    public partial class frmLogin : Form
    {
        #region 全局变量声明开始

        Point mouseOff; //鼠标移动位置变量
        bool leftFlag; //标签是否为左键

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

            this.linklabUpdatePad.Click += new EventHandler(Linklab_Click);

            #endregion 界面事件声明结束

            //窗体初使化
            Init();
        }

        private void Linklab_Click(object sender, EventArgs e)
        {
            UpdatePassWord updatePassword = new UpdatePassWord();
            IWin32Window iw = new frmLogin();
            updatePassword.Show(iw);
        }

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
                btnOK.Image = (Image)JHDock.Properties.Resources.登录默认;
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
            btn.BackgroundImage = (Image)JHDock.Properties.Resources.取消默认;
        }
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCancel_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox btn = sender as PictureBox;
            btn.BackgroundImage = (Image)JHDock.Properties.Resources.取消触发;
        }

        #endregion

        #region 确定按钮事件
        /// <summary>
        /// 点击确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnOK_Click(object sender, EventArgs e)
        {
            ////陈
            //FormatXml formatXml = new FormatXml();
            //bool isSaveXmlsuccess=formatXml.SaveXml(this.txtName.Text, this.txtPassword.Text);
            //毕：分开数据库验证与CA验证
            IAuthentication userCheck = AuthenticationFactory.Create(false);
            string result = "";
            bool IsPass = true;
            try
            {
                //IsPass = userCheck.VerifyUser(this.txtName.Text.Trim(), this.txtPassword.Text.Trim(), out result);
                if (string.IsNullOrEmpty(result))
                {
                    if (!IsPass)
                    {
                        MessageBox.Show("用户名或密码不正确", "提示");
                    }
                    else
                    {
                        //formatXml.SetXmlStrToImageAndSave();
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
                else
                {
                    MessageBox.Show("验证用户时出错：" + result, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("登录时错误：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            btn.Image = (Image)JHDock.Properties.Resources.登录默认;
        }
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnOK_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox btn = sender as PictureBox;
            btn.Image = (Image)JHDock.Properties.Resources.登录触发;
        }

        #endregion

        #region private methods

        /// <summary>
        /// 验证帐号 
        /// </summary>
        private void CheckLogin()
        {
            string name = this.txtName.Text;
            string pass = this.txtPassword.Text;
        }
        #endregion
    }
}
