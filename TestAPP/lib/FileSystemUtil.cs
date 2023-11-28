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
            if (FileSystemUtil.isRootDriveLetterDirPath(directory))
            {
                // If Drive Char is given example 
                // C
                directory+="\\";
            }
            string rootPath = directory;
            Console.WriteLine("Root path is "+rootPath);
           
            String[] sub_directories = Directory.GetDirectories(rootPath);

            // Get all the paths of sub directories 
            // present in vignan
            for (int i = 0; i < sub_directories.Length; i++)
            {
                String subDirectory = sub_directories[i].Substring(directory.Length);
                if (subDirectory.StartsWith("\\"))
                {
                    subDirectory = subDirectory.Substring(1);
                }
                directories.Add(subDirectory);
                Console.WriteLine(subDirectory);
            }
            return directories;
        }
        private static Boolean isRootDriveLetterDirPath(string path)
        {
            return path.Length == 2 && path.Substring(1) == ":";
        }
        private static ArrayList getFiles(string directory)
        {
            ArrayList files = new ArrayList();
            if (directory == "ROOT")
            {
                return files;
            }

            // For Drive based data
            if (FileSystemUtil.isRootDriveLetterDirPath(directory))
            {
                // If Drive Char is given example 
                // C:
                directory += "\\";
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
            String workingDir = command;
            command = command.Replace("/", "\\");
            Console.WriteLine("New Command is "+command);

            ArrayList directories = getSubDirectories(command);
            ArrayList files = FileSystemUtil.getFiles(command);
            
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
