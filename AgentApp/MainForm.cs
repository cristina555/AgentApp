using AgentApp.AditionalClasses;
using AgentApp.Agents;
using MobileAgent.AgentManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgentApp
{
    public partial class MainForm : Form
    {
        Agency _agency;
        ConfigParser _configParser;
        
        Random r;
        
        public MainForm()
        {
            
            InitializeComponent();
            _configParser = new ConfigParser();
            r = new Random();
            StartAgency();
        }
        private void StartAgency()
        {
            Dictionary<IPAddress, bool> _hosts = _configParser.GetHosts();
            int hostIndex = r.Next(_hosts.Count);


            IPAddress ipAddress = _hosts.ElementAt(hostIndex).Key;
            int port = r.Next(1000, 2000);

            _configParser.UpdateHosts(ipAddress, "true");

            _agency = new Agency(ipAddress, port);
            info.Text = "Agentia se afla la " + ipAddress + ", portul " + port;
            _agency.Activate();
            _agency.Start();
            UpdateAgentsList();

        }
        private void UpdateAgentsList()
        {
            List<AgentProxy> _agentsList = _agency.GetAgentProxies();
            agentsList.Clear();
            listAgents.Items.Clear();
            if (_agentsList != null)
            {
                foreach (AgentProxy agentProxy in _agentsList)
                {
                    agentsList.Text = agentProxy.GetAgentCodebase() + " " + agentProxy.GetAgentId() + System.Environment.NewLine;
                    listAgents.Items.Add(agentProxy.GetAgentCodebase());
                }
            }
        }
        private void UpdateFreeAgencies()
        {
            Dictionary<IPAddress, bool> _hosts = _configParser.GetHosts();
            foreach(IPAddress ipAddress in _hosts.Keys)
            {
                if(_hosts[ipAddress] == true)
                {
                    connectedAgencies.Items.Add(ipAddress);
                }
            }
        }
        private void createButton_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int agenctID = random.Next(1000, 9999);
            AgentInfo agent = new AgentInfo(agenctID);
            int idHost = _agency.GetAgencyID();
            agent.SetAgencyHost(idHost);
            agent.SetAgentCodebase("Get Info");
            agent.SetAgentContext(_agency.GetAgencyContext());
            _agency.CreateAgent(agent);
            UpdateAgentsList();
        }

        private void dispatchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = this.listAgents.GetItemText(this.listAgents.SelectedItem);
                AgentProxy agentDispatched = _agency.GetAgentProxy(selected);
                string ipAddress = this.connectedAgencies.GetItemText(this.connectedAgencies.SelectedItem);
                int portNumber = Int32.Parse(textBoxPort.Text);
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), portNumber);
                _agency.Dispatch(agentDispatched, ipEndPoint);
                UpdateAgentsList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                        
        }

        private void disposeButton_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateAgentsList();
        }

        private void deactivateButton_Click(object sender, EventArgs e)
        {
            _configParser.Reset(_agency.GetAgencyContext().Address);
            _agency.ShutDown();
            this.Close();
        }

    }
}
