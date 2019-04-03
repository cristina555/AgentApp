using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAgent.AdditionalClasses;
using MobileAgent.AgentManager;

namespace MobileAgent.EventAgent
{
    public class MobilityEvent : AgentEvent
    {
        #region Fields
        public const int AGLET_MOBILITY_FIRST = 30;
        public const int AGLET_MOBILITY_LAST = 32;
        public const int DISPATCHING = AGLET_MOBILITY_FIRST;
        public const int REVERTING = AGLET_MOBILITY_FIRST + 1;
        public const int ARRIVAL = AGLET_MOBILITY_FIRST + 2;
        private static String[] name = {
            "DISPATCHING", "REVERTING", "ARRIVAL",
        };
        private Ticket _ticket;
        #endregion Fields

        #region Constructors
        public MobilityEvent(AgentProxy target, int id , Ticket ticket) : base(target, id)
        {
            _ticket = ticket;
        }
        public MobilityEvent(AgentProxy target, int id, URL loc) : base(target, id)
        {
            _ticket = new Ticket(loc);
        }
        #endregion Constructors

        #region Methods
        public AgentProxy GetAgentProxy()
        {
            return (AgentProxy)_source;
        }
        public URL GetLocation()
        {
            return _ticket.GetDestination();
        }
        public Ticket GetTicket()
        {
            return _ticket;
        }
        public override String ToString()
        {
            return "MobilityEvent[" + name[_id - AGLET_MOBILITY_FIRST] + "]";
        }
        #endregion Methods



    }
}
