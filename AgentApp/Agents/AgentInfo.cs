using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAgent.AgentManager;
using System.Net;
using System.Threading;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentInfo : Agent
    {
        #region Fields
        List<IPEndPoint> _agenciesVisited = null;
        #endregion Fields

        #region Constructor
        public AgentInfo(int id) : base(id)
        {
            _agenciesVisited = new List<IPEndPoint>();
        }
        #endregion Constructor

        #region Methods
        private void AddAgency()
        {
            if (!_agenciesVisited.Contains(this.GetAgentContext()))
            {
                _agenciesVisited.Add(this.GetAgentContext());
            }
        }
        private void ShowAgencies()
        {
            AddAgency();
            Console.WriteLine("Agentul a vizitat");
            foreach(IPEndPoint context in _agenciesVisited)
            {
                Console.WriteLine(context);
            }
        }
        public override void Run()
        {
            ShowAgencies();
        }
        #endregion Methods

    }
}
