using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Configuration;
using System.Xml;

namespace JHDockCA
{
    public struct SReponseInfo
    {
        public string MessageStatue;    //认证请求状态
        public string MessageContent; //认证请求返回内容
        public string Original;       //认证请求原文内容
    };

    /// <summary>
    /// 湖北CA-HBCA
    /// 王静然-20150923
    /// </summary>
    public partial class HBCA : UserControl, InterfaceCA, IDisposable
    {
        string hbca_service_ip = ConfigurationManager.ConnectionStrings["HBCAServiceIP"].ConnectionString;//湖北CA-网关服务器IP
        string hbca_app_flag = ConfigurationManager.ConnectionStrings["HBCAAppFlag"].ConnectionString;//湖北CA-网关应用App标识
        string hbca_port = ConfigurationManager.ConnectionStrings["HBCAPort"].ConnectionString;//湖北CA-网关服务器端口
        string issuerDN = "C=CN, S=HUBEI, L=WUHAN, O=HuiBei Digital Certificate Authority Center CO.LTD, CN=HBCA";  //测试CA环境
        //string issuerDN = "C=CN, S=Hubei, L=Wuhan, O=Hubei Digital Certificate Authority Center CO Ltd., CN=HBCA";  //正式CA环境
        bool flag = false;//判断是否已选择用户
        public CAUser caUser = null;
        SVS_C_SDK_COMLib.CSVS_C_SDKClass clientSdk = null;
        HBCA_SOFSEALLib.SealClass hbcaSeal = null;
        public HBCA()
        {
            InitializeComponent();
            //clientSdk = new SVS_C_SDK_COMLib.CSVS_C_SDKClass();
            hbcaSeal = new HBCA_SOFSEALLib.SealClass();
            clientSdk = new SVS_C_SDK_COMLib.CSVS_C_SDKClass();
        }

