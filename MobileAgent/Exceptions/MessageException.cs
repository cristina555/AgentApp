using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.Exceptions
{
    public class MessageException
    {
        #region Fields
        private String _exception;
        #endregion Fields

        #region Constructors
        public MessageException(String exception)
        {
            _exception = exception;
        }
        public MessageException(String exception, String stackTrace) : base(stackTrace)
        {
            _exception = exception;
        }
        #endregion Constructors

        #region Methods
        public String GetException()
        {
            return _exception;
        }
        #endregion Methods
    }
}
