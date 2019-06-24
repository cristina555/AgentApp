using Microsoft.Win32;
using MobileAgent.AgentManager;
using System;
using System.Management;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentOSSP : Agent, IStationary
    {
        #region Private Static Fields
        private static string _info = "";
        #endregion Private Static Fields

        #region Constructors
        public AgentOSSP() : base()
        {
            this.SetName("AgentOSSP");
            this.SetAgentInfo("Informatii legate de pachetul de servicii al sistemului de operare.");
        }
        public AgentOSSP(int id) : base(id)
        {
            this.SetName("AgentOSSP");
            this.SetAgentInfo("Informatii legate de pachetul de servicii al sistemului de operare.");
        }
        #endregion Constructors

        #region Private Methods
        private void GetOSServicePackInfo()
        {
            _info = "";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["CSDVersion"] != null)
                {
                    _info += "Sistemul de operare( Service Pack):  " + managementObject["CSDVersion"].ToString() + Environment.NewLine;
                }
                else
                {
                    _info += "Nu s-a gasit sistemul de operare - Service Pack!";
                }
            }
        }

        #endregion Private Methods

        #region Public Override Methods
        public override String GetInfo()
        {
            ResetLifetime();
            SetAgentStateInfo("");
            GetOSServicePackInfo();
            this.SetAgentStateInfo(_info);
            return GetAgentStateInfo();
        }
        public override void Run()
        {
            throw new NotImplementedException();

        }
        public override void GetUI()
        {
            throw new NotImplementedException();
        }
        #endregion Public Override Methods
    }
}
