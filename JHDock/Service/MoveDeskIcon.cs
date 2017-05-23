using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace JHDock
{
    public class MoveDeskIcon
    {
        private const uint LVM_FIRST = 0x1000;
        private static int LVIF_TEXT = 0x0001;
        private const uint LVM_GETITEMCOUNT = LVM_FIRST + 4;
        private const uint LVM_GETITEMW = LVM_FIRST + 75;
        private const uint LVM_GETITEMPOSITION = LVM_FIRST + 16;
        private const uint LVM_GETHEADER = LVM_FIRST + 31;
        private const uint LVM_SETITEMW = LVM_FIRST + 76;
        private const uint LVM_SETITEMPOSITION = LVM_FIRST + 15;
        private const uint LVM_REDRAWITEMS = LVM_FIRST + 21;
        public const uint PROCESS_VM_OPERATION = 0x0008;
        public const uint PROCESS_VM_READ = 0x0010;
        public const uint PROCESS_VM_WRITE = 0x0020;
        public const uint MEM_COMMIT = 0x1000;
        public const uint MEM_RELEASE = 0x8000;
        public const uint MEM_RESERVE = 0x2000;
        public const uint PAGE_READWRITE = 4;
        private IList<IcoObjModel> icoObj = null;
        private IList<IcoObjModel> icoObjLast = null;
        private const int GWL_STYLE = -16;
        private const int LVS_AUTOARRANGE = 0x0100;
        public void MoveDeskIconAll()
        {
            for (int i = 0; i < icoObjLast.Count; i++)
            {
                WinAPI.SendMessage(icoObjLast[i].itemIntPtr, LVM_SETITEMPOSITION, i, MAKELPARAM(icoObjLast[i].itemPoint.X, icoObjLast[i].itemPoint.Y));
            }
            /// <summary>
        }
        private int GetminX()
        {
            int bMinIndex = 0;
            icoObj = getICO();
            if (icoObj != null && icoObj.Count > 0)
            {
                int aMin = icoObj[0].itemPoint.X;
                for (int i = 1; i < icoObj.Count; i++)
                {

                    if (icoObj[i].itemPoint.X < aMin)
                    {
                        aMin = icoObj[i].itemPoint.X;
                        bMinIndex = i;
                    }
                }
            }
            return bMinIndex;
        }
        /// 将桌面所有图标右移动指定坐标的位置
        /// </summ
        /// <param name="point">要移动的坐标</param>
        public void MoveAllDeskIconOfp(Point point)
        {
            icoObj = getICO();
            int minIndex = -1;
            if (minIndex == -1)
            {
                minIndex = GetminX();
            }
            DisableAutoArrange();
            if (icoObj != null && icoObj.Count > 0)
            {

                if (icoObj[minIndex].itemPoint.X < 88)
                {
                    for (int i = 0; i < icoObj.Count; i++)
                    {
                        WinAPI.SendMessage(icoObj[i].itemIntPtr, LVM_SETITEMPOSITION, i, MAKELPARAM(icoObj[i].itemPoint.X + point.X, icoObj[i].itemPoint.Y + point.Y));
                    }
                }
            }
        }
        /// <summary> 
        /// 撤销自动排列 
        /// </summary> 
        private void DisableAutoArrange()
        {
            try
            {
                
                IntPtr hwnd = WinAPI.FindWindow("Progman", null);
                hwnd = WinAPI.FindWindowEx(hwnd, IntPtr.Zero, "SHELLDLL_DefView", null);
                hwnd = WinAPI.FindWindowEx(hwnd, IntPtr.Zero, "SysListView32", null);
                int oldValue = WinAPI.GetWindowLong(hwnd, GWL_STYLE);
                WinAPI.SetWindowLong(hwnd, GWL_STYLE, oldValue & ~LVS_AUTOARRANGE);
            }
            catch (Exception e)
            { }
        }
        /// <summary>
        /// 获取所有桌面图标对象集合
        /// </summary>
        private IList<IcoObjModel> getICO()
        {
            IntPtr vHandle = WinAPI.FindWindow("Progman", null);
            vHandle = WinAPI.FindWindowEx(vHandle, IntPtr.Zero, "SHELLDLL_DefView", null);
            vHandle = WinAPI.FindWindowEx(vHandle, IntPtr.Zero, "SysListView32", null);
            int vItemCount = ListView_GetItemCount(vHandle);
            uint vProcessId;
            WinAPI.GetWindowThreadProcessId(vHandle, out vProcessId);
            IntPtr vProcess = WinAPI.OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_READ | PROCESS_VM_WRITE, false, vProcessId);
            IntPtr vPointer = WinAPI.VirtualAllocEx(vProcess, IntPtr.Zero, 4096, MEM_RESERVE | MEM_COMMIT, PAGE_READWRITE);
            IList<IcoObjModel> icoObj = new List<IcoObjModel>();//所有桌面项目
            try
            {
                for (int i = 0; i < vItemCount; i++)
                {
                    byte[] vBuffer = new byte[256];
                    LVITEM[] vItem = new LVITEM[1];
                    vItem[0].mask = LVIF_TEXT;
                    vItem[0].iItem = i;
                    vItem[0].iSubItem = 0;
                    vItem[0].cchTextMax = vBuffer.Length;
                    vItem[0].pszText = (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(LVITEM)));
                    uint vNumberOfBytesRead = 0;
                    WinAPI.WriteProcessMemory(vProcess, vPointer, Marshal.UnsafeAddrOfPinnedArrayElement(vItem, 0), Marshal.SizeOf(typeof(LVITEM)), ref vNumberOfBytesRead);
                    WinAPI.SendMessage(vHandle, LVM_GETITEMW, i, vPointer.ToInt32());
                    WinAPI.ReadProcessMemory(vProcess, (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(LVITEM))), Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0), vBuffer.Length, ref vNumberOfBytesRead);
                    string vText = Encoding.Unicode.GetString(vBuffer, 0, (int)vNumberOfBytesRead);
                    string str = vText.Substring(0, vText.IndexOf("\0"));
                    ListView_GetItemPosition(vHandle, i, vPointer);
                    Point[] vPoint = new Point[1];
                    WinAPI.ReadProcessMemory(vProcess, vPointer, Marshal.UnsafeAddrOfPinnedArrayElement(vPoint, 0), Marshal.SizeOf(typeof(Point)), ref vNumberOfBytesRead);
                    icoObj.Add(new IcoObjModel(vText.Substring(0, vText.IndexOf("\0")), vHandle, vPoint[0]));
                }
                if (icoObjLast == null)
                {
                    icoObjLast = icoObj;
                }
            }

            finally
            {
                WinAPI.VirtualFreeEx(vProcess, vPointer, 0, MEM_RELEASE);
                WinAPI.CloseHandle(vProcess);
            }
            return icoObj;
        }
        private uint MAKELPARAM(double wLow, double wHigh)
        {
            return (uint)(wHigh * 0x10000 + wLow);
        }

        /// <summary>
        /// 获取ListView行数
        /// </summary>
        /// <param name="AHandle">对象句柄</param>
        /// <returns>返回行数</returns>
        private int ListView_GetItemCount(IntPtr AHandle)
        {
            return WinAPI.SendMessage(AHandle, LVM_GETITEMCOUNT, 0, 0);
        }
        /// <summary>
        /// 获取ListView标题栏句柄
        /// </summary>
        /// <param name="AHandle"></param>
        /// <returns></returns>
        private IntPtr ListView_GetHeader(IntPtr AHandle)
        {
            return (IntPtr)WinAPI.SendMessage(AHandle, LVM_GETHEADER, 0, 0);
        }
        private bool ListView_GetItemPosition(IntPtr AHandle, int AIndex, IntPtr APoint)
        {
            return WinAPI.SendMessage(AHandle, LVM_GETITEMPOSITION, AIndex, APoint.ToInt32()) != 0;
        }
        private bool ListView_RedrawItems(IntPtr AHandle, int iFirst, int iLast)
        {
            return WinAPI.SendMessage(AHandle, LVM_REDRAWITEMS, iFirst, iLast) != 0;
        }
        // 矩形结构
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            int left;
            int top;
            int right;
            int bottom;
        }
        public struct LVITEM
        {
            public int mask;
            public int iItem;
            public int iSubItem;
            public int state;
            public int stateMask;
            public IntPtr pszText; // string
            public int cchTextMax;
            public int iImage;
            public IntPtr lParam;
            public int iIndent;
            public int iGroupId;
            public int cColumns;
            public IntPtr puColumns;

        }
    }
}
