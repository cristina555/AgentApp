﻿using Microsoft.Win32;
using MobileAgent.AgentManager;
using System;
using System.Management;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentP : Agent , IStationary
    {
        #region Private Static Fields
        private static string _info = "";
        #endregion Private Static Fields

        #region Constructors
        public AgentP() : base()
        {
            this.SetName("AgentP");
            this.SetAgentInfo("Informatii despre procesor.");
        }
        public AgentP(int id) : base(id)
        {
            this.SetName("AgentP");
            this.SetAgentInfo("Informatii despre procesor.");
        }
        #endregion Constructors

        #region Private Methods
        private void GetProcessorInfo()
        {
            _info = "";
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);
            if (processor_name != null)
            {
                if (processor_name.GetValue("ProcessorNameString") != null)
                {
                    _info += "Procesor: " + processor_name.GetValue("ProcessorNameString") + Environment.NewLine;
                }
                else
                {
                    _info += "Nu s-a gasit procesorul!"+ Environment.NewLine;
                }
            }
        }
        #endregion Private Methods

        #region Public Override Methods
        public override String GetInfo()
        {
            ResetLifetime();
            SetAgentStateInfo("");
            GetProcessorInfo();
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
