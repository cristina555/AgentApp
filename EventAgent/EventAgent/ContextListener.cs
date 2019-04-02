using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.EventAgent
{
    public interface ContextListener
    {
        void AgentActivated(ContextEvent contextEvent);
        void AgentArrived(ContextEvent contextEvent);
        void AgentCloned(ContextEvent contextEvent);
        void AgentCreated(ContextEvent contextEvent);
        void AgentDeactivated(ContextEvent contextEvent);
        void AgentDispatched(ContextEvent contextEvent);
        void AgentDisposed(ContextEvent contextEvent);
        void AgentResumed(ContextEvent contextEvent);
        void AgentStateChanged(ContextEvent contextEvent);
        void AgentReverted(ContextEvent ev);
        void AgentSuspended(ContextEvent contextEvent);
        void ContextShutDown(ContextEvent contextEvent);
        void ContextStarted(ContextEvent contextEvent);
        void ShowMessage(ContextEvent contextEvent);

    }
}
