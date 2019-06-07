using Microsoft.Win32;
using MobileAgent.AgentManager;
using System;
using System.Management;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentP : Agent
    {
        #region Fields
        static string _info = "";
        #endregion Fields   

        #region Constructors
        public AgentP()
        {
            this.SetName("AgentP");
            this.SetAgentInfo("Get Agency Processor Info");
        }
        public AgentP(int id) : base(id)
        {
            
        }
        #endregion Constructors

        #region Private Methods
        private void GetProcessorInfo()
        {
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);
            if (processor_name != null)
            {
                if (processor_name.GetValue("ProcessorNameString") != null)
                {
                    _info += processor_name.GetValue("ProcessorNameString") + Environment.NewLine;
                }
            }
        }
        #endregion Private Methods

        #region Public Methods
        public override void Run()
        {
            GetProcessorInfo();
            this.SetAgentCodebase(_info);
        }
        #endregion Public Methods
    }
}
