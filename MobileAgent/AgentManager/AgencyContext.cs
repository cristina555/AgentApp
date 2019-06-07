using MobileAgent.EventAgent;
using System.Collections.Generic;
using System.Net;

namespace MobileAgent.AgentManager
{
    public interface AgencyContext
    {
        string GetName();
        List<string> GetNeighbours();
        AgentProxy GetLastRunnableAgent();
        void SetName(string name);
        void SetNeighbours(List<string> neighbours);
        void SetLastRunnableAgent(AgentProxy agentProxy);
        IPEndPoint GetAgencyIPEndPoint();
        void CreateAgent(AgentProxy agentProxy);
        void Clone(AgentProxy agentCloned);
        void Dispatch(AgentProxy agentProxy, IPEndPoint destination);
        void Dispose(AgentProxy agentProxy);
        AgentProxy GetMobileAgentProxy(string name);
        AgentProxy GetStationaryAgent(string name);
        void RetractAgent(AgentProxy agentProxy, IPEndPoint location);
        void ShutDown();
        void Start();
        void Activate();
        void Deactivate(long duration);
        //void DispatchEvent(AgentEvent ev);

    }
}
