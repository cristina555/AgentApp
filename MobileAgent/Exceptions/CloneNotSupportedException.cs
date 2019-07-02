using MobileAgent.AgentManager;
using System;

namespace MobileAgent.Exceptions
{
    public class CloneNotSupportedException : Exception
    {
        #region Fields
        private string _message;
        private IAgentProxy _agentProxy = null;
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
        public CloneNotSupportedException(string message, IAgentProxy agentProxy) : base()
        {
            _agentProxy = agentProxy;
            _message = string.Format("{0} : Agentia  {1:F2}", message, agentProxy.GetName());
        }
        #endregion Constructors
    }
}
