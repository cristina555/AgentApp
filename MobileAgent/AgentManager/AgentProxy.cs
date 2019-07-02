using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.AgentManager
{
    public interface IAgentProxy
    {
		int GetAgentId();
        IPEndPoint GetAgencyCreationContext();
        IAgencyContext GetAgentCurrentContext();
        String GetAgentStateInfo();
        String GetAgentInfo();
        string GetName();
        string GetCreationTime();
        
        void SetAgentId(int id);
        void SetAgentCurrentContext(IAgencyContext currentContext);
        void SetCreationTime();
        void SetAgentStateInfo(String codebase);
        void SetAgentInfo(String info);
        void SetAgencyCreationContext(IPEndPoint context);
        void SetName(string name);
        void SetStatus(int status);
        void SetState(int state);
        void SetWorkStatus(int status);
        void SetMobility(int mobility);
        void SetType(int type);
        void SetClone(IMobile ap);
        bool IsActive();
        bool IsRemote();
        bool IsReady();
        bool IsMobile();
        bool IsBoomerang();
        bool IsStatusOK();
        void ResetLifetime();
    }
}
