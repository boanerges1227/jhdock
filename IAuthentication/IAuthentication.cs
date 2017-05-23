using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHDockInterface
{
    public interface IAuthentication
    {
        /// <summary>
        /// 帐号
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        string Password { get; set; }
        /// <summary>
        /// 用户身份验证
        /// </summary>
        /// <param name="name">帐号</param>
        /// <param name="password">密码</param>
        /// <param name="password">是否启用CA</param>
        /// <param name="result">结果</param>
        /// <returns>是否成功</returns>
        bool VerifyUser(string name, string password,bool isCA, out string result);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="name">帐号</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>是否成功</returns>
        bool ChangePassword(string name, string oldPassword, string newPassword, out string result);
    }
}
