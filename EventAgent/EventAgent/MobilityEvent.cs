using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAgent;

namespace MobileAgent.EventAgent
{
    public class MobilityEvent : AgentEvent
    {
        #region Fields
        public static int AGLET_MOBILITY_FIRST = 30;
        public static int AGLET_MOBILITY_LAST = 32;
        public static int DISPATCHING = AGLET_MOBILITY_FIRST;
        public static int REVERTING = AGLET_MOBILITY_FIRST + 1;
        public static int ARRIVAL = AGLET_MOBILITY_FIRST + 2;
        private static String[] name = {
            "DISPATCHING", "REVERTING", "ARRIVAL",
        };
        private Uri _location;
        #endregion Fields

        #region Constructors
        public MobilityEvent(AgentProxy source, int id ) : base(source, id)
        {

        }
        public MobilityEvent(AgentProxy source, int id, Uri loc) : base(source, id)
        {
            _location = loc;
        }
        #endregion Constructors

        #region Methods
        public AgentProxy GetAgentProxy()
        {
            return (AgentProxy)_source;
        }
        public Uri getLocation()
        {
            return _location;
        }
        public override String ToString()
        {
            return "MobilityEvent[" + name[_id - AGLET_MOBILITY_FIRST] + "]";
        }
        #endregion Methods



    }
}
