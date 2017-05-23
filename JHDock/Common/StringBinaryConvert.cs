using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JHDock
{
    /// <summary>
    /// 二进制与字符串相转换，2014-1-6 毕兰
    /// </summary>
    public  class StringBinaryConvert
    {
        /// <summary>
        /// 将 字符串 转成 二进制 “10011100000000011100011111111101”
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertToBinary(string s)
        {
            byte[] data = Encoding.Unicode.GetBytes(s);
            StringBuilder result = new StringBuilder(data.Length * 8);

            foreach (byte b in data)
            {
                result.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return result.ToString();
        }
        /// <summary>
        /// 将二进制 “10011100000000011100011111111101” 转成 字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertToString(string s)
        {
            System.Text.RegularExpressions.CaptureCollection cs =
                System.Text.RegularExpressions.Regex.Match(s, @"([01]{8})+").Groups[1].Captures;
            byte[] data = new byte[cs.Count];
            for (int i = 0; i < cs.Count; i++)
            {
                data[i] = Convert.ToByte(cs[i].Value, 2);
            }
            return Encoding.Unicode.GetString(data, 0, data.Length);
        }
        /// <summary>
        /// 将指定路径下的文件转化成字符串
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>转换后的字符串</returns>
        public static string SetFileToStr(string filePath)
        {
            Byte[] fileBytes=File.ReadAllBytes(filePath);
            return Convert.ToBase64String(fileBytes);
        }
        /// <summary>
        /// 将指定路径下的文件转化成字符串
        /// </summary>
        /// <param name="filePath">二进制字符串</param>
        /// <returns>是否保存成功</returns>
        public static bool SetStrToFile(string strBinary,string filePath)
        {
            bool isSaveSuccess = false;
            byte[] fileBytes=null;
            try
            {
                fileBytes = Convert.FromBase64String(strBinary);
                File.WriteAllBytes(filePath, fileBytes);
                isSaveSuccess = true;
            }
            catch (Exception e)
            {
                isSaveSuccess = false;
            }
            return isSaveSuccess;
        }

    }
}