        public bool ReadKey()
        {
            try
            {
                //tested by Chunhui Chen 20160218
                //先调用签章方法，防止程序获取签章图片内存报错AccessViolationException
                //hbcaSeal.SOF_GetKeyPictureEx("", "");

                string random = GetRandOriginal();
                string authInfo;
                // 取得认证原文，原文非base64格式

                // 未取得原文退出
                if (string.IsNullOrEmpty(random))
                {
                    return false;
                }

                // 生成认证请求信息
                string authRequest = "";
                string dn = "";
                authRequest = buildAuthRequest(dn, random);
                if (string.IsNullOrEmpty(authRequest))
                {
                    return false;
                }

                // 组织认证原文信息
                CBase64 base64 = new CBase64();
                string RandOriginalB64 = random;

                RandOriginalB64 = base64.Encode(random, random.Length);
                authInfo = "<authCredential authMode=\"cert\">\r\n";
                authInfo += "<detach>";
                authInfo += authRequest;
                authInfo += "</detach>\r\n";
                authInfo += "<original>";
                authInfo += RandOriginalB64;
                authInfo += "</original>\r\n";
                authInfo += "</authCredential>\r\n";


                // 到应用服务器验证用户是否合法
                bool start_flag = StartPlainAuth(authInfo, hbca_service_ip);
                //verifyAuthRequest(ref serverIP, ref serverPort, ref authInfo);
                if (start_flag)
                {
                    return true;
                }
                //StartPlainAuth();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            bool result = ReadKey();
            MessageBox.Show(result.ToString());
        }

        public string GetRandOriginal()
        {
            // 读取IP地和端口
            string strGateWayIP = hbca_service_ip;
            UInt16 nGatePort = ushort.Parse(hbca_port);

            // 应用标识
            string csAppFlag = hbca_app_flag;

            ///////////////////////////////////////////////////
            // 生成认证请求报文
            ///////////////////////////////////////////////////
            string csRequest = BuildRndRequestXML(csAppFlag);

            ///////////////////////////////////////////////////
            // 发送请求报文到认证服务器
            ///////////////////////////////////////////////////
            string strReponseHttpBody = "";
            if (SendRequest_Plain(csRequest, strGateWayIP, nGatePort, ref strReponseHttpBody) != 0)
                return "认证原文请求失败！";

            ///////////////////////////////////////////////////
            // 解析服务回应请求到结构体
            ///////////////////////////////////////////////////
            SReponseInfo reponseInfo = new SReponseInfo();
            ParseReponseInfo(strReponseHttpBody, ref reponseInfo);

            // 显示认证原文请求结果
            string strDispResult = string.Format("=====================================================\r\n认证原文：{0}\r\n",
                reponseInfo.Original);

            //UpdateTestInfo(strDispResult);
            return reponseInfo.Original;
        }
        string BuildRndRequestXML(string pszAppFlag)
        {
            string csRequest = string.Format(
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" +
                "<message>\r\n" +
                "<head>\r\n" +
                "<version>1.0</version>\r\n" +
                "<serviceType>OriginalService</serviceType>\r\n" +
                "</head>\r\n" +
                "<body>\r\n" +
                "<appId>{0}</appId>\r\n" +
                "</body>\r\n" +
                "</message>\r\n",
                pszAppFlag);
            return csRequest;
        }
        long SendRequest_Plain(string pszReqInfo, string pszGateWayIP, UInt16 usGatePort, ref string strReponseHttpBody)
        {
            // 准备请求对象
            string strUrl = string.Format("http://{0}:{1}/MessageService", pszGateWayIP, usGatePort);

            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(strUrl);
            string sendMsg = pszReqInfo;
            byte[] bs = Encoding.UTF8.GetBytes(sendMsg);
            HttpWReq.Method = "POST";
            HttpWReq.ContentType = "application/x-www-form-urlencoded";
            HttpWReq.ContentLength = bs.Length;

            using (Stream reqStream = HttpWReq.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }
            using (HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse())
            {
                StreamReader sr = new StreamReader(HttpWResp.GetResponseStream(), Encoding.UTF8);
                strReponseHttpBody = sr.ReadToEnd();
                sr.Close();
                HttpWResp.Close();
            }

            HttpWReq.Abort();
            return string.IsNullOrEmpty(strReponseHttpBody) ? 1 : 0;
        }
        // 解析回应信息
        void ParseReponseInfo(string pszPolicym, ref SReponseInfo reponseInfo)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode node;

            // 加载服务器返回的结果
            doc.LoadXml(pszPolicym);
            XmlElement root = doc.DocumentElement;

            XmlNode headNode = SelectChildNode(root, "head");
            node = SelectChildNode(headNode, "messageState");
            if (node == null)
            {
                node = SelectChildNode(headNode, "messagestate");
            }

            reponseInfo.MessageStatue = node.InnerText;

            reponseInfo.MessageContent = pszPolicym;

            XmlNode bodyNode = SelectChildNode(root, "body");
            node = SelectChildNode(bodyNode, "original");
            reponseInfo.Original = node != null ? node.InnerText : "";
        }
        public XmlNode SelectChildNode(XmlNode parent, string name)
        {
            foreach (XmlNode child in parent.ChildNodes)
            {
                if (child.Name == name)
                {
                    return child;
                }
            }
            return null;
        }

