using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.Exceptions
{
    public class MessageException : AgentException
    {
        #region Fields
        private String _exception;
        #endregion Fields

        #region Constructors
        public MessageException(String exception)
        {
            _exception = exception;
        }
        public MessageException(String exception, String m) : base(m)
        {
            _exception = exception;
        }
        #endregion Constructors

        #region Methods
        public String GetException()
        {
            return _exception;
        }
        public override String ToString()
        {
            return base.ToString() + " (" + _exception + ")";
        }
        #endregion Methods
    }
}
