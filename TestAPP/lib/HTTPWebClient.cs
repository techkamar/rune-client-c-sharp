using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;

namespace TestAPP.lib
{
    public class HTTPWebClient
    {
        public static string sendPostJSON(String baseAddress, String parsedContent)
        {
            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(parsedContent);

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();
            return content;
        }

        public static string sendFileToServer(String baseAddress, String fullFilePath)
        {
            var wc = new WebClient();
            byte[] response = wc.UploadFile(baseAddress, "POST", fullFilePath);
            string s = System.Text.Encoding.ASCII.GetString(response);
            Console.WriteLine(s);
            return s;
        }
    }
}