        public bool StartPlainAuth(string pszReqInfo, string pszClientIP)
        {
            ///////////////////////////////////////////////////
            // 参数检查
            ///////////////////////////////////////////////////
            if (string.IsNullOrEmpty(pszReqInfo))
                return false;

            // 读取IP地和端口
            string strGateWayIP = hbca_service_ip;
            UInt16 nGatePort = UInt16.Parse(hbca_port);

            // 应用标识
            string csAppFlag = hbca_app_flag;

            ///////////////////////////////////////////////////
            // 生成认证请求报文
            ///////////////////////////////////////////////////
            string csRequest = BuildRequestXML(pszReqInfo, csAppFlag, pszClientIP);

            ///////////////////////////////////////////////////
            // 发送请求报文到认证服务器
            ///////////////////////////////////////////////////
            string strReponseHttpBody = "";
            if (SendRequest_Plain(csRequest, strGateWayIP, nGatePort, ref strReponseHttpBody) != 0)
                return false;

            ///////////////////////////////////////////////////
            // 解析服务回应请求到结构体
            ///////////////////////////////////////////////////
            SReponseInfo reponseInfo = new SReponseInfo();
            ParseReponseInfo(strReponseHttpBody, ref reponseInfo);

            ///////////////////////////////////////////////////
            // 显示回应信息  reponseInfo是一个结构体列表，用户在该列表中可以找到自己需要的属性信息
            ///////////////////////////////////////////////////
            DisplayReponseInfo(reponseInfo);
            if (reponseInfo.MessageStatue.Equals("true"))
            {
                MessageBox.Show("认证失败！");
                return false;
                
            }
            MessageBox.Show("认证成功！");

            //get certId from reponseinfo
             #region 获取CA信息：名称、序列号、工号、日期、图片
            clientSdk.SOF_SetCertAppPolicy("SIGN");
            //clientSdk.SOF_ExportExChangeUserCert("");
            //clientSdk.SOF_ExportUserCert("");

            string UserList = clientSdk.SOF_GetUserList();
            if (!string.IsNullOrEmpty(UserList))
            {

                string[] templist = UserList.Split(new string[] { "&&&" }, StringSplitOptions.None);

                for (int i = 0; i < templist.Length; i++)
                {


                    string[] UserList_infos = templist[i].Split(new string[] { "||" }, 4, StringSplitOptions.None);

                    //中文名字
                    string name = UserList_infos[1].Split(',')[0].Remove(0, 3);

                    //序列号
                    string certId = UserList_infos[0].ToString();

                    //判断取到的证书ID  是不是responseinfo里面放回的
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(reponseInfo.MessageContent);

                    XmlNode root_node = xmldoc.SelectSingleNode("message");
                    XmlNode body_node = root_node.SelectSingleNode("body");
                    XmlNode attr_node = body_node.SelectSingleNode("attributes");
                    XmlNodeList attr_child_nodes = attr_node.ChildNodes;
                    string certid_from_respon = attr_child_nodes[2].InnerText.ToUpper();

                    if (certId.Equals(certid_from_respon))
                    { //证书完整DN项
                        string certFullDN = UserList_infos[1].ToString();
                        string certB64 = clientSdk.SOF_ExportUserCert(certId);
                        string userCert = clientSdk.SOF_GetCertInfoByOid(certId, "2.4.16.11.7.3");

                        //有效日期
                        string strVaryDate = clientSdk.SOF_GetCertInfo(certId, 0x00000010);
                        if (string.IsNullOrEmpty(userCert))
                        {
                            //return false;
                        }

                        userCert = GetUserCert(userCert);

                        //图片二进制流转换为base64字符串
                        string strImageB64 = ConvertGif2Jpg(certId, userCert);

                        caUser = new CAUser();
                        caUser.Identification = certId;
                        caUser.UserCert = userCert;
                        caUser.ContainerName = name;
                        caUser.TICKET_VALIDATE = DateTime.Parse(strVaryDate);
                        caUser.ImageB64 = strImageB64;
                    }

                }
            }
            else
            {
                MessageBox.Show("没有检测到证书，请插入key!");
                //return "没有检测到证书，请插入key!";
                return false;
            }
            #endregion

            return true;
        }

