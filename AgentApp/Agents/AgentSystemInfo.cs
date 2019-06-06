using Microsoft.Win32;
using MobileAgent.AgentManager;
using System;
using System.Management;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentSystemInfo : Agent
    {
        #region Fields
        static string _info="";
        #endregion Fields

        #region Constructors
        public AgentSystemInfo()
        {
            
        }
        public AgentSystemInfo(int id) : base(id)
        {
            this.SetName("AgentSystemInfo");
            this.SetAgentInfo("Get Agency System Info");
        }
        #endregion Constructors

        #region Properties
        public string GetOperatingSystem { get; private set; }
        public string GetOperatingSystemArchitecture { get; private set; }
        public string GetSystemServicePack { get; private set; }
        public string GetProcesor { get; private set; }
        #endregion Properties

        #region Private Methods
        private void GetOperatingSystemInfo()
        {
            //Console.WriteLine("Displaying operating system info...\n");
            _info += "Displaying operating system info...\n\n";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["Caption"] != null)
                {
                    //Console.WriteLine("Operating System: " + managementObject["Caption"].ToString());   //Display operating system caption
                    _info += "Operating System: " + managementObject["Caption"].ToString()+"\n";
                    GetOperatingSystem = managementObject["Caption"].ToString();

                }
                if (managementObject["OSArchitecture"] != null)
                {
                    //Console.WriteLine("Operating System Architecture  :  " + managementObject["OSArchitecture"].ToString());   //Display operating system architecture.
                    _info += "Operating System Architecture  :  " + managementObject["OSArchitecture"].ToString()+"\n";
                    GetOperatingSystemArchitecture = managementObject["OSArchitecture"].ToString();
                }
                if (managementObject["CSDVersion"] != null)
                {
                    //Console.WriteLine("Operating System Service Pack   :  " + managementObject["CSDVersion"].ToString());     //Display operating system version.
                    _info += "Operating System Service Pack   :  " + managementObject["CSDVersion"].ToString()+"\n";
                    GetSystemServicePack = managementObject["CSDVersion"].ToString();
                }
            }
        }

        private void GetProcessorInfo()
        {
            //Console.WriteLine("\n\nDisplaying Processor Name...\n");
            _info += "\n\nDisplaying Processor Name...\n\n";
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);

            if (processor_name != null)
            {
                if (processor_name.GetValue("ProcessorNameString") != null)
                {
                    //Console.WriteLine(processor_name.GetValue("ProcessorNameString"));   //Display processor ingo.
                    _info += processor_name.GetValue("ProcessorNameString")+"\n";
                    GetProcesor = processor_name.GetValue("ProcessorNameString").ToString();
                }
            }
        }
        #endregion Private Methods

        #region Public Methods
        public override void Run()
        {
            GetOperatingSystemInfo();
            GetProcessorInfo();
            this.SetAgentCodebase(_info);

        }
        #endregion Public Methods
    }
}
