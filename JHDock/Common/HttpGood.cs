using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using SHDocVw;
using System.Windows.Forms;
using Microsoft.Win32;

namespace JHDock
{
    /// <summary>
    /// 网络数据获取以及传输类 2014-1-17 陈宝栋创建
    /// </summary>
    class HttpGood
    {
        /// <summary>
        /// Post传参并获取返回值
        /// </summary>
        /// <param name="purl">url</param>
        /// <param name="strPdw">权限指令</param>
        /// <returns>返回请求数据</returns>
        public static string PostData(string purl, string strPdw)
        {
            try
            {
                byte[] data = System.Text.Encoding.GetEncoding("utf-8").GetBytes(strPdw);
                // 准备请求   
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(purl);
                //设置超时   
                req.Timeout = 30000;
                req.Method = "Post";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = data.Length;
                Stream stream = req.GetRequestStream();
                // 发送数据   
                stream.Write(data, 0, data.Length);
                stream.Close();
                HttpWebResponse rep = (HttpWebResponse)req.GetResponse();
                Stream receiveStream = rep.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(receiveStream, encode);
                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                StringBuilder strBuilder = new StringBuilder("");
                while (count > 0)
                {
                    String readstr = new String(read, 0, count);
                    strBuilder.Append(readstr);
                    count = readStream.Read(read, 0, 256);
                }
                rep.Close();
                readStream.Close();
                return strBuilder.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string GetData(string strUrl)
        {
            string strRet = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
                request.Timeout = 2000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                System.IO.Stream resStream = response.GetResponseStream();
                Encoding encode = System.Text.Encoding.Default;
                StreamReader readStream = new StreamReader(resStream, encode);
                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                while (count > 0)
                {
                    String str = new String(read, 0, count);
                    strRet = strRet + str;
                    count = readStream.Read(read, 0, 256);
                }
                resStream.Close();
            }
            catch (Exception e)
            {
                strRet = "";
            }
            return strRet;
        }
        /// <summary>
        /// 打开新的浏览器,并且提交POST数据
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="postData">POST 数据</param>
        public static void OpenNewIe(string url, string postData,string systemName="")
        {
            InternetExplorer ie = new InternetExplorer();
            if (ie.FullName.IndexOf("iexplore.exe") <= 0)
            {
                ResetIEDefaultBrowser();
            }
            object vPost, vHeaders, vFlags, vTargetFrame;
            vPost = null;
            vFlags = null;
            vTargetFrame = null;
            vHeaders = "Content-Type: application/x-www-form-urlencoded" + Convert.ToChar(10) + Convert.ToChar(13);

            if (!string.IsNullOrEmpty(postData))
                vPost = ASCIIEncoding.ASCII.GetBytes(postData);
                //vPost = Encoding.UTF8.GetBytes(postData);
            ie.Visible = true;
            ie.Navigate(url, ref vFlags, ref vTargetFrame, ref vPost, ref vHeaders);
           
            //if (systemName != "HIS")
            //{
            //    WinAPI.ShowWindow((IntPtr)ie.HWND, 3);//把窗口最大化
            //}
        }
        /// <summary>
        /// 恢复IE为默认浏览器
        /// </summary>
        /// <returns></returns>
        public static bool ResetIEDefaultBrowser()
        {
            string mainKey = @"http\shell\open\command";
            string nameKey = @"http\shell\open\ddeexec\Application";
            string IEPath = @"C:\Program Files\Internet Explorer\iexplore.exe";
            bool result = false;

            try
            {
                string value = string.Format("\"{0}\" -- \"%1\"", IEPath);
                RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(mainKey, true);
                regKey.SetValue("", value);
                regKey.Close();

                regKey = Registry.ClassesRoot.OpenSubKey(nameKey, true);
                regKey.SetValue("", "IExplore");
                regKey.Close();

                result = true;
            }
            catch
            {
            }

            return result;
        }
    }
}
