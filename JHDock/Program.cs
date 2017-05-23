using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Configuration;

namespace JHDock
{
    public class Program
    {
        [STAThread]
        public static void Main() 
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (process.MainModule.FileName
                    == current.MainModule.FileName)
                    {
                        MessageBox.Show("已经打开一个单点登录程序，不能再打开！");
                        return;
                    }
                }
            }
           
            frmLogin login = new frmLogin();
            if (login.ShowDialog() == DialogResult.OK) 
            {
                try
                {
                    GC.Collect();
                    //DockWindow doc = new DockWindow();
                    PublicVariableModel.lockScreenTime = ConfigurationManager.ConnectionStrings["lockScreenTime"].ConnectionString;
                    if (ConfigurationManager.ConnectionStrings["MainModel"].ConnectionString == "QQ")
                    {
                        FrmMainQQ doc = FrmMainQQ.GetInstance();
                        doc.Left = 0;
                        Application.Run(doc);
                    }
                    if (ConfigurationManager.ConnectionStrings["MainModel"].ConnectionString == "Tile")
                    {
                        FrmMainTile doc =new  FrmMainTile();
                        doc.Left = 0;
                        Application.Run(doc);
                    }
                    
                }
                catch (Exception)
                {
                    Console.WriteLine("server is test");
                }
                

            }
        }
        
       
        private static DockWindow transDefaultFormDockWindow = null;
        public static DockWindow TransDefaultFormDockWindow
        {
            get
            {
                if (transDefaultFormDockWindow == null)
                {
                    transDefaultFormDockWindow = new DockWindow();
                }
                return transDefaultFormDockWindow;
            }
        }
    }
    
}
