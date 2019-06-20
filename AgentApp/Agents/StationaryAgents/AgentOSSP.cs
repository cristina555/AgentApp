using Microsoft.Win32;
using MobileAgent.AgentManager;
using System;
using System.Management;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentOSSP : Agent, IStationary
    {
        #region Fields
        static string _info = "";
        #endregion Fields

        #region Constructors
        public AgentOSSP() : base()
        {
            this.SetName("AgentOSSP");
            this.SetAgentInfo("Informatii legate de pachetul de servicii al sistemului de operare.");
        }
        public AgentOSSP(int id) : base(id)
        {
            
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

        #region Public Methods
        public override void Run()
        {
            throw new NotImplementedException();

        }
        public override void GetUI()
        {
            throw new NotImplementedException();
        }

        public String GetInfo()
        {
            SetAgentStateInfo("");
            GetOSServicePackInfo();
            this.SetAgentStateInfo(_info);
            return GetAgentStateInfo();
        }
        #endregion Public Methods
    }
}
