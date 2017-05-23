using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data;
using JHDockCA;
using System.Threading;

namespace JHDock
{
    /// <summary>
    /// 创建人：陈宝栋
    /// 创建时间：2014-01-01
    /// 功能：数据交互业务类
    /// </summary>
    public class FormatXml
    {
        static DataSet dtPicFile = null;
        /// <summary>
        /// 格式化XML字符串并且保存
        /// </summary>
        /// <param name="strXml">要格式化的字符串</param>
        /// <param name="fileName">文件保存的路径</param>
        public static void SetStrFormatXML<T>(T ObjXmlDoc, string strXml, string fileName)
        {
            //解析XML字符串
            T objXmlDoc = XmlSerializerHelper.Deserializer<T>(strXml);
            //保存解析的对象
            XmlSerializerHelper.SerializerToFile<T>(objXmlDoc, fileName);
        }
        /// <summary>
        /// 将Xml格式化为Xml字符串
        /// </summary>
        /// <param name="strFrom_Sys">第一个节点值</param>
        /// <param name="strCode">第二个节点值</param>
        /// <param name="strPassWord">第三个节点值</param>
        /// <param name="strAtru">根节点属性值</param>
        /// <returns>返回Xml字符串</returns>
        public static string SetXMLformatStr(string strFrom_Sys, string strCode, string strPassWord, string strAtru)
        {
            LoginInfoModel loginInfoModel = new LoginInfoModel();
            //loginInfoModel.from_Sys = strFrom_Sys;
            loginInfoModel.code = strCode;
            loginInfoModel.passWord = strPassWord;
            loginInfoModel.JHIPMsgVersion = strAtru;
            string strUserInfoXml = XmlSerializerHelper.Serializer<LoginInfoModel>(loginInfoModel);
            strUserInfoXml = strUserInfoXml.Remove(0, "<?xml version=\"1.0\" encoding=\"utf-8\" ?> ".Length - 2);
            //strUserInfoXml = strUserInfoXml.Replace("JHIPMsgVersion=\"1.0.1\"", "JHIPMsgVersion='1.0.1'");
            return strUserInfoXml;
        }
        /// <summary>
        /// 生成CAWeb服务字符串
        /// </summary>
        /// <param name="strCode">CA编号</param>
        /// <returns></returns>
        public static string SetXMLformatStr(string strCode)
        {
            string strCAXml = @"<REQUEST JHIPMsgVersion='1.0.1'>" +
                                "<CA_CODE>" + strCode + "</CA_CODE>" +
                                "</REQUEST>";
            return strCAXml;
        }

