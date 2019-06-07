using Microsoft.Win32;
using MobileAgent.AgentManager;
using System;
using System.Management;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentOSA : Agent
    {
        #region Fields
        static string _info = "";
        #endregion Fields

        #region Constructors
        public AgentOSA()
        {
            this.SetName("AgentOSA");
            this.SetAgentInfo("Get Agency Operating System Architecture Info");
        }
        public AgentOSA(int id) : base(id)
        {
            
        }
        #endregion Constructors

        #region Private Methods
        private void GetOSArchitectureInfo()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject managementObject in mos.Get())
            {

                if (managementObject["OSArchitecture"] != null)
                {
                    _info += "Operating System Architecture  :  " + managementObject["OSArchitecture"].ToString() + Environment.NewLine;
                }               
            }
        }
        #endregion Private Methods

        #region Public Methods
        public override void Run()
        {
            GetOSArchitectureInfo();
            this.SetAgentCodebase(_info);

        }
        #endregion Public Methods
    }
}
