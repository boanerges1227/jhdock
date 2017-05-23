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
    public partial class FrmUserInfo : BaseForm
    {
        private static FrmUserInfo instance;
        public static FrmUserInfo Instance 
        {
            get {
                if (instance == null)
                {
                    instance = new FrmUserInfo();
                }
                return instance;
            }
        }

        private bool flag = false;
        Point mouseOff; //鼠标移动位置变量
        private FrmUserInfo()
        {
            InitializeComponent();
            this.btnUpdatePassWord.MouseMove += new MouseEventHandler(btnOk_MouseMove);
            this.btnUpdatePassWord.MouseLeave += new EventHandler(btnOk_MouseLeave);
            this.btnUpdatePassWord.Click += new EventHandler(btnOK_Click);
            this.MouseDown += new MouseEventHandler(FromUpdatePassWord_MouseDown);
            this.MouseUp += new MouseEventHandler(FromUpdatePassWord_MouseUp);
            this.MouseMove += new MouseEventHandler(FromUpdatePassWord_MouseMove);
            this.btnCancel.MouseMove += new MouseEventHandler(btnCancel_MouseMove);
            this.btnCancel.MouseLeave += new EventHandler(btnCancel_MouseLeave);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            //this.btnClose.Click += new EventHandler(btnClose_Click);
            //this.BackColor = Color.FromArgb(255, 31, 105, 156);
            //this.BackColor = Color.FromArgb(255, 13, 13, 13);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            this.Region = new System.Drawing.Region(GetRoundedRectPath(rect, 25));
            //加载用户信息
            SelectUserInfo();
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

        #region 关闭按钮事件
        private void btnCancel_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnCancel.Image = (Image)JHDock.Properties.Resources.关闭点击态;
        }
        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            this.btnCancel.Image = (Image)JHDock.Properties.Resources.关闭常态;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 修改密码按钮事件
        private void btnOk_MouseMove(object sender, EventArgs e)
        {
            this.btnUpdatePassWord.Image = (Image)JHDock.Properties.Resources.修改密码点击态;
        }
        public void btnOk_MouseLeave(object sender, EventArgs e)
        {
            this.btnUpdatePassWord.Image = (Image)JHDock.Properties.Resources.修改密码常态;
        }
        public void btnOK_Click(object sender, EventArgs e)
        {
            FrmUpdatePassWord updatePassword = FrmUpdatePassWord.Instance;
            updatePassword.Show();
            this.Close();
        }
        #endregion

        #region 加载用户信息
        /// <summary>
        /// 加载用户信息
        /// </summary>
        private void SelectUserInfo()
        {
            this.txtUserCode.Text = PublicVariableModel.userInfoModelList.UserInfoList[0].User_Code;
            this.txtUserName.Text = PublicVariableModel.userInfoModelList.UserInfoList[0].Name;
            this.txtUserLoginName.Text = PublicVariableModel.userInfoModelList.UserInfoList[0].User_Login_Name;
            this.txtDeptCode.Text = PublicVariableModel.userInfoModelList.UserInfoList[0].Dept_Code;
            this.txtDeptName.Text = PublicVariableModel.userInfoModelList.UserInfoList[0].Dept_Name;
        }
        #endregion
    }
}
