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
using MobileAgent.EventAgent;
using Timer = System.Timers.Timer;
using System.Timers;

namespace AgentApp
{
    public partial class AgencyForm : Form
    {
        #region Private Fields
        private Random _random;
        #endregion Private Fields

        #region Private Static Fields
        static List<AgentProxy> aps = new List<AgentProxy>();
        static List<AgentProxy> aaps = new List<AgentProxy>();
        #endregion Private Static Fields

        #region Public Static Fields
        public static Agency agency = null;
        public static ConfigParser configParser = null;
        public static GeneralSettings gs = null;
        public static Timer aTimer = new Timer(2000);
        #endregion Public Static Fields

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
            //Dictionary<IPAddress, Tuple<string, int, string[]>> _hosts = configParser.NetworkHosts;
            //try
            //{
            //    IPAddress ipAddress = IPAddress.Parse(GetLocalIPAddress());
            //    Tuple<string, int, string[]> t = _hosts[ipAddress];
            //    int port = t.Item2;
            //    string name = t.Item1;
            //    string[] neighbours = t.Item3;

            //    agency = new Agency(ipAddress, port);
            //    agency.SetName(name);
            //    agency.SetNeighbours(new List<string>(neighbours));
            //    info.Text = "Agentia: " + name + " se afla la " + ipAddress + ", portul " + port;
            //    agency.Activate();
            //    agency.Start();

