using System;
using System.Collections.Generic;


namespace MobileAgent.AgentManager
{
    public class Agency : AgentContext
    {
        public  AgentProxy CreateAgent()
        {
            AgentProxy a=null;
            return a;
        }
        public Agent Clone(Agent Agent)
        {
            Agent AgentCloned= null;
            //AgentCloned.  = 
            return AgentCloned;
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
        public void ShutDown()
        {

        }
        public void Start()
        {

        }
        public void Activate()
        {

        }
        public void Clone()
        {

        }
        public void Deactivate()
        {

        }
    }
}
