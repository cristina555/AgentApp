using System;
using System.Collections.Generic;
using MobileAgent.AgentManager;
using System.Net;
using System.Xml;
using MobileAgent.EventAgent;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentInfo : Agent
    {
        #region Private Static Fields
        private static List<IPEndPoint> _agenciesVisited = new List<IPEndPoint>();
        private static string info = "" ;
        #endregion Private Static Fields

        #region Constructors
        public AgentInfo() : base()
        {
            this.SetType(Agent.BOOMERANG);
            this.SetName("AgentInfo");
            this.SetAgentInfo("Preia lista agențiilor vizitate");
        }
        public AgentInfo(int id) : base(id)
        {
            this.SetType(Agent.BOOMERANG);
            this.SetName("AgentInfo");
            this.SetAgentInfo("Preia lista agențiilor vizitate");
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
        private void AddInformation()
        {
            AddAgency();
            info = "Agentul a vizitat" + Environment.NewLine;
            foreach (IPEndPoint context in _agenciesVisited)
            {
                Console.WriteLine(context);
                info += context;
                info += Environment.NewLine;
            }
            this.SetAgentStateInfo(info);
        }
        private void RunBoomerangAgent(AgencyContext agencyContext)
        {
            if (!GetAgencyCreationContext().Equals(agencyContext.GetAgencyIPEndPoint()))
            {
                AddInformation();

                MobilityEventArgs args = new MobilityEventArgs();                
                args.Source = "Agentul " + GetName() + " ruleaza ...";
                args.Information = GetAgentStateInfo();
                agencyContext.OnArrival(args);

                RetractAgent();

            }
            else
            {
                MobilityEventArgs args = new MobilityEventArgs();
                args.Source = "Agentul " + GetName() + " a adunat informatiile...";
                args.Information = GetAgentStateInfo();
                agencyContext.OnArrival(args);
            }
        }
        #endregion Private Methods

        #region Public Override Methods
        public override void Run()
        {
            ResetLifetime();
            AgencyContext agencyContext = GetAgentCurrentContext();
            RunBoomerangAgent(agencyContext);
        }
        public override void GetUI()
        {
            //Not need implementation
        }
        public override String GetInfo()
        {
            throw new NotImplementedException();
        }
        #endregion Public Override Methods

    }
}
