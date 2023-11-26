using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAPP.lib
{
    public class SystemInfo
    {
        public String username { get; set; }
        public String macAddress { get; set; }
        public String hostName { get; set; }
        public String operatingSystem { get; set; }

        public SystemInfo(String username, String macAddress, String hostName, String operatingSystem)
        {
            this.username = username;
            this.macAddress = macAddress;
            this.hostName = hostName;
            this.operatingSystem = operatingSystem;
        }
    }
}