        // 生成认证请求报文
        string BuildRequestXML(string pszReqInfo, string pszAppFlag, string pszClientIP)
        {
            string csRequest;
            string csReqInfo = pszReqInfo;

            //证书认证时注释掉
            csRequest = string.Format(
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" +
                "<message>\r\n" +
                "<head>\r\n" +
                "<version>1.0</version>\r\n" +
                "<serviceType>AuthenService</serviceType>\r\n" +
                "</head>\r\n" +
                "<body>\r\n" +
                "<clientInfo><clientIP>{0}</clientIP></clientInfo>\r\n" +
                "<appId>{1}</appId>\r\n" +
                "<authen>{2}</authen>\r\n" +
                "<accessControl>false</accessControl>\r\n" +
                "<attributes attributeType=\"all\">\r\n",
                pszClientIP, pszAppFlag, pszReqInfo);

            if (csReqInfo.IndexOf("<authCredential authMode=\"cert\">") != -1)
            {
                string csCertInfo =
                    "<attr name=\"X509Certificate.NotBefore\" namespace=\"http://www.jit.com.cn/cinas/ias/ns/saml/saml11/X.509\"></attr>\r\n" +
                    "<attr name=\"X509Certificate.NotAfter\" namespace=\"http://www.jit.com.cn/cinas/ias/ns/saml/saml11/X.509\"></attr>\r\n" +
                    "<attr name=\"X509Certificate.SubjectDN\" namespace=\"http://www.jit.com.cn/cinas/ias/ns/saml/saml11/X.509\"></attr>\r\n" +
                    "<attr name=\"X509Certificate.SerialNumber\" namespace=\"http://www.jit.com.cn/cinas/ias/ns/saml/saml11/X.509\"></attr>\r\n" +
                    "<attr name=\"X509Certificate.IssuerDN\" namespace=\"http://www.jit.com.cn/cinas/ias/ns/saml/saml11/X.509\"></attr>\r\n";

                csRequest += csCertInfo;
            }
            if (csReqInfo.IndexOf("<authCredential authMode=\"password\">") != -1)
            {
                string csPasswordInfo =
                    "<attr name=\"UMS.UserID\" namespace=\"http://www.jit.com.cn/ums/ns/user\"></attr>\r\n" +
                    "<attr name=\"UMS.Username\" namespace=\"http://www.jit.com.cn/ums/ns/user\"></attr>\r\n" +
                    "<attr name=\"UMS.LogonName\" namespace=\"http://www.jit.com.cn/ums/ns/user\"></attr>\r\n";

                csRequest += csPasswordInfo;
            }
            csRequest +=
                "<attr name=\"privilege\" namespace=\"http://www.jit.com.cn/pmi/pms/ns/privilege\"></attr>\r\n" +
                "<attr name=\"role\" namespace=\"http://www.jit.com.cn/pmi/pms/ns/role\"></attr>\r\n" +
                "<attr name=\"性别\" namespace=\"http://www.jit.com.cn/ums/ns/user\"></attr>\r\n" +
                "<attr name=\"职务\" namespace=\"http://www.jit.com.cn/ums/ns/user\"></attr>\r\n" +
                "<attr name=\"身份证\" namespace=\"http://www.jit.com.cn/ums/ns/user\"></attr>\r\n" +
                "<attr name=\"部门\" namespace=\"http://www.jit.com.cn/ums/ns/user\"></attr>\r\n" +
                "<attr name=\"机构字典\" namespace=\"http://www.jit.com.cn/ums/ns/user\"></attr>\r\n" +
                "</attributes>\r\n" +
                "</body>\r\n" +
                "</message>";

            return csRequest;
        }
        // 显示服务响应结果
        void DisplayReponseInfo(SReponseInfo reponseInfo)
        {
            // 显示信息
            string strResult = "=====================================================\r\n";
            string strAuthResult = string.Format(
                "认证结果：{0}（false：成功，true：失败）\r\n",
                reponseInfo.MessageStatue);
            strResult += strAuthResult;
            strResult += "=====================================================\r\n";
            strResult += "=                     返回内容                       =\r\n";
            strResult += "=====================================================\r\n";
            strResult += reponseInfo.MessageContent;

            //UpdateTestInfo(strResult);
        }

