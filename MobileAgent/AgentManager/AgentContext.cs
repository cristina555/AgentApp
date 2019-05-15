using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAgent.EventAgent;
using System.Net;

namespace MobileAgent.AgentManager
{
    public interface AgentContext
    {
        void CreateAgent(AgentProxy agentProxy);
        AgentProxy Clone(AgentProxy agentCloned);
        void Dispatch(AgentProxy agentProxy, IPEndPoint destination);
        void Dispose(AgentProxy agentProxy); 
        AgentProxy GetAgentProxy(string codebase);
		AgentProxy RetractAglet(IPEndPoint location);
        void ShutDown();
        void Start();
        void Activate();
        void Deactivate(long duration);
        void DispatchEvent(AgentEvent ev);
        
    }
}
