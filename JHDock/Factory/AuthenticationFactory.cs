using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JHDockInterface;
using JHDockCA;

namespace JHDock
{
    public class AuthenticationFactory
    {
        /// <summary>
        /// 创建验证类
        /// </summary>
        /// <param name="IsCA">是否CA验证</param>
        /// <returns></returns>
        public static IAuthentication Create(bool IsCA)
        {
            IAuthentication userCheck = null;
            if (!IsCA)
            {
                userCheck = new DataAuthentication();
            }
            else
            {
                userCheck = new CAuthentication();
            }
            return userCheck;
        }
    }
}
