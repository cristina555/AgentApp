using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.AgentManager
{
    public interface MessageManager
    {
		void Destroy();
		void ExitMonitor();
		void NotifyAllMessages();
		void NotifyMessage();
		void SetPriority(String kind, int priority);
    }
}
