using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    public class YourIp
    {
        public string GetIp()
        {
            string localIp = "?";
            IPHostEntry myHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in myHostEntry.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIp = ip.ToString();
                }
            }
            return localIp;
        }
    }
}
