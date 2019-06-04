using Microsoft.Win32;
using MobileAgent.AgentManager;
using System;
using System.Management;

namespace AgentApp.Agents
{
    public class AgentSystemInfo : Agent
    {
        #region Fields
        static String _info="";
        #endregion Fields

        #region Constructors
        public AgentSystemInfo()
        {

        }
        public AgentSystemInfo(int id) : base(id)
        {

        }
        #endregion Constructors

        #region Methods
        public static void getOperatingSystemInfo()
        {
            Console.WriteLine("Displaying operating system info...\n");
            _info += "Displaying operating system info...\n\n";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["Caption"] != null)
                {
                    Console.WriteLine("Operating System: " + managementObject["Caption"].ToString());   //Display operating system caption
                    _info += "Operating System: " + managementObject["Caption"].ToString()+"\n";
                }
                if (managementObject["OSArchitecture"] != null)
                {
                    Console.WriteLine("Operating System Architecture  :  " + managementObject["OSArchitecture"].ToString());   //Display operating system architecture.
                    _info += "Operating System Architecture  :  " + managementObject["OSArchitecture"].ToString()+"\n";
                }
                if (managementObject["CSDVersion"] != null)
                {
                    Console.WriteLine("Operating System Service Pack   :  " + managementObject["CSDVersion"].ToString());     //Display operating system version.
                    _info += "Operating System Service Pack   :  " + managementObject["CSDVersion"].ToString()+"\n";
                }
            }
        }

        public static void getProcessorInfo()
        {
            Console.WriteLine("\n\nDisplaying Processor Name...\n");
            _info += "\n\nDisplaying Processor Name...\n\n";
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);

            if (processor_name != null)
            {
                if (processor_name.GetValue("ProcessorNameString") != null)
                {
                    Console.WriteLine(processor_name.GetValue("ProcessorNameString"));   //Display processor ingo.
                    _info += processor_name.GetValue("ProcessorNameString")+"\n";

                }
            }
        }
        public override void Run()
        {
            this.SetAgentCodebase(_info);
            getOperatingSystemInfo();
            getProcessorInfo();
        }
        public override void GetUI()
        {
            throw new NotImplementedException();
        }
        #endregion Methods
    }
}
