using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAgent;

namespace MobileAgent.EventAgent
{
    public class PersistencyEvent : AgentEvent
    {
        #region Fields
        public readonly static int AGLET_PERSISTENCY_FIRST = 40;
        public readonly static int AGLET_PERSISTENCY_LAST = 41;
        public readonly static int DEACTIVATING = AGLET_PERSISTENCY_FIRST;
        public readonly static int ACTIVATION = AGLET_PERSISTENCY_FIRST+1;
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
