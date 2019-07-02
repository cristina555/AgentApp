using MobileAgent.EventAgent;
using System.Collections.Generic;
using System.Net;

namespace MobileAgent.AgentManager
{
    public interface IAgencyContext
    {
        string GetName();
        List<string> GetNeighbours();
        IAgentProxy GetDispatchedAgent();
        void SetName(string name);
        void SetNeighbours(List<string> neighbours);
        void SetDispatchedAgent(IAgentProxy agentProxy);
        void SetAgencyFeedback();
        void ResetAgencyFeedback();
        IPEndPoint GetAgencyIPEndPoint();
        void CreateAgent(IAgentProxy agentProxy);
        void Clone(IMobile agentCloned);
        bool GetConnection(IPEndPoint destination);
        void Dispatch(IMobile agentProxy, IPEndPoint destination);
        void Deactivate(IAgentProxy agentProxy);
        IMobile GetMobileAgentProxy(string name);
        IMobile GetMobileAgentProxy(int id);
        IStationary GetStationaryAgent(string name);
        void RetractAgent(IMobile agentProxy);
        void ShutDown();
        void Start();
        void Activate();
        void OnRefuseConnection(UnconnectedAgencyArgs e);
        void OnArrival(MobilityEventArgs e);
        void OnDispatching(MobilityEventArgs e);
        void OnDeactivating(PersistencyEventArgs e);
        void OnActivating(PersistencyEventArgs e);
        void OnCloning(CloneEventArgs e);
        void RunAgent(IMobile agentProxy);
        void RemoveAgent(IMobile agentProxy);
        void SetBookedTime(int milli);
        bool IsBooked();
    }
}
