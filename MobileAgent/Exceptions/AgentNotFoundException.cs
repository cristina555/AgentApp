using MobileAgent.AgentManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.Exceptions
{
    public class AgentNotFoundException : Exception
    {
        #region Fields
        private string _message;
        private AgentProxy _agentProxy = null;
        #endregion Fields

        #region Properties
        public override string Message
        {
            get
            {
                return _message;
            }
        }
        #endregion Properties
        #region Constructors
        public AgentNotFoundException(string message, AgentProxy agentProxy) : base()
        {
            _agentProxy = agentProxy;
            _message = string.Format("{0} : Agentia  {1:F2}", message, agentProxy.GetName());
        }
        #endregion Constructors
    }
}
