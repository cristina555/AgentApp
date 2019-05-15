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
        IPEndPoint GetAgentContext();
        String GetAgentCodebase();
        String GetAgentInfo();
        void SetAgentId(int id);
        void SetAgencyHost(int agencyHostID);
        void SetCreationTime();
        void SetAgentCodebase(String codebase);
        void SetAgentInfo(String info);
        void SetAgentContext(IPEndPoint context);
        bool IsActive();
        bool IsRemote();
		bool IsValid();
        void Suspend();
        void Run();

    }
}
