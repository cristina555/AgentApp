using AgentApp.Agents;
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
            CreateStaticListofAgents();
        }
        #endregion Constructor

        #region Properties
        public Dictionary<AgentProxy, Form> StaticListofAgents { get; } = new Dictionary<AgentProxy, Form>();
        #endregion Properties

        #region  Private Methods
        private void  CreateStaticListofAgents()
        {
            AgentInfo agentInfo = new AgentInfo();
            StaticListofAgents.Add(agentInfo, new Form());

            AgentPI agentPI = new AgentPI();
            Form agentPiUI = new Interfaces.AgentPiUI();
            StaticListofAgents.Add(agentPI, agentPiUI);

            AgentRemote agentRemote = new AgentRemote();
            Form agentRemoteUI = new Interfaces.AgentRemoteUI();
            StaticListofAgents.Add(agentRemote, agentRemoteUI);

        }
        #endregion Private Methods

        #region Public Methods
        public AgentProxy GetAgentProxy(String info)
        {
            AgentProxy agent = null;
            foreach (AgentProxy ap in StaticListofAgents.Keys)
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
                return StaticListofAgents[ap];
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
