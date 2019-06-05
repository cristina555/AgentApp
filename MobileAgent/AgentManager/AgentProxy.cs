using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.AgentManager
{
    public interface AgentProxy
    {
		int GetAgentId();
        IPEndPoint GetAgencyCreationContext();
        IPEndPoint GetAgentCurentContext();
        String GetAgentCodebase();
        String GetAgentInfo();
        void SetAgentId(int id);
        void SetAgentContext(IPEndPoint currentContext);
        void SetCreationTime();
        void SetAgentCodebase(String codebase);
        void SetAgentInfo(String info);
        void SetAgencyCreationContext(IPEndPoint context);
        bool IsActive();
        bool IsRemote();
        void Suspend();
        void Run();
    }
}
