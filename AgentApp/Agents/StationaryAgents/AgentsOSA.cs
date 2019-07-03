using Microsoft.Win32;
using MobileAgent.AgentManager;
using System;
using System.Management;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentOSA : Agent, IStationary
    {
        #region Private Static Fields
        private static string _info = "";
        #endregion Private Static Fields

        #region Constructors
        public AgentOSA() : base()
        {
            this.SetName("AgentOSA");
            this.SetAgentInfo("Informatii despre arhitectura sistemului de operare.");
        }
        public AgentOSA(int id) : base(id)
        {
            this.SetName("AgentOSA");
            this.SetAgentInfo("Informatii despre arhitectura sistemului de operare.");
        }
        #endregion Constructors

        #region Private Methods
        private void GetOSArchitectureInfo()
        {
            _info = "";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["OSArchitecture"] != null)
                {
                    _info += "Arhitectura sistemului de operare:  " + managementObject["OSArchitecture"].ToString() + Environment.NewLine;
                }
                else
                {
                    _info += "Nu s-a gasit arhitectura sistemului de operare!"+ Environment.NewLine;
                }
            }
        }
        #endregion Private Methods

        #region Public Override Methods
        public override String GetInfo()
        {
            ResetLifetime();
            SetAgentStateInfo("");
            GetOSArchitectureInfo();
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
