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
        public MainForm()
        {
            
            InitializeComponent();

            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            Random random = new Random();
            int port = random.Next(1000, 2000);
            _agency = new Agency(ipAddress, port);
            info.Text = "Agentia a fost creata la portul " + port;
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

        private void createButton_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int agenctID = random.Next(1000, 9999);
            AgentPI agent = new AgentPI(agenctID);
            int idHost = _agency.GetAgencyID();
            agent.SetAgencyHost(idHost);
            agent.SetAgentCodebase("Calculate PI");
            agent.SetDec = 4;
            _agency.CreateAgent(agent);
            UpdateAgentsList();
        }

        private void dispatchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = this.listAgents.GetItemText(this.listAgents.SelectedItem);
                AgentProxy agentDispatched = _agency.GetAgentProxy(selected);
                int portNumber = Int32.Parse(textBoxPort.Text);
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portNumber);
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
    }
}
