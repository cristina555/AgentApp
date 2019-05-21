using MobileAgent.AgentManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.EventAgent
{
    public class AgentEvent
    {
        #region Fields
        protected Object _source;
        public int _id;
        #endregion Fields

        #region Constructors
        public AgentEvent(Object source, int id)
        {
            _source = source;
            _id = id;
        }
        #endregion Constructors

        #region Methods
        public int GetId()
        {
            return _id;
        }
        public Object GetSource()
        {
            return _source;
        }
        #endregion Methods
    }
}
