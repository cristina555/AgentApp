using System;

namespace MobileAgent.Exceptions
{
    public class AgentException : Exception
    {
        #region Fields
        private string _message = null;
        #endregion Fields

        #region Constructors
        public AgentException(string message)         
        {
            _message = message;
        }
        #endregion Constructors

        #region Properties
        public override string Message
        {
            get
            {
                return _message;
            }
        }
        #endregion Properties
        
    }
}
