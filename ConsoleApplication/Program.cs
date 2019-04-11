using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using MobileAgent.AgentManager;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
            //IPAddress ipAddress2 = Dns.GetHostEntry("localhost").AddressList[1];
            //IPEndPoint ip = new IPEndPoint(ipAddress, 9989);
            //AgentPi agentPi = new AgentPi(1);
            //Console.WriteLine(agentPi.CalculPi(4));
            ////Agency agency = new Agency(ipAddress, 2222);
            ////agency.Activate();
            ////agency.Start();
            ////Agency agency2 = new Agency(ipAddress2, 3333);
            ////agency2.Activate();
            ////agency2.Start();
            Tutorial obj = new Tutorial();
            obj.ID = 1;
            obj.Name = ".Net";

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@"E:\ExampleNew.txt", FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, obj);
            stream.Close();

            stream = new FileStream(@"E:\ExampleNew.txt", FileMode.Open, FileAccess.Read);
            Tutorial objnew = (Tutorial)formatter.Deserialize(stream);

            Console.WriteLine(objnew.ID);
            Console.WriteLine(objnew.Name);

            Console.ReadKey();
        }
        
    }
    
}
