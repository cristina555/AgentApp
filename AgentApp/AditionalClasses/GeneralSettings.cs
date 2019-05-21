using AgentApp.Agents;
using MobileAgent.AgentManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentApp.AditionalClasses
{
    public class GeneralSettings
    {
        #region Fields
        private List<AgentProxy> _agentsToCreate = null;
        #endregion Fields

        #region Constructor
        public GeneralSettings()
        {
            _agentsToCreate = new List<AgentProxy>();
            CreateStaticListofAgents();
        }
        #endregion Constructor

        #region Properties
        public AgentProxy GetAgentProxy(String info)
        {
            AgentProxy agent = null;
            foreach(AgentProxy ap in _agentsToCreate)
            {
                if(ap.GetAgentInfo().Equals(info))
                {
                    agent = ap;
                }
            }
            return agent;
        }
        #endregion Properties

        #region Methods
        private void  CreateStaticListofAgents()
        {
            AgentInfo agentInfo = new AgentInfo();
            _agentsToCreate.Add(agentInfo);

            AgentPI agentPI = new AgentPI();
            _agentsToCreate.Add(agentPI);
        }
        public List<AgentProxy> GetStaticListofAgents()
        {
            return _agentsToCreate;
        }
        #endregion Methods
    }
}
