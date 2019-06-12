using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        public String Name { get; set; }
        #endregion Properties
    }
}
