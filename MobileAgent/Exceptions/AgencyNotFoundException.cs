using System;

namespace MobileAgent.Exceptions
{
    public class AgencyNotFoundException : Exception
    {
        #region Fields
        private string _message;
        private string _agencyName;
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
        public AgencyNotFoundException(string message, string agencyName) :base()
        {
            _agencyName = agencyName;
            _message = string.Format("{0}: Agentia {1:F1}", message, _agencyName);
        }
        #endregion Constructors

        
    }
}
