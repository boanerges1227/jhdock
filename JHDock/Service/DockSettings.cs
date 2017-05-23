using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace JHDock
{
    /// <summary>
    /// 边界
    /// </summary>
    public class DockMargins
    {
        public string Path;
        public float LeftMargin;
        public float TopMargin;
        public float RightMargin;

        public float BottomMargin;
        public float OuterLeftMargin;
        public float OuterTopMargin;
        public float OuterRightMargin;
        public float OuterBottomMargin;
    }
    /// <summary>
    /// 信息
    /// </summary>
    public class DockInfo
    {
        internal DockInfo(string SkinPath)
        {
            IniFile pConfigFile = new IniFile(SkinPath + "\\background.ini", true);
            ArrayList pKeys = pConfigFile.GetKeys("Background");
            BackGround = new DockMargins();
            var _with1 = BackGround;
            _with1.Path = SkinPath + "\\" + ((Key)pKeys[0]).Value.ToString().Trim();
            _with1.LeftMargin = Convert.ToInt32(((Key)pKeys[1]).Value.ToString().Trim());
            _with1.TopMargin = Convert.ToInt32(((Key)pKeys[2]).Value.ToString().Trim());
            _with1.RightMargin = Convert.ToInt32(((Key)pKeys[3]).Value.ToString().Trim());
            _with1.BottomMargin = Convert.ToInt32(((Key)pKeys[4]).Value.ToString().Trim());
            _with1.OuterLeftMargin = Convert.ToInt32(((Key)pKeys[5]).Value.ToString().Trim());
            _with1.OuterTopMargin = Convert.ToInt32(((Key)pKeys[6]).Value.ToString().Trim());
            _with1.OuterRightMargin = Convert.ToInt32(((Key)pKeys[7]).Value.ToString().Trim());
            _with1.OuterBottomMargin = Convert.ToInt32(((Key)pKeys[8]).Value.ToString().Trim());
        }
        public DockMargins BackGround;
        public DockMargins Seprator;
    }
    /// <summary>
    /// 设置
    /// </summary>
    public class Settings
    {



        private string m_ConfigPathName;
        public Settings(string pConfigSettingPath)
        {
            m_ConfigPathName = pConfigSettingPath;
            General = new cGeneral(m_ConfigPathName);
            Icons = new cIcons(m_ConfigPathName);
            Position = new cPosition(m_ConfigPathName);
            Style = new cStyle(m_ConfigPathName);
            Behavior = new cBehavior(m_ConfigPathName);
        }


        public void Save()
        {
            General.Save();
            Icons.Save();
            Position.Save();
            Style.Save();
            Behavior.Save();
        }

        public cGeneral General;
        public cIcons Icons;
        public cPosition Position;
        public cStyle Style;

        public cBehavior Behavior;

        public void About()
        {
        }
    }
    /// <summary>
    /// 综合
    /// </summary>
    public class cGeneral
    {
        private string m_ConfigFile = "";
        public bool RunAtStartUp = false;
        public bool PortableINI = false;
        public bool MinWindowsToDock = false;
        public bool LockItems = false;

        public bool OpenInstance = false;

        internal cGeneral(string pConfigSettingPath)
        {
            m_ConfigFile = pConfigSettingPath;
            SynchIniData(true);
        }


        private void SynchIniData(bool LoadData)
        {
            IniFile pConfigFile = new IniFile(m_ConfigFile, true);
            bool pChanged = false;
            pChanged = false;

            object transTemp29 = RunAtStartUp;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "General", "RunAtStartUp", true, ref transTemp29, ref pConfigFile);
            if (transTemp29.ToString().ToUpper().Equals("TRUE")) RunAtStartUp = true;
            else RunAtStartUp = false;

            object transTemp30 = PortableINI;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "General", "PortableINI", false, ref transTemp30,ref pConfigFile);
            if (transTemp30.ToString().ToUpper().Equals("TRUE")) PortableINI = true;
            else PortableINI = false;

            object transTemp31 = MinWindowsToDock;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "General", "MinWindowsToDock", false, ref transTemp31,ref pConfigFile);
            if (transTemp31.ToString().ToUpper().Equals("TRUE")) MinWindowsToDock = true;
            else MinWindowsToDock = false;

            object transTemp32 = false;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "General", "LockItems", 64, ref transTemp32,ref pConfigFile);

            object transTemp33 = OpenInstance;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "General", "OpenInstance", false, ref transTemp33,ref pConfigFile);
            if (transTemp33.ToString().ToUpper().Equals("TRUE")) OpenInstance = true;
            else OpenInstance = false;

            if (pChanged)
                pConfigFile.Save(m_ConfigFile);
        }

        internal void Save()
        {
            SynchIniData(false);
        }

    }
    /// <summary>
    /// 图标
    /// </summary>
    public class cIcons
    {
        public enum IconQuality
        {
            Low = 1,
            Average = 2,
            High = 3
        }


        private string m_ConfigFile;
        internal cIcons(string pConfigSettingPath)
        {
            m_ConfigFile = pConfigSettingPath;
            SynchIniData(true);
        }


        private void SynchIniData(bool LoadData)
        {
            IniFile pConfigFile = new IniFile(m_ConfigFile, true);
            bool pChanged = false;
            pChanged = false;
            object transTemp1 = Quality;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Icons", "Quality", Convert.ToInt64(IconQuality.Average),ref transTemp1,ref pConfigFile);
            object transTemp2 = Opacity;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Icons", "Opacity", 100, ref transTemp2,ref pConfigFile);
            object transTemp3 = SizePx;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Icons", "SizePx", 48, ref transTemp3,ref pConfigFile);
            object transTemp4 = ZoomPx;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Icons", "ZoomPx", 64, ref transTemp4,ref pConfigFile);
            object transTemp5 = ZoomWidth;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Icons", "ZoomWidth", 4, ref transTemp5,ref pConfigFile);
            object transTemp6 = ZoomDurationMs;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Icons", "ZoomDurationMs", 200, ref transTemp6,ref pConfigFile);
            if (pChanged)
                pConfigFile.Save(m_ConfigFile);
        }

        internal void Save()
        {
            SynchIniData(false);
        }

        public IconQuality Quality = IconQuality.Average;
        public float Opacity = 100;
        public float SizePx = 48;
        public float ZoomPx = 48;
        public float ZoomWidth = 4;
        public float ZoomDurationMs = 100;
    }
    /// <summary>
    /// 位置
    /// </summary>
    public class cPosition
    {
        public enum ScreenPos
        {
            Top = 1,
            Bottom = 2,
            Left = 3,
            Right = 4
        }

        public enum ScreenLayer
        {
            Top = 1,
            Normal = 2,
            Bottom = 3
        }

        private string m_ConfigFile = "";
        internal cPosition(string pConfigSettingPath)
        {
            m_ConfigFile = pConfigSettingPath;
            SynchIniData(true);
        }


        private void SynchIniData(bool LoadData)
        {
            IniFile pConfigFile = new IniFile(m_ConfigFile, true);
            bool pChanged = false;
            pChanged = false;

            object transTemp1 = Monitor;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Position", "Monitor", "Monitor 1", ref transTemp1,ref pConfigFile);
            object transTemp2 = ScreenPosition;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Position", "ScreenPosition", Convert.ToInt64(ScreenPos.Top), ref transTemp2,ref pConfigFile);
            object transTemp3 = Layering;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Position", "Layering", Convert.ToInt64(ScreenLayer.Top), ref transTemp3,ref pConfigFile);
            object transTemp4 = Centering;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Position", "Centering", 0, ref transTemp4,ref pConfigFile);
            object transTemp5 = EdgeOffsetPx;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Position", "EdgeOffsetPx", 0,ref transTemp5,ref pConfigFile);

            if (pChanged)
                pConfigFile.Save(m_ConfigFile);
        }

        internal void Save()
        {
            SynchIniData(false);
        }
        public string Monitor = "";
        public ScreenPos ScreenPosition = ScreenPos.Top;
        public ScreenLayer Layering = ScreenLayer.Top;
        public float Centering = 0;
        public float EdgeOffsetPx = 0;
    }
    /// <summary>
    /// 类型
    /// </summary>
    public class cStyle
    {

        private string m_ConfigFile;

        internal cStyle(string pConfigSettingPath)
        {
            m_ConfigFile = pConfigSettingPath;

            SynchIniData(true);

        }


        private void SynchIniData(bool LoadData)
        {
            IniFile pConfigFile = new IniFile(m_ConfigFile, true);
            bool pChanged = false;
            pChanged = false;
            object transTemp1 = Theme;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Style", "Theme", "Vista", ref transTemp1, ref pConfigFile);
            Theme =  transTemp1.ToString();
            object transTemp2 = Opacity;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Style", "Opacity", 100, ref transTemp2, ref pConfigFile);
            Opacity = Convert.ToInt64(transTemp2.ToString());
            object transTemp3 = FontName;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Style", "FontName", "Tahoma", ref transTemp3, ref pConfigFile);
            FontName = transTemp3.ToString();
            object transTemp4 = FontSize;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Style", "FontSize", 10, ref transTemp4, ref pConfigFile);
            FontSize = Convert.ToInt64(transTemp4.ToString());
            object transTemp5 = ShadowColor;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Style", "ShadowColor", Color.Black.Name, ref transTemp5, ref pConfigFile);
            ShadowColor = Color.FromName(transTemp5.ToString());
            object transTemp6 = OutLineColor;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Style", "OutLineColor", Color.Black.Name, ref transTemp6, ref pConfigFile);
            OutLineColor = Color.FromName(transTemp6.ToString());
            object transTemp7 = OutLineOpacity;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Style", "OutLineOpacity", 55, ref transTemp7, ref pConfigFile);
            OutLineOpacity = Convert.ToInt64(transTemp7.ToString());
            object transTemp8 = ShadowOpacity;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Style", "ShadowOpacity", 25, ref transTemp8, ref pConfigFile);
            ShadowOpacity = Convert.ToInt64(transTemp8.ToString());
            if (pChanged)
                pConfigFile.Save(m_ConfigFile);
        }

        internal void Save()
        {
            SynchIniData(false);
        }

        public string Theme
        {
            get { return m_Theme; }
            set
            {
                m_Theme = value;
                SkinDockInfo = new DockInfo(Application.StartupPath + "\\Skins\\" + "Test1"); //m_Theme);
            }
        }

        private string m_Theme = "Vista";
        public DockInfo SkinDockInfo;
        public float Opacity = 100;
        public string FontName = "Tahoma";
        public float FontSize = 10;
        public Color ShadowColor = Color.Black;
        public Color OutLineColor = Color.Black;
        public float OutLineOpacity = 55;
        public float ShadowOpacity = 25;
    }
    /// <summary>
    /// 行为
    /// </summary>
    public class cBehavior
    {
        /// <summary>
        /// 图标响应
        /// </summary>
        public enum IconEffects
        {
            /// <summary>
            /// 无
            /// </summary>
            None = 1,
            Umber = 2,
            /// <summary>
            /// 弹跳
            /// </summary>
            Bounce = 3
        }

        private string m_ConfigFile;
        internal cBehavior(string pConfigSettingPath)
        {
            m_ConfigFile = pConfigSettingPath;

            SynchIniData(true);

        }

        /// <summary>
        /// 同步初使数据
        /// </summary>
        /// <param name="LoadData">是否加载数据</param>
        private void SynchIniData(bool LoadData)
        {
            IniFile pConfigFile = new IniFile(m_ConfigFile, true);
            bool pChanged = false;
            pChanged = false;

            object transTemp1 = IconEffect;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Behavior", "IconEffect", Convert.ToInt64(IconEffects.Bounce), ref transTemp1,ref pConfigFile);
            object transTemp2 = AutoHide;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Behavior", "AutoHide", false, ref transTemp2,ref pConfigFile);
            object transTemp3 = AutoHideDurationMs;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Behavior", "AutoHideDurationMs", 250, ref transTemp3,ref pConfigFile);
            object transTemp4 = AutoHideDelayMs;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Behavior", "AutoHideDelayMs", 250, ref transTemp4,ref pConfigFile);
            object transTemp5 = PopUpOnMouseOver;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Behavior", "PopUpOnMouseOver", false, ref transTemp5,ref pConfigFile);
            object transTemp6 = PopUpDelayMs;
            pChanged = pChanged | Common.LoadSaveData(LoadData, "Behavior", "PopUpDelayMs", 350, ref transTemp6,ref pConfigFile);
            if (pChanged)
                pConfigFile.Save(m_ConfigFile);
        }

        internal void Save()
        {
            SynchIniData(false);
        }

        public IconEffects IconEffect = IconEffects.Bounce;
        public bool AutoHide = false;
        public float AutoHideDurationMs = 250;
        public float AutoHideDelayMs = 250;
        public bool PopUpOnMouseOver = false;
        public float PopUpDelayMs = 350;
    }

}
