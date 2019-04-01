using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.Exceptions
{
    class AgentException : Exception
    {
        #region Fields
        private bool _original;
        private String _stackTrace;
        #endregion Fields

        #region Constructors
        public AgentException()
        {

        }
        public AgentException(String stackTrace)
        {
            _stackTrace = stackTrace;
        }
        #endregion Constructors

        #region Methods
        public void PrintStackTrace()
        {

        }
        #endregion Methods
    }
}
