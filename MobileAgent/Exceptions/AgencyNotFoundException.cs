using System;

namespace MobileAgent.Exceptions
{
    public class AgencyNotFoundException : AgentException
    {
        #region Fields
        private string _source;
        private string _message = null;
        #endregion Fields

        #region Constructors
        public AgencyNotFoundException(string message, string source) : base(message)
        {
            _source = source;
            _message = string.Format("{0} Agentia: {1:F2}", message, source);
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
