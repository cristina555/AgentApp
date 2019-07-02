using System;

namespace MobileAgent.EventAgent
{
    public class CloneEventArgs : EventArgs
    {
        #region Constructor
        public CloneEventArgs()
        {

        }
        #endregion Constructor

        #region Properties
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public String Information { get; set; }
        #endregion Properties
    }
}
