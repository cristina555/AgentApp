using System;

namespace MobileAgent.AgentManager
{
    public class Agent : AgentProxy
    {
        //private int _id;
        //private string _codebase;
        protected Agent()
        {

        }
        public string GetSetCodebase { get; set; }

        public AgentProxy Dispatch(Uri destination)
        {
            AgentProxy Agent_Proxy = null;
            return Agent_Proxy;
        }
        public void Dispose()
        {

        }
        public Agent GetAgent()
        {
            return null;

        }
       
        public bool IsActive()
        {
            return false;
        }
        public bool IsRemote()
        {
            return false;
        }
        public object SendMeassage(Message msg)
        {
            return null;
        }
        public void Suspend()
        {

        }

        public int GetAgentId()
        {
            return 0;
        }
        public string GetAgentCodebase()
        {
            return null;
        }
        public AgentProxy GetAgentProxy()
        {
            return null;
        }
        public void Run()
        {

        }
    }
}
