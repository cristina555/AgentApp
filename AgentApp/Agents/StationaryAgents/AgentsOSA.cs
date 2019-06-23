using Microsoft.Win32;
using MobileAgent.AgentManager;
using System;
using System.Management;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentOSA : Agent, IStationary
    {
        #region Fields
        static string _info = "";
        #endregion Fields

        #region Constructors
        public AgentOSA() : base()
        {
            this.SetName("AgentOSA");
            this.SetAgentInfo("Informatii despre arhitectura sistemului de operare.");
        }
        public AgentOSA(int id) : base(id)
        {
            
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
                    _info += "Nu s-a gasit arhitectura sistemului de operare!";
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

        public override String GetInfo()
        {
            ResetLifetime();
            SetAgentStateInfo("");
            GetOSArchitectureInfo();
            this.SetAgentStateInfo(_info);
            return GetAgentStateInfo();
        }
        #endregion Public Methods
    }
}
