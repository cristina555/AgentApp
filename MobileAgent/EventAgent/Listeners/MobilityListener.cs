using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.EventAgent
{
    public interface MobilityListener 
    {
        void OnArrival(MobilityEvent mobilityEvent);
        void OnDispatching(MobilityEvent mobilityEvent);
        void OnReverting(MobilityEvent mobilityEvent);

    }
}
