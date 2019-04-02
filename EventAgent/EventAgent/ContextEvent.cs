using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAgent;

namespace MobileAgent.EventAgent
{
    public class ContextEvent : AgentEvent
    {
        #region Fields
        public readonly static int CONTEXT_FIRST =0;
        public readonly static int CONTEXT_LAST = 12;
        public readonly static int STARTED = CONTEXT_FIRST;
        public readonly static int SHUTDOWN = CONTEXT_FIRST + 1;
        public readonly static int CREATED = CONTEXT_FIRST + 2;
        public readonly static int CLONED = CONTEXT_FIRST + 3;
        public readonly static int DISPOSED = CONTEXT_FIRST + 4;
        public readonly static int DISPATCHED = CONTEXT_FIRST + 5;
        public readonly static int REVERTED = CONTEXT_FIRST + 6;
        public readonly static int ARRIVED = CONTEXT_FIRST + 7;
        public readonly static int DEACTIVATED = CONTEXT_FIRST + 8;
        public readonly static int SUSPENDED = CONTEXT_FIRST + 9;
        public readonly static int ACTIVATED = CONTEXT_FIRST + 10;
        public readonly static int RESUMED = CONTEXT_FIRST + 11;
        public readonly static int STATE_CHANGED = CONTEXT_FIRST + 12;
        public readonly static int MESSAGE = CONTEXT_FIRST + 13;
        public readonly static int NO_RESPONSE = CONTEXT_FIRST + 14;
        protected AgentProxy _agletProxy;
        public Object _arguments = null;
        private static String[] name =
        {
            "STARTED", "STOPPED", "CREATED", "CLONED", "DISPOSED", "DISPATCHED",
            "REVERTED", "ARRIVED", "DEACTIVATED", "SUSPENDED", "ACTIVATED",
            "RESUMED", "TEXT_CHANGED", "MESSAGE", "NO_RESPONSE",
        };
        #endregion Fields

        #region Constructors
        public ContextEvent(Object source, int id, AgentProxy target) : base(source, id)
        {
            _agletProxy = target;
        }
        public ContextEvent( Object source, int id, AgentProxy target, Object arguments) : base(source, id)
        {
            _agletProxy = target;
            _arguments = arguments;
        }
        #endregion Constructors

        #region Methods
        public AgentContext GetAgentContext()
        {
            return (AgentContext)_source;
        }
        public AgentProxy getAgentProxy()
        {
            return _agletProxy;
        }
        public String GetMessage()
        {
            if(_id ==  MESSAGE)
            {
                return (String)_arguments;
            }
            return "No message: Implement exception";
        }
        public String GetText()
        {
            if (_id == STATE_CHANGED)
            {
                return (String)_arguments;
            }
            return "No message: Implement exception";
        }
        public String ToString()
        {
            return "ContextEvent[" + name[_id - CONTEXT_FIRST] + "]";
        }
        #endregion Methods
    }
}
