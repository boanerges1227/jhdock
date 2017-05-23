using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace JHDock
{
    /// <summary>
    /// 读取XML
    /// </summary>
    public class XmlService
    {
        /// <summary>
        /// XML文件位置
        /// </summary>
        public string xmlPath { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        public XmlService()
        { }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="path">XML路径</param>
        public XmlService(string path)
        {
            this.xmlPath = path;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public List<IDockItem> findAllXml()
        {
            List<IDockItem> xmlList = new List<IDockItem>();
            XmlDocument doc = new XmlDocument();
            string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "ToolBarButtons.xml";
            string imgString = "";
            string imgPath = "";
            byte[] img = null;
            DataSet ds = XmlSerializerHelper.ConvertXMLFileToDataSet(xmlPath);
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        IDockItem xmlItm = new DefDockItem();
                        xmlItm.Name = row["Name"].ToString();
                        xmlItm.LockItem = Convert.ToBoolean(row["LockItem"].ToString());
                        xmlItm.Target = row["Target"].ToString();
                        xmlItm.Argument = row["Argument"].ToString();
                        xmlItm.StartIn = row["StartIn"].ToString();
                        imgString = row["Icon"].ToString();
                        imgPath = AppDomain.CurrentDomain.BaseDirectory + @"\Image\" + xmlItm.Name.Trim().ToUpper() + ".png";
                        xmlItm.IconPath = row["IconPath"].ToString();
                        //检测图片文件是否存在，不存在则创建
                        if (imgString.Length > 0 && File.Exists(imgPath))
                        {
                            //如果服务器没有传过图片字符串并且本地也没有可用图片则选择默认图片
                            if (imgString == "")
                            {
                                xmlItm.IconPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\Image\gougou.bmp";
                            }
                            else
                            {
                                img = Encoding.Unicode.GetBytes(imgString);
                                Common.SaveImage(imgPath, img);
                                xmlItm.Icon = null;
                            }
                        }
                        xmlList.Add(xmlItm);
                    }
                }
            }
            return xmlList;
        }

        public List<IDockItem> findAllXmlData()
        {
            List<IDockItem> xmlList = new List<IDockItem>();
            XmlDocument doc = new XmlDocument();
            string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "WebServiceXMLSysInfo.XML";
            string imgString = "";
            string imgPath = "";
            byte[] img = null;

            DataSet ds = XmlSerializerHelper.ConvertXMLFileToDataSet(xmlPath);
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.TableName == "SYSTEM_INFO")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                IDockItem xmlItm = new DefDockItem();
                                xmlItm.ID = row["SYSTEM_NO"].ToString();
                                xmlItm.Name = row["REGIST_NAME"].ToString();
                                xmlItm.LocTarget = row["LOC_RUN_PROGRAM"].ToString();
                                xmlItm.Target = row["RUN_PROGRAM"].ToString();
                                xmlItm.Argument = row["CONTENT"].ToString();
                                xmlItm.StartIn = row["RUN_FRAMEWORK"].ToString();
                                imgString = row["IMAGE_INFO"].ToString();
                                imgPath = Application.StartupPath+@"\Image\" + xmlItm.Name.Trim() + ".png".ToUpper();
                                xmlItm.IconPath = imgPath;
                                //检测图片文件是否存在，不存在则创建
                                if (!File.Exists(imgPath))
                                {
                                    if (imgString.Length > 0)
                                    {
                                        //毕
                                        //陈宝栋 2014-1-9 修改
                                        img = Convert.FromBase64String(imgString);
                                        Common.SaveImage(imgPath, img);
                                        xmlItm.Icon = null;
                                    }
                                }
                                xmlList.Add(xmlItm);
                            }
                        }
                    }
                }
            }
            return xmlList;
        }

    }

}
