using AgentApp.AditionalClasses;
using AgentApp.Agents;
using MobileAgent.AgentManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgentApp
{
    public partial class AgencyForm : Form
    {
        MobileAgent.AgentManager.Agency _agency = null;
        ConfigParser _configParser = null;
        GeneralSettings gs = null;
        Random r;
        
        public AgencyForm()
        {
            
            InitializeComponent();
            _configParser = new ConfigParser();
            gs = new GeneralSettings();
            r = new Random();
            StartAgency();
            CreateStationaryAgent();
            FillAgentsList();
           

        }
        private void StartAgency()
        {
            List<IPEndPoint> _hosts = _configParser.GetHosts();
            int hostIndex = r.Next(_hosts.Count);


            IPAddress ipAddress = _hosts[hostIndex].Address;
            int port = _hosts[hostIndex].Port;

            //_configParser.DeleteHost(_hosts[hostIndex]);
            _hosts.Remove(_hosts[hostIndex]);
            UpdateFreeAgencies(_hosts);

            _agency = new MobileAgent.AgentManager.Agency(ipAddress, port);
            info.Text = "Agentia se afla la " + ipAddress + ", portul " + port;
            _agency.Activate();
            _agency.Start();

            _agency.OnArrival+= new Agency.dgEventRaiser(agent_OnArrival);
            //UpdateAgentsList();

        }
        public void agent_OnArrival()
        {
            AgentProxy agent = _agency.GetAgentProxies().Last();
            console.Text += "Agentul ruleaza...";
            console.Text += agent.GetAgentCodebase();
        }
        private void CreateStationaryAgent()
        {
            Random random = new Random();
            int agenctID = random.Next(1000, 9999);
            AgentSystemInfo agentSystemInfo = new AgentSystemInfo(agenctID);
            int idHost = _agency.GetAgencyID();
            agentSystemInfo.SetAgencyHost(idHost);
            agentSystemInfo.SetAgentInfo("Get Agency System Info");
            agentSystemInfo.SetAgentContext(_agency.GetAgencyContext());
            _agency.SetStationaryAgent(agentSystemInfo);
            UpdateAgentsList();
            textBoxStationaryAgent.Text = agentSystemInfo.GetAgentInfo();
        }
        private void UpdateAgentsList()
        {
            List<AgentProxy> _agentsList = _agency.GetAgentProxies();
            listAgents.Items.Clear();
            if (_agentsList != null)
            {
                foreach (AgentProxy agentProxy in _agentsList)
                {
                    agentsList.Text = agentProxy.GetAgentInfo() + " " + agentProxy.GetAgentId() + System.Environment.NewLine;
                    listAgents.Items.Add(agentProxy.GetAgentInfo());
                }
            }
        }
        private void UpdateFreeAgencies(List<IPEndPoint> iPEnds)
        {
            foreach(IPEndPoint ipAddress in iPEnds)
            {
               connectedAgencies.Items.Add(ipAddress.Address);
            }
        }
        private void createButton_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = this.listAgents.GetItemText(this.comboBoxAgents.SelectedItem);
                Random random = new Random();
                int agentID = random.Next(1000, 9999);
                AgentProxy agentToCreate= gs.GetAgentProxy(selected);
                agentToCreate.SetAgentId(agentID);
                int idHost = _agency.GetAgencyID();
                agentToCreate.SetAgencyHost(idHost);
                agentToCreate.SetAgentContext(_agency.GetAgencyContext());
                _agency.CreateAgent(agentToCreate);
                UpdateAgentsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
           // _configParser.Reset(_agency.GetAgencyContext().Address);
            _agency.ShutDown();
            this.Close();
        }
        private void FillAgentsList()
        {            
            List<AgentProxy> ags = gs.GetStaticListofAgents();
            foreach(AgentProxy aproxy in ags)
            {
                comboBoxAgents.Items.Add(aproxy.GetAgentInfo());
            }
        }

    }
}
