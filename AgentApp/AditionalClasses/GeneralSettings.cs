using AgentApp.Agents;
using AgentApp.Agents.StationaryAgents;
using MobileAgent.AgentManager;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AgentApp.AditionalClasses
{
    public class GeneralSettings
    {
        #region Constructor
        public GeneralSettings()
        {
            CreateListofAgentsM();
            CreateListofAgentsS();
        }
        #endregion Constructor

        #region 
        public List<string> ListofAgentsM { get; } = new List<string>();
        public List<IStationary> ListofAgentsS { get; } = new List<IStationary> ();

        #endregion Properties

        #region  Private Methods
        private void CreateListofAgentsM()
        {
            ListofAgentsM.Add("AgentInfo");
            ListofAgentsM.Add("AgentPI");
            ListofAgentsM.Add("AgentRemote");
            ListofAgentsM.Add("AgentR");
            ListofAgentsM.Add("AgentSelfRemote");
        }
        private void CreateListofAgentsS()
        {
            AgentOS agentOS = new AgentOS();
            ListofAgentsS.Add(agentOS);

            AgentOSA agentOSA = new AgentOSA();
            ListofAgentsS.Add(agentOSA);

            AgentOSSP agentOSSP = new AgentOSSP();
            ListofAgentsS.Add(agentOSSP);

            AgentP agentP = new AgentP();
            ListofAgentsS.Add(agentP);

            AgentVC agentVC = new AgentVC();
            ListofAgentsS.Add(agentVC);

        }
        #endregion Private Methods

        #region Public Methods
        public AgentProxy GetAgentProxy(String name)
        {
            try
            {
                switch (name)
                {
                    case "AgentSelfRemote":
                        {
                            return new AgentSelfRemote();
                        }
                    case "AgentR":
                        {
                            return new AgentR();
                        }
                    case "AgentRemote":
                        {
                            return new AgentRemote();
                        }
                    case "AgentInfo":
                        {
                            return new AgentInfo();
                        }
                    case "AgentPI":
                        {
                            return new AgentPI();
                        }
                    default:
                        {
                            return null;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception caught! Mesaj: " + ex.Message);
            }
            return null;
        }
        #endregion Public Methods
    }
}
