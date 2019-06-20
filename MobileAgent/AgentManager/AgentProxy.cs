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
        String GetAgentStateInfo();
        String GetAgentInfo();
        string GetName();
        string GetCreationTime();
        List<AgentProxy> GetCloneList();
        AgentProxy GetClone(int id);
        void SetAgentId(int id);
        void SetAgentCurrentContext(AgencyContext currentContext);
        void SetCreationTime();
        void SetAgentStateInfo(String codebase);
        void SetAgentInfo(String info);
        void SetAgencyCreationContext(IPEndPoint context);
        void SetName(string name);
        void SetStatus(int status);
        void SetWorkStatus(int status);
        void SetMobility(int mobility);
        void SetType(int type);
        void SetClone(AgentProxy ap);
        void Clone();
        bool Dispatch(IPEndPoint destination);
        bool IsActive();
        bool IsRemote();
        bool IsReady();
        bool IsMobile();
        bool IsBoomerang();
        bool IsStatusOK();
        void Run();
        void GetUI();
    }
}
