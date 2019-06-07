using Microsoft.Win32;
using MobileAgent.AgentManager;
using System;
using System.Management;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentOSSP : Agent
    {
        #region Fields
        static string _info = "";
        #endregion Fields

        #region Constructors
        public AgentOSSP()
        {
            this.SetName("AgentOSSP");
            this.SetAgentInfo("Get Agency Operating System Service Pack Info");
        }
        public AgentOSSP(int id) : base(id)
        {
            
        }
        #endregion Constructors

        #region Private Methods
        private void GetOSServicePackInfo()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["CSDVersion"] != null)
                {
                    _info += "Operating System Service Pack   :  " + managementObject["CSDVersion"].ToString() + Environment.NewLine;
                }
            }
        }

        #endregion Private Methods

        #region Public Methods
        public override void Run()
        {
            GetOSServicePackInfo();
            this.SetAgentCodebase(_info);

        }
        #endregion Public Methods
    }
}
