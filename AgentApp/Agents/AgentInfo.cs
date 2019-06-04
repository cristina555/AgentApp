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
        #endregion Fields

        #region Constructor
        public AgentInfo()
        {
            _agenciesVisited = new List<IPEndPoint>();
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
            IPEndPoint ip = this.GetAgentCurentContext(); 
            if (!_agenciesVisited.Contains(ip))
            {
                _agenciesVisited.Add(this.GetAgentCurentContext());
            }
        }
        private void ShowAgencies()
        {
            AddAgency();
            String codebase="";
            Console.WriteLine("Agentul a vizitat");
            codebase += "Agentul a vizitat"+Environment.NewLine;
            foreach (IPEndPoint context in _agenciesVisited)
            {
                Console.WriteLine(context);
                codebase += context;
                codebase += Environment.NewLine;
            }
            this.SetAgentCodebase(codebase);
        }
        public override void Run()
        {
            ShowAgencies();
        }
        public override void GetUI()
        {
            //var thread = new Thread(() =>
            //{
            //    Form f = new Form();
            //    Application.Run(f);
            //});
            //thread.Start();
            throw new NotImplementedException();
        }
        #endregion Methods

    }
}
