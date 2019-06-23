using MobileAgent.AgentManager;
using System;
using System.Management;


namespace AgentApp.Agents.StationaryAgents
{
    [Serializable]
    public class AgentVC : Agent, IStationary
    {
        #region Fields
        static string _info = "";
        #endregion Fields

        #region Constructors
        public AgentVC() : base()
        {
            this.SetName("AgentVC");
            this.SetAgentInfo("Informatii despre placa video.");
        }
        public AgentVC(int id) : base(id)
        {

        }
        #endregion Constructors

        #region Private Methods
        private void GetVCInfo()
        {
            _info = "";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration");
            foreach (ManagementObject mo in mos.Get())
            {
                foreach (PropertyData property in mo.Properties)
                {
                    if (property.Name == "Description")
                    {
                        _info += "Placa video: " + property.Value.ToString();
                    }

                }
            }
            if (_info == null)
            {
                _info += "Nu s-a gasit descrierea placii video";
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
            GetVCInfo();
            this.SetAgentStateInfo(_info);
            return GetAgentStateInfo();
        }
        #endregion Public Methods
    }
}