        //---------------------------------------------------------------------------------------
        // 生成认证请求数据
        string buildAuthRequest(string strRootDN, string strRandOriginal)
        {
            string csAuthRequest = "";
            JITComVCTKExLib.JITVCTKEx VCTK = null;

            try
            {
                VCTK = new JITComVCTKExLib.JITVCTKEx();
            }
            catch
            {
                MessageBox.Show("创建VCTK对象失败。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "";
            }

            // 根据根证书主题过滤证书
            VCTK.Initialize("<?xml version=\"1.0\" encoding=\"gb2312\"?><authinfo><liblist><lib type=\"CSP\" version=\"1.0\" dllname=\"\" ><algid val=\"SHA1\" sm2_hashalg=\"sm3\"/></lib><lib type=\"SKF\" version=\"1.1\" dllname=\"SERfR01DQUlTLmRsbA==\" ><algid val=\"SHA1\" sm2_hashalg=\"sm3\"/></lib><lib type=\"SKF\" version=\"1.1\" dllname=\"U2h1dHRsZUNzcDExXzMwMDBHTS5kbGw=\" ><algid val=\"SHA1\" sm2_hashalg=\"sm3\"/></lib><lib type=\"SKF\" version=\"1.1\" dllname=\"U0tGQVBJLmRsbA==\" ><algid val=\"SHA1\" sm2_hashalg=\"sm3\"/></lib></liblist></authinfo>");
            if (VCTK.GetErrorCode() != 0)
            {
                MessageBox.Show("初始化失败。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                VCTK.Finalize();
                return csAuthRequest;
            }

            VCTK.SetCertChooseType(1);

            
            strRootDN = issuerDN;
            if (string.IsNullOrEmpty(strRootDN))
            {
                VCTK.SetCert("SC", "", "", "", "", "");
            }
            else
            {
                VCTK.SetCert("SC", "", "", "", strRootDN, "");
            }

            if (VCTK.GetErrorCode() != 0)
            {
                MessageBox.Show("选择认证证书失败。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                VCTK.Finalize();
                return csAuthRequest;
            }

            // 生成认证原文认证请求信息
            string strSource = strRandOriginal;
            byte[] bsSource = Encoding.UTF8.GetBytes(strSource);
            //string userList = VCTK.GetCertList("", "", "", "", strRootDN, -1);

            csAuthRequest = VCTK.DetachSignStr("", strSource);
            if (csAuthRequest.Length == 0)
            {
                MessageBox.Show("生成认证原文认证请求信息失败。 " + VCTK.GetErrorMessage(VCTK.GetErrorCode()), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                VCTK.Finalize();
                return csAuthRequest;
            }            

            VCTK.Finalize();
            return csAuthRequest;
        }

        private string GetUserCert(string userCert)
        {
            string hexTmp = "";
            List<string> listHex = new List<string>();
            for (int i = 0; i < userCert.Length; i++)
            {
                hexTmp = hexTmp + userCert[i];
                if (hexTmp.Length == 2)
                {
                    listHex.Add(hexTmp);
                    hexTmp = "";
                }
            }
            //工号（唯一标识）
            userCert = Hex2String(listHex.ToArray());
            return userCert;
        }

        #region 获取签名图片
        /// <summary>
        /// 获取签名图片，把图片转化为bate64位的字符串返回
        /// certId:KEY序列号
        /// userCert:KEY工号
        /// </summary>
        /// <returns>bate64位的字符串</returns>
        public string ConvertGif2Jpg(string certId, string userCert)
        {
            string str_PIC = null;

            if (string.IsNullOrEmpty(certId))
            {
                return null;
            }

            string imageB64 = hbcaSeal.SOF_GetKeyPictureEx(certId, userCert);
            if (imageB64 == null || imageB64 == "")
            {
                MessageBox.Show("获取图片失败!");
                return null;
            }
            byte[] imageBytes = Convert.FromBase64String(imageB64);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBytes);
            Image image = Image.FromStream(ms);
            image.Save("D:\\temp.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            try
            {
                byte[] bytes = System.IO.File.ReadAllBytes(@"D:\\temp.jpg"); //读取文件到byte数组
                str_PIC = Convert.ToBase64String(bytes);//byte 数组转字符流（base64编码）
                //删除缓存的图片
                System.IO.File.Delete("D:\\temp.jpg");
            }
            catch (Exception ex)
            {
                MessageBox.Show("用户图片转换失败，请联系管理员！" + ex.Message);
            }
            return str_PIC;
        }
        #endregion

        #region 转换工号为十进制
        private string Hex2String(string[] hexs)
        {
            StringBuilder sb = new StringBuilder(256);
            for (int i = 2; i < hexs.Length; i++)
            {
                int iStr = Convert.ToInt32(hexs[i], 16);
                sb.Append(Convert.ToChar(iStr));
            }
            return sb.ToString();
        }
        #endregion

        #region 返回用户
        public CAUser getUserInfo()
        {
            flag = false;
            return this.caUser;
        }
        #endregion

        #region 实现接口
        public bool UserLogin(int certID, string PinMa)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 垃圾回收
        public new void Dispose()
        {
            Disposeble(true);
            // GC.SuppressFinalize(this);
        }
        protected virtual void Disposeble(bool disposing)
        {
            if (disposing)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(clientSdk);
                clientSdk = null;
                if (clientSdk != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(hbcaSeal);
                    hbcaSeal = null;
                }

                //GC.Collect();
            }
        }
        ~HBCA()
        {
            Disposeble(false);
        }
        #endregion

       
    }
}