using MobileAgent.AgentManager;
using System;

namespace MobileAgent.Exceptions
{
    public class AgencyNotFoundException : Exception
    {
        #region Fields
        private string _message;
        private Agency _agency = null;
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
        public AgencyNotFoundException(string message, Agency agency) :base()
        {
            _agency = agency;
            _message = string.Format("{0} : Agentia  {1:F2}", message, agency.GetName());
        }
        #endregion Constructors

        
    }
}
