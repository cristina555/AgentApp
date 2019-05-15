using MobileAgent.AgentManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.EventAgent
{
    public class CloneEvent : AgentEvent
    {
        #region Fields
        public const int AGLET_CLONE_FIRST = 20;
        public const int AGLET_CLONE_LAST = 22;
        public const int CLONING = AGLET_CLONE_FIRST;
        public const int CLONE = AGLET_CLONE_FIRST + 1;
        public const int CLONED = AGLET_CLONE_FIRST + 2;
        public static String[] name = {
            "CLONING", "CLONE", "CLONED"
        };
        #endregion Fields

        #region Constructors
        public CloneEvent( AgentProxy source, int id) : base(source, id)
        {
        }
        #endregion Constructors

        #region Methods
        public AgentProxy GetAgentProxy()
        {
            return (AgentProxy)_source;
        }
        public override String ToString()
        {
            return "CloneEvent[" + name[_id - AGLET_CLONE_FIRST] + "]";
        }
        #endregion Methods
    }
}
