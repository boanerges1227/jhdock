using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace JHDock
{
    /// <summary>
    /// 系统图标接口
    /// </summary>
    public interface    IDockItem
    {
        //2013-12-30 毕兰 添加ID标识属性
        /// <summary>
        /// 系统标识
        /// </summary>
        string ID { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        Bitmap Icon { get; set; }
        /// <summary>
        /// 图标路径
        /// </summary>
        string IconPath { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 程序目标
        /// </summary>
        string Target { get; set; }
        /// <summary>
        /// 本地程序路径或者URL
        /// </summary>
        string LocTarget { get; set; }
        /// <summary>
        /// 使用框架：BS、CS
        /// </summary>
        string StartIn { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        string Argument { get; set; }
        /// <summary>
        /// 子系统ID
        /// </summary>
        string SubID { get; set; }

        System.Windows.Forms.FormWindowState WindowdType { get; set; }
        int X { get; set; }
        int Y { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        //2013-12-06 庞少军 修改成靠右
        int RightX { get; set; }
        bool LockItem { get; set; }
        int CentreX { get; }
        int CentreY { get; }
        void InitilizeCentre(int X, int Y);
        void Paint(Graphics DstGraphics, Settings DockSettings,string tip="");
        void Onclick(object sender, System.Windows.Forms.MouseEventArgs e);
    }
}
