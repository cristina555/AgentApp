using Microsoft.Win32;
using MobileAgent.AgentManager;
using System;
using System.Management;

namespace AgentApp.Agents
{
    public class AgentSystemInfo : Agent
    {
        #region Fields
        static string _info="";
        string _operatingSystem;
        string _operatingSystemArchitecture;
        string _operatingSystemServicePack;
        string _processor;
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
        public string GetOperatingSystem
        {
            get
            {
                return _operatingSystem;
            }
        }
        public string GetOperatingSystemArchitecture
        {
            get
            {
                return _operatingSystemArchitecture;
            }
        }
        public string GetSystemServicePack
        {
            get
            {
                return _operatingSystemServicePack;
            }
        }
        public string GetProcesor
        {
            get
            {
                return _processor;
            }
        }
        //public string SetOperatingSystem
        //{
        //    set
        //    {
        //        _operatingSystem = value;
        //    }
        //}
        //public string SetProcessor
        //{
        //    set
        //    {
        //        _processor = value;
        //    }
        //}
        #endregion Properties

        #region Methods
        private void GetOperatingSystemInfo()
        {
            string operatingSystem = "";
            //Console.WriteLine("Displaying operating system info...\n");
            _info += "Displaying operating system info...\n\n";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["Caption"] != null)
                {
                    //Console.WriteLine("Operating System: " + managementObject["Caption"].ToString());   //Display operating system caption
                    _info += "Operating System: " + managementObject["Caption"].ToString()+"\n";
                    _operatingSystem = managementObject["Caption"].ToString();

                }
                if (managementObject["OSArchitecture"] != null)
                {
                    //Console.WriteLine("Operating System Architecture  :  " + managementObject["OSArchitecture"].ToString());   //Display operating system architecture.
                    _info += "Operating System Architecture  :  " + managementObject["OSArchitecture"].ToString()+"\n";
                    _operatingSystemArchitecture = managementObject["OSArchitecture"].ToString();
                }
                if (managementObject["CSDVersion"] != null)
                {
                    //Console.WriteLine("Operating System Service Pack   :  " + managementObject["CSDVersion"].ToString());     //Display operating system version.
                    _info += "Operating System Service Pack   :  " + managementObject["CSDVersion"].ToString()+"\n";
                    _operatingSystemServicePack = managementObject["CSDVersion"].ToString();
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
                    _processor = processor_name.GetValue("ProcessorNameString").ToString();
                }
            }
        }
        public override void Run()
        {
            GetOperatingSystemInfo();
            GetProcessorInfo();
            this.SetAgentCodebase(_info);

        }
        #endregion Methods
    }
}
