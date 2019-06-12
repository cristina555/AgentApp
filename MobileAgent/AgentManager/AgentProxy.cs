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
        AgencyContext GetAgentCurrentContext();
        String GetAgentCodebase();
        String GetAgentInfo();
        string GetName();
        int GetStatus();
        int GetMobility();
        void SetAgentId(int id);
        void SetAgentCurrentContext(AgencyContext currentContext);
        void SetCreationTime();
        void SetAgentCodebase(String codebase);
        void SetAgentInfo(String info);
        void SetAgencyCreationContext(IPEndPoint context);
        void SetName(string name);
        void SetStatus(int status);
        void SetMobility(int mobility);
        bool IsActive();
        bool IsRemote();
        void Suspend();
        void Run();
        void GetUI();
    }
}
