using AgentApp.AditionalClasses;
using AgentApp.Agents;
using MobileAgent.AgentManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;
using MobileAgent.Exceptions;
using System.Threading;

namespace AgentApp
{
    public partial class AgencyForm : Form
    {
        Agency _agency = null;
        ConfigParser _configParser = null;
        GeneralSettings gs = null;
        Random r;
        Dictionary<IPAddress, Tuple<string, int, string[]>> _hosts = null;


        public AgencyForm()
        {
            
            InitializeComponent();
            _configParser = new ConfigParser();
            gs = new GeneralSettings();
            r = new Random();
            _hosts = new Dictionary<IPAddress, Tuple<string, int, string[]>>();
            StartAgency();
            CreateStationaryAgent();
            FillAgentsList();
           

        }
        private void StartAgency()
        {

            //Real
            //Dictionary<IPAddress, Tuple<string, int, string[]>> _hosts = _configParser.GetNetworkHosts();
            //try
            //{
            //    IPAddress ipAddress = IPAddress.Parse(GetLocalIPAddress());
            //    Tuple<string, int, string[]> t = _hosts[ipAddress];
            //    int port = t.Item2;
            //    string name = t.Item1;
            //    string[] neighbours = t.Item3;

            //    _agency = new Agency(ipAddress, port);
            //    _agency.SetName = name;
            //    _agency.SetNeighboursHosts = neighbours;
            //    info.Text = "Agentia: " + name + " se afla la " + ipAddress + ", portul " + port;
            //    _agency.Activate();
            //    _agency.Start();
            //    _agency.OnArrival += new Agency.dgEventRaiser(agent_OnArrival);
            //}
            //catch (FormatException e)
            //{
            //    MessageBox.Show("Adresa IP invalida!");
            //}

            // simulare
            _hosts = _configParser.GetNetworkHosts();
            try
            {

                int hostIndex = r.Next(_hosts.Count);
                IPAddress ipAddress = _hosts.ElementAt(hostIndex).Key;
                Tuple<string, int, string[]> t = _hosts[ipAddress];
                int port = t.Item2;
                string name = t.Item1;
                string[] neighbours = t.Item3;

                _agency = new Agency(ipAddress, port);
                _agency.SetName = name;
                _agency.SetNeighboursHosts = neighbours;
                info.Text = "Agentia: " + name + " se afla la " + ipAddress + ", portul " + port;
                _agency.Activate();
                _agency.Start();
                _agency.OnArrival += new Agency.dgEventRaiser(agent_OnArrival);
            }
            catch (FormatException e)
            {
                MessageBox.Show("Adresa IP invalida!");
            }

        }
        public void agent_OnArrival()
        {
            AgentProxy agent = _agency.GetAgentProxies().Last();
            console.Text += "Agentul ruleaza...";
            console.Text += agent.GetAgentCodebase();
            UpdateAgentsList();

        }
        private void CreateStationaryAgent()
        {
            Random random = new Random();
            int agentID = random.Next(1000, 9999);
            AgentSystemInfo agentSystemInfo = new AgentSystemInfo(agentID);
            int idHost = _agency.GetAgencyID();
            agentSystemInfo.SetAgentInfo("Get Agency System Info");
            agentSystemInfo.SetAgentContext(_agency.GetAgencyContext());
            _agency.SetStationaryAgent(agentSystemInfo);
            UpdateAgentsList();
            textBoxStationaryAgent.Text = agentSystemInfo.GetAgentInfo();
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
                    agentsList.Text = agentProxy.GetAgentInfo() + " " + agentProxy.GetAgentId() + System.Environment.NewLine;
                    listAgents.Items.Add(agentProxy.GetAgentInfo());
                }
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
                Form ui = gs.GetUI(agentToCreate);
                agentToCreate.SetAgentId(agentID);
                int idHost = _agency.GetAgencyID();
                agentToCreate.SetAgencyCreationContext(_agency.GetAgencyContext());
                agentToCreate.SetAgentContext(_agency.GetAgencyContext());

                var thread = new Thread(() =>
                {
                   Application.Run(ui);
                });
                thread.Start();

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
                string ipAddress = textBoxIpAddress.Text;
                int portNumber = Int32.Parse(textBoxPort.Text);
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), portNumber);
                _agency.Dispatch(agentDispatched, ipEndPoint);
                _agency.OnDispatching += new Agency.dgEventRaiser(UpdateAgentsList);
            }
            catch (AgentNotFoundException anfe)
            {
                MessageBox.Show("AgentNotFoundException caught!!!");
                MessageBox.Show("Source : " + anfe.Source);
                MessageBox.Show("Message : " + anfe.Message);
            }
            catch (IOException io)
            {
                MessageBox.Show("IOException caught!!!");
                MessageBox.Show("Source : " + io.Source);
                MessageBox.Show("Message : " + io.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception caught!!!");
                MessageBox.Show("Source : " + ex.Source);
                MessageBox.Show("Message : " + ex.Message);

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
            Dictionary<AgentProxy, Form> ags = gs.GetStaticListofAgents();
            foreach(AgentProxy aproxy in ags.Keys)
            {
                comboBoxAgents.Items.Add(aproxy.GetAgentInfo());
            }
        }
        public static string GetLocalIPAddress()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    if (ni.Name == "Wi-Fi")
                    {
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                return ip.Address.ToString();
                            }
                        }
                    }
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");

        }

    }
}
