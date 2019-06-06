using MobileAgent.AgentManager;
using System;
using System.Collections.Generic;
using System.Net;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentRemote : Agent
    {
        public List<string> agenciesVisited =  new List<string>();
        public Queue<string> queue = new Queue<string>();
        #region Constructor
        public AgentRemote()
        {
            Parameters = new List<string>();
            this.SetName("AgentRemote");
            this.SetAgentInfo("Collect information from network");            
        }
        #endregion Constructor

        #region Properties
        public List<string> Parameters { get;  set; }
        public Dictionary<IPAddress, Tuple<string, int, string[]>> GetHosts { get; private set; }
        public Dictionary<IPAddress, Tuple<string, int, string[]>> SetHosts
        {
            set
            {
                GetHosts = value;
            }
        }
        #endregion Properties

        #region Private Methods
        private string GetInfo(string parameter)
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
            string info = "";
            foreach(string i in Parameters)
            {
                topics.Add(GetInfo(i));
                info += GetInfo(i) + Environment.NewLine;
                Console.WriteLine(GetInfo(i));
            }
            AgencyContext agentProxy = this.GetAgentCurrentContext();
            this.SetAgentCodebase(info);
        }
        private void CollectInfo()
        {
          
            AgencyContext agencyContext = this.GetAgentCurrentContext();
            string s = queue.Dequeue();
            foreach (string n in agencyContext.GetNeighbours())
            {
                if (!agenciesVisited.Contains(n))
                {
                    queue.Enqueue(n);
                    agenciesVisited.Add(n);
                }
            }
            if (queue.Count != 0)
            {

                string next = queue.Peek();
                IPAddress ipAddress = AgencyForm._configParser.GetIPAdress(next);
                int portNumber = AgencyForm._configParser.GetPort(next);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
                this.SetAgentCodebase("AgentRemote");
                agencyContext.Dispatch(this, ipEndPoint);
            }
            else
            {
                SetAgentCodebase("Stop");
            }
        }
        #endregion Private Methods

        #region Public Methods
        public override void Run()
        {
            CollectInfo();
        }
        #endregion Public Methods

    }
}
