using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAgent.AgentManager;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentInfo : Agent
    {
        #region Fields
        List<IPEndPoint> _agenciesVisited = null;
        String info = "" ;
        #endregion Fields

        #region Constructor
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
        #endregion Constructor

        #region Methods
        private void AddAgency()
        {
            IPEndPoint ip = this.GetAgentCurrentContext(); 
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
        public override void Run()
        {
            ShowAgencies();
        }
        #endregion Methods

    }
}
