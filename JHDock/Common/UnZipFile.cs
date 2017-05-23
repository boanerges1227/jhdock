using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using ICSharpCode.SharpZipLib.Zip;

namespace JHDock
{
    /// <summary>
    /// 解压文件类 2014-1-16 陈宝栋创建
    /// </summary>
    public class UnZipFile
    {
        private string _fileName;
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }
        /// <summary>
        /// 获取解压文件的进度
        /// </summary>
        /// <param name="CurrentUnZipFileLength">当前文件解压的流长</param>
        /// <param name="length">当前解压文件的总长度</param>
        public delegate void GetUnZipFileProgress(int CurrentUnZipFileLength, long length);

        /// <summary>
        /// 获取解压文件的进度事件
        /// </summary>
        public event GetUnZipFileProgress GetUnZipFileProgressEvent;

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="args">下标为0,是需要解压的源文件,下标为1,是解压后的路径</param>
        //public void UnZip(string[] args)
        //{
        //    FileStream fileStream = File.OpenRead(args[0]);
        //    //获取源文件的长度
        //    long length = fileStream.Length;

        //    //获取读取流
        //    ZipInputStream s = new ZipInputStream(fileStream);

        //    ZipEntry theEntry;
        //    int readlength = 0;
        //    while ((theEntry = s.GetNextEntry()) != null)
        //    {
        //        string directoryName = Path.GetDirectoryName(args[1]);
        //        string fileName = Path.GetFileName(theEntry.Name);
        //        _fileName = theEntry.Name;
        //        try
        //        {
        //            if (fileName.Length == 0)
        //            {
        //                continue;
        //            }
        //            //生成解压目录 

        //            if (directoryName == null)
        //            {
        //                Directory.CreateDirectory(args[1] + theEntry.Name.Replace(fileName, ""));
        //            }
        //            else
        //            {
        //                Directory.CreateDirectory(directoryName + "\\" + theEntry.Name.Replace(fileName, ""));
        //            }
        //            if (fileName != String.Empty)
        //            {
        //                //解压文件到指定的目录 
        //                FileStream streamWriter = File.Create(args[1] + theEntry.Name);
        //                int size = 2048;
        //                byte[] data = new byte[2048];
        //                while (true)
        //                {
        //                    size = s.Read(data, 0, data.Length);
        //                    if (size > 0)
        //                    {
        //                        streamWriter.Write(data, 0, size);
        //                        readlength += size;
        //                        if (readlength >= length)
        //                        {
        //                            readlength = (int)length;
        //                        }
        //                        if (GetUnZipFileProgressEvent != null)
        //                        {
        //                            GetUnZipFileProgressEvent(readlength, length);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        break;
        //                    }
        //                }
        //                streamWriter.Flush();
        //                streamWriter.Close();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //            //Log.Writer.WriteLog(ex);
        //        }
        //    }
        //    //s.Flush();
        //    fileStream.Close();
        //    s.Close();
        //}


        /// <summary>
        /// 从 Zip 包中提取指定的文件
        /// </summary>
        /// <param name="zipFilename">Zip压缩包</param>
        /// <param name="filename">要提取的文件</param>
        /// <param name="directoryName">提取后存储的位置</param>
        //public void ExtractFile(string zipFilename, String filename, String directoryName)
        //{
        //    FileStream fileStream = File.Open(zipFilename, FileMode.Open, FileAccess.Read, FileShare.Read);
        //    //获取源文件的长度
        //    long length = fileStream.Length;
        //    if (length <= 0)
        //        return;

        //    //获取读取流
        //    ZipInputStream s = new ZipInputStream(fileStream);

        //    ZipEntry theEntry;
        //    int readlength = 0;
        //    while ((theEntry = s.GetNextEntry()) != null)
        //    {

        //        _fileName = theEntry.Name;
        //        string zipEntryName = Path.GetFileName(theEntry.Name);
        //        if (filename.Equals(theEntry.Name))
        //        {

        //            try
        //            {

        //                if (zipEntryName != String.Empty)
        //                {
        //                    //解压文件到指定的目录 
        //                    FileStream streamWriter = File.Create(System.IO.Path.Combine(directoryName, zipEntryName));
        //                    int size = 2048;
        //                    byte[] data = new byte[2048];
        //                    while (true)
        //                    {
        //                        size = s.Read(data, 0, data.Length);
        //                        if (size > 0)
        //                        {
        //                            streamWriter.Write(data, 0, size);
        //                            readlength += size;
        //                            if (readlength >= length)
        //                            {
        //                                readlength = (int)length;
        //                            }
        //                            if (GetUnZipFileProgressEvent != null)
        //                            {
        //                                GetUnZipFileProgressEvent(readlength, length);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            break;
        //                        }
        //                    }
        //                    streamWriter.Flush();
        //                    streamWriter.Close();
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //                //Log.Writer.WriteLog(ex);
        //            }
        //            break;
        //        }

        //    }//end while

        //    //s.Flush();
        //    fileStream.Close();
        //    s.Close();
        //}//end method

        /// <summary>
        /// 解压指定文件夹(但是此文件夹不能嵌套文件夹)
        /// </summary>
        /// <param name="args">下标为0,是需要解压的源文件,下标为1,是解压后的路径</param>
        /// <param name="folderName">指定文件夹名称</param>
        //public void UnZipSpecifiedFolder(string[] args, string folderName)
        //{
        //    FileStream fileStream = File.OpenRead(args[0]);
        //    //获取源文件的长度
        //    long length = fileStream.Length;

        //    //获取读取流
        //    ZipInputStream s = new ZipInputStream(fileStream);

        //    ZipEntry theEntry;
        //    int readlength = 0;
        //    while ((theEntry = s.GetNextEntry()) != null)
        //    {
        //        string directoryName = Path.GetDirectoryName(args[1]);
        //        string fileName = Path.GetFileName(theEntry.Name);
        //        _fileName = theEntry.Name;
        //        try
        //        {
        //            if (fileName.Length == 0)
        //            {
        //                continue;
        //            }
        //            string str = Path.
        //            GetDirectoryName(theEntry.Name.Replace(fileName, ""));
        //            if (Path.GetDirectoryName(theEntry.Name.Replace(fileName, "")) != folderName)
        //            {
        //                continue;
        //            }
        //            //生成解压目录 
        //            if (directoryName == null)
        //            {
        //                Directory.CreateDirectory(args[1] + theEntry.Name.Replace(fileName, ""));
        //            }
        //            else
        //            {
        //                Directory.CreateDirectory(directoryName + "\\" + theEntry.Name.Replace(fileName, ""));
        //            }
        //            if (fileName != String.Empty)
        //            {
        //                //解压文件到指定的目录 
        //                FileStream streamWriter = File.Create(args[1] + theEntry.Name);
        //                int size = 2048;
        //                byte[] data = new byte[2048];
        //                while (true)
        //                {
        //                    size = s.Read(data, 0, data.Length);
        //                    if (size > 0)
        //                    {
        //                        streamWriter.Write(data, 0, size);
        //                        readlength += size;
        //                        if (readlength >= length)
        //                        {
        //                            readlength = (int)length;
        //                        }
        //                        if (GetUnZipFileProgressEvent != null)
        //                        {
        //                            GetUnZipFileProgressEvent(readlength, length);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        break;
        //                    }
        //                }
        //                streamWriter.Flush();
        //                streamWriter.Close();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //            //Log.Writer.WriteLog(ex);
        //        }
        //    }
        //    //s.Flush();
        //    fileStream.Close();
        //    s.Close();
        //}

    }
}
