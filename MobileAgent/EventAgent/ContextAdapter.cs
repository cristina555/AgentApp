using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.EventAgent
{
    public class ContextAdapter : ContextListener
    {
        public void AgentAdded(ContextEvent ev)
        {
        }
        public void AgentRemoved(ContextEvent ev)
        {
        }
        public void AgentActivated(ContextEvent ev)
        {
            AgentAdded(ev);
        }
        public void AgentArrived(ContextEvent ev)
        {
            AgentAdded(ev);
        }
        public void AgentCloned(ContextEvent ev)
        {
            AgentAdded(ev);
        }
        public void AgentCreated(ContextEvent ev)
        {
            AgentAdded(ev);
        }
        public void AgentDeactivated(ContextEvent ev)
        {
            AgentRemoved(ev);
        }
        public void AgentDispatched(ContextEvent ev)
        {
            AgentRemoved(ev);
        }
        public void AgentDisposed(ContextEvent ev)
        {
            AgentRemoved(ev);
        }
        public void AgentResumed(ContextEvent ev)
        {
            AgentAdded(ev);
        }
        public void AgentReverted(ContextEvent ev)
        {
            AgentRemoved(ev);
        }
        public void AgentStateChanged(ContextEvent ev)
        {
        }
        public void AgentSuspended(ContextEvent ev)
        {
            AgentRemoved(ev);
        }
        public void ContextShutDown(ContextEvent ev)
        {
        }
        public void ContextStarted(ContextEvent ev)
        {
        }
        public void ShowMessage(ContextEvent ev)
        {
        }
    }
}
