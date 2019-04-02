﻿using System;
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
        private URL _location;
        #endregion Fields

        #region Constructors
        public MobilityEvent(AgentProxy source, int id ) : base(source, id)
        {

        }
        public MobilityEvent(AgentProxy source, int id, URL loc) : base(source, id)
        {
            _location = loc;
        }
        #endregion Constructors

        #region Methods
        public AgentProxy GetAgentProxy()
        {
            return (AgentProxy)_source;
        }
        public URL getLocation()
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
