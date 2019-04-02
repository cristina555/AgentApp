using System;
using System.Collections.Generic;


namespace MobileAgent.AgentManager
{
    public class Agency : AgentContext
    {
		#region Methods
		public void AddContextListener()
		{
			
		}
		public void ClearCache(Uri codebase)
		{
			
		}
        public  AgentProxy CreateAgent(Uri codebase, String code, Object init)
        {
            AgentProxy a=null;
            return a;
        }
        public AgentProxy Clone(Agent agent) // throws CloneNotSupportedException
        {
            AgentProxy agentCloned= null;
            return AgentCloned;
        }
		public AgletProxy Dispatch(AgletProxy agletProxy, Ticket ticket)// throws IOException, AgletException
		{
			AgentProxy agentDispatched= null;
            return agentDispatched;
		}
		public AgletProxy Dispatch(Uri destination)
		{
			
		}
		public void Dispose(AgletProxy agletProxy) //throws InvalidAgletException
		{
			
		}
        public List<AgentProxy> GetAgentProxies()
        {
            List<AgentProxy> Agent_List = new List<AgentProxy>();
            return Agent_List;

        }
        public AgentProxy GetAgentProxy(int id)
        {
            AgentProxy a = null;
            return a;

        }
        public String GetName()
        {
            String Name = "";
            return Name;
        }
		public Object GetProperty(String key)
		{
			return null;
		}
        public Object GetProperty(String key, Object defaultValue)
		{
			return null;
		}
		public void RemoveContextListener(ContextListener listener)
		{
			
		}
		public AgentProxy retractAglet(java.net.URL url) //throws IOException, AgletException
		{
			
		}
		public AgletProxy retractAglet(java.net.URL url, int agentId)//  throws IOException, AgletException
		{
			
		}
		public void setProperty(String key, Object value)
		{
			
		}
        public void ShutDown()
        {

        }
        public void Start()
        {

        }
        public void Activate()
        {

        }
        public void Deactivate(long duration) // throws IOException
        {

        }
		public void ExitMonitor()
		{
			
		}
		#endregion Methods
    }
}
