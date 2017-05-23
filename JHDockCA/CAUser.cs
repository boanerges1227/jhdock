using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHDockCA
{
    public class CAUser
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 容器号
        /// </summary>
        public string ContainerName { get; set; }
        /// <summary>
        /// 身份证（KEY序列号）
        /// </summary>
        public string Identification { get; set; }
        /// <summary>
        /// 用户证书(用户工号唯一标识）
        /// </summary>
        public string UserCert { get; set; }
        /// <summary>
        /// 签名证书
        /// </summary>
        public string ChangeUserCert { get; set; }
        /// <summary>
        /// 客户端证书序列号
        /// </summary>
        public static string CertID;
        /// <summary>
        /// 证书有效期
        /// </summary>
        public string AvailableDate { get; set; }
        /// <summary>
        /// 证书颁发者
        /// </summary>
        public string Issuser { get; set; }
        /// <summary>
        /// 证书颁发时间
        /// </summary>
        public DateTime TICKET_VALIDATE { get; set; }
        /// <summary>
        /// 签名图片
        /// </summary>
        public string ImageB64 { get; set; }
    }
}
