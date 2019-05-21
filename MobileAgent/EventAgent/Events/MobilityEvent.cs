﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAgent.AgentManager;
using System.Net;

namespace MobileAgent.EventAgent
{
    public class MobilityEvent : AgentEvent
    {
        #region Fields
        public const int AGENT_MOBILITY_FIRST = 30;
        public const int AGENT_MOBILITY_LAST = 32;
        public const int DISPATCHING = AGENT_MOBILITY_FIRST;
        public const int REVERTING = AGENT_MOBILITY_FIRST + 1;
        public const int ARRIVAL = AGENT_MOBILITY_FIRST + 2;
        private static String[] name = {
            "DISPATCHING", "REVERTING", "ARRIVAL",
        };
        IPEndPoint _ip;
        #endregion Fields

        #region Constructors
        public MobilityEvent(AgentProxy target, int id , IPEndPoint ticket) : base(target, id)
        {
            _ip = ticket;
        }

        #endregion Constructors

        #region Methods
        public AgentProxy GetAgentProxy()
        {
            return (AgentProxy)_source;
        }
    
        public override String ToString()
        {
            return "MobilityEvent[" + name[_id - AGENT_MOBILITY_FIRST] + "]";
        }
        #endregion Methods



    }
}
