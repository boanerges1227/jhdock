using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Microsoft.Win32;

namespace JHDock
{
    //2014-1-13 陈宝栋 创建
    //2014-1-16 陈宝栋 修改MD5不对称加密和加CA签名
    //字符串处理类
    public class ConvertEx
    {
        #region Private Compute Hash
        /// <summary>
        /// 将指定的字符串转换为长度为40位SHA1哈希字串，字符串将使用UTF-8编码
        /// </summary>
        /// <param name="originalString">指定的字符串</param>
        /// <returns>返回结果长度为40位SHA1字串值</returns>
        public static string StringTo40SHA(String originalString)
        {
            return StringToSHA(originalString, 40);
        }

        /// <summary>
        /// 字符串转换为SHA哈希串，字符串将使用UTF-8编码
        /// </summary>
        /// <param name="originalString">原始字串</param>
        /// <param name="newStringLength">新字串的长度</param>
        /// <returns>转换后的新字串</returns>
        public static string StringToSHA(String originalString, Int32 newStringLength)
        {
            HashAlgorithm sha;
            String newString;
            switch (newStringLength)
            {
                case 40:
                    sha = SHA1.Create();
                    break;
                case 64:
                    sha = SHA256.Create();
                    break;
                case 96:
                    sha = SHA384.Create();
                    break;
                case 128:
                    sha = SHA512.Create();
                    break;
                default:
                    sha = SHA1.Create();
                    break;
            }

            newString = StringToSHA(originalString, sha);
            return newString;
        }


        /// <summary>
        /// 根据指定的哈希算法将指定的字符串转换成哈希字串，字符串将使用UTF-8编码
        /// </summary>
        /// <param name="originalString">指定的字符串</param>
        /// <param name="sha">指定的哈希算法</param>
        /// <returns>哈希字串</returns>
        private static string StringToSHA(String originalString, HashAlgorithm sha)
        {
            System.Diagnostics.Debug.Assert(sha != null, "参数sha需指定具体算法的实现");
            byte[] hashedPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(originalString));
            StringBuilder shaStringBuilder = new StringBuilder();
            for (int i = 0; i < hashedPassword.Length; i++)
            {
                shaStringBuilder.Append(hashedPassword[i].ToString("x2"));
            }
            return shaStringBuilder.ToString();
        }


        /// <summary>
        /// 将指定字串转换为长度为32位MD5哈希字串，字符串将使用UTF-8编码
        /// </summary>
        /// <param name="inputStrings"></param>
        /// <returns></returns>
        public static string StringTo32MD5(String originalString)
        {
            return StringToMD5(originalString);
        }

