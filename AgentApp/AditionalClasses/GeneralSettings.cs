using AgentApp.Agents;
using AgentApp.Agents.MobileAgents;
using AgentApp.Agents.StationaryAgents;
using MobileAgent.AgentManager;
using MobileAgent.Exceptions;
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

        #region Properties
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
            ListofAgentsM.Add("AgentClone");
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
        public IAgentProxy GetAgentProxy(String name)
        {
            try
            {
                switch (name)
                {
                    case "AgentClone":
                        {
                            return new AgentClone();
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
                            throw new AgentNotFoundException("Agentul nu există:", name);
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
