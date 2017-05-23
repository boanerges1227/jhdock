using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;


namespace JHDock
{

    static class Common
    {

        public static bool LoadSaveData(bool LoadData, string pSection, string pKey, object pDefValue, ref object pValue, ref IniFile pIniFile)
        {
            if (LoadData)
            {
                return LoadIniData(pSection, pKey, pDefValue, ref pValue, ref pIniFile);
            }
            else
            {
                SaveIniData(pSection, pKey, pDefValue, ref pValue, ref pIniFile);
                return true;
            }
        }

        private static bool LoadIniData(string pSection, string pKey, object pDefValue, ref object pValue, ref IniFile pIniFile)
        {
            Key pIniKey = default(Key);
            bool pChanged = false;
            pIniKey = pIniFile.GetKey(pKey, pSection);
            if (pIniKey == null)
            {
                pChanged = pIniFile.AddKey(pKey, Cast(pDefValue, pValue).ToString(), pSection);
                pValue = Cast(pDefValue, pValue);
            }
            else
            {
                pValue = Cast(pIniKey.Value, pValue);
            }
            return pChanged;
        }

        private static object Cast(object Value, object Variable)
        {
            object pReturnObject = null;
            try
            {
                if (Variable.GetType().Equals(Color.Black.GetType()))
                {
                    pReturnObject = Color.FromName(Value.ToString());
                }
                else if (Information.IsNumeric(Value))
                {
                    pReturnObject = Convert.ToInt64(Value.ToString());
                }
                else
                {
                    pReturnObject = Value.ToString();
                }
            }
            catch (Exception ex)
            {
                pReturnObject = Convert.ChangeType(Value, Variable.GetType());
                //MessageBox.Show(ex.Message)
            }
            return pReturnObject;
        }

        private static void SaveIniData(string pSection, string pKey, object pDefValue, ref object pValue, ref IniFile pIniFile)
        {
            Key pIniKey = default(Key);
            bool pChanged = false;
            pIniKey = pIniFile.GetKey(pKey, pSection);
            if (pIniKey == null)
            {
                pIniFile.AddKey(pKey, Cast(pValue, pDefValue).ToString(), pSection);
            }
            else
            {
                pIniFile.ChangeValue(pKey, pSection, Cast(pValue, pDefValue).ToString());
            }
        }
        /// <summary>
        /// 保存图片到本地默认地址，2014-1-6 毕兰
        /// </summary>
        /// <param name="imgName">图片名称</param>
        /// <param name="imgBinary">图片二进制信息</param>
        public static void SaveImage(string imgName, byte[] imgBinary)
        {
            //string imgPath = Application.StartupPath + "\\" + imgName;
            System.IO.File.WriteAllBytes(imgName, imgBinary);
        }
        /// <summary>
        /// 保存图片到本地，2014-1-6 毕兰
        /// </summary>
        /// <param name="imgPath">图片保存地址</param>
        /// <param name="imgName">图片名称</param>
        /// <param name="imgBinary">图片二进制信息</param>
        public static void SaveImage(string imgPath, string imgName, byte[] imgBinary)
        {
            System.IO.File.WriteAllBytes(imgPath + imgName + ".png", imgBinary);
        }
        public static void IsHaveSameProcess()
        {                
        
  
        }

    }

}
