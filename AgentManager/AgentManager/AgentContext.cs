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
		AgentProxy Clone(AgentProxy );
		AgletProxy Dispatch(AgletProxy agletProxy, Ticket ticket);// throws IOException, AgletException
		AgletProxy Dispatch(Uri destination);// 
		void Dispose(AgletProxy agletProxy); //throws InvalidAgletException
        List<AgentProxy> GetAgentProxies();
        AgentProxy GetAgentProxy(int id);
        Uri GetHostingURL();
        String GetName();
        Object GetProperty(String key);
        Object GetProperty(String key, Object defaultValue);
        void RemoveContextListener(ContextListener listener);
		AgentProxy retractAglet(java.net.URL url); //throws IOException, AgletException
		AgletProxy retractAglet(java.net.URL url, int agentId);//  throws IOException, AgletException
		void setProperty(String key, Object value);
        void ShutDown();
        void Start();
        void Activate();
        void Deactivate(long duration);
    }
}