        /// <summary>
        /// 比较两个List集合
        /// </summary>
        /// <param name="strXmlFir">要比较的List集合</param>
        /// <param name="strXmlSec">要比较的List集合</param>
        /// <returns>返回是否相等</returns>
        private bool CompareList(List<System_InfosModel> list1, List<System_InfosModel> list2, ref List<string> serverList, ref List<string> loctionList)
        {
            bool isEqual = true;
            List<string> listString1 = new List<string>();
            List<string> listString2 = new List<string>();
            if (list1.Count != list2.Count)
            {
                for (int i = 0; i < list1.Count; i++)
                {
                    listString1.Add(list1[i].Row_Key + "|" + list1[i].Last_Date);
                }
                for (int i = 0; i < list2.Count; i++)
                {
                    listString2.Add(list2[i].Row_Key + "|" + list2[i].Last_Date);
                }
                serverList = listString1;
                loctionList = listString2;
                return false;
            }
            for (int i = 0; i < list1.Count; i++)
            {
                listString1.Add(list1[i].Row_Key + "|" + list1[i].Last_Date);
                listString2.Add(list2[i].Row_Key + "|" + list2[i].Last_Date);
            }
            for (int j = 0; j < listString1.Count; j++)
            {
                if (!listString1.Contains(listString2[j]))
                {
                    isEqual = false;
                }
            }
            serverList = listString1;
            loctionList = listString2;
            return isEqual;
        }
        /// <summary>
        /// 初始化数据或更新数据
        /// </summary>
        public static string InitPic()
        {
            //保存Xml文件的路径
            string xmlPicFileName = Application.StartupPath + "\\WebServiceXMLPicInfo.XML";
            //WebServiceUserInfo.JhipWebsvcClient jhipWebsvsClient = new WebServiceUserInfo.JhipWebsvcClient();
            JHIPSSO.SsoWebsvcClient jhipWebsvsClient = new JHIPSSO.SsoWebsvcClient();
            string isInitalize = "";
            string strXmlSystem = "";
            StringBuilder stBReWebService = new StringBuilder();
            stBReWebService.Append("<REQUEST JHIPMsgVersion=\"1.0.1\">");
            string strReturn = "";
            DataSet dtSysLoction = new DataSet();
            DataSet dtSysServer = new DataSet();
            try
            {
                //首次登录
                if (!File.Exists(xmlPicFileName))
                {
                    isInitalize = "1";
                    stBReWebService.Append("<ISINITALIZE>" + isInitalize + "</ISINITALIZE><SYSTEM_INFOS><SYSTEM_INFO/></SYSTEM_INFOS></REQUEST>");
                    strXmlSystem = jhipWebsvsClient.LoadAllSystem(stBReWebService.ToString());
                    dtSysLoction = XmlSerializerHelper.ConvertXMLToDataSet(strXmlSystem);
                    //dtSysLoction.Tables[3].Columns.Add("LOC_RUN_PROGRAM", typeof(string));
                    //for (int i = 0; i < dtSysLoction.Tables[3].Rows.Count; i++)
                    //{
                    //    dtSysLoction.Tables[3].Rows[i]["LOC_RUN_PROGRAM"] = "";
                    //}
                    PublicVariableModel.isUserCA = dtSysLoction.Tables[0].Rows[0]["USE_CA"].ToString();
                    XmlSerializerHelper.ConvertDataSetToXMLFile(dtSysLoction, xmlPicFileName);
                }
                else
                {
                    isInitalize = "0";
                    dtSysLoction = XmlSerializerHelper.ConvertXMLFileToDataSet(xmlPicFileName);
                    stBReWebService.Append("<ISINITALIZE>" + isInitalize + "</ISINITALIZE><SYSTEM_INFOS>");
                    for (int i = 0; i < dtSysLoction.Tables[3].Rows.Count; i++)
                    {
                        string strLastDate = dtSysLoction.Tables[3].Rows[i]["LAST_DATE"].ToString();
                        string strSysNo = dtSysLoction.Tables[3].Rows[i]["SYSTEM_NO"].ToString();
                        stBReWebService.Append("<SYSTEM_INFO><SYSTEM_NO>" + strSysNo + "</SYSTEM_NO><LAST_DATE>" + strLastDate + "</LAST_DATE></SYSTEM_INFO>");
                    }
                    //if (dtSysLoction.Tables[5] != null)
                    //{
                    //    for (int j = 0; j < dtSysLoction.Tables[5].Rows.Count; j++)
                    //    {
                    //        string strLastDate = dtSysLoction.Tables[5].Rows[j]["LAST_DATE"].ToString();
                    //        string strSysNo = dtSysLoction.Tables[5].Rows[j]["SYSTEM_NO"].ToString();
                    //        stBReWebService.Append("<SYSTEM_INFO><SYSTEM_NO>" + strSysNo + "</SYSTEM_NO><LAST_DATE>" + strLastDate + "</LAST_DATE></SYSTEM_INFO>");
                    //    }
                    //}
                    stBReWebService.Append("</SYSTEM_INFOS></REQUEST>");
                    //更新本地系统数据的web接口
                    strXmlSystem = jhipWebsvsClient.LoadAllSystem(stBReWebService.ToString());
                    dtSysServer = XmlSerializerHelper.ConvertXMLToDataSet(strXmlSystem);
                    PublicVariableModel.isUserCA = dtSysLoction.Tables[0].Rows[0]["USE_CA"].ToString();
                    if (dtSysServer.Tables.Count > 3)
                    {
                        //为本地Table设置主键
                        DataColumn[] clos = new DataColumn[1];
                        clos[0] = dtSysLoction.Tables[3].Columns["SYSTEM_NO"];
                        dtSysLoction.Tables[3].PrimaryKey = clos;
                        //dtSysServer.Tables[3].Columns.Add("LOC_RUN_PROGRAM", typeof(string));
                        for (int j = 0; j < dtSysServer.Tables[3].Rows.Count; j++)
                        {
                            //dtSysServer.Tables[3].Rows[j]["LOC_RUN_PROGRAM"] = "";
                            //如果存在先删除再添加
                            if (dtSysLoction.Tables[3].Rows.Contains(dtSysServer.Tables[3].Rows[j]["SYSTEM_NO"]))
                            {
                                DataRow dr = dtSysLoction.Tables[3].Rows.Find(dtSysServer.Tables[3].Rows[j]["SYSTEM_NO"]);
                                dtSysLoction.Tables[3].Rows.Remove(dr);
                            }
                            dtSysLoction.Tables[3].Rows.Add(dtSysServer.Tables[3].Rows[j].ItemArray);
                        }
                        XmlSerializerHelper.ConvertDataSetToXMLFile(dtSysLoction, xmlPicFileName);
                    }
                }
                strReturn = "true";
            }
            catch (Exception e)
            {
                strReturn = "初始化数据失败，请检查本地网络是否可用！";
                MessageBox.Show(e.Message);
            }
            return strReturn;
        }

