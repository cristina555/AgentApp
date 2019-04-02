using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Tracing;

namespace MobileAgent.EventAgent
{
    public interface PersistencyListener 
    {
        void OnActivation(PersistencyEvent pesistencyEvent);
        void OnDeactivating(PersistencyEvent persistencyEvent);

    }
}
