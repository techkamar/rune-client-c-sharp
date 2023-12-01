using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestAPP.lib
{
    public class FileDownloadUtil
    {
        public static bool downloadFileFromURL(string url,string fileName) {
            try
            {
                String currentWorkingDirectory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                WebClient webClient = new WebClient();
                string downloadFullFilePath = currentWorkingDirectory + "\\" + fileName;
                webClient.DownloadFile(url, downloadFullFilePath);

                //File exists post download check
                if (!File.Exists(downloadFullFilePath))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            

        }

        public static void sendResponseToServer(string mac, string responseText)
        {
            JSONHolder payloadJSON = new JSONHolder();
            payloadJSON.addStringEntry("mac", mac);
            payloadJSON.addStringEntry("content", responseText);
            Console.WriteLine("Response = " + responseText);
            HTTPWebClient.sendPostJSON(Constants.API_SLAVE_TEXT_RESPONSE_URL, payloadJSON.getParsedJSONString());
        }
    }
}
