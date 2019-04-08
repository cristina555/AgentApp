using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using MobileAgent.AgentManager;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
            IPAddress ipAddress2 = Dns.GetHostEntry("localhost").AddressList[1];
            IPEndPoint ip = new IPEndPoint(ipAddress, 9989);

            Agency agency = new Agency(ipAddress, 2222);
            agency.Activate();
            agency.Start();
            Agency agency2 = new Agency(ipAddress2, 3333);
            agency2.Activate();
            agency2.Start();
        }
        
    }
    
}
