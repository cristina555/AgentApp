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
		String getAddress();//  throws InvalidAgletException
        AgentProxy GetAgent();
		String GetAgentClassName();
		int GetAgentID();// throws InvalidAgletException
		String GetAgentInfo();
        bool IsActive();
        bool IsRemote();
	    bool IsState(int type);
		bool IsValid();
        Object SendMessage(Message msg);
        void Suspend();

    }
}
