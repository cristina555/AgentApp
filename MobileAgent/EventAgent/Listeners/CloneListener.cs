using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.EventAgent
{
    public interface CloneListener 
    {
        void OnClone(CloneEvent cloneEvent);
        void OnCloned(CloneEvent cloneEvent);
        void OnCloning(CloneEvent cloneEvent);

    }
}
