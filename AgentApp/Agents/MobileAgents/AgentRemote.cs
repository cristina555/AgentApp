using MobileAgent.AgentManager;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentRemote : Agent
    {
        #region Public Fields
        public List<string> agenciesVisited =  new List<string>();
        public Queue<string> queue = new Queue<string>();
        Dictionary<string, String> _info = null; 
        #endregion Public Fields

        #region Constructor
        public AgentRemote()
        {
            Parameters = new List<string>();
            this.SetName("AgentRemote");
            this.SetAgentInfo("Collect information from network");
            _info = new Dictionary<string, String>();
        }
        #endregion Constructor

        #region Properties
        public List<string> Parameters { get;  set; }
        #endregion Properties

        #region Private Methods
        private string GetInfo(string parameter)
        {
            string type = "";
            switch(parameter)
            {
                case "Sistem de operare":
                    {
                        type = "AgentOS";
                        break;
                    }
                case "Arhitectura sistem de operare":
                    {
                        type = "AgentOSA";
                        break;
                    }
                case "Service Pack sistem de operare":
                    {
                        type = "AgentOSSP";
                        break;
                    }
                case "Informatii procesor":
                    {
                        type = "AgentP";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return type;
        }
        private void RunNetwork()
        {
            List<string> topics = new List<string>();
            foreach(string i in Parameters)
            {
                topics.Add(GetInfo(i));                
            }
            
        }
        private void CollectInfo()
        {
            String information = "";
            AgencyContext agencyContext = GetAgentCurrentContext();
            agencyContext.SetLastRunnableAgent(this);
            if (queue.Count != 0)
            {
                
                foreach (string par in Parameters)
                {
                    AgentProxy agentStatic = agencyContext.GetStationaryAgent(GetInfo(par));
                    agentStatic.Run();
                    if (!_info.ContainsKey(agencyContext.GetName()))
                    {
                        _info.Add(agencyContext.GetName(), agentStatic.GetAgentCodebase());
                    }
                    else
                    {
                        _info[agencyContext.GetName()] += agentStatic.GetAgentCodebase();
                    }
                    information += agentStatic.GetAgentCodebase();

                }
                SetAgentCodebase(GetAgentCodebase() + agencyContext.GetName() + ": " + Environment.NewLine + information + Environment.NewLine);

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
                    IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(next);
                    int portNumber = AgencyForm.configParser.GetPort(next);
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
                    agencyContext.Dispatch(this, ipEndPoint);
                }
                else
                {
                    agenciesVisited.Clear();
                    agencyContext.Dispatch(this, GetAgencyCreationContext());
                }
            }
        }
        #endregion Private Methods

        #region Public Methods
        public override void Run()
        {
            CollectInfo();
            //foreach (string s in _info.Keys)
            //{
            //    SetAgentCodebase(GetAgentCodebase() + s + ": " + _info[s] + Environment.NewLine);
            //}
            //_info.Clear();
        }
        #endregion Public Methods

    }
}
