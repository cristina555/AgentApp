using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.AgentManager
{
    public interface AgentProxy
    {
        void Activate();
        void Clone();
        void DE
        AgentProxy Dispatch(Uri destination);
        void  Dispose();
        Agent GetAgent();
        bool IsActive();
        bool IsRemote();
        object SendMeassage(Message msg);
        void Suspend();

    }
}
