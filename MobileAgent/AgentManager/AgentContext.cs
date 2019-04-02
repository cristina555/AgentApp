﻿using System;
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
		AgentProxy Clone(AgentProxy agentCloned);
        AgentProxy Dispatch(AgentProxy agletProxy, Ticket ticket);// throws IOException, AgletException
        AgentProxy Dispatch(Uri destination);// 
		void Dispose(AgentProxy agentProxy); //throws InvalidAgletException
        List<AgentProxy> GetAgentProxies();
        AgentProxy GetAgentProxy(int id);
        Uri GetHostingURL();
        String GetName();
        Object GetProperty(String key);
        Object GetProperty(String key, Object defaultValue);
        void RemoveContextListener(ContextListener listener);
		AgentProxy RetractAglet(Uri url); //throws IOException, AgletException
        AgentProxy RetractAglet(Uri url, int agentId);//  throws IOException, AgletException
		void setProperty(String key, Object value);
        void ShutDown();
        void Start();
        void Activate();
        void Deactivate(long duration);
    }
}
