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
        #region Private Fields
        Agency _agency = null;        
        Random r;
        #endregion Private Fields

        #region Public Fields
        public static ConfigParser _configParser = null;
        public static GeneralSettings gs = null;
        #endregion Public Fields

        #region Constructors
        public AgencyForm()
        {
            
            InitializeComponent();
            _configParser = new ConfigParser();
            gs = new GeneralSettings();
            r = new Random();
            StartAgency();
            CreateStationaryAgent();
            FillAgentsList();
            FillIPAddressAndPortsList();
        }
        #endregion Constructors 

        #region Private Methods
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
            Dictionary<IPAddress, Tuple<string, int, string[]>>  _hosts = _configParser.NetworkHosts;
            try
            {

                int hostIndex = r.Next(_hosts.Count);
                IPAddress ipAddress = _hosts.ElementAt(hostIndex).Key;
                Tuple<string, int, string[]> t = _hosts[ipAddress];
                int port = t.Item2;
                string name = t.Item1;
                string[] neighbours = t.Item3;

                _agency = new Agency(ipAddress, port);
                this.Text += " " + name;
                _agency.SetName(name);
                _agency.SetNeighbours(new List<string>(neighbours));
                info.Text = "Agentia: " + name + " se afla la " + ipAddress + ", portul " + port;
                _agency.Activate();
                _agency.Start();
                _agency.OnArrival += new Agency.dgEventRaiser(agent_OnArrival);
            }
            catch (FormatException fe)
            {
                MessageBox.Show("FormatException !" + fe.Message + " --> Start Agentie, adresa IP invalida!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception !" + ex.Message + " --> Start Agentie!");
            }

        }
        private void agent_OnArrival()
        {
            AgentProxy agent = _agency.GetAgentProxies().Last();
            console.Text += "Agentul " + agent.GetName() + " ruleaza..." + Environment.NewLine;
            console.Text += agent.GetAgentCodebase() + Environment.NewLine;
            UpdateAgentsList();
        }
        private void CreateStationaryAgent()
        {
            Random random = new Random();
            int agentID = random.Next(1000, 9999);
            AgentSystemInfo agentSystemInfo = new AgentSystemInfo(agentID);
            agentSystemInfo.SetAgentCurrentContext(_agency);
            _agency.SetStationaryAgent(agentSystemInfo);
            textBoxStationaryAgent.Text = agentSystemInfo.GetName() + "-> " + agentSystemInfo.GetAgentInfo() + System.Environment.NewLine;
        }
        private void UpdateAgentsList()
        {
            List<AgentProxy> _agentsList = _agency.GetAgentProxies();
            agentsList.Clear();
            listAgents.Items.Clear();
            listAgents.Text = "";
            if (_agentsList != null)
            {
                foreach (AgentProxy agentProxy in _agentsList)
                {
                    agentsList.Text += agentProxy.GetName() + "-> " + agentProxy.GetAgentInfo() + System.Environment.NewLine;
                    listAgents.Items.Add(agentProxy.GetName());
                    
                }
            }
        }
        private void FillIPAddressAndPortsList()
        {
            Dictionary<IPAddress, Tuple<string, int, string[]>> _hosts = _configParser.NetworkHosts;
            foreach (IPAddress aproxy in _hosts.Keys)
            {
                comboBoxIPAddresses.Items.Add(aproxy);
                comboBoxPorts.Items.Add(_hosts[aproxy].Item2);
                
            }
            foreach(string s in _hosts[_agency.GetAgencyIPEndPoint().Address].Item3 )
            {
                comboBoxN.Items.Add(s);
            }
        }
        private void FillAgentsList()
        {
            Dictionary<AgentProxy, Form> ags = gs.StaticListofAgents;
            foreach (AgentProxy aproxy in ags.Keys)
            {
                comboBoxAgents.Items.Add(aproxy.GetName());
            }
        }
        private static string GetLocalIPAddress()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    if ("Wi-Fi Wireless Network Connection".Contains(ni.Name))
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
        #endregion Private Methods

        #region Controlers
        private void createButton_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = this.listAgents.GetItemText(this.comboBoxAgents.SelectedItem);
                Random random = new Random();
                int agentID = random.Next(1000, 9999);

                AgentProxy agentToCreate = gs.GetAgentProxy(selected);
                Form ui = gs.GetUI(agentToCreate);
                agentToCreate.SetAgentId(agentID);
                agentToCreate.SetAgencyCreationContext(_agency.GetAgencyIPEndPoint());
                agentToCreate.SetAgentCurrentContext(_agency);
                if (ui.Controls.Count != 0)
                {
                    var thread = new Thread(() =>
                    {
                        Application.Run(ui);
                    });
                    thread.Start();
                }
                _agency.CreateAgent(agentToCreate);
                UpdateAgentsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception ! Mesaj: " + ex.Message + " -> Creare agent!");
            }

        }
        private void dispatchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = this.listAgents.GetItemText(listAgents.SelectedItem);
                AgentProxy agentDispatched = _agency.GetAgentProxy(selected);

                //string ipAddress = comboBoxIPAddresses.GetItemText(comboBoxIPAddresses.SelectedItem);
                //int portNumber = Int32.Parse(comboBoxPorts.GetItemText(comboBoxPorts.SelectedItem));
                string location = comboBoxN.GetItemText(comboBoxN.SelectedItem);
                IPAddress ipAddress = _configParser.GetIPAdress(location);
                int portNumber = _configParser.GetPort(location);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
                _agency.Dispatch(agentDispatched, ipEndPoint);
                //_agency.OnRefuseConnection += new Agency.dgEventRaiser1(agent_OnRefuse);
                UpdateAgentsList();
            }
            catch (AgentNotFoundException anfe)
            {
                MessageBox.Show("AgentNotFoundException ! Mesaj: " + anfe.Message + " -> Trimite agent!");
            }
            catch (NullReferenceException nfe)
            {
                MessageBox.Show("NullReferenceException caught! Mesaj : " + nfe.Message + " " + nfe.StackTrace + " --> Agency Dispach Agent.");
            }
            catch (SocketException io)
            {
                MessageBox.Show("SocketException ! Mesaj: " + io.Message + " -> Trimite agent!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception ! Mesaj: " + ex.Message +" -> Trimite agent!");
            }
        }
        private void dispatchButtonNetwork_Click(object sender, EventArgs e)
        {
            string selected = this.listAgents.GetItemText(listAgents.SelectedItem);

            AgentRemote agentDispatched = (AgentRemote)_agency.GetAgentProxy(selected);
            agentDispatched.agenciesVisited.Add(_agency.GetName());
            foreach (string s in _agency.GetNeighbours())
            {
                if (!agentDispatched.agenciesVisited.Contains(s))
                {
                    agentDispatched.queue.Enqueue(s);
                    agentDispatched.agenciesVisited.Add(s);
                }
            }
            string next = agentDispatched.queue.Peek();
            IPAddress ipAddress = _configParser.GetIPAdress(next);
            int portNumber = _configParser.GetPort(next);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
            _agency.Dispatch(agentDispatched, ipEndPoint);
            UpdateAgentsList();
        }
        public void agent_OnRefuse()
        {
            MessageBox.Show("Nu se poate realiza connexiunea ");
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
            _agency.ShutDown();
            this.Close();
        }
        #endregion Controlers        
    }
}
