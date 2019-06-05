using AgentApp.Agents;
using MobileAgent.AgentManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgentApp.AditionalClasses
{
    public class GeneralSettings
    {
        #region Fields
        private Dictionary<AgentProxy, Form> _agentsToCreate = null;
        #endregion Fields

        #region Constructor
        public GeneralSettings()
        {
            _agentsToCreate = new Dictionary<AgentProxy, Form>();
            CreateStaticListofAgents();
        }
        #endregion Constructor

        #region Properties
        public AgentProxy GetAgentProxy(String info)
        {
            AgentProxy agent = null;
            foreach (AgentProxy ap in _agentsToCreate.Keys)
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
                return _agentsToCreate[ap];
            }
            catch(KeyNotFoundException knfe)
            {
                MessageBox.Show(knfe.Message);
            }
            return null;
        }
        #endregion Properties

        #region Methods
        private void  CreateStaticListofAgents()
        {
            AgentInfo agentInfo = new AgentInfo();
            _agentsToCreate.Add(agentInfo, new Form());

            AgentPI agentPI = new AgentPI();
            Form agentPiUI = new Interfaces.AgentPiUI();
            _agentsToCreate.Add(agentPI, agentPiUI);

            AgentRemote agentRemote = new AgentRemote();
            Form agentRemoteUI = new Interfaces.AgentRemoteUI();
            _agentsToCreate.Add(agentRemote, agentRemoteUI);

        }
        public Dictionary<AgentProxy, Form> GetStaticListofAgents()
        {
            return _agentsToCreate;
        }
        #endregion Methods
    }
}
