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

        #region Properties
        public Dictionary<AgentProxy, string> ListofAgentsM { get; } = new Dictionary<AgentProxy, string>();
        public List<AgentProxy> ListofAgentsS { get; } = new List<AgentProxy> ();

        #endregion Properties

        #region  Private Methods
        private void  CreateListofAgentsM()
        {
            AgentInfo agentInfo = new AgentInfo();
            ListofAgentsM.Add(agentInfo, "");

            AgentPI agentPI = new AgentPI();
            ListofAgentsM.Add(agentPI, "agentPiUI");

            AgentRemote agentRemote = new AgentRemote();
            ListofAgentsM.Add(agentRemote, "agentRemoteUI");

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

        }
        #endregion Private Methods

        #region Public Methods
        public AgentProxy GetAgentProxy(String info)
        {
            AgentProxy agent = null;
            foreach (AgentProxy ap in ListofAgentsM.Keys)
            {
                if (ap.GetName().Equals(info))
                {
                    agent = ap;
                }
            }
            return agent;
        }
        public Form GetUI(AgentProxy ap)
        {
            try
            {
                string ui =  ListofAgentsM[ap];
                switch (ui)
                {
                    case "agentPiUI":
                        {
                            return new Interfaces.AgentPiUI();
                        }
                    case "agentRemoteUI":
                        {
                           return new Interfaces.AgentRemoteUI();
                        }
                    default:
                        {
                            return new Form();

                        }
                }
                    
            }
            catch (KeyNotFoundException knfe)
            {
                MessageBox.Show("KeyNotFoundException caught! Mesaj: "+ knfe.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Exception caught! Mesaj: " + ex.Message);
            }
            return null;
        }
        #endregion Public Methods
    }
}
