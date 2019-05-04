using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.AgentManager
{
    public interface AgentProxy
    {
		void DelegateMessage(Message msg);//throws InvalidAgletException
		int GetAgentId();// throws InvalidAgletException
        String GetAgentCodebase();
        String GetAgentInfo();
        bool IsActive();
        bool IsRemote();
		bool IsValid();
        Object SendMessage(Message msg);
        void Suspend();

    }
}
