using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace TestAPP.lib
{
    internal class SysInfoGrabber
    {
        public static string getWinVersionNumber()
        {
            var info = Environment.OSVersion.Version;
            return String.Format("Windows Version: {0}.{1}.{2}", info.Major, info.Minor, info.Build);
        }
        public static String getMacAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                return adapter.GetPhysicalAddress().ToString();
            }
            return "";
        }
        public static SystemInfo getSystemInfo()
        {
            string hostName = Dns.GetHostName();
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string operatingSystem = SysInfoGrabber.getWinVersionNumber();
            string macAddress = SysInfoGrabber.getMacAddress();

            SystemInfo sysInfo = new SystemInfo(userName, macAddress, hostName, operatingSystem);
            return sysInfo;
        }
    }
}