        /// <summary>
        /// 字串转换为MD5，字符串将使用UTF-8编码
        /// </summary>
        /// <param name="inputStrings"></param>
        /// <returns></returns>
        private static string StringToMD5(String originalString)
        {
            try
            {
                //Create An Instance
                System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
                byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(originalString));
                StringBuilder md5StringBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    md5StringBuilder.Append(data[i].ToString("x2"));
                }
                return md5StringBuilder.ToString();
            }
            catch (Exception ex)
            {
                return originalString;
            }
        }
        /// <summary>       
        /// 有密码的AES加密        
        /// /// </summary>       
        /// /// <param name="text">加密字符</param>        
        /// <param name="password">加密的密码</param>        
        /// <param name="iv">密钥</param>        
        /// <returns></returns>       
        public static string AESEncrypt(string text, string password, string iv)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
                len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes(iv);
            rijndaelCipher.IV = ivBytes;
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(text);
            byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);
            return Convert.ToBase64String(cipherBytes);
        }
        /// <summary>        
        /// 随机生成密钥        
        /// </summary>        
        /// <returns></returns>        
        public static string GetIv(int n)
        {
            char[] arrChar = new char[]{'a','b','d','c','e','f','g','h','i','j','k','l','m','n','p','r','q','s','t','u','v','w','z','y','x', '0','1','2','3','4','5','6','7','8','9',
                                        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','Q','P','R','T','S','V','U','W','X','Y','Z'};
            StringBuilder num = new StringBuilder();
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < n; i++)
            {
                num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());
            }
            return num.ToString();
        }
        /// <summary>        
        /// AES解密       
        /// </summary>       
        /// /// <param name="text">加密的字符串</param>        
        /// <param name="password">解密密码</param>        
        /// <param name="iv">解密密钥</param>       
        /// <returns>返回揭秘后的算法</returns>        
        public static string AESDecrypt(string text, string password, string iv)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] encryptedData = Convert.FromBase64String(text);
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes(iv);
            rijndaelCipher.IV = ivBytes;
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }

        /// <summary>
        /// 生成公私钥
        /// </summary>
        /// <param name="PrivateKeyPath"></param>
        /// <param name="PublicKeyPath"></param>
        public void RSAKey(string PrivateKeyPath, string PublicKeyPath)
        {
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                this.CreatePrivateKeyXML(PrivateKeyPath, provider.ToXmlString(true));
                this.CreatePublicKeyXML(PublicKeyPath, provider.ToXmlString(false));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        /// <summary>
        /// 对原始数据进行MD5加密
        /// </summary>
        /// <param name="m_strSource">待加密数据</param>
        /// <returns>返回机密后的数据</returns>
        public string GetHash(string m_strSource)
        {
            HashAlgorithm algorithm = HashAlgorithm.Create("MD5");
            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(m_strSource);
            byte[] inArray = algorithm.ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="m_strEncryptString">MD5加密后的数据</param>
        /// <returns>RSA公钥加密后的数据</returns>
        public string RSAEncrypt(string xmlPublicKey, string m_strEncryptString)
        {
            string strEncrypt;
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlPublicKey);
                byte[] bytes = new UnicodeEncoding().GetBytes(m_strEncryptString);
                strEncrypt = Convert.ToBase64String(provider.Encrypt(bytes, false));
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return strEncrypt;
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="m_strDecryptString">待解密的数据</param>
        /// <returns>解密后的结果</returns>
        public string RSADecrypt(string xmlPrivateKey, string m_strDecryptString)
        {
            string str2;
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlPrivateKey);
                byte[] rgb = Convert.FromBase64String(m_strDecryptString);
                byte[] buffer2 = provider.Decrypt(rgb, false);
                str2 = new UnicodeEncoding().GetString(buffer2);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }
        /// <summary>
        /// 对MD5加密后的密文进行签名
        /// </summary>
        /// <param name="p_strKeyPrivate">私钥</param>
        /// <param name="m_strHashbyteSignature">MD5加密后的密文</param>
        /// <returns></returns>
        public string SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature)
        {
            byte[] rgbHash = Convert.FromBase64String(m_strHashbyteSignature);
            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.FromXmlString(p_strKeyPrivate);
            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
            formatter.SetHashAlgorithm("MD5");
            byte[] inArray = formatter.CreateSignature(rgbHash);
            return Convert.ToBase64String(inArray);
        }
        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="p_strKeyPublic">公钥</param>
        /// <param name="p_strHashbyteDeformatter">待验证的用户名</param>
        /// <param name="p_strDeformatterData">注册码</param>
        /// <returns></returns>
        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, string p_strDeformatterData)
        {
            try
            {
                byte[] rgbHash = Convert.FromBase64String(p_strHashbyteDeformatter);
                RSACryptoServiceProvider key = new RSACryptoServiceProvider();
                key.FromXmlString(p_strKeyPublic);
                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);
                deformatter.SetHashAlgorithm("MD5");
                byte[] rgbSignature = Convert.FromBase64String(p_strDeformatterData);
                if (deformatter.VerifySignature(rgbHash, rgbSignature))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 获取硬盘ID
        /// </summary>
        /// <returns>硬盘ID</returns>
        //public string GetHardID()
        //{
        //    string HDInfo = "";
        //    ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
        //    ManagementObjectCollection moc1 = cimobject1.GetInstances();
        //    foreach (ManagementObject mo in moc1)
        //    {
        //        HDInfo = (string)mo.Properties["Model"].Value;
        //    }
        //    return HDInfo;
        //}
        /// <summary>
        /// 读注册表中指定键的值
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns>返回键值</returns>
        private string ReadReg(string key)
        {
            string temp = "";
            try
            {
                RegistryKey myKey = Registry.LocalMachine;
                RegistryKey subKey = myKey.OpenSubKey(@"SOFTWARE/JX/Register");
                temp = subKey.GetValue(key).ToString();
                subKey.Close();
                myKey.Close();
                return temp;
            }
            catch (Exception)
            {
                throw;//可能没有此注册项
            }
        }

        /// <summary>

        /// 创建注册表中指定的键和值

        /// </summary>

        /// <param name="key">键名</param>

        /// <param name="value">键值</param>

        private void WriteReg(string key, string value)
        {

            try
            {

                RegistryKey rootKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE/JX/Register");

                rootKey.SetValue(key, value);

                rootKey.Close();

            }

            catch (Exception)
            {

                throw;

            }

        }

        /// <summary>
        /// 创建公钥文件
        /// </summary>
        /// <param name="path">公钥文件存放路径</param>
        /// <param name="publickey">公钥</param>
        public void CreatePublicKeyXML(string path, string publickey)
        {

            try
            {

                FileStream publickeyxml = new FileStream(path, FileMode.Create);

                StreamWriter sw = new StreamWriter(publickeyxml);

                sw.WriteLine(publickey);

                sw.Close();

                publickeyxml.Close();

            }

            catch
            {

                throw;

            }

        }

        /// <summary>
        /// 创建私钥文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="privatekey"></param>
        public void CreatePrivateKeyXML(string path, string privatekey)
        {

            try
            {
                FileStream privatekeyxml = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(privatekeyxml);
                sw.WriteLine(privatekey);
                sw.Close();
                privatekeyxml.Close();

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 读取公钥
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadPublicKey(string path)
        {
            StreamReader reader = new StreamReader(path);
            string publickey = reader.ReadToEnd();
            reader.Close();
            return publickey;
        }

        /// <summary>
        /// 读取私钥
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadPrivateKey(string path)
        {
            StreamReader reader = new StreamReader(path);
            string privatekey = reader.ReadToEnd();
            reader.Close();
            return privatekey;
        }

        /// <summary>
        /// 初始化注册表，程序运行时调用，在调用之前更新公钥xml
        /// </summary>
        /// <param name="path">公钥路径</param>
        public void InitialReg(string path)
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE/JX/Register");
            Random ra = new Random();
            string publickey = this.ReadPublicKey(path);
            if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE/JX/Register").ValueCount <= 0)
            {
                this.WriteReg("RegisterRandom", ra.Next(1, 100000).ToString());
                this.WriteReg("RegisterPublicKey", publickey);
            }
            else
            {
                this.WriteReg("RegisterPublicKey", publickey);
            }
        }



        //截取字符串
        public static string CutString(string str, int length)//文字省略
        {
            int Zj = 0, Hc = 0, YingWen = 0, ZongWen = 0;
            string NewString = "", Tstr = ""; string[] XinXi = new string[2];

            XinXi = str.Split(new char[3] { ' ', '\r', '\n' });//去掉换行、回车、空格

            for (int Qd = 0; Qd < XinXi.Length; Qd++)//重新组合
            { Tstr += XinXi[Qd].ToString(); }

            int i = System.Text.Encoding.Default.GetBytes(Tstr).Length;//混合长度
            int j = Tstr.Length;//本身长度

            if (Tstr != "")
            {
                if (i / j == 2 || i == j || j < length) { Hc = length; }//纯中文、纯英文、本身长度小于
                else//混合字符串
                {
                    for (int Hh = 0; Hh < Tstr.Length; Hh++)
                    {
                        int Zz = System.Text.Encoding.Default.GetBytes(Tstr.Substring(Hh, 1)).Length;
                        if (Zz == 2) { length = length - 1; }
                        if (Zz == 1) { Zj++; if (Zj % 2 == 0) { length = length - 1; } }//逢二减一
                        if (length == 0) //特殊处理
                        {
                            ZongWen = System.Text.Encoding.Default.GetBytes(Tstr.Substring(Hh - 1, 1)).Length;
                            YingWen = System.Text.Encoding.Default.GetBytes(Tstr.Substring(Hh + 1, 1)).Length;
                            if (Zj % 2 == 0 && Zz == 2) { Hc = Hh + 1; break; }//逢偶补一
                            else if (Zz == 1 && YingWen == 1 && ZongWen == 1) { Hc = Hh + 2; break; }//前中后都是英文的特殊处理 
                            else { Hc = Hh; break; }
                        }
                    }
                }
                if (j > Hc) { NewString = Tstr.Substring(0, Hc) + "..."; } else { NewString = Tstr; }
            }
            return NewString;
        }

        #endregion Private Compute Hash
    }
}
