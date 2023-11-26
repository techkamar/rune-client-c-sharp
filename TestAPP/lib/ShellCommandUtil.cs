using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace TestAPP.lib
{
    public class ShellCommandUtil
    {
        private static void writeFile(String content)
        {
            using (StreamWriter writer = new StreamWriter("D:\\123.json"))
            {
                writer.WriteLine(content);
            }
        }
        private static String getCommandOutputString(String command)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/C "+command;
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
            return output;
        }
        // MAIN METHOD
        public static String getCommandOutputJSON(String mac, String command)
        {
            String commandOutput = getCommandOutputString(command);
            commandOutput = commandOutput.Replace("\n", "<br>");
            commandOutput = commandOutput.Replace("\r", "");
            commandOutput = commandOutput.Replace("\\", "/");
            commandOutput = commandOutput.Replace("<DIR>", "");

            JSONHolder json = new JSONHolder();
            json.addStringEntry("mac", mac);
            json.addStringEntry("content", commandOutput);
            
            String jsonResponse = json.getParsedJSONString();
            writeFile(jsonResponse);
            return jsonResponse;
        }
    }
}
