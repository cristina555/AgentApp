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
        Random _random;
        #endregion Private Fields

        #region Public Fields
        public static Agency agency = null;
        public static ConfigParser configParser = null;
        public static GeneralSettings gs = null;
        #endregion Public Fields

        #region Constructors
        public AgencyForm()
        {
            
            InitializeComponent();
            configParser = new ConfigParser();
            gs = new GeneralSettings();
            _random = new Random();
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

            //    agency = new Agency(ipAddress, port);
            //    agency.SetName = name;
            //    agency.SetNeighboursHosts = neighbours;
            //    info.Text = "Agentia: " + name + " se afla la " + ipAddress + ", portul " + port;
            //    agency.Activate();
            //    agency.Start();
            //    agency.OnArrival += new Agency.dgEventRaiser(agent_OnArrival);
            //}
            //catch (FormatException e)
            //{
            //    MessageBox.Show("Adresa IP invalida!");
            //}

            // simulare
            Dictionary<IPAddress, Tuple<string, int, string[]>>  _hosts = configParser.NetworkHosts;
            try
            {

                int hostIndex = _random.Next(_hosts.Count);
                IPAddress ipAddress = _hosts.ElementAt(hostIndex).Key;
                Tuple<string, int, string[]> t = _hosts[ipAddress];
                int port = t.Item2;
                string name = t.Item1;
                string[] neighbours = t.Item3;

                agency = new Agency(ipAddress, port);
                this.Text += " " + name;
                agency.SetName(name);
                agency.SetNeighbours(new List<string>(neighbours));
                info.Text = "Agentia: " + name + " se afla la " + ipAddress + ", portul " + port;
                agency.Activate();
                agency.Start();
                agency.OnArrival += new Agency.dgEventRaiser(Agent_OnArrival);
                agency.RefuseConnection += Agency_RefuseConnection;
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
        private static void Agency_RefuseConnection( object sender, UnconnectedAgencyArgs e)
        {
            MessageBox.Show("Nu se poate realiza conexiunea cu agentia: " + e.Name);

        }
        private void Agent_OnArrival()
        {
            AgentProxy agent = agency.GetLastRunnableAgent();
            if(!agent.GetAgencyCreationContext().Equals(agency.GetAgencyIPEndPoint()))
            {
                console.Text += "Agentul " + agent.GetName() + " ruleaza..." + Environment.NewLine;
                console.Text += agent.GetAgentCodebase() + Environment.NewLine;
                //agent.SetAgentCodebase("");
                UpdateAgentsList();
            }
            else
            {
                console.Text += "Agentul " + agent.GetName() + " a adunat informatiile: " + Environment.NewLine;
                console.Text += agent.GetAgentCodebase() + Environment.NewLine;
            }
            UpdateAgentsList();
        }
        private void CreateStationaryAgent()
        {
            List<AgentProxy> list = gs.ListofAgentsS;
            foreach(AgentProxy ap in list)
            {
                int agentID = _random.Next(1000, 9999);
                ap.SetAgentId(agentID);
                ap.SetMobility(Agent.STATIC);
                agency.CreateAgent(ap);

            }
        }
        private void UpdateAgentsList()
        {
            List<AgentProxy> aList = agency.GetAgentProxies(Agent.MOBILE);
            agentsList.Clear();
            listAgents.Items.Clear();
            listAgents.Text = "";
            if (aList != null)
            {
                foreach (AgentProxy agentProxy in aList)
                {
                    if (agentProxy.GetMobility() == Agent.MOBILE)
                    {
                        agentsList.Text += agentProxy.GetAgentId() + ": "+agentProxy.GetName() + "-> " + agentProxy.GetAgentInfo() + System.Environment.NewLine;
                        listAgents.Items.Add(agentProxy.GetName());
                    }
                }
            }
        }
        private void FillIPAddressAndPortsList()
        {
            Dictionary<IPAddress, Tuple<string, int, string[]>> _hosts = configParser.NetworkHosts;
            foreach (IPAddress aproxy in _hosts.Keys)
            {
                comboBoxIPAddresses.Items.Add(aproxy);
                comboBoxPorts.Items.Add(_hosts[aproxy].Item2);
                
            }
            foreach(string s in _hosts[agency.GetAgencyIPEndPoint().Address].Item3 )
            {
                comboBoxN.Items.Add(s);
            }
        }
        private void FillAgentsList()
        {
            foreach (string aproxy in gs.ListofAgentsM)
            {
                comboBoxAgents.Items.Add(aproxy);
            }
            foreach (AgentProxy aproxy in gs.ListofAgentsS)
            {
                stationaryAgents.Text += aproxy.GetAgentId() +": " +aproxy.GetName() + "-> " + aproxy.GetAgentInfo() + Environment.NewLine;
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
                int agentID = _random.Next(1000, 9999);

                AgentProxy agentToCreate = gs.GetAgentProxy(selected);
                agentToCreate.SetMobility(Agent.MOBILE);
                agentToCreate.SetAgentId(agentID);

           
                Form ui = gs.GetUI(agentToCreate.GetName(), agentID);

                if (ui.Controls.Count != 0)
                {
                    var thread = new Thread(() =>
                    {
                        Application.Run(ui);
                    });
                    thread.Start();
                }

                agency.CreateAgent(agentToCreate);
                UpdateAgentsList();
            }
            catch(NullReferenceException nre)
            {
                MessageBox.Show("NullReferenceException ! Mesaj: " + nre.Message + " -> Creare agent!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception ! Mesaj: " + ex.Message + " -> Creare agent!");
            }
        }
        private void cloneButton_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = listAgents.GetItemText(listAgents.SelectedItem);
                int agentID = _random.Next(1000, 9999);

                AgentProxy agentToCreate = gs.GetAgentProxy(selected);
                agentToCreate.SetMobility(Agent.MOBILE);
                agentToCreate.SetAgentId(agentID);

                Form ui = gs.GetUI(selected, agentID);
                if (ui.Controls.Count != 0)
                {
                    var thread = new Thread(() =>
                    {
                        Application.Run(ui);
                    });
                    thread.Start();
                }

                agency.Clone(agentToCreate);
                UpdateAgentsList();
            }
            catch (NullReferenceException nre)
            {
                MessageBox.Show("NullReferenceException ! Mesaj: " + nre.Message + " -> Clonare agent!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception ! Mesaj: " + ex.Message + " -> Clonare agent!");
            }
        }
        private void dispatchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = listAgents.GetItemText(listAgents.SelectedItem);
                AgentProxy agentDispatched = agency.GetMobileAgentProxy(selected);

                //string ipAddress = comboBoxIPAddresses.GetItemText(comboBoxIPAddresses.SelectedItem);
                //int portNumber = Int32.Parse(comboBoxPorts.GetItemText(comboBoxPorts.SelectedItem));
                string location = comboBoxN.GetItemText(comboBoxN.SelectedItem);
                IPAddress ipAddress = configParser.GetIPAdress(location);
                int portNumber = configParser.GetPort(location);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
                agency.Dispatch(agentDispatched, ipEndPoint);
                //agency.OnRefuseConnection += new Agency.dgEventRaiser1(agent_OnRefuse);
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
            try
            {
                string selected = listAgents.GetItemText(listAgents.SelectedItem);

                AgentRemote agentDispatched = (AgentRemote)agency.GetMobileAgentProxy(selected);
                agentDispatched.agenciesVisited.Add(agency.GetName());
                foreach (string s in agency.GetNeighbours())
                {
                    if (!agentDispatched.agenciesVisited.Contains(s))
                    {
                        agentDispatched.queue.Enqueue(s);
                        agentDispatched.agenciesVisited.Add(s);
                    }
                }
                string next = agentDispatched.queue.Peek();
                IPAddress ipAddress = configParser.GetIPAdress(next);
                int portNumber = configParser.GetPort(next);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
                agency.Dispatch(agentDispatched, ipEndPoint);
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
                MessageBox.Show("Exception ! Mesaj: " + ex.Message + " -> Trimite agent!");
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
            agency.ShutDown();
            Close();
        }

        #endregion Controlers        

     }
}
