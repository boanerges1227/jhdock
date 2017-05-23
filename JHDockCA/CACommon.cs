using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JHDockCA
{
    /// <summary>
    /// CA所使用的公共函数类 2014-1-16 陈宝栋创建
    /// </summary>
    public class CACommon
    {
        /// <summary>
        /// 注册OCX控件
        /// </summary>
        /// <param name="processName">要注册的OCX控件的本地地址</param>
        public void StartProcessAndEnfolOCX(string processName)
        {
            System.Diagnostics.Process processEnrol = new System.Diagnostics.Process();
            processEnrol.StartInfo.FileName = "Regsvr32.exe";
            //获取要注册的OCX控件的本地地址
            processEnrol.StartInfo.Arguments = processName;
            try
            {
                //启动程序
                processEnrol.Start();
                while (!processEnrol.HasExited)
                {
                    //process.Close();
                }
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// 安装程序的方法
        /// </summary>
        /// <param name="processName">文件名</param>
        public void StartProcessAndWait(string processName)
        {
            //实例化安装程序的类
            System.Diagnostics.Process processSetting = new System.Diagnostics.Process();
            processSetting.StartInfo.FileName = processName;
            //当进程终止时激发此事件
            processSetting.EnableRaisingEvents = true;
            try
            {
                processSetting.Start();
                //指示当前软件安装程序运行完毕后再运行其他程序
                while (!processSetting.HasExited)
                {
                    //process.Close();
                }

            }
            catch
            {

            }
            //CA需要解压缩后运行安装程序，在此截获解压后运行的程序
            System.Diagnostics.Process[] processDecompression = System.Diagnostics.Process.GetProcessesByName("CertAppIns");
            if (processDecompression != null && processDecompression.Length == 1)
            {
                while (!processDecompression[0].HasExited)
                { }

            }
        }
    }
}
