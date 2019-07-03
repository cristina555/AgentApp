using System.Collections.Generic;
using System.Net;


namespace MobileAgent.AgentManager
{
    public interface IMobile : IAgentProxy
    {
        List<IMobile> GetCloneList();
        IMobile GetClone(int id);
        IMobile GetParent();
        int GetWorkType();
        void SetParent(IMobile ap);
        void SetWorkType(int status);
       
        IMobile Clone();
        bool Dispatch(IPEndPoint destination);
        void Run();
        void GetUI();
    }
}
