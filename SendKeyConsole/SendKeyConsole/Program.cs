using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SendKeyConsole
{
    class Program
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        public static string filePath;
        public static DateTime StartTime = DateTime.Now;
        public static DateTime EndTime = DateTime.Now.AddHours(5);
        static void Main(string[] args)
        {
            Log startLog = new Log();

            startLog.LogText("!!UPPDATERING STARTAD!! \n");

            try
            {
                // Find the XML-document
                var xmlFile = XDocument.Load("FilePath.xml");
                // Parce out the path and execute the file
                if (xmlFile.Root == null) return;
                filePath = xmlFile.Root.Value;
            }
            catch (Exception e)
            {
                startLog.LogText("Fel vid XML-inläsningen \n" + e.Message);
                Environment.Exit(1);
            }

            try
            {
                Process.Start(filePath);
            }
            catch (Exception e)
            {

                startLog.LogText("Kunde inte hitta filen genom sökvägen i 'FilePath.xml' \n" + "SYSTEM MESSAGE:     " + e.Message);
                Environment.Exit(1);
            }

            // Sleep 3 min
            System.Threading.Thread.Sleep(180000);

            // Alt
            SendKeys.SendWait("%");
            System.Threading.Thread.Sleep(2000);

            // Right-arrow x3
            for (int i = 0; i < 3; i++)
            {
                SendKeys.SendWait("{RIGHT}");
                System.Threading.Thread.Sleep(2000);
            }

            // Down-arrow x2
            for (int i = 0; i < 2; i++)
            {
                SendKeys.SendWait("{DOWN}");
                System.Threading.Thread.Sleep(2000);
            }

            // Enter
            System.Threading.Thread.Sleep(2000);
            SendKeys.SendWait("{ENTER}");

            // Enter
            System.Threading.Thread.Sleep(2000);
            SendKeys.SendWait("{ENTER}");

            // Enter
            System.Threading.Thread.Sleep(2000);
            SendKeys.SendWait("{ENTER}");

            // Check for error messages
            while (true)
            {
                // Sleep 5 min
                System.Threading.Thread.Sleep(300000);

                var timeElapsed = DateTime.Now - StartTime;

                // Get "Error" window
                var error = Process.GetProcesses().FirstOrDefault(x => x.MainWindowTitle == "Error");

                // Check if "Error" window is up
                if (error != null)
                {
                    IntPtr h = error.MainWindowHandle;
                    SetForegroundWindow(h);
                    startLog.LogText("Stängde ner 'Error' rutan");
                    SendKeys.SendWait("{ENTER}");
                    break;
                }

                // Get "Lime EASY" window
                var limeEasy = Process.GetProcesses().FirstOrDefault(x => x.MainWindowTitle == "LIME Easy");

                // Check if "Lime EASY" window is up
                if (limeEasy != null)
                {
                    IntPtr h = limeEasy.MainWindowHandle;
                    SetForegroundWindow(h);
                    startLog.LogText("Stängde ner 'LIME Easy' rutan");
                    SendKeys.SendWait("{ENTER}");
                }

                if (timeElapsed < EndTime.TimeOfDay)
                {
                    startLog.LogText("!!UPPDATERING AVSLUTAD!!\n" + "-----------------------------------");
                    Environment.Exit(1);
                }
            }
        }
    }
}
