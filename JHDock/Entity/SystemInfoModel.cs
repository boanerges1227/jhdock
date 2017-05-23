using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JHDock
{
    //陈宝栋 2014-01-03 创建
    /// <summary>
    /// 格式化Xml实体类集合(系统信息)
    /// </summary>
    [XmlType("RESPONSE")]
    public class SystemInfoModelList
    {
        private List<string> result_System_NoList = new List<string>();
        private List<SystemInfoModelSonList> systemInfoModelSonList = new List<SystemInfoModelSonList>();
        private string jHIPMsgVersion;
        ///// <summary>
        ///// 格式化XML根节点属性
        ///// </summary>
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
        public List<string> Result_System_NoList
        {
            get { return result_System_NoList; }
            set { result_System_NoList = value; }
        }
        [XmlElement("RESULT_CONTENT")]
        public List<SystemInfoModelSonList> SysInfoModelSonList
        {
            get { return systemInfoModelSonList; }
            set { systemInfoModelSonList = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        [XmlElement("CREATE_DATE ")]
        public string Create_Date { get; set; }

    }
    /// <summary>
    /// 结果描述
    /// </summary>
    [XmlType("RESULT_CONTENT")]
    public class SystemInfoModelSonList
    {
        private List<SystemInfoModelGrSonList> systemInfoModelsonList = new List<SystemInfoModelGrSonList>();
        [XmlElement("SYSTEM_INFOS")]
        public List<SystemInfoModelGrSonList> SystemInfoModeSonlList
        {
            get { return systemInfoModelsonList; }
            set { systemInfoModelsonList = value; }
        }
    }
    /// <summary>
    /// 系统信息集合
    /// </summary>
    [XmlType("SYSTEM_INFOS")]
    public class SystemInfoModelGrSonList
    {
        private List<SystemInfoModel> systemInfoModelList = new List<SystemInfoModel>();
        [XmlElement("SYSTEM_INFO")]
        public List<SystemInfoModel> SystemInfoModelList
        {
            get { return systemInfoModelList; }
            set { systemInfoModelList = value; }
        }

    }
    /// <summary>
    /// 系统信息
    /// </summary>
    [XmlType("SYSTEM_INFO")]
    public class SystemInfoModel
    {
        /// <summary>
        /// 系统信息唯一标识
        /// </summary>
        /// 
        [XmlElement("SYSTEM_NO")]
        public string Row_Key {get;set; }
        /// <summary>
        /// 系统图标
        /// </summary>
        [XmlElement("IMAGE_INFO")]
        public string Image_Info { get; set; }
        /// <summary>
        /// 系统运行程序目录路径
        /// </summary>
        [XmlElement("RUN_PROGRAM")]
        public string Run_Progame { get; set; }
        /// <summary>
        /// 系统运行架构类型
        /// </summary>
        [XmlElement("RUN_FRAMEWORK")]
        public string Run_FrameWork { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        [XmlElement("REGIST_NAME")]
        public string Regist_Name { get; set; }
        /// <summary>
        /// 系统编码
        /// </summary>
        [XmlElement("SYSTEM_CODE")]
        public string System_No { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [XmlElement("CREATE_DATE")]
        public string Create_date { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [XmlElement("CONTENT")]
        public string Content { get; set; }
    }
}
