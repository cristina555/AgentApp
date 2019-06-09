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
        #region Properties
        public int ID { get; set; }

        #endregion Properties
        #region Constructor
        public AgentRemoteUI(int id)
        {
            InitializeComponent();
            ID = id;
        }
        #endregion Constructor

        #region Private Methods
        //private AgentProxy KeyByValue(string agentName)
        //{
        //    GeneralSettings gs = new GeneralSettings();
        //    Dictionary<AgentProxy, string> dict = AgencyForm.gs.ListofAgentsM;
        //    foreach (KeyValuePair<AgentProxy, string> pair in dict)
        //    {
        //        if (pair.Key.GetName() == agentName)
        //        {
        //            return pair.Key;
        //        }
        //    }
        //    return null;
        //}
        #endregion Private Methods

        #region Controlers
        private void buttonSend_Click(object sender, EventArgs e)
        {
            try
            {
                AgentRemote agentRemote = null;
                List<string> _parameters = new List<string>();
                foreach (object itemChecked in checkedListBox1.CheckedItems)
                {
                    _parameters.Add(itemChecked.ToString());
                }
                agentRemote = (AgentRemote)AgencyForm.agency.GetMobileAgentProxy(ID); 
                agentRemote.Parameters = _parameters;
                ConfigParser configParser = new ConfigParser();
                Dictionary<IPAddress, Tuple<string, int, string[]>> hosts = configParser.NetworkHosts;
                Close();
            }
            catch(NullReferenceException nre)
            {
                MessageBox.Show("NullReferenceException !" + nre.Message + " --> Trimite parametrii agentului!");

            }
            catch(Exception ex)
            {
                MessageBox.Show("Exception !" + ex.Message + " --> Trimite parametrii agentului!");
            }
        }
        #endregion controlers
    }
}
