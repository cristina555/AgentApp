using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.Exceptions
{
    public class AgentException : Exception
    {
        #region Fields
        private bool _original = true;
        private String _message = null;
        #endregion Fields

        #region Constructors
        public AgentException() : base()
        {

        }
        public AgentException(String message) : base(message)        
        {   

        }
        #endregion Constructors

        #region Methods
        public void PrintExceptionMessage()
        {
            if (_original)
            {
                Console.WriteLine(base.Message);
            }
            else
            {
                Console.WriteLine(this);
                Console.WriteLine(_message);
            }
        }
        #endregion Methods
    }
}
