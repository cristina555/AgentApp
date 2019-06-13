using MobileAgent.AgentManager;
using System;
using System.Management;

namespace AgentApp.Agents.StationaryAgents
{
    [Serializable]
    public class AgentOS : Agent , IStationary
    {
        #region Fields
        static string _info = "";
        #endregion Fields

        #region Constructors
        public AgentOS()
        {
            this.SetName("AgentOS");
            this.SetAgentInfo("Get Agency Operating System Info");
        }
        public AgentOS(int id) : base(id)
        {
            
        }
        #endregion Constructors

        #region Private Methods
        private void GetOSInfo()
        {
            
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["Caption"] != null)
                {
                    _info += "Sistem de operare: " + managementObject["Caption"].ToString() + Environment.NewLine;
                }
                else
                {
                    _info += "Nu s-a gasit sistemul de operare!";
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
            SetAgentCodebase("");
            GetOSInfo();
            this.SetAgentCodebase(_info);
            return GetAgentCodebase();
        }
        #endregion Public Methods
    }
}