        /// <summary>
        /// 获取指定用户的系统功能
        /// </summary>
        /// <param name="strXmlRowKey">请求的xml字符串</param>
        /// <returns>返回系统功能xml字符串</returns>
        private string GetSysFuntion(string strXmlRowKey)
        {
            string xmlPicFileName = Application.StartupPath + "\\WebServiceXMLPicInfo.XML";
            DataSet dtSysLoction = XmlSerializerHelper.ConvertXMLFileToDataSet(xmlPicFileName);
            DataSet dtSysOfUser = XmlSerializerHelper.ConvertXMLToDataSet(strXmlRowKey);
            StringBuilder strBXmlReturn = new StringBuilder();
            strBXmlReturn.Append("<RESPONSE JHIPMsgVersion=\"1.0.1\"><RESULT_SYSTEM_NO>true</RESULT_SYSTEM_NO><RESULT_CONTENT><SYSTEM_INFOS>");
            //为本地Table设置主键
            DataColumn[] clos = new DataColumn[1];
            clos[0] = dtSysOfUser.Tables[2].Columns["SYSTEM_NO"];
            dtSysOfUser.Tables[2].PrimaryKey = clos;
            for (int i = 0; i < dtSysLoction.Tables[3].Rows.Count; i++)
            {
                string strSysUp= dtSysLoction.Tables[3].Rows[i]["SYS_UP"].ToString();
                string strSysNo= dtSysLoction.Tables[3].Rows[i]["SYSTEM_NO"].ToString();
                if (dtSysOfUser.Tables[2].Rows.Contains(strSysNo) && strSysNo==strSysUp)
                {
                    string strImageInfo = dtSysLoction.Tables[3].Rows[i]["IMAGE_INFO"].ToString();
                    string strRunProgame = dtSysLoction.Tables[3].Rows[i]["RUN_PROGRAM"].ToString();
                    string strRunFrameWork = dtSysLoction.Tables[3].Rows[i]["RUN_FRAMEWORK"].ToString();
                    string strRegistName = dtSysLoction.Tables[3].Rows[i]["REGIST_NAME"].ToString();
                    string strSystemCode = dtSysLoction.Tables[3].Rows[i]["SYSTEM_CODE"].ToString();
                    string strContent = dtSysLoction.Tables[3].Rows[i]["CONTENT"].ToString();
                    //string strCreateDate = dtSysLoction.Tables[3].Rows[i]["CREATE_DATE"].ToString();
                    string strLastDate = dtSysLoction.Tables[3].Rows[i]["LAST_DATE"].ToString();
                    strBXmlReturn.Append("<SYSTEM_INFO><SYSTEM_NO>" + strSysNo + "</SYSTEM_NO>");
                    strBXmlReturn.Append("<IMAGE_INFO>" + strImageInfo + "</IMAGE_INFO>");
                    strBXmlReturn.Append("<RUN_PROGRAM>" + strRunProgame + "</RUN_PROGRAM>");
                    //添加本地路径
                    //strBXmlReturn.Append("<LOC_RUN_PROGRAM/>");
                    strBXmlReturn.Append("<RUN_FRAMEWORK>" + strRunFrameWork + "</RUN_FRAMEWORK>");
                    strBXmlReturn.Append("<REGIST_NAME>" + strRegistName + "</REGIST_NAME>");
                    strBXmlReturn.Append("<SYSTEM_CODE>" + strSystemCode + "</SYSTEM_CODE>");
                    strBXmlReturn.Append("<CONTENT>" + strContent + "</CONTENT>");
                    //strBXmlReturn.Append("<CREATE_DATE>" + strCreateDate + "</CREATE_DATE>");
                    strBXmlReturn.Append("<LAST_DATE>" + strLastDate + "</LAST_DATE>");
                    strBXmlReturn.Append("</SYSTEM_INFO>");
                }
            }
            strBXmlReturn.Append("</SYSTEM_INFOS></RESULT_CONTENT></RESPONSE>");
            return strBXmlReturn.ToString();
        }
        /// <summary>
        /// 用户名密码登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool LogIn(string userName, string password)
        {
            JHIPSSO.SsoWebsvcClient jhipWebsvsClient = new JHIPSSO.SsoWebsvcClient();
            bool result = false;
            string strFormatXml = SetXMLformatStr("JHIP", userName, password, "1.0.1");
            //调用身份验证，数据验证
            string strReformatXml = jhipWebsvsClient.LogIn(strFormatXml);
            PublicVariableModel.userInfoModelList = XmlSerializerHelper.Deserializer<UserInfoModelList>(strReformatXml);
            if (PublicVariableModel.userInfoModelList.SystemInfoList[0] == "true")
            {
                result = true;
            }
            return result;
        }

