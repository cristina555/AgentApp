using System;
using System.Collections.Generic;
using MobileAgent.AgentManager;
using System.Net;
using System.Xml;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentInfo : Agent
    {
        #region Fields
        List<IPEndPoint> _agenciesVisited = null;
        string info = "" ;
        #endregion Fields

        #region Constructors
        public AgentInfo()
        {
            _agenciesVisited = new List<IPEndPoint>();
            this.SetName("AgentInfo");
            this.SetAgentInfo("Get the list of visited agencies");
        }
        public AgentInfo(int id) : base(id)
        {
             _agenciesVisited = new List<IPEndPoint>();
        }
        #endregion Constructors

        #region Private Methods
        private void AddAgency()
        {
            IPEndPoint ip = GetAgentCurrentContext().GetAgencyIPEndPoint(); 
            if (!_agenciesVisited.Contains(ip))
            {
                _agenciesVisited.Add(ip);
            }
        }
        private void ShowAgencies()
        {
            AddAgency();
            info = "Agentul a vizitat" + Environment.NewLine;
            foreach (IPEndPoint context in _agenciesVisited)
            {
                Console.WriteLine(context);
                info += context;
                info += Environment.NewLine;
            }
            this.SetAgentCodebase(info);
        }
        #endregion Private Methods

        #region Public Methods
        public override void Run()
        {
            ShowAgencies();
        }
        public override void GetUI()
        {
            XmlDocument xmlDoc = new XmlDocument();
        }
        #endregion Public Methods

    }
}
