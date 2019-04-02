using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.Exceptions
{
    public class InvalidAgentException : AgentException
    {
        public InvalidAgentException()
        {

        }
        public InvalidAgentException(String m) : base(m)
        {

        }
    }
}
