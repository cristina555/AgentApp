using MobileAgent.EventAgent;
using System;
using System.Collections.Generic;
using System.Net;

namespace MobileAgent.AgentManager
{
    public interface AgencyContext
    {
        string GetName();
        List<string> GetNeighbours();
        AgentProxy GetDispatchedAgent();
        void SetName(string name);
        void SetNeighbours(List<string> neighbours);
        void SetDispatchedAgent(AgentProxy agentProxy);
        IPEndPoint GetAgencyIPEndPoint();
        void CreateAgent(AgentProxy agentProxy);
        void Clone(AgentProxy agentCloned);
        bool Dispatch(AgentProxy agentProxy, IPEndPoint destination);
        void Dispose(AgentProxy agentProxy);
        AgentProxy GetMobileAgentProxy(string name);
        AgentProxy GetMobileAgentProxy(int id);
        IStationary GetStationaryAgent(string name);
        void RetractAgent(AgentProxy agentProxy);
        void ShutDown();
        void Start();
        void Activate();
        void Deactivate(AgentProxy agentProxy);
        void OnRefuseConnection(UnconnectedAgencyArgs e);
        void OnArrival(MobilityEventArgs e);
        void OnDispatching(MobilityEventArgs e);
        void RunAgent(AgentProxy agentProxy);
        void RemoveAgent(AgentProxy agentProxy);

        //void DispatchEvent(AgentEvent ev);

    }
}
