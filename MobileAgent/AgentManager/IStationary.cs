using System;

namespace MobileAgent.AgentManager
{
    public interface IStationary : IAgentProxy
    {
        String GetInfo();
    }
}