            //    agency.MobilityEventArr += Agent_OnArrival;
            //    agency.MobilityEventDis += Agent_OnDispaching;
            //    agency.RefuseConnectionEvent += Agency_RefuseConnection;
            //    agency.UpdateAgency += TimerIsUp;
            //    aTimer.Elapsed += new ElapsedEventHandler((source, e) => UpdateAgentsList());
            //    aTimer.Enabled = true;
            //}
            //catch (FormatException e)
            //{
            //    MessageBox.Show("Adresa IP invalida!");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Exception !" + ex.Message + " --> Start Agentie!");
            //}
            // simulare
            Dictionary<IPAddress, Tuple<string, int, string[]>> _hosts = configParser.NetworkHosts;
            try
            {
                int hostIndex = int.Parse(Console.ReadLine());
                //int hostIndex = _random.Next(_hosts.Count);
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

                agency.MobilityEventArr += Agent_OnArrival;
                agency.MobilityEventDis += Agent_OnDispaching;
                agency.RefuseConnectionEvent += Agency_RefuseConnection;
                agency.UpdateAgency += TimerIsUp;
                aTimer.Elapsed += new ElapsedEventHandler((source, e) => UpdateAgentsList());
                aTimer.Enabled = true;
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
        private void TimerIsUp()
        {

            var parts = new List<string>();
            Action<int, string> add = (val, unit) => { if (val > 0) parts.Add(val + unit); };
            var t = TimeSpan.FromMilliseconds(agency.GetRezervationTime());

            add(t.Days, "d");
            add(t.Hours, "h");
            add(t.Minutes, "m");
            add(t.Seconds, "s");
            add(t.Milliseconds, "ms");

            string text= "Timpul de rezervare pentru " + agency.GetName() + ":" + string.Join(" ", parts);
            labelShowClock.Text = text;
        }
        
        private void buttonShow_Click(object source, EventArgs e, Form ui)
        {
            ui.Close();
        }
        private void CreateStationaryAgent()
        {
            List<IStationary> list = gs.ListofAgentsS;
            foreach(IStationary ap in list)
            {
                int agentID = _random.Next(1000, 9999);
                ap.SetAgentId(agentID);
                ap.SetMobility(Agent.STATIC);
                agency.CreateAgent(ap);

            }
        }
        private void UpdateAgentsList()
        {
            Invoke((MethodInvoker)delegate
            {
                List<AgentProxy> aList = agency.GetAgentProxies(Agent.MOBILE);
                List<AgentProxy> activeAP = agency.GetActiveAgentProxies();
                if (aList.Count != aps.Count || activeAP.Count != aaps.Count)
                {
                    aps = aList;
                    aaps = activeAP;
                    agentsList.Clear();
                    listAgents.Items.Clear();
                    listAgents.Text = "";
                    if (aList != null)
                    {
                        foreach (AgentProxy agentProxy in aList)
                        {
                            if (agentProxy.IsMobile())
                            {
                                if (agentProxy.IsActive())
                                {
                                    listAgents.Items.Add(agentProxy.GetAgentId() + ": " + agentProxy.GetName());
                                }
                                agentsList.Text += agentProxy.GetAgentId() + ": " + agentProxy.GetName() + "-> " + agentProxy.GetAgentInfo() + System.Environment.NewLine;
                            }
                        }
                    }
                }
                if(!agency.IsBooked())
                {
                    labelShowClock.Text = "";
                }
            });
        }
        private void FillIPAddressAndPortsList()
        {
            Dictionary<IPAddress, Tuple<string, int, string[]>> _hosts = configParser.NetworkHosts;
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
        private string GetLocalIPAddress()
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

        #region Private Methods for Handling Events
        private void Agency_RefuseConnection(object sender, UnconnectedAgencyArgs e)
        {
            e.Date = DateTime.Now;
            this.Invoke((MethodInvoker)delegate
            {
                if (e.Name!=null)
                {
                    console.AppendText(e.Date + Environment.NewLine);
                    string name = configParser.GetName(e.Name);
                    console.AppendText("Nu se poate realiza conexiunea cu " + name + Environment.NewLine);
                    console.AppendText(".................................." + Environment.NewLine);
                }
            });
        }
        private void Agent_OnArrival(object sender, MobilityEventArgs e)
        {
            e.Date = DateTime.Now;
            this.Invoke((MethodInvoker)delegate
            {
                if (e.Source != null && e.Information != null)
                {
                    console.AppendText(e.Date + Environment.NewLine);
                    console.AppendText(e.Source + Environment.NewLine);
                    console.AppendText(e.Information + Environment.NewLine);
                    console.AppendText(".................................." + Environment.NewLine);
                }

            });
        }
        private void Agent_OnDispaching(object sender, MobilityEventArgs e)
        {
            e.Date = DateTime.Now;
            this.Invoke((MethodInvoker)delegate
            {
                if (e.Source != null && e.Information != null)
                {
                    console.AppendText(e.Date + Environment.NewLine);
                    console.AppendText(e.Source + Environment.NewLine);
                    console.AppendText(e.Information + Environment.NewLine);
                    console.AppendText(".................................." + Environment.NewLine);

                }
            });
        }
        #endregion Private Methods for Handling Events
        
        #region Controlers
        private void createButton_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = listAgents.GetItemText(comboBoxAgents.SelectedItem);
                int agentID = _random.Next(1000, 9999);

                AgentProxy agentToCreate = gs.GetAgentProxy(selected);
                agentToCreate.SetMobility(Agent.MOBILE);
                agentToCreate.SetAgentId(agentID);

                agency.CreateAgent(agentToCreate);
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

                agency.Clone((IMobile)agentToCreate);
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
                IMobile agentDispatched = agency.GetMobileAgentProxy(selected);
                if (agentDispatched.GetAgentType() != Agent.WALKER)
                {
                  
                    string location = comboBoxN.GetItemText(comboBoxN.SelectedItem);
                    IPAddress ipAddress = configParser.GetIPAdress(location);
                    int portNumber = configParser.GetPort(location);
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
                    agency.Dispatch(agentDispatched,ipEndPoint);
                    
                    if(agentDispatched.GetAgentType() != Agent.ONEWAY)
                    {
                        agency.RunAgent(agentDispatched);
                    }

                }
                else
                {
                    MessageBox.Show("Agentul nu poate fi trimis in retea: nu respecta tipul. Alegeti alt agent.");
                }
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
                AgentProxy agentDispatched = agency.GetMobileAgentProxy(selected);
                if (!agentDispatched.IsBoomerang())
                {
                    agency.RunAgent((IMobile)agentDispatched);
                }
                else
                {
                    MessageBox.Show("Agentul nu poate fi trimis in retea: nu respecta tipul. Alegeti alt agent.");
                }
            }
            catch(NullReferenceException  nre)
            {
                MessageBox.Show("NullReferenceException caught! Mesaj : " + nre.Message + " " + nre.StackTrace + " --> Agency Dispose Agent.");
            }
            catch(AgencyNotFoundException anfe)
            {
                MessageBox.Show("AgentNotFoundException ! Mesaj: " + anfe.Message + " -> Agency Dispose Agent!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception ! Mesaj: " + ex.Message + " -> Agency Dispose Agent!");
            }

        }
        private void disposeButton_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = listAgents.GetItemText(listAgents.SelectedItem);
                AgentProxy agentDisposed = agency.GetMobileAgentProxy(selected);
                agency.Deactivate(agentDisposed);
                UpdateAgentsList();
            }
            catch (NullReferenceException nre)
            {
                MessageBox.Show("NullReferenceException caught! Mesaj : " + nre.Message + " " + nre.StackTrace + " --> Agency Dispose Agent.");
            }
            catch (AgencyNotFoundException anfe)
            {
                MessageBox.Show("AgentNotFoundException ! Mesaj: " + anfe.Message + " -> Agency Dispose Agent!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception ! Mesaj: " + ex.Message + " -> Agency Dispose Agent!");
            }
        }
        private void deactivateButton_Click(object sender, EventArgs e)
        {
            agency.ShutDown();
            Close();
        }
        private void showButton_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = listAgents.GetItemText(listAgents.SelectedItem);
                AgentProxy agentInfo = agency.GetMobileAgentProxy(selected);
                string info = "";
                info += "Nume: " + agentInfo.GetName() + Environment.NewLine;
                info += "Informatii: " + agentInfo.GetAgentInfo() + Environment.NewLine;
                info += "Creat: " + agentInfo.GetCreationTime() + Environment.NewLine;

                MessageBox.Show(info);
            }
            catch (NullReferenceException nre)
            {
                MessageBox.Show("NullReferenceException caught! Mesaj : " + nre.Message + " " + nre.StackTrace + " --> Agency Dispose Agent.");
            }
            catch (AgencyNotFoundException anfe)
            {
                MessageBox.Show("AgentNotFoundException ! Mesaj: " + anfe.Message + " -> Agency Dispose Agent!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception ! Mesaj: " + ex.Message + " -> Agency Dispose Agent!");
            }
        }
        private void buttonDeactivateA_Click(object sender, EventArgs e)
        {
            Form ui = new Form();
            Label label1 = new Label();
            Button buttonAdd = new Button();
            ComboBox comboBox1 = new ComboBox();
            //
            //label
            //
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(37, 36);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(122, 17);
            label1.TabIndex = 5;
            label1.Text = "Agentii dezactivati";
            // 
            // button1
            // 
            buttonAdd.Location = new System.Drawing.Point(296, 67);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new System.Drawing.Size(98, 30);
            buttonAdd.TabIndex = 4;
            buttonAdd.Text = "Activeaza";
            buttonAdd.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(40, 71);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(238, 24);
            comboBox1.TabIndex = 6;
            // 
            // Form1
            // 
            ui.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            ui.AutoScaleMode = AutoScaleMode.Font;
            ui.ClientSize = new System.Drawing.Size(425, 119);
            ui.Controls.Add(comboBox1);
            ui.Controls.Add(label1);
            ui.Controls.Add(buttonAdd);
            ui.Name = "Form1";
            ui.Text = "Activare agenti";
            ui.ResumeLayout(false);
            ui.PerformLayout();

            foreach (AgentProxy ap in agency.GetAgentProxies(Agent.MOBILE))
            {
                if (!ap.IsActive())
                {
                    comboBox1.Items.Add(ap.GetAgentId() + ": " + ap.GetName());
                }
            }
            foreach (AgentProxy ap in agency.GetAgentProxies(Agent.STATIC))
            {
                if (!ap.IsActive())
                {
                    comboBox1.Items.Add(ap.GetAgentId() + ": " + ap.GetName());
                }
            }
            buttonAdd.Click += new EventHandler((senderUI, eUI) => buttonAdd_Click(sender, e, ui));

            var thread = new Thread(() =>
            {
                Application.Run(ui);
            });
            thread.Start();
        }
        private void buttonAdd_Click(object sender, EventArgs e, Form ui)
        {
            try
            {
                foreach (Control c in ui.Controls)
                {
                    if (c is ComboBox)
                    {
                        ComboBox comboBox = (ComboBox)c;
                        string selected = comboBox.GetItemText(comboBox.SelectedItem);
                        AgentProxy agent = agency.GetMobileAgentProxy(selected);
                        if (agent == null)
                        {
                            agent = agency.GetStatAgentProxy(selected);
                        }
                        agency.ActivateAgent(agent);
                    }
                }
                ui.Close();
            }
            catch (NullReferenceException nre)
            {
                MessageBox.Show("NullReferenceException caught! Mesaj : " + nre.Message + " " + nre.StackTrace + " --> Agency Dispose Agent.");
            }
            catch (AgencyNotFoundException anfe)
            {
                MessageBox.Show("AgentNotFoundException ! Mesaj: " + anfe.Message + " -> Agency Dispose Agent!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception ! Mesaj: " + ex.Message + " -> Agency Dispose Agent!");
            }
        }
        #endregion Controlers        

        private void ButtonShowClock_Click(object sender, EventArgs e)
        {
            Label label1 = new Label();
            Button button1 = new Button();
            Form ui = new Form();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(37, 33);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(351, 17);
            label1.TabIndex = 5;
            label1.Text = "";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(151, 77);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(98, 30);
            button1.TabIndex = 4;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            ui.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            ui.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ui.ClientSize = new System.Drawing.Size(404, 119);
            ui.Controls.Add(label1);
            ui.Controls.Add(button1);
            ui.Name = "Rezrvare Agentie - timp ramas";
            ui.Text = "Activare agenti";
            ui.ResumeLayout(false);
            ui.PerformLayout();

            button1.Click += new System.EventHandler((source, eee) => this.buttonShow_Click(source, eee, ui));

            var thread = new Thread(() =>
            {
                Application.Run(ui);
            });
            thread.Start();
        }

       
    }
}
