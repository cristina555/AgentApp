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
        private void RunBoomerangAgent(IAgencyContext agencyContext)
        {
            if (!GetAgencyCreationContext().Equals(agencyContext.GetAgencyIPEndPoint()))
            {
                AddInformation();
                if (!agencyContext.IsBooked())
                {
                    MobilityEventArgs args = new MobilityEventArgs
                    {
                        Source = "Agentul " + GetName() + " ruleaza ...",
                        Information = GetAgentStateInfo()
                    };
                    agencyContext.OnArrival(args);
                }
                else
                {
                    MobilityEventArgs args = new MobilityEventArgs
                    {
                        Source = "Agentul " + GetName() + "nu poate rula!",
                        Information = "Agenția este rezervată"
                    };
                    agencyContext.OnArrival(args);
                }
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
            IAgencyContext agencyContext = GetAgentCurrentContext();
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
