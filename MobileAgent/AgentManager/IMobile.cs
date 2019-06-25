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
        void Clone();
        //bool GetConnection(IPEndPoint destination);
        bool Dispatch(IPEndPoint destination);
        void Run();
        void GetUI();
    }
}
