using MobileAgent.AgentManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentRemote : Agent
    {
        List<string> _parameters;
        Dictionary<IPAddress, Tuple<string, int, string[]>> _hosts;
        public AgentRemote()
        {
            _parameters = new List<string>();
            this.SetAgentInfo("Collect information from network");
        }
        public List<string> GetParameters
        {
            get
            {
                return _parameters;
            }
        }
        public Dictionary<IPAddress, Tuple<string, int, string[]>> GetHosts
        {
            get
            {
                return _hosts;
            }
        }
        public List<string>  SetParameters
        {
            set
            {
                _parameters = value;
            }
        }
        public Dictionary<IPAddress, Tuple<string, int, string[]>> SetHosts
        {
            set
            {
                _hosts = value;
            }
        }
        public string GetInfo(string parameter)
        {
            string info = "";
            switch(parameter)
            {
                case "Sistem de operare":
                    {
                        info = "OperatingSystem";
                        break;
                    }
                case "Arhitectura sistem de operare":
                    {
                        info = "OperatingSystemArchitecture";
                        break;
                    }
                case "Service Pack sistem de operare":
                    {
                        info = "OperatingSystemServicePack";
                        break;
                    }
                case "Informatii procesor":
                    {
                        info = "Processor";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return info;
        }
        private void RunNetwork()
        {
            List<string> topics = new List<string>();
            foreach(string i in _parameters)
            {
                topics.Add(GetInfo(i));
                Console.WriteLine(GetInfo(i));
            }
        }
        public override void Run()
        {
            RunNetwork();
        }

    }
}
