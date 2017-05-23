using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace JHDock
{
    /// <summary>
    /// 构建图标对象
    /// </summary>
    public class IcoObjModel
    {
        /// <summary>
        /// 构建桌面图标对象
        /// </summary>
        /// <param name="str">图标文件名</param>
        /// <param name="intptr">图标句柄</param>
        /// <param name="itemPoint">图标坐标</param>
        public IcoObjModel(string str, IntPtr intptr, Point itemPoint)
        {
            this.str = str;
            this.itemIntPtr = intptr;
            this.itemPoint = itemPoint;
        }
        public string str { get; set; }
        public IntPtr itemIntPtr { get; set; }
        public Point itemPoint { get; set; }
    }
}