        #region 以前的登录方法 陈宝栋修改 2014-04-12
        /// <summary>
        /// 综合处理WebService交互的Xml
        /// </summary>
        /// <param name="txtName">用户名</param>
        /// <param name="txtPassword">密码</param>
        //public bool SaveXml(string txtName, string txtPassword, bool isCA, out string showInfo)
        //{
        //    bool result = false;
        //    try
        //    {
        //        //保存Xml文件的路径
        //        string xmlFileName = Application.StartupPath + "\\WebServiceXMLSysInfo.XML";
        //        string xmlUserFileName = Application.StartupPath + "\\WebServiceXMLUserInfo.XML";
        //        WebServiceUserInfo.JhipWebsvcClient jhipWebsvsClient = new WebServiceUserInfo.JhipWebsvcClient();
        //        UserInfoModelList userInfoModelList = new UserInfoModelList();
        //        SystemInfoModelList systemInfoModelList = new SystemInfoModelList();
        //        StringBuilder stBReWebService = new StringBuilder();
        //        //将登录信息转换成Xml格式的字符串
        //        string strFormatXml = "";
        //        string strReformatXml = "";
        //        if (isCA)
        //        {
        //            strFormatXml = SetXMLformatStr(txtName);
        //            //调用CA身份验证，数据验证
        //            strReformatXml = jhipWebsvsClient.CAlLogin(strFormatXml);
        //        }
        //        //添加CA验证判断 2014-02-21陈宝栋修改
        //        else
        //        {
        //            strFormatXml = SetXMLformatStr("JHIP", txtName, txtPassword, "1.0.1");
        //            //调用身份验证，数据验证
        //            strReformatXml = jhipWebsvsClient.LogIn(strFormatXml);
        //        }
        //        string strUserRowKey = "";
        //        userInfoModelList = XmlSerializerHelper.Deserializer<UserInfoModelList>(strReformatXml);
        //        //获取员工编号
        //        PublicVariableModel.userName = userInfoModelList.UserInfoList[0].User_Code;
        //        //用户名或密码不正确
        //        if (userInfoModelList.SystemInfoList[0] == "false")
        //        {
        //            showInfo = "用户名或密码不正确!";
        //        }
        //        else
        //        {
        //            List<System_InfosModel> sys_InfosModelList = userInfoModelList.UserInfoList[0].System_InfosModelList[0].SysInfosModelList;
        //            strUserRowKey = userInfoModelList.UserInfoList[0].User_Code;
        //            stBReWebService.Append("<REQUEST JHIPMsgVersion='1.0.1'><FROM_SYS>JHIP</FROM_SYS><ROW_KEY>" + strUserRowKey + "</ROW_KEY><SYSTEM_INFOS>");
        //            showInfo = "";
        //            //2014-1-6 毕兰 获取登录返回值
        //            //第一次登陆
        //            if (!File.Exists(xmlUserFileName))
        //            {
        //                XmlSerializerHelper.SerializerToFile<UserInfoModelList>(userInfoModelList, xmlUserFileName);
        //                PublicVariableModel.IsUpdateImage = "true";
        //                if (userInfoModelList.SystemInfoList[0] == "true")
        //                {
        //                    result = true;
        //                    strUserRowKey = userInfoModelList.UserInfoList[0].User_Code;
        //                    if (userInfoModelList.UserInfoList[0].System_InfosModelList.Count == 0)
        //                    {
        //                        showInfo = "该用户尚未设置权限！";
        //                    }
        //                    else
        //                    {
        //                        for (int i = 0; i < sys_InfosModelList.Count; i++)
        //                        {

