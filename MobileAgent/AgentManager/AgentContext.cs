using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAgent.EventAgent;

namespace MobileAgent.AgentManager
{
    public interface AgentContext
    {
        void AddContextListener();
        void ClearCache(Uri codebase);
        AgentProxy CreateAgent(Uri codebase, String code, Object init);
        List<AgentProxy> GetAgentProxies();
        List<AgentProxy> GetAgentProxies(int type);
        AgentProxy GetAgentProxy(int id);
        Uri GetHostingURL();
        String GetName();
        Object GetProperty(String key);
        Object GetProperty(String key, Object defaultValue);
        void RemoveContextListener(ContextListener listener);
        void ShutDown();
        void Start();
        void Activate();
        void Clone();
        void Deactivate();
    }
}
