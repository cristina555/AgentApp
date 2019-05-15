using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAgent;
using MobileAgent.AgentManager;

namespace MobileAgent.EventAgent
{
    public class PersistencyEvent : AgentEvent
    {
        #region Fields
        public const int AGLET_PERSISTENCY_FIRST = 40;
        public const int AGLET_PERSISTENCY_LAST = 41;
        public const int DEACTIVATING = AGLET_PERSISTENCY_FIRST;
        public const int ACTIVATION = AGLET_PERSISTENCY_FIRST+1;
        private static String[] name = {
            "DEACTIVATING", "ACTIVATION"
        };
        private long _duration;
        #endregion Fields

        #region Constructors
        public PersistencyEvent(AgentProxy source, int id, long duration) : base(source, id)
        {
            _duration = duration;
        }
        #endregion Constructors

        #region Methods
        public AgentProxy getAgentProxy()
        {
            return (AgentProxy)_source;
        }
        public long getDuration()
        {
            return _duration;
        }
        public override String ToString()
        {
            return "PersistencyEvent[" + name[_id - AGLET_PERSISTENCY_FIRST] + "]";
        }
        #endregion Methods

    }
}
