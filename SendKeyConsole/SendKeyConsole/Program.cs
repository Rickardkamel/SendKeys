using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendKeyConsole
{
    class Program
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);
        static void Main(string[] args)
        {
            //SendKeys.SendWait("^" + "%" + "{RIGHT}");
            //SendKeys.SendWait("^" + "%" + "{DOWN}");
            //SendKeys.SendWait("^" + "%" + "{LEFT}");
            //SendKeys.SendWait("^" + "%" + "{UP}");

            SendKeys.SendWait("^" + "{ESC}");
            System.Threading.Thread.Sleep(500);
            SendKeys.SendWait("Notepad");
            System.Threading.Thread.Sleep(500);
            SendKeys.SendWait("{ENTER}");
            System.Threading.Thread.Sleep(500);
            SendKeys.SendWait("%");

            for (int i = 0; i < 4; i++)
            {
                System.Threading.Thread.Sleep(500);
                SendKeys.SendWait("{RIGHT}");
            }

            for (int i = 0; i < 2; i++)
            {
                System.Threading.Thread.Sleep(500);
                SendKeys.SendWait("{DOWN}");
            }

            System.Threading.Thread.Sleep(500);
            SendKeys.SendWait("{ENTER}");
            System.Threading.Thread.Sleep(500);
            SendKeys.SendWait("{ENTER}");

            System.Threading.Thread.Sleep(500);
            SendKeys.SendWait("Command completed!");

            while (true)
            {
                System.Threading.Thread.Sleep(10000);
                var pp = Process.GetProcesses().FirstOrDefault(x => x.MainWindowTitle == "Command Prompt");
                if (pp != null)
                {
                    IntPtr h = pp.MainWindowHandle;
                    SetForegroundWindow(h);

                    SendKeys.SendWait("{ENTER}");
                    break;
                }
            }

            //Process[] processlist = Process.GetProcesses();

            //foreach (Process process in processlist)
            //{
            //    if (!String.IsNullOrEmpty(process.MainWindowTitle))
            //    {
            //        Console.WriteLine("Process: {0} ID: {1} Window title: {2}", process.ProcessName, process.Id, process.MainWindowTitle);
            //    }
            //}



            Console.ReadKey();
        }
    }
}
