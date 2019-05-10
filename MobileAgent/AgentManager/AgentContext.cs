﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAgent.EventAgent;
using MobileAgent.AdditionalClasses;
using System.Net;

namespace MobileAgent.AgentManager
{
    public interface AgentContext
    {
        void AddContextListener();
        void CreateAgent(AgentProxy agentProxy);
        AgentProxy Clone(AgentProxy agentCloned);
        void Dispatch(AgentProxy agletProxy, Ticket ticket);// throws IOException, AgletException
        AgentProxy Dispatch(URL destination);// 
        void Dispatch(AgentProxy agentProxy, IPEndPoint destination);
        void Dispose(AgentProxy agentProxy); //throws InvalidAgletException
        List<AgentProxy> GetAgentProxies();
        AgentProxy GetAgentProxy(int id);
        AgentProxy GetAgentProxy(URL contextAddress, int id);
        URL GetHostingURL();
        String GetName();
        Object GetProperty(String key);
        Object GetProperty(String key, Object defaultValue);
        void RemoveContextListener(ContextListener listener);
		AgentProxy RetractAglet(URL url); //throws IOException, AgletException
        AgentProxy RetractAglet(URL url, int agentId);//  throws IOException, AgletException
		void SetProperty(String key, Object value);
        void ShutDown();
        void Start();
        void Activate();
        void Deactivate(long duration);
    }
}
