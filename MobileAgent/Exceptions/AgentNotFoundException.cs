using System;

namespace MobileAgent.Exceptions
{
    public class AgentNotFoundException : Exception
    {
        #region Fields
        private string _message;
        private string _agentProxyName = null;
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
        public AgentNotFoundException(string message, string agentProxy) : base()
        {
            _agentProxyName = agentProxy;
            _message = string.Format("{0} : Agentia  {1:F2}", message, _agentProxyName);
        }
        #endregion Constructors
    }
}
