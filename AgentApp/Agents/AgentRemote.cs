using MobileAgent.AgentManager;
using System;
using System.Collections.Generic;
using System.Net;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentRemote : Agent
    {
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
        #endregion Private Methods

        #region Public Methods
        public override void Run()
        {
            RunNetwork();
        }
        #endregion Public Methods

    }
}
