using MobileAgent.AgentManager;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AgentApp.Agents;
using AgentApp.AditionalClasses;
using System.Net;

namespace AgentApp.Interfaces
{
    public partial class AgentRemoteUI : Form
    {
        public AgentRemoteUI()
        {
            InitializeComponent();            
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            AgentRemote agentRemote = null;
            List<string> _parameters = new List<string>();
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                _parameters.Add(itemChecked.ToString());
            }
            GeneralSettings gs = new GeneralSettings();
            Dictionary<AgentProxy, Form> dict = gs.GetStaticListofAgents();
            foreach(KeyValuePair<AgentProxy, Form> pair in dict) 
            {
                if (pair.Key.GetAgentInfo() == "Collect information from network")
                {
                    agentRemote= (AgentRemote)pair.Key;
                }
            }
            agentRemote.SetParameters = _parameters;
            ConfigParser configParser = new ConfigParser();
            Dictionary<IPAddress, Tuple<string, int, string[]>> hosts = configParser.GetNetworkHosts();

            agentRemote.SetHosts = hosts;
            this.Close();
        }
        

    }
}
