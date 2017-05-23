using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;

namespace JHDock
{
    ///  2014-1-14 陈宝栋修改  
    ///   这个类可以让你得到一个在运行中程序的所有鼠标事件
    ///   并且引发一个带MouseEventArgs参数的.NET鼠标事件以便你很容易使用这些信息
    public class MouseHook
    {
        private int mouseX1 = 0;
        private int mouseX2 = 0;
        public bool timeEnabled=false;
        MoveDeskIcon moveDeskIcon = null;
        private const int WM_MOUSEMOVE = 0x200;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_RBUTTONDOWN = 0x204;
        private const int WM_MBUTTONDOWN = 0x207;
        private const int WM_LBUTTONUP = 0x202;
        private const int WM_RBUTTONUP = 0x205;
        private const int WM_MBUTTONUP = 0x208;
        private const int WM_LBUTTONDBLCLK = 0x203;
        private const int WM_RBUTTONDBLCLK = 0x206;
        private const int WM_MBUTTONDBLCLK = 0x209;
        //全局的事件
        public event MouseEventHandler OnMouseActivity;
        static int hMouseHook = 0;   //鼠标钩子句柄
        //鼠标常量
        public const int WH_MOUSE_LL = 14;   //mouse   hook   constant
        HookProc MouseHookProcedure;   //声明鼠标钩子事件类型.
        //声明一个Point的封送类型
        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int x;
            public int y;
        }
        //声明鼠标钩子的封送结构类型
        [StructLayout(LayoutKind.Sequential)]
        public class MouseHookStruct
        {
            public POINT pt;
            public int hWnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }
        //装置钩子的函数
        [DllImport("user32.dll ", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        //卸下钩子的函数
        [DllImport("user32.dll ", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        //下一个钩挂的函数
        [DllImport("user32.dll ", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);
        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        public void Start(MoveDeskIcon moveDeskIcon)
        {
            //安装鼠标钩子
            if (hMouseHook == 0)
            {
                //生成一个HookProc的实例.
                MouseHookProcedure = new HookProc(MouseHookProc);
                IntPtr intp = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);
                hMouseHook = SetWindowsHookEx(WH_MOUSE_LL, MouseHookProcedure, intp, 0);
                this.moveDeskIcon = moveDeskIcon;
                //如果装置失败停止钩子
                if (hMouseHook == 0)
                {
                    Stop();
                    throw new Exception("SetWindowsHookEx failed. ");
                }
            }
        }
        public void Stop()
        {
            bool retMouse = true;
            if (hMouseHook != 0)
            {
                retMouse = UnhookWindowsHookEx(hMouseHook);
                hMouseHook = 0;
            }

            //如果卸下钩子失败
            if (!(retMouse)) throw new Exception("UnhookWindowsHookEx   failed. ");
        }
        private int MouseHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            //如果正常运行并且用户要监听鼠标的消息
            //MouseButtons button = MouseButtons.None;
            MouseHookStruct MyMouseHookStruct = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));
            switch (wParam)
            {
                case WM_LBUTTONDOWN:
                    mouseX1= MyMouseHookStruct.pt.x;
                    timeEnabled = false;
                    break;
                case WM_LBUTTONUP:
                    mouseX2 = MyMouseHookStruct.pt.x;
                    if (Math.Abs(mouseX1 - mouseX2)>60)
                    {
                        timeEnabled = false;
                    }
                    else
                    {
                        timeEnabled = true;
                    }
                    break;
            }
            return CallNextHookEx(hMouseHook, nCode, wParam, lParam);
        }
    }
}
