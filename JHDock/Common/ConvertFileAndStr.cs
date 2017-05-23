using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace JHDock
{
    /// <summary>
    /// 二进制与字符串相转换，2014-1-6 毕兰
    /// </summary>
    /// 文件处理的公共类（二进制与字符串和File之间的转换和存储） 2014-1-10 陈宝栋修改 
    /// StringBinaryConvert修改成ConvertFileAndStr
    public class ConvertFileAndStr
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
        /// 将指定路径下的文件转化成字符串 2014-1-9 陈宝栋创建
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>转换后的字符串</returns>
        public static string SetFileToStr(string filePath)
        {
            Byte[] fileBytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(fileBytes);
        }
        /// <summary>
        /// 将指定路径下的文件转化成字符串 2014-1-9 陈宝栋创建
        /// </summary>
        /// <param name="filePath">二进制字符串</param>
        /// <returns>是否保存成功</returns>
        public static bool SetStrToFile(string strBinary, string filePath)
        {
            bool isSaveSuccess = false;
            byte[] fileBytes = null;
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
        /// <summary>
        /// 查找指定文件在本地的路径
        /// </summary>
        /// <param name="strDir">磁盘名称</param>
        /// <param name="strFileName">文件名称</param>
        /// <returns>返回文件路径</returns>
        public static string GetFullFileName(string strDir, string strFileName, ProgressBar progressBar)
        {
            //存放树节点的栈
            Stack<string> skNode = new Stack<string>();
            string fullFileName = "";
            string[] drives = null;
            int count = 0;
            //添加磁盘列表
            if (strDir == "全盘查找")
            {
                drives = Directory.GetLogicalDrives();
            }
            else
            {
                drives = new string[1];
                drives[0] = strDir;
            }
            for (int i = drives.Length - 1; i >= 0; i--)
            {
                //将各磁盘目录压入栈中
                skNode.Push(drives[i]);
            }
            while (skNode.Count > 0 && fullFileName == "")
            {
                //弹出栈顶目录，并获取路径
                string path = skNode.Pop();
                FileInfo fInfo = new FileInfo(path);
                //判断是一个目录并且该目录是合法的
                if ((fInfo.Attributes & FileAttributes.Directory) != 0)
                {
                    string[] subDirs = null;
                    string[] subFiles = null;
                    try
                    {
                        //获取当前目录下的所有子目录
                        subDirs = Directory.GetDirectories(path);
                        //获取当前目录下的所有文件
                        subFiles = Directory.GetFiles(path);
                    }
                    catch
                    { }
                    //判断路径和文件都不为空
                    if (subDirs != null && subFiles != null)
                    {
                        //目录入栈
                        for (int i = 0; i < subDirs.Length; i++)
                        {
                            skNode.Push(subDirs[i]);
                        }
                        //文件无需入栈
                        for (int i = 0; i < subFiles.Length; i++)
                        {
                            string fileName = Path.GetFileName(subFiles[i]);
                            if (fileName == strFileName)
                            {
                                fullFileName = subFiles[i];
                                break;
                            }
                        }
                    }
                    //添加查找进度条
                    count++;
                    if (progressBar.Maximum < count)
                    {
                        progressBar.Maximum = 3 * count;
                    }
                    progressBar.Value = count;
                }
            }
            progressBar.Visible = false;
            return fullFileName;
        }

    }
}
