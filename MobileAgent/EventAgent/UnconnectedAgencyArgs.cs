using System;
using System.Net;

namespace MobileAgent.AgentManager
{
    public class UnconnectedAgencyArgs : EventArgs
    {
        #region Constructor
        public UnconnectedAgencyArgs()
        {

        }
        #endregion Constructor

        #region Properties
        public DateTime Date { get; set; }
        public IPEndPoint Name { get; set; }
        #endregion Properties
    }
}
