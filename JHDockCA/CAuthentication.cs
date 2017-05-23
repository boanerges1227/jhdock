using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JHDockInterface;

namespace JHDockCA
{
    public class CAuthentication : IAuthentication
    {
        private string jh_name;
        /// <summary>
        /// 用户帐号
        /// </summary>
        public string UserName
        {
            get
            {
                return jh_name;
            }
            set
            {
                jh_name = value;
            }
        }
        /// <summary>
        /// 密码
        /// </summary>
        private string jh_password;
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password
        {
            get
            {
                return jh_password;
            }
            set
            {
                jh_password = value;
            }
        }
        /// <summary>
        /// 用户身份验证
        /// </summary>
        /// <param name="name">帐号</param>
        /// <param name="password">密码</param>
        /// <param name="result">结果</param>
        /// <returns>是否成功</returns>
        public bool VerifyUser(string name, string password, bool isCA, out string result)
        {
            result = "";
            bool IsPass = false;
            try
            {

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return IsPass;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="name">帐号</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>是否成功</returns>
        public bool ChangePassword(string name, string oldPassword, string newPassword, out string result)
        {
            result = "";
            bool IsPass = false;
            try
            {

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return IsPass;
        }
    }
}
