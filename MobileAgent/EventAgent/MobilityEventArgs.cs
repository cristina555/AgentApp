using System;

namespace MobileAgent.EventAgent
{
    public class MobilityEventArgs : EventArgs
    {
        #region Constructor
        public MobilityEventArgs()
        {

        }
        #endregion Constructor

        #region Properties
        public DateTime Date {get; set;}
        public string Source { get; set; }
        public String Information { get; set; }
        #endregion Properties
    }
}
