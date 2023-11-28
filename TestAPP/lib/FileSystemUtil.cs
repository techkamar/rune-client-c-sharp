using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace TestAPP.lib
{
    internal class FileSystemUtil
    {
        private static ArrayList getSubDirectories(string directory)
        {
            ArrayList directories = new ArrayList();
            if (directory == "ROOT")
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (DriveInfo d in allDrives)
                {
                    String driveLetter = d.Name.Substring(0, d.Name.Length - 1);
                    directories.Add(driveLetter);
                }
                return directories;
            }

            // For Drive based data
            string rootPath = directory;
            string[] dirs = Directory.GetDirectories(rootPath, "*", SearchOption.TopDirectoryOnly);

            foreach (string dir in dirs)
            {
                directories.Add(dir);
            }
            return directories;
        }
        
        private static ArrayList getFiles(string directory)
        {
            ArrayList files = new ArrayList();
            if (directory == "ROOT")
            {
                return files;
            }
            DirectoryInfo d = new DirectoryInfo(@directory);

            FileInfo[] Files = d.GetFiles("*.*"); //Getting Text files

            foreach (FileInfo file in Files)
            {
                Hashtable myMap = new Hashtable();
                myMap.Add("name", file.Name);
                myMap.Add("size", file.Length);
                files.Add(myMap);
            }
            return files;

        }
        public static string getFilesNFolders(SystemInfo sysInfo, string command)
        {

            Console.WriteLine("Command is  " + command);
            ArrayList directories = getSubDirectories(command);
            ArrayList files = FileSystemUtil.getFiles(command);
            String workingDir = command;
            String mac = sysInfo.macAddress;

            JSONHolder json = new JSONHolder();
            json.addStringEntry("working_dir", workingDir);
            json.addStringEntry("mac", mac);
            json.addListEntry("directories", directories);
            json.addListEntry("files", files);

            return json.getParsedJSONString();


        }
    }
}
