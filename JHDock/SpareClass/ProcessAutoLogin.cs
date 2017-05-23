﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace JHDock.Service
{
    //2014-1-13 陈宝栋 创建
    //指定程序自动登录类
    public class ProcessAutoLogin
    {
        //SendMessage参数
        const int WM_KEYDOWN = 0x0100;//普通按键按下
        const int WM_KEYUP = 0x0101;//普通按键放开
        const int WM_SYSKEYDOWN = 0x104;//系统按键按下
        const int WM_SYSKEYUP = 0x105;//系统按键按下放开
        const int WM_SYSCHAR = 0x0106;//发送单个字符
        const int WM_SETTEXT = 0x000C;//发送文本
        const int WM_CLICK = 0x00F5;//模拟鼠标左键点击
        const int WM_SETFOCUS = 0x0007;//设置焦点
        const int SW_RESTORE = 9;//恢复最小化的窗口
        static IntPtr mainHwnd = IntPtr.Zero;//主窗口句柄
        public static void AutoLogin(string qq, string passwd, string strLpszClass, string strLpszWindow, string strRegeditPath,string InputLpszClass)
        {
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(strRegeditPath, true);
            string qq2013Path = regKey.GetValue("TypePath").ToString();
            regKey.Close();
            if (!string.IsNullOrEmpty(qq2013Path))
            {
                Process.Start(qq2013Path);
                mainHwnd = WinAPI.FindWindow(strLpszClass, strLpszWindow);
                if (mainHwnd != IntPtr.Zero)
                {
                    Rectangle rectMain = new Rectangle();
                    WinAPI.GetWindowRect(mainHwnd.ToInt32(), ref rectMain);
                    if (rectMain.Height - rectMain.Y != 250)
                    {

                        Thread.Sleep(500);
                        while (true)
                        {
                            mainHwnd = WinAPI.FindWindow(strLpszClass, strLpszWindow);
                            if (mainHwnd != IntPtr.Zero)
                                break;
                            Thread.Sleep(100);
                        }
                    }
                    else
                    {
                        MessageBox.Show("未能找到QQ2013安装目录，请手动打开QQ程序！");
                        return;
                    }
                }
            }
            WinAPI.ShowWindow(mainHwnd, SW_RESTORE);//将最小化窗口还原
            WinAPI.SetForegroundWindow(mainHwnd);
            Thread.Sleep(500);

            //获取账号框句柄
            IntPtr userHwnd = WinAPI.FindWindowEx(mainHwnd, IntPtr.Zero, InputLpszClass, "");
            //SendMessage(passHwnd, WM_SETFOCUS, 0, 0);//设置焦点到密码框，发送tab键不能转到密码框

            Rectangle rect = new Rectangle();
            WinAPI.GetWindowRect(mainHwnd.ToInt32(), ref rect);//获取文本框居中相对屏幕中的位置
            //模拟鼠标左键点击事件，将Tab还原到文本框，WM_SETFOCUS不能还原Tab
            SendMouseLeftClick((int)MOUSEEVENTF.LEFTDOWN, (int)MOUSEEVENTF.LEFTUP, rect.Left + 5, rect.Height - 10);

            //删除原来qq号码
            WinAPI.SendMessage(userHwnd, WM_SYSKEYDOWN, VK_Delete, 0);
            WinAPI.SendMessage(userHwnd, WM_SYSKEYUP, VK_Delete, 0);

            WinAPI.SendMessage(userHwnd, WM_SETTEXT, IntPtr.Zero, qq);//发送qq号码 
            Thread.Sleep(10);
            //发送tab键,两种方法
            WinAPI.SendMessage(userHwnd, WM_KEYDOWN, VK_TAB, 0);//发送tab
            WinAPI.SendMessage(userHwnd, WM_KEYUP, VK_TAB, 0);//释放tab 
            //WindowsAPI.SendSingleKey(WindowsAPI.VK_TAB);
            Thread.Sleep(10);
            SendDelCode(10);//删除原来记住的密码 
            SendNoUnicode(passwd);
            Thread.Sleep(500);
            //发送回车键
            //PostMessage(mainHwnd, WM_KEYDOWN, 13, 0);
            //PostMessage(mainHwnd, WM_KEYUP, 13, 0);
            WinAPI.SendMessage(userHwnd, WM_KEYDOWN, 0x0D, 0);
        }

        public enum InputType
        {
            INPUT_MOUSE = 0,
            INPUT_KEYBOARD = 1,
            INPUT_HARDWARE = 2,
        }
        [Flags()]
        public enum MOUSEEVENTF
        {
            MOVE = 0x0001, //mouse move
            LEFTDOWN = 0x0002, //left button down
            LEFTUP = 0x0004, //left button up
            RIGHTDOWN = 0x0008, //right button down
            RIGHTUP = 0x0010, //right button up
            MIDDLEDOWN = 0x0020, //middle button down
            MIDDLEUP = 0x0040, //middle button up
            XDOWN = 0x0080, //x button down
            XUP = 0x0100, //x button down
            WHEEL = 0x0800, //wheel button rolled
            VIRTUALDESK = 0x4000, //map to entire virtual desktop
            ABSOLUTE = 0x8000, //absolute move
        }

        ///<summary>
        /// 模拟鼠标左键点击
        ///</summary>
        public static void SendMouseClick()
        {
            WinAPI.INPUT input_down = new WinAPI.INPUT();
            input_down.mi.dx = 0;
            input_down.mi.dy = 0;
            input_down.mi.mouseData = 0;
            input_down.mi.dwFlags = (int)MOUSEEVENTF.LEFTDOWN;
            WinAPI.SendInput(1, ref input_down, Marshal.SizeOf(input_down));

            WinAPI.INPUT input_up = input_down;
            input_up.mi.dwFlags = (int)MOUSEEVENTF.LEFTUP;
            WinAPI.SendInput(1, ref input_up, Marshal.SizeOf(input_up));
        }

        ///<summary>
        /// 在x,y位置模拟鼠标点击
        ///</summary>
        ///<param name="dwFlagsDown">MOUSEEVENTF</param>
        ///<param name="dwFlagsUp">MOUSEEVENTF</param>
        ///<param name="x">点击的x坐标</param>
        ///<param name="y">点击的y坐标</param>
        public static void SendMouseLeftClick(int dwFlagsDown, int dwFlagsUp, int x, int y)
        {
            WinAPI.INPUT[] input = new WinAPI.INPUT[2];
            WinAPI.MOUSEINPUT mouseinput_down = new WinAPI.MOUSEINPUT();
            WinAPI.MOUSEINPUT mouseinput_up = new WinAPI.MOUSEINPUT();

            mouseinput_down.dwFlags = dwFlagsDown;
            mouseinput_up.dwFlags = dwFlagsUp;
            input[0].type = (int)InputType.INPUT_MOUSE;
            input[0].mi = mouseinput_down;
            input[1].type = (int)InputType.INPUT_MOUSE;
            input[1].mi = mouseinput_up;
            WinAPI.SetCursorPos(x, y);
            WinAPI.SendInput(1, input, Marshal.SizeOf(input[0]));//down
            WinAPI.SendInput(1, input, Marshal.SizeOf(input[1]));//up
        }

        ///<summary>
        /// 发送单一按键，如Tab,Shift,Home,End
        ///</summary>
        ///<param name="wVk">VK_TAB,VK_SHIFT</param>
        public static void SendSingleKey(int wVk)
        {
            WinAPI.INPUT input_down = new WinAPI.INPUT();
            input_down.type = (int)InputType.INPUT_KEYBOARD;
            input_down.ki.dwFlags = 0;
            input_down.ki.wVk = (short)wVk;
            WinAPI.SendInput(1, ref input_down, Marshal.SizeOf(input_down));//keydown

            WinAPI.INPUT input_up = new WinAPI.INPUT();
            input_up.type = (int)InputType.INPUT_KEYBOARD;
            input_up.ki.wVk = (short)wVk;
            input_up.ki.dwFlags = (int)WinAPI.KEYEVENTF.KEYUP;
            WinAPI.SendInput(1, ref input_up, Marshal.SizeOf(input_up));//keyup 
        }

        ///<summary>
        /// 发送组合键
        ///</summary>
        ///<param name="wVk">VK_SHIFT，VK_TAB;如Shift+A，SendNoUnicodeByParam(VK_SHIFT,Keys.A)</param>
        public static void SendKeyCombination(int wVk, Keys key)
        {
            WinAPI.INPUT[] inDown = new WinAPI.INPUT[4];
            inDown[0] = new WinAPI.INPUT();
            inDown[1] = new WinAPI.INPUT();
            inDown[2] = new WinAPI.INPUT();
            inDown[3] = new WinAPI.INPUT();
            inDown[0].type = inDown[1].type = inDown[2].type = inDown[3].type = (int)InputType.INPUT_KEYBOARD;
            inDown[0].ki.wVk = inDown[2].ki.wVk = (short)wVk;
            inDown[1].ki.wVk = inDown[3].ki.wVk = (short)key;
            inDown[0].ki.dwFlags = inDown[1].ki.dwFlags = 0;
            inDown[2].ki.dwFlags = inDown[3].ki.dwFlags = (int)WinAPI.KEYEVENTF.KEYUP;
            WinAPI.SendInput(1, ref inDown[0], Marshal.SizeOf(inDown[0]));//down
            WinAPI.SendInput(1, ref inDown[1], Marshal.SizeOf(inDown[1]));//char down
            WinAPI.SendInput(1, ref inDown[2], Marshal.SizeOf(inDown[2]));//up
            WinAPI.SendInput(1, ref inDown[3], Marshal.SizeOf(inDown[3]));//char up
        }

        ///<summary>
        /// 发送Del键
        ///</summary>
        ///<param name="count">发送Del键个数</param>
        public static void SendDelCode(int count)
        {
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    WinAPI.INPUT clear_down = new WinAPI.INPUT();
                    clear_down.type = (int)InputType.INPUT_KEYBOARD;
                    clear_down.ki.dwFlags = 0;
                    clear_down.ki.wVk = VK_BACK;
                    WinAPI.SendInput(1, ref clear_down, Marshal.SizeOf(clear_down));//down
                    WinAPI.INPUT clear_up = new WinAPI.INPUT();
                    clear_up.type = (int)InputType.INPUT_KEYBOARD;
                    clear_up.ki.wVk = VK_BACK;
                    clear_up.ki.dwFlags = (int)WinAPI.KEYEVENTF.KEYUP;
                    WinAPI.SendInput(1, ref clear_up, Marshal.SizeOf(clear_up));//keyup
                }
            }
        }

        ///<summary>
        /// 发送unicode字符，可发送任意字符(包括汉字)，但不能作用于qq上
        ///</summary>
        ///<param name="message">发送的字符串</param>
        public static void SendUnicode(string message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                WinAPI.INPUT input_down = new WinAPI.INPUT();
                input_down.type = (int)InputType.INPUT_KEYBOARD;
                input_down.ki.dwFlags = (int)WinAPI.KEYEVENTF.UNICODE;
                input_down.ki.wScan = (short)message[i];
                WinAPI.SendInput(1, ref input_down, Marshal.SizeOf(input_down));//keydown

                WinAPI.INPUT input_up = new WinAPI.INPUT();
                input_up.type = (int)InputType.INPUT_KEYBOARD;
                input_up.ki.wScan = (short)message[i];
                input_up.ki.dwFlags = (int)(WinAPI.KEYEVENTF.KEYUP | WinAPI.KEYEVENTF.UNICODE);
                WinAPI.SendInput(1, ref input_up, Marshal.SizeOf(input_up));//keyup 
            }
        }

        ///<summary>
        /// 发送非unicode字符
        ///</summary>
        ///<param name="message">发送的字符串</param>
        public static void SendNoUnicode(string message)
        {
            string lower = "abcdefghijklmnopqrstuvwxyz+-*/. []\\;',.`";//密码是小写字母和+ - * / .[]\\;',.`和空格
            string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";//密码是大写字母
            string other = "~!@#$%^&()_{}|:\"<>?=";

            for (int i = 0; i < message.Length; i++)
            {
                short sendChar = 0;

                if (other.IndexOf(message[i].ToString()) > -1)
                {
                    sendChar = (short)getKeysByChar2(message[i]);
                    WinAPI.INPUT[] inDown = new WinAPI.INPUT[4];
                    inDown[0] = new WinAPI.INPUT();
                    inDown[1] = new WinAPI.INPUT();
                    inDown[2] = new WinAPI.INPUT();
                    inDown[3] = new WinAPI.INPUT();
                    inDown[0].type = inDown[1].type = inDown[2].type = inDown[3].type = (int)InputType.INPUT_KEYBOARD;
                    inDown[0].ki.wVk = inDown[2].ki.wVk = (int)VK_SHIFT;
                    inDown[1].ki.wVk = inDown[3].ki.wVk = sendChar;
                    inDown[0].ki.dwFlags = inDown[1].ki.dwFlags = 0;
                    inDown[2].ki.dwFlags = inDown[3].ki.dwFlags = (int)WinAPI.KEYEVENTF.KEYUP;
                    WinAPI.SendInput(1, ref inDown[0], Marshal.SizeOf(inDown[0]));//shift down
                    WinAPI.SendInput(1, ref inDown[1], Marshal.SizeOf(inDown[1]));//char down
                    WinAPI.SendInput(1, ref inDown[2], Marshal.SizeOf(inDown[2]));//shift up
                    WinAPI.SendInput(1, ref inDown[3], Marshal.SizeOf(inDown[3]));//char up
                    continue;
                }
                else if (upper.IndexOf(message[i].ToString()) > -1)//如果是大写字母，和Shift一起发送
                {
                    sendChar = (short)getKeysByChar(message[i]);
                    //上面发送组合键不能作用于QQ,QQ必须一个一个的发送
                    WinAPI.INPUT[] inDown = new WinAPI.INPUT[4];
                    inDown[0] = new WinAPI.INPUT();
                    inDown[1] = new WinAPI.INPUT();
                    inDown[2] = new WinAPI.INPUT();
                    inDown[3] = new WinAPI.INPUT();
                    inDown[0].type = inDown[1].type = inDown[2].type = inDown[3].type = (int)InputType.INPUT_KEYBOARD;
                    inDown[0].ki.wVk = inDown[2].ki.wVk = (int)VK_SHIFT;
                    inDown[1].ki.wVk = inDown[3].ki.wVk = sendChar;
                    inDown[0].ki.dwFlags = inDown[1].ki.dwFlags = 0;
                    inDown[2].ki.dwFlags = inDown[3].ki.dwFlags = (int)WinAPI.KEYEVENTF.KEYUP;
                    WinAPI.SendInput(1, ref inDown[0], Marshal.SizeOf(inDown[0]));//shift down
                    WinAPI.SendInput(1, ref inDown[1], Marshal.SizeOf(inDown[1]));//char down
                    WinAPI.SendInput(1, ref inDown[2], Marshal.SizeOf(inDown[2]));//shift up
                    WinAPI.SendInput(1, ref inDown[3], Marshal.SizeOf(inDown[3]));//char up
                    continue;
                }
                else if (lower.IndexOf(message[i].ToString()) > -1)//小写字母
                {
                    sendChar = (short)getKeysByChar(message[i]);
                }
                else
                {
                    sendChar = (short)message[i];//数字
                }

                WinAPI.INPUT input_down = new WinAPI.INPUT();
                input_down.type = (int)InputType.INPUT_KEYBOARD;
                input_down.ki.dwFlags = 0;
                input_down.ki.wVk = sendChar;
                WinAPI.SendInput(1, ref input_down, Marshal.SizeOf(input_down));//keydown

                WinAPI.INPUT input_up = new WinAPI.INPUT();
                input_up.type = (int)InputType.INPUT_KEYBOARD;
                input_up.ki.wVk = sendChar;
                input_up.ki.dwFlags = (int)WinAPI.KEYEVENTF.KEYUP;
                WinAPI.SendInput(1, ref input_up, Marshal.SizeOf(input_up));//keyup 
            }
        }

        private static Keys getKeysByChar(char c)
        {
            string str = "abcdefghijklmnopqrstuvwxyz+-*/. []\\;',.`";
            int index = str.IndexOf(c.ToString().ToLower());
            switch (index)
            {
                case 0:
                    return Keys.A;
                case 1:
                    return Keys.B;
                case 2:
                    return Keys.C;
                case 3:
                    return Keys.D;
                case 4:
                    return Keys.E;
                case 5:
                    return Keys.F;
                case 6:
                    return Keys.G;
                case 7:
                    return Keys.H;
                case 8:
                    return Keys.I;
                case 9:
                    return Keys.J;
                case 10:
                    return Keys.K;
                case 11:
                    return Keys.L;
                case 12:
                    return Keys.M;
                case 13:
                    return Keys.N;
                case 14:
                    return Keys.O;
                case 15:
                    return Keys.P;
                case 16:
                    return Keys.Q;
                case 17:
                    return Keys.R;
                case 18:
                    return Keys.S;
                case 19:
                    return Keys.T;
                case 20:
                    return Keys.U;
                case 21:
                    return Keys.V;
                case 22:
                    return Keys.W;
                case 23:
                    return Keys.X;
                case 24:
                    return Keys.Y;
                case 25:
                    return Keys.Z;
                case 26:
                    return Keys.Add;
                case 27:
                    return Keys.Subtract;
                case 28:
                    return Keys.Multiply;
                case 29:
                    return Keys.Divide;
                case 30:
                    return Keys.Decimal;
                case 31:
                    return Keys.Space;
                case 32:
                    return Keys.Oem4;//[]\\;',.
                case 33:
                    return Keys.Oem6;
                case 34:
                    return Keys.Oem5;
                case 35:
                    return Keys.Oem1;
                case 36:
                    return Keys.Oem7;
                case 37:
                    return Keys.Oemcomma;
                case 38:
                    return Keys.Oemtilde;
                default:
                    return Keys.None;
            }
        }

        public static Keys getKeysByChar2(char c)
        {
            string str = "~!@#$%^&()_{}|:\"<>?=";
            int index = str.IndexOf(c.ToString().ToLower());
            switch (index)
            {
                case 0:
                    return Keys.Oemtilde;//~
                case 1:
                    return Keys.D1;//!
                case 2:
                    return Keys.D2;//@
                case 3:
                    return Keys.D3;//#
                case 4:
                    return Keys.D4;//$
                case 5:
                    return Keys.D5;//%
                case 6:
                    return Keys.D6;//^
                case 7:
                    return Keys.D7;//&
                case 8:
                    return Keys.D9;//(
                case 9:
                    return Keys.D0;//)
                case 10:
                    return Keys.OemMinus;//_
                case 11:
                    return Keys.Oem4;//{
                case 12:
                    return Keys.Oem6;//}
                case 13:
                    return Keys.Oem5;//|
                case 14:
                    return Keys.Oem1;//:
                case 15:
                    return Keys.Oem7;//"
                case 16:
                    return Keys.Oemcomma;//<
                case 17:
                    return Keys.OemPeriod;//>
                case 18:
                    return Keys.OemQuestion;//?
                case 19:
                    return Keys.Oemplus;//=
                default: return Keys.None;
            }
        }
        //Windows 使用的256个虚拟键码
        public const int VK_LBUTTON = 0x1;
        public const int VK_RBUTTON = 0x2;
        public const int VK_CANCEL = 0x3;
        public const int VK_MBUTTON = 0x4;
        public const int VK_BACK = 0x8;
        public const int VK_TAB = 0x9;
        public const int VK_CLEAR = 0xC;
        public const int VK_RETURN = 0xD;
        public const int VK_SHIFT = 0x10;
        public const int VK_CONTROL = 0x11;
        public const int VK_MENU = 0x12;
        public const int VK_PAUSE = 0x13;
        public const int VK_CAPITAL = 0x14;
        public const int VK_ESCAPE = 0x1B;
        public const int VK_SPACE = 0x20;
        public const int VK_PRIOR = 0x21;
        public const int VK_NEXT = 0x22;
        public const int VK_END = 0x23;
        public const int VK_HOME = 0x24;
        public const int VK_LEFT = 0x25;
        public const int VK_UP = 0x26;
        public const int VK_RIGHT = 0x27;
        public const int VK_DOWN = 0x28;
        public const int VK_Select = 0x29;
        public const int VK_PRINT = 0x2A;
        public const int VK_EXECUTE = 0x2B;
        public const int VK_SNAPSHOT = 0x2C;
        public const int VK_Insert = 0x2D;
        public const int VK_Delete = 0x2E;
        public const int VK_HELP = 0x2F;
        public const int VK_0 = 0x30;
        public const int VK_1 = 0x31;
        public const int VK_2 = 0x32;
        public const int VK_3 = 0x33;
        public const int VK_4 = 0x34;
        public const int VK_5 = 0x35;
        public const int VK_6 = 0x36;
        public const int VK_7 = 0x37;
        public const int VK_8 = 0x38;
        public const int VK_9 = 0x39;
        public const int VK_A = 0x41;
        public const int VK_B = 0x42;
        public const int VK_C = 0x43;
        public const int VK_D = 0x44;
        public const int VK_E = 0x45;
        public const int VK_F = 0x46;
        public const int VK_G = 0x47;
        public const int VK_H = 0x48;
        public const int VK_I = 0x49;
        public const int VK_J = 0x4A;
        public const int VK_K = 0x4B;
        public const int VK_L = 0x4C;
        public const int VK_M = 0x4D;
        public const int VK_N = 0x4E;
        public const int VK_O = 0x4F;
        public const int VK_P = 0x50;
        public const int VK_Q = 0x51;
        public const int VK_R = 0x52;
        public const int VK_S = 0x53;
        public const int VK_T = 0x54;
        public const int VK_U = 0x55;
        public const int VK_V = 0x56;
        public const int VK_W = 0x57;
        public const int VK_X = 0x58;
        public const int VK_Y = 0x59;
        public const int VK_Z = 0x5A;
        public const int VK_STARTKEY = 0x5B;
        public const int VK_CONTEXTKEY = 0x5D;
        public const int VK_NUMPAD0 = 0x60;
        public const int VK_NUMPAD1 = 0x61;
        public const int VK_NUMPAD2 = 0x62;
        public const int VK_NUMPAD3 = 0x63;
        public const int VK_NUMPAD4 = 0x64;
        public const int VK_NUMPAD5 = 0x65;
        public const int VK_NUMPAD6 = 0x66;
        public const int VK_NUMPAD7 = 0x67;
        public const int VK_NUMPAD8 = 0x68;
        public const int VK_NUMPAD9 = 0x69;
        public const int VK_MULTIPLY = 0x6A;
        public const int VK_ADD = 0x6B;
        public const int VK_SEPARATOR = 0x6C;
        public const int VK_SUBTRACT = 0x6D;
        public const int VK_DECIMAL = 0x6E;
        public const int VK_DIVIDE = 0x6F;
        public const int VK_F1 = 0x70;
        public const int VK_F2 = 0x71;
        public const int VK_F3 = 0x72;
        public const int VK_F4 = 0x73;
        public const int VK_F5 = 0x74;
        public const int VK_F6 = 0x75;
        public const int VK_F7 = 0x76;
        public const int VK_F8 = 0x77;
        public const int VK_F9 = 0x78;
        public const int VK_F10 = 0x79;
        public const int VK_F11 = 0x7A;
        public const int VK_F12 = 0x7B;
        public const int VK_F13 = 0x7C;
        public const int VK_F14 = 0x7D;
        public const int VK_F15 = 0x7E;
        public const int VK_F16 = 0x7F;
        public const int VK_F17 = 0x80;
        public const int VK_F18 = 0x81;
        public const int VK_F19 = 0x82;
        public const int VK_F20 = 0x83;
        public const int VK_F21 = 0x84;
        public const int VK_F22 = 0x85;
        public const int VK_F23 = 0x86;
        public const int VK_F24 = 0x87;
        public const int VK_NUMLOCK = 0x90;
        public const int VK_OEM_SCROLL = 0x91;
        public const int VK_OEM_1 = 0xBA;
        public const int VK_OEM_PLUS = 0xBB;
        public const int VK_OEM_COMMA = 0xBC;
        public const int VK_OEM_MINUS = 0xBD;
        public const int VK_OEM_PERIOD = 0xBE;
        public const int VK_OEM_2 = 0xBF;
        public const int VK_OEM_3 = 0xC0;
        public const int VK_OEM_4 = 0xDB;
        public const int VK_OEM_5 = 0xDC;
        public const int VK_OEM_6 = 0xDD;
        public const int VK_OEM_7 = 0xDE;
        public const int VK_OEM_8 = 0xDF;
        public const int VK_ICO_F17 = 0xE0;
        public const int VK_ICO_F18 = 0xE1;
        public const int VK_OEM102 = 0xE2;
        public const int VK_ICO_HELP = 0xE3;
        public const int VK_ICO_00 = 0xE4;
        public const int VK_ICO_CLEAR = 0xE6;
        public const int VK_OEM_RESET = 0xE9;
        public const int VK_OEM_JUMP = 0xEA;
        public const int VK_OEM_PA1 = 0xEB;
        public const int VK_OEM_PA2 = 0xEC;
        public const int VK_OEM_PA3 = 0xED;
        public const int VK_OEM_WSCTRL = 0xEE;
        public const int VK_OEM_CUSEL = 0xEF;
        public const int VK_OEM_ATTN = 0xF0;
        public const int VK_OEM_FINNISH = 0xF1;
        public const int VK_OEM_COPY = 0xF2;
        public const int VK_OEM_AUTO = 0xF3;
        public const int VK_OEM_ENLW = 0xF4;
        public const int VK_OEM_BACKTAB = 0xF5;
        public const int VK_ATTN = 0xF6;
        public const int VK_CRSEL = 0xF7;
        public const int VK_EXSEL = 0xF8;
        public const int VK_EREOF = 0xF9;
        public const int VK_PLAY = 0xFA;
        public const int VK_ZOOM = 0xFB;
        public const int VK_NONAME = 0xFC;
        public const int VK_PA1 = 0xFD;
        public const int VK_OEM_CLEAR = 0xFE;
    }
}
