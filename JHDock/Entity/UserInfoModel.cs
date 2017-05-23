using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;

namespace JHDock
{
    //陈宝栋 2014-01-01 创建
    /// <summary>
    /// 格式化Xml实体类集合(用户信息)
    /// </summary>
    [Serializable]
    [XmlType("RESPONSE")]
    public class UserInfoModelList
    {
        private List<string> systemInfoList = new List<string>();
        private List<UserInfoModel> userInfoList = new List<UserInfoModel>();
        private string jHIPMsgVersion;
        /// <summary>
        /// 格式化XML根节点属性
        /// </summary>
        [XmlAttribute(AttributeName = "JHIPMsgVersion")]
        public string JHIPMsgVersion
        {
            get { return jHIPMsgVersion; }
            set { jHIPMsgVersion = value; }
        }
        /// <summary>
        /// 结果编码
        /// </summary>
        [XmlElement("RESULT_SYSTEM_NO")]
        public List<string> SystemInfoList
        {
            get { return systemInfoList; }
            set { systemInfoList = value; }
        }
        /// <summary>            
        /// 结果内容
        /// </summary>
        [XmlElement("RESULT_CONTENT")]
        public List<UserInfoModel> UserInfoList
        {
            get { return userInfoList; }
            set { userInfoList = value; }
        }
    }
    /// <summary>
    /// 结果内容
    /// </summary>
    [XmlType("RESULT_CONTENT")]
    public class UserInfoModel
    {
        /// <summary>
        /// 行号
        /// </summary>
        [XmlElement("ROW_KEY")]
        public string Row_Key { get; set; }
        /// <summary>
        /// 登陆名
        /// </summary>
        [XmlElement("USER_LOGIN_NAME")]
        public string User_Login_Name { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        [XmlElement("USER_CODE")]
        public string User_Code { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [XmlElement("USER_NAME")]
        public string Name { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        [XmlElement("DEPT_CODE")]
        public string Dept_Code { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [XmlElement("DEPT_NAME")]
        public string Dept_Name { get; set; }
        /// <summary>
        /// SessionID
        /// 20160418李斌增加
        /// </summary>
        [XmlElement("JHSESSEIONID")]
        public string SessionID { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        [XmlElement("LOGIN_TIME")]
        public string Login_Time { get; set; }
        /// <summary>
        /// 与用户有关系的系统列表
        /// </summary>
        List<SysInfoList> row_KeyList = new List<SysInfoList>();
        [XmlElement("SYSTEM_INFOS")]
        public List<SysInfoList> System_InfosModelList
        {
            get { return row_KeyList; }
            set { row_KeyList = value; }
        }
    }
    /// <summary>
    /// 与用户有关系的系统列表
    /// </summary>
    [XmlType("SYSTEM_INFOS")]
    public class SysInfoList
    {
        List<System_InfosModel> row_KeyList = new List<System_InfosModel>();
        /// <summary>
        /// 系统信息
        /// </summary>
        [XmlElement("SYSTEM_INFO")]
        public List<System_InfosModel> SysInfosModelList
        {
            get { return row_KeyList; }
            set { row_KeyList = value; }
        }
    }
    /// <summary>
    /// 系统信息
    /// </summary>
    [XmlType("SYSTEM_INFO")]
    public class System_InfosModel
    {
        /// <summary>
        /// 系统信息唯一标识
        /// </summary>
        [XmlElement("SYSTEM_NO")]
        public string Row_Key { get; set; }
        [XmlElement("LAST_DATE")]
        public string Last_Date { get; set; }
    }
}
