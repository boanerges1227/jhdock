using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JHDockCA
{
    public partial class GDCA : UserControl, InterfaceCA
    {
        public GDCA()
        {
            InitializeComponent();
        }
        public static GDCA _instance;
        public CAUser user = null;
        TextBox _txtUserLoginName;
        public bool m_bUserLogin = false;//登录与签名
        public string m_strUserLoginName = "";//记录登录名
        public bool m_bisCA = true;//是否应用CA
        public string m_strSignContent = "";
        public string ID = "";//记录身份证号
        public string strPIN = "";//记录PIN码
        public bool m_bisCATeacher = false;//老师帐号是否应用CA
        public static GDCA getInstance()
        {
            if (_instance == null)
            {
                _instance = new GDCA();
            }
            return _instance;
        }
        GDCA_COM_ADVANCELib.gdca_com UseCom = new GDCA_COM_ADVANCELib.gdca_com();//创建
        GDCAWSCOMLib.WSClientCom agwCom = new GDCAWSCOMLib.WSClientCom();//创建连接网关对象
        public int Loginret = 0;
        public bool ReadKey()
        {
            //AgwComWebMethod();
            //if (agwCom.GetError() != 0)
            //{
            //    //MessageBox.Show("网关连接失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    UseCom.GDCA_Finalize();
            //    return false;
            //}
            //else
            //{
            String[] Tureid = new string[2];
            int KeyType = UseCom.GDCA_GetDevicType();
            if (KeyType < 0)
            {
                // MessageBox.Show("您还没有插入UsbKey！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                int ret = UseCom.GDCA_SetDeviceType(KeyType);
                ret = UseCom.GDCA_Initialize();
                int ErrorCode = UseCom.GetError();
                if (ErrorCode != 0)
                {
                    ret = UseCom.GDCA_Finalize();
                    return false;
                }
                else
                {
                    try
                    {
                        String ReadOutData = UseCom.GDCA_ReadLabel("LAB_USERCERT_SIG", 7);
                        Tureid[0] = UseCom.GDCA_GetInfoByOID(ReadOutData, 2, "1.2.86.21.1.1", 0);
                        Tureid[1] = UseCom.GDCA_GetInfoByOID(ReadOutData, 2, "1.2.86.21.1.3", 0);
                        //alert(Tureid[1]);
                        //证书唯一标识
                        string TrustID = Tureid[1] + Tureid[0];
                        if (TrustID.Length < 0)
                        {
                            //MessageBox.Show("读取证书信息失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        user = new CAUser();
                        user.Identification = TrustID;
                        CAUser.CertID = TrustID;
                    }
                    catch (Exception ex)
                    { }
                    return true;
                }
                // }
            }
        }
        public void AgwComWebMethod()
        {
            //连接网关
            object[] obj = new object[1];
            obj[0] = "url";
            string target = ""; //EmrSysWebservicesUse.myEmrGenralStr(obj);
            agwCom.Initialize(target);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="certID"></param>
        /// <param name="PinMa"></param>
        /// <returns></returns>
        public bool UserLogin(int certID, string PinMa)
        {
            int ErrorCode = agwCom.GetError();
            //string userCert = UseCom.GDCA_ReadLabel("LAB_USERCERT_SIG", 7);
            //string _CheckCertData = agwCom.CheckCert("BHYYDZBL", "BHYYDZBL-CheckCert", userCert);
            //if (_CheckCertData != null)
            //{
            Loginret = UseCom.GDCA_Login(2, PinMa);
            if (Loginret != 0)
            {

                MessageBox.Show("Pin码输入错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (ErrorCode != 0 && Loginret != 0)
            {
                MessageBox.Show("登录失败！'" + Loginret + "'", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UseCom.GDCA_Finalize();
                return false;
            }
            else
            {
                return true;
            }
            //}
            //else
            //{
            //    MessageBox.Show("证书验证失败！", "提示");
            //    return false;
            //}
        }
        public string SignFile(string inFile)
        {
            string strResult = string.Empty;
            GDCA_COM_ADVANCELib.gdca_com UseCom = new GDCA_COM_ADVANCELib.gdca_com();//创建
            GDCAWSCOMLib.WSClientCom agwCom = new GDCAWSCOMLib.WSClientCom();//创建连接网关对象
            String SignKey = UseCom.GDCA_ReadFile(0, inFile, 1);
            strResult = UseCom.GDCA_Base64Encode(SignKey);
            return strResult;
        }

        /// <summary>
        /// 数据验签
        /// </summary>
        /// <param name="cert"></param>
        /// <param name="inData"></param>
        /// <param name="signValue"></param>
        /// <returns></returns>
        public bool VerifySignedData(string cert, string inData, string signValue)
        {
            string result = agwCom.PKCS7Verify("BHYYDZBL", "BHYYDZBL-Verify", cert, inData);
            if (string.IsNullOrEmpty(result))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
