using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JHDock
{
    [Serializable]
    [XmlType("RESPONSE")]
    public class PostUserInfoXmlModelList
    {
        private List<string> from_Sys = new List<string>();
        private List<string> row_Key = new List<string>();
        private List<PostUserInfoXmlModel> postUserInfoXmlModelList = new List<PostUserInfoXmlModel>();
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
        [XmlElement("FROM_SYS")]
        public List<string> From_Sys
        {
            get { return from_Sys; }
            set { from_Sys = value; }
        }
        [XmlElement("ROW_KEY")]
        public List<string> Row_Key
        {
            get { return row_Key; }
            set { row_Key = value; }
        }
        /// <summary>
        /// 结果内容
        /// </summary>
        [XmlElement("SYSTEM_INFOS")]
        public List<PostUserInfoXmlModel> PostUserInfoXmlModelListA
        {
            get { return postUserInfoXmlModelList; }
            set { postUserInfoXmlModelList = value; }
        }
    }
    /// <summary>
    /// 与用户有关系的系统列表
    /// </summary>
    [XmlType("SYSTEM_INFOS")]
    public class PostUserInfoXmlModel
    {
        List<PostUserInfoModel> row_KeyList = new List<PostUserInfoModel>();
        /// <summary>
        /// 系统信息
        /// </summary>
        [XmlElement("SYSTEM_INFO")]
        public List<PostUserInfoModel> PostUserInfoModelList
        {
            get { return row_KeyList; }
            set { row_KeyList = value; }
        }
    }
    /// <summary>
    /// 系统信息
    /// </summary>
    [XmlType("SYSTEM_INFO")]
    public class PostUserInfoModel
    {
        /// <summary>
        /// 系统信息唯一标识
        /// </summary>
        [XmlElement("ROW_KEY")]
        public string Row_Key { get; set; }
    }
}