        //                            stBReWebService.Append("<SYSTEM_INFO><SYSTEM_NO>" + sys_InfosModelList[i].Row_Key + "</SYSTEM_NO></SYSTEM_INFO>");
        //                        }
        //                        //拼接完成的WebService参数字符串
        //                        stBReWebService.Append("</SYSTEM_INFOS></REQUEST>");
        //                        string strSysTemp = GetSysFuntion(stBReWebService.ToString());
        //                        //陈宝栋修改 2014-1-21
        //                        DataSet ds = XmlSerializerHelper.ConvertXMLToDataSet(strSysTemp);
        //                        XmlSerializerHelper.ConvertDataSetToXMLFile(ds, xmlFileName);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                UserInfoModelList userInfoModelList1 = new UserInfoModelList();
        //                userInfoModelList1 = XmlSerializerHelper.DeserializerFromFile<UserInfoModelList>(xmlUserFileName);
        //                if (userInfoModelList.UserInfoList[0].User_Code != userInfoModelList1.UserInfoList[0].User_Code)
        //                {
        //                    XmlSerializerHelper.SerializerToFile<UserInfoModelList>(userInfoModelList, xmlUserFileName);
        //                    if (userInfoModelList.SystemInfoList[0] == "true")
        //                    {
        //                        result = true;
        //                        if (userInfoModelList.UserInfoList[0].System_InfosModelList.Count == 0)
        //                        {
        //                            showInfo = "该用户尚未设置权限！";
        //                        }
        //                        else
        //                        {
        //                            for (int i = 0; i < sys_InfosModelList.Count; i++)
        //                            {
        //                                stBReWebService.Append("<SYSTEM_INFO><SYSTEM_NO>" + sys_InfosModelList[i].Row_Key + "</SYSTEM_NO></SYSTEM_INFO>");
        //                            }
        //                            //拼接完成的WebService参数字符串
        //                            stBReWebService.Append("</SYSTEM_INFOS></REQUEST>");
        //                            string strSysTemp = GetSysFuntion(stBReWebService.ToString());
        //                            //陈宝栋修改 2014-1-21
        //                            DataSet ds = XmlSerializerHelper.ConvertXMLToDataSet(strSysTemp);
        //                            XmlSerializerHelper.ConvertDataSetToXMLFile(ds, xmlFileName);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (userInfoModelList.SystemInfoList[0] == "true")
        //                    {
        //                        result = true;
        //                        if (userInfoModelList.UserInfoList[0].System_InfosModelList.Count == 0)
        //                        {
        //                            showInfo = "该用户尚未设置权限！";
        //                            XmlSerializerHelper.SerializerToFile<UserInfoModelList>(userInfoModelList, xmlUserFileName);
        //                        }
        //                        else
        //                        {
        //                            //服务器数据集合
        //                            List<string> serverList = new List<string>();
        //                            //本地数据集合
        //                            List<string> loctionList = new List<string>();
        //                            bool isEqual = CompareList(userInfoModelList.UserInfoList[0].System_InfosModelList[0].SysInfosModelList, userInfoModelList1.UserInfoList[0].System_InfosModelList[0].SysInfosModelList, ref serverList, ref loctionList);
        //                            if (!isEqual)
        //                            {
        //                                PublicVariableModel.IsUpdateImage = "true";
        //                                DataSet dtSystem = XmlSerializerHelper.ConvertXMLFileToDataSet(xmlFileName);
        //                                int count1 = serverList.Count;
        //                                int count2 = loctionList.Count;
        //                                int temp = 0;
        //                                if (count1 > count2)
        //                                {
        //                                    temp = count1;
        //                                }
        //                                else
        //                                {
        //                                    temp = count2;
        //                                }
        //                                for (int i = 0; i < temp; i++)
        //                                {
        //                                    if (i > loctionList.Count - 1)
        //                                    {
        //                                        break;
        //                                    }
        //                                    if (!serverList.Contains<string>(loctionList[i]))
        //                                    {
        //                                        for (int j = 0; j < dtSystem.Tables[3].Rows.Count; j++)
        //                                        {
        //                                            if (j > dtSystem.Tables[3].Rows.Count - 1)
        //                                            {
        //                                                break;
        //                                            }
        //                                            string str1 = dtSystem.Tables[3].Rows[j]["SYSTEM_NO"].ToString();
        //                                            string str2 = loctionList[i].ToString().Split('|')[0];
        //                                            if (str1 == str2)
        //                                            {
        //                                                dtSystem.Tables[3].Rows[j].Delete();
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                                for (int i = 0; i < temp; i++)
        //                                {
        //                                    if (i > serverList.Count - 1)
        //                                    {
        //                                        break;
        //                                    }
        //                                    if (!loctionList.Contains<string>(serverList[i]))
        //                                    {
        //                                        stBReWebService.Append("<SYSTEM_INFO><SYSTEM_NO>" + serverList[i].Split('|')[0] + "</SYSTEM_NO></SYSTEM_INFO>");
        //                                    }
        //                                }
        //                                stBReWebService.Append("</SYSTEM_INFOS></REQUEST>");
        //                                string strSysTemp = GetSysFuntion(stBReWebService.ToString());
        //                                DataSet dtServer = XmlSerializerHelper.ConvertXMLToDataSet(strSysTemp);
        //                                if (dtServer.Tables.Count > 3)
        //                                {
        //                                    for (int i = 0; i < dtServer.Tables[3].Rows.Count; i++)
        //                                    {
        //                                        dtSystem.Tables[3].Rows.Add(dtServer.Tables[3].Rows[i].ItemArray);
        //                                    }
        //                                }
        //                                XmlSerializerHelper.SerializerToFile<UserInfoModelList>(userInfoModelList, xmlUserFileName);
        //                                XmlSerializerHelper.ConvertDataSetToXMLFile(dtSystem, xmlFileName);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        showInfo = e.Message;
        //    }
        //    //登录成功后，本地加载主界面的图片
        //    if (result)
        //    {
        //        XmlService m_xmlService = new XmlService();
        //        PublicVariableModel.pList = m_xmlService.findAllXmlData();
        //    }
        //    return result;
        //}
        #endregion
        /// <summary>
        /// 综合处理WebService交互的Xml(登录)
        /// </summary>
        /// <param name="txtName">用户名</param>
        /// <param name="txtPassword">密码</param>
        public bool SaveXml(string txtName, string txtPassword, bool isCA, out string showInfo)
        {
            bool result = false;
            try
            {
                //保存Xml文件的路径
                string xmlFileName = Application.StartupPath + "\\WebServiceXMLSysInfo.XML";
                string xmlUserFileName = Application.StartupPath + "\\WebServiceXMLUserInfo.XML";
                //WebServiceUserInfo.JhipWebsvcClient jhipWebsvsClient = new WebServiceUserInfo.JhipWebsvcClient();
                JHIPSSO.SsoWebsvcClient jhipWebsvsClient = new JHIPSSO.SsoWebsvcClient();
                PublicVariableModel.userInfoModelList = new UserInfoModelList();
                int hashTable = PublicVariableModel.userInfoModelList.GetHashCode();
                SystemInfoModelList systemInfoModelList = new SystemInfoModelList();
                StringBuilder stBReWebService = new StringBuilder();
                //将登录信息转换成Xml格式的字符串
                string strFormatXml = "";
                string strReformatXml = "";
                if (isCA)
                {
                    strFormatXml = SetXMLformatStr(txtName);
                    //调用CA身份验证，数据验证
                    strReformatXml = jhipWebsvsClient.CAlLogin(strFormatXml);
                }
                //添加CA验证判断 2014-02-21陈宝栋修改
                else
                {
                    strFormatXml = SetXMLformatStr("JHIP", txtName, txtPassword, "1.0.1");
                    //调用身份验证，数据验证
                    strReformatXml = jhipWebsvsClient.LogIn(strFormatXml);
                }
                string strUserRowKey = "";
                PublicVariableModel.userInfoModelList = XmlSerializerHelper.Deserializer<UserInfoModelList>(strReformatXml);
                //获取员工编号
                PublicVariableModel.userName = PublicVariableModel.userInfoModelList.UserInfoList[0].User_Code;
                PublicVariableModel.userLoginName = PublicVariableModel.userInfoModelList.UserInfoList[0].User_Login_Name;
                PublicVariableModel.SessionID = PublicVariableModel.userInfoModelList.UserInfoList[0].SessionID;
                if (PublicVariableModel.userName == null)
                {

                    //MessageBox.Show("此单点用户还没有设置系统模块");
                    showInfo = "该用户不存在或密码错误或没有单点登录的权限！";
                    return false;
                }
                
                //用户名或密码不正确
                if (PublicVariableModel.userInfoModelList.SystemInfoList[0] == "false")
                {
                    DataSet dtShowsInfo = XmlSerializerHelper.ConvertXMLToDataSet(strReformatXml);
                    showInfo = dtShowsInfo.Tables[0].Rows[0]["RESULT_CONTENT"].ToString();
                }
                else
                {
                    List<System_InfosModel> sys_InfosModelList = PublicVariableModel.userInfoModelList.UserInfoList[0].System_InfosModelList[0].SysInfosModelList;
                    strUserRowKey = PublicVariableModel.userInfoModelList.UserInfoList[0].User_Code;
                    stBReWebService.Append("<REQUEST JHIPMsgVersion=\"1.0.1\"><FROM_SYS>JHIP</FROM_SYS><ROW_KEY>" + strUserRowKey + "</ROW_KEY><SYSTEM_INFOS>");
                    showInfo = "";
                    //2014-1-6 毕兰 获取登录返回值
                    //第一次登陆
                    XmlSerializerHelper.SerializerToFile<UserInfoModelList>(PublicVariableModel.userInfoModelList, xmlUserFileName);
                    PublicVariableModel.IsUpdateImage = "true";
                    if (PublicVariableModel.userInfoModelList.SystemInfoList[0] == "true")
                    {
                        result = true;
                        strUserRowKey = PublicVariableModel.userInfoModelList.UserInfoList[0].User_Code;
                        if (PublicVariableModel.userInfoModelList.UserInfoList[0].System_InfosModelList.Count == 0)
                        {
                            showInfo = "该用户尚未设置权限！";
                        }
                        else
                        {
                            for (int i = 0; i < sys_InfosModelList.Count; i++)
                            {

                                stBReWebService.Append("<SYSTEM_INFO><SYSTEM_NO>" + sys_InfosModelList[i].Row_Key + "</SYSTEM_NO></SYSTEM_INFO>");
                            }
                            //拼接完成的WebService参数字符串
                            stBReWebService.Append("</SYSTEM_INFOS></REQUEST>");
                            string strSysTemp = GetSysFuntion(stBReWebService.ToString());
                            //陈宝栋修改 2014-1-21
                            //将用户信息集合存入全局变量中
                            PublicVariableModel.dsUserInfo = XmlSerializerHelper.ConvertXMLToDataSet(strSysTemp);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                showInfo = e.Message;
            }
            //登录成功后，本地加载主界面的图片
            if (result)
            {
                XmlService m_xmlService = new XmlService();
                PublicVariableModel.pList = m_xmlService.findAllXmlData();
            }
            return result;
        }
        /// <summary>
        /// 修改本地C/S程序路径
        /// </summary>
        /// <param name="strSystem_NO">系统编号</param>
        public static void UpdateLoc_Run_Program(string strSystem_NO, string strLoc_Run_Program)
        {
            string xmlFileName = Application.StartupPath + "\\WebServiceXMLPicInfo.XML";
            DataSet dt_Sys = XmlSerializerHelper.ConvertXMLFileToDataSet(xmlFileName);
            for (int i = 0; i < dt_Sys.Tables[3].Rows.Count; i++)
            {
                string strSysNo = dt_Sys.Tables[3].Rows[i]["SYSTEM_NO"].ToString();
                if (strSysNo == strSystem_NO)
                {
                    dt_Sys.Tables[3].Rows[i]["LOC_RUN_PROGRAM"] = strLoc_Run_Program;
                    break;
                }
            }
            XmlSerializerHelper.ConvertDataSetToXMLFile(dt_Sys, xmlFileName);
        }
        /// <summary>
        /// 判断指定系统编号的系统是否有子系统
        /// </summary>
        /// <param name="sysNO">系统编号</param>
        /// <returns>是否有子系统</returns>
        public static bool isHaveNoSysSub(string sysNO)
        {
            string xmlPicFileName = Application.StartupPath + "\\WebServiceXMLPicInfo.XML";
            bool isHaveSysSub = false;
            if (dtPicFile == null)
            {
                dtPicFile = XmlSerializerHelper.ConvertXMLFileToDataSet(xmlPicFileName);
            }
            for (int i = 0; i < dtPicFile.Tables[3].Rows.Count; i++)
            {
              string strSysSub=  dtPicFile.Tables[3].Rows[i]["SYS_UP"].ToString();
              string strSysNO = dtPicFile.Tables[3].Rows[i]["SYSTEM_NO"].ToString();
              if (strSysSub == sysNO && strSysNO!=strSysSub)
              {
                  isHaveSysSub = true;
                  break;
              }
            }
            return isHaveSysSub;
        }
        /// <summary>
        /// 获取指定父系统的子系统列表
        /// </summary>
        /// <param name="sysNO">指定父系统编号</param>
        /// <returns>返回子系统集合</returns>
        public static List<IDockItem> GetSysSubList(string sysNO)
        {
            string xmlPicFileName = Application.StartupPath + "\\WebServiceXMLPicInfo.XML";
            List<IDockItem> xmlList = new List<IDockItem>();
            string imgString = "";
            string imgPath = "";
            byte[] img = null;
            if (dtPicFile == null)
            {
                dtPicFile = XmlSerializerHelper.ConvertXMLFileToDataSet(xmlPicFileName);
            }
            XmlService xmlService = new XmlService();
            for (int i = 0; i < dtPicFile.Tables[3].Rows.Count; i++)
            {
                string strSysSub = dtPicFile.Tables[3].Rows[i]["SYS_UP"].ToString();
                string strSysNO=dtPicFile.Tables[3].Rows[i]["SYSTEM_NO"].ToString();
                if (strSysSub != strSysNO && sysNO == strSysSub)
                {
                    IDockItem xmlItm = new DefDockItem();
                    xmlItm.ID = strSysNO;
                    xmlItm.Name =dtPicFile.Tables[3].Rows[i]["REGIST_NAME"].ToString();                   
                    xmlItm.Target = dtPicFile.Tables[3].Rows[i]["RUN_PROGRAM"].ToString();
                    xmlItm.Argument = dtPicFile.Tables[3].Rows[i]["CONTENT"].ToString();
                    xmlItm.StartIn = dtPicFile.Tables[3].Rows[i]["RUN_FRAMEWORK"].ToString();
                    imgString = dtPicFile.Tables[3].Rows[i]["IMAGE_INFO"].ToString();
                    xmlItm.IconPath =Application.StartupPath + @"\Image\" + xmlItm.Name.Trim() + ".png".ToUpper();
                    imgPath = xmlItm.IconPath;
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
            return xmlList;
        }
        ///陈宝栋修改 2014-02-10
        /// <summary>
        /// 根据CA编号获取功能
        /// </summary>
        /// <param name="txtName">CA编号</param>
        /// <returns>员工编号</returns>
        //public string CASaveXml(string txtName)
        //{
        //    string strUsuerCode = "";
        //    string xmlFileName = Application.StartupPath + "\\WebServiceXMLSysInfo.XML";
        //    string strCAXml = @"<REQUEST JHIPMsgVersion='1.0.1'><FROM_SYS>JHIP</FROM_SYS>" +
        //                        "<CA_CODE>" + txtName + "</CA_CODE>" +
        //                        "</REQUEST>";
        //    WebServiceUserInfo.JhipWebsvcClient jhipWebsvsClient = new WebServiceUserInfo.JhipWebsvcClient();
        //    string strSysInfoXml = jhipWebsvsClient.CAlLogin(strCAXml);
        //    DataSet ds = XmlSerializerHelper.ConvertXMLToDataSet(strSysInfoXml);
        //    strUsuerCode = ds.Tables["RESULT_CONTENT"].Rows[0]["USER_CODE"].ToString();
        //    XmlSerializerHelper.ConvertDataSetToXMLFile(ds, xmlFileName);
        //    return strUsuerCode;
        //}
        //public bool SetXmlStrToImageAndSave()
        //{
        //    bool isSaveSuccess = false;
        //    try
        //    {
        //        string xmlPath = Application.StartupPath + "\\WebServiceXMLSysInfo.XML";
        //        string resourceStr = Application.StartupPath;
        //        resourceStr = resourceStr.Substring(0, resourceStr.Length - 9) + @"\Resources";
        //        SystemInfoModelList systemInfoModelList = new SystemInfoModelList();
        //        systemInfoModelList = XmlSerializerHelper.DeserializerFromFile<SystemInfoModelList>(xmlPath);
        //        string strImage = "";
        //        strImage = systemInfoModelList.SysInfoModelSonList[0].SystemInfoModeSonlList[0].SystemInfoModelList[0].Image_Info;
        //        StringBinaryConvert.SetStrToFile(strImage, resourceStr);
        //        isSaveSuccess = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        isSaveSuccess = false;
        //    }
        //    return isSaveSuccess;
        //}
    }
}
