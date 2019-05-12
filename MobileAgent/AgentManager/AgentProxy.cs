using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.AgentManager
{
    public interface AgentProxy
    {
		int GetAgentId();
        AgentContext GetAgletContext();
        String GetAgentCodebase();
        String GetAgentInfo();
        AgentProxy GetProxy();
        void SetAgentId(int id);
        void SetAgencyHost(int agencyHostID);
        void SetCreationTime();
        void SetAgentCodebase(String codebase);
        void SetAgentInfo(String info);
        bool IsActive();
        bool IsRemote();
		bool IsValid();
        void Suspend();
        void Run();

    }
}
