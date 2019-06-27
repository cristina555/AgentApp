using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.AgentManager
{
    public interface IMobile : AgentProxy
    {
        List<IMobile> GetCloneList();
        IMobile GetClone(int id);
        IMobile GetParent();
        int GetWorkType();
        void SetParent(IMobile ap);
        void SetWorkType(int status);
        int GetAgentType();
        IMobile Clone();
        //bool GetConnection(IPEndPoint destination);
        bool Dispatch(IPEndPoint destination);
        void Run();
        void GetUI();
    }
}
