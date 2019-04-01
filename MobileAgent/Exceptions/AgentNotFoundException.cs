using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.Exceptions
{
    public class AgentNotFoundException : AgentException
    {
        #region Constructors
        public AgentNotFoundException()
        {

        }
        public AgentNotFoundException(String stackTrace) : base(stackTrace)
        {
        }
        #endregion Constructors
    }
}
