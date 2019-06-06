using MobileAgent.EventAgent;
using System.Collections.Generic;
using System.Net;

namespace MobileAgent.AgentManager
{
    public interface AgencyContext
    {
        string GetName();
        List<string> GetNeighbours();
        void SetName(string name);
        void SetNeighbours(List<string> neighbours);
        IPEndPoint GetAgencyIPEndPoint();
        void CreateAgent(AgentProxy agentProxy);
        AgentProxy Clone(AgentProxy agentCloned);
        void Dispatch(AgentProxy agentProxy, IPEndPoint destination);
        void Dispose(AgentProxy agentProxy);
        AgentProxy GetAgentProxy(string codebase);
        void RetractAgent(AgentProxy agentProxy, IPEndPoint location);
        void ShutDown();
        void Start();
        void Activate();
        void Deactivate(long duration);
        //void DispatchEvent(AgentEvent ev);

    }
}
