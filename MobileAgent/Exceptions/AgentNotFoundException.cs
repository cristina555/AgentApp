using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.Exceptions
{
    public class AgentNotFoundException : AgentException
    {
        #region Fields
        private string _source = null;
        private string _message = null;
        #endregion Fields

        #region Constructors
        public AgentNotFoundException(string message, string source) : base(message)
        {
            _source = source;
            _message = string.Format("{0} Agentul: {1:F2}", message, source);
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
