using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JHDock
{
    /// <summary>
    /// 结果内容
    /// </summary>
    /// [Serializable]
    [XmlType("REQUEST")]
    public class UpdatePassWordModel
    {
        private string jHIPMsgVersion;
        [XmlAttribute(AttributeName = "JHIPMsgVersion")]
        public string JHIPMsgVersion
        {
            get { return jHIPMsgVersion; }
            set { jHIPMsgVersion = value; }
        }
        ///// <summary>
        ///// 系统
        ///// </summary>
        //[XmlElement("FROM_SYS")]
        //public string from_Sys { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [XmlElement("USER_LOGIN_NAME")]
        public string code { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [XmlElement("PASSWORD")]
        public string passWord { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [XmlElement("NEW_PASSWORD")]
        public string newPassWord { get; set; }
    }
}
