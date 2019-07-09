using MobileAgent.AgentManager;
using MobileAgent.EventAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AgentApp.Agents.MobileAgents
{
    [Serializable]
    public class AgentClone : Agent, IMobile
    {
        #region Private Fields
        private static readonly Object obj = new Object();
        #endregion Private Fields

        #region Constructor
        /// <summary>
        /// Constructorul care setează datele specifice ale agentului
        /// </summary>
        public AgentClone():base()
        {
            SetType(WALKER);
            SetName("AgentClone");
            SetAgentInfo("Colecteaza informatii din retea.");
        }
        #endregion Constructor

        /// <summary>
        /// Datele de stare ale agentului
        /// </summary>
        #region Properties
        public List<string> Parameters { get; set; } = new List<string>();
        public int NumberOfSlaves { get; set; } = 0;
        #endregion Properties

        #region Private Methods
        /// <summary>
        /// Metoda care lanseaza clone in retea si aduna informatiile
        /// </summary>
        /// <param name="agencyContext">contextul agentiei</param>
        private void RunNetwork(IAgencyContext agencyContext)
        {
            MobilityEventArgs args = new MobilityEventArgs();
            CloneEventArgs argsc = new CloneEventArgs();
            if (agencyContext.GetAgencyIPEndPoint().Equals(GetAgencyCreationContext()))
            {
                if (IsMaster())
                {
                    SetAgentStateInfo("");
                    foreach (String n in agencyContext.GetNeighbours())
                    {
                        IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(n);
                        int portNumber = AgencyForm.configParser.GetPort(n);
                        IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
                        if (agencyContext.GetConnection(ipEndPoint))
                        {
                            IMobile agentCloned = this.Clone();
                            

                            agentCloned.SetParent(this);
                            agentCloned.SetWorkType(SLAVE);
                            NumberOfSlaves++;
                            agentCloned.SetAgentId(GetAgentId() + NumberOfSlaves);
                            agentCloned.SetName(GetName() + "_cloned_" + NumberOfSlaves);
                            agentCloned.GetParent().SetAgentCurrentContext(null);

                            argsc.Source = "Agentul " + GetName();
                            argsc.Information = "A creat clona : " + agentCloned.GetName();
                            agencyContext.OnCloning(argsc);

                            agencyContext.Dispatch(agentCloned, ipEndPoint);
                        }
                    }
                    while (GetCloneList().Count() < NumberOfSlaves) ;

                    if (!GetAgentStateInfo().Equals(""))
                    {
                        args.Source = "Punct de stop: ";
                        args.Information = "Agentul  " + GetName() + " a adunat informațiile: " + Environment.NewLine + GetAgentStateInfo();
                        agencyContext.OnArrival(args);
                        Console.Beep(800, 1000);
                    }
                    else
                    {
                        args.Source = "Punct de stop: ";
                        args.Information = "Agentul " + GetName() + " nu a adunat nicio informație !";
                        agencyContext.OnArrival(args);
                        Console.Beep(800, 1000);
                    }
                    GetCloneList().Clear();
                }
                else
                {
                    IMobile parent = agencyContext.GetMobileAgentProxy(GetParent().GetAgentId());
                    lock (obj)
                    {
                        parent.SetAgentStateInfo(parent.GetAgentStateInfo() + GetAgentStateInfo());
                        parent.SetClone(this);
                    }
                    agencyContext.RemoveAgent(this);

                }
            }
            else
            {
                if (!agencyContext.IsBooked())
                {
                    args.Source = "Punct de rulare: ";
                    args.Information = "Agentul  " + GetName() + " rulează..." + Environment.NewLine + agencyContext.GetName() + ": " + ColectInformation(agencyContext);
                    agencyContext.OnArrival(args);
                    Console.Beep();
                    agencyContext.Dispatch(this, GetAgencyCreationContext());
                }
                else
                {
                    args.Source = "Punct de rulare: ";
                    args.Information = "Agentul  " + GetName() + " nu poate rula! Agentie rezervată";
                    agencyContext.OnArrival(args);
                    Console.Beep();
                    agencyContext.Dispatch(this, GetAgencyCreationContext());

                }
            }
            
        }
        /// <summary>
        /// Metoda care colecteaza informatiile de la agentii stationari
        /// </summary>
        /// <param name="agencyContext">contextul agentiei</param>
        /// <returns></returns>
        private string ColectInformation(IAgencyContext agencyContext)
        {
            string information = "";
            foreach (string par in Parameters)
            {
                IStationary agentStatic = agencyContext.GetStationaryAgent(MapAgentName(par));
                String i = agentStatic.GetInfo();
                information += i;

            }
            SetAgentStateInfo(GetAgentStateInfo() + agencyContext.GetName() + ": " + information + Environment.NewLine);
            return information;
        }
        /// <summary>
        /// Metoda care face conversia intre parametrii de intrare si agentul stationar necesar
        /// </summary>
        /// <param name="parameter">resursa indicata</param>
        /// <returns>agentul stationar care detine resursa</returns>
        private string MapAgentName(string parameter)
        {
            string type = "";
            switch (parameter)
            {
                case "Sistem de operare":
                    {
                        type = "AgentOS";
                        break;
                    }
                case "Arhitectură sistem de operare":
                    {
                        type = "AgentOSA";
                        break;
                    }
                case "Service Pack sistem de operare":
                    {
                        type = "AgentOSSP";
                        break;
                    }
                case "Informații procesor":
                    {
                        type = "AgentP";
                        break;
                    }
                case "Informații placă video":
                    {
                        type = "AgentVC";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return type;
        }
        /// <summary>
        /// Controlerul care seteaza parametrii agentului din interfata grafica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="ui"></param>
        private void buttonSend_Click(object sender, EventArgs e, Form ui)
        {
            try
            {
                List<string> _parameters = new List<string>();
                foreach (Control c in ui.Controls)
                {
                    if (c is CheckedListBox)
                    {
                        CheckedListBox control = (CheckedListBox)c;
                        foreach (object itemchecked in control.CheckedItems)
                        {
                            _parameters.Add(itemchecked.ToString());
                        }
                        break;
                    }

                }
                Parameters = _parameters;
                ui.Close();
            }
            catch (NullReferenceException nre)
            {
                MessageBox.Show("NullReferenceException !" + nre.Message + " --> Trimite parametrii agentului!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception !" + ex.Message + " --> Trimite parametrii agentului!");
            }
        }
        #endregion Private Methods

        #region Public Override Methods
        /// <summary>
        /// Metoda Run() specifica agentului
        /// </summary>
        public override void Run()
        {
            IAgencyContext agencyContext = GetAgentCurrentContext();
            RunNetwork(agencyContext);
        }
        /// <summary>
        /// Metoda de afisare a interfetei grafice specifica agentului
        /// </summary>
        public override void GetUI()
        {
            Form ui = new Form();

            Label label1;
            Button button1;
            CheckedListBox checkedListBox1;

            label1 = new Label();
            button1 = new Button();
            checkedListBox1 = new CheckedListBox();
            ui.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(13, 25);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(65, 17);
            label1.TabIndex = 5;
            label1.Text = "Informatii";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(301, 89);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(107, 38);
            button1.TabIndex = 4;
            button1.Text = "Trimite";
            button1.UseVisualStyleBackColor = true;

            // 
            // checkedListBox1
            // 
            checkedListBox1.CheckOnClick = true;
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Items.AddRange(new object[] {
            "Sistem de operare",
            "Arhitectură sistem de operare",
            "Service Pack sistem de operare",
            "Informații procesor",
            "Informații placă video"});
            checkedListBox1.Location = new System.Drawing.Point(13, 58);
            checkedListBox1.Margin = new Padding(4);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new System.Drawing.Size(259, 106);
            checkedListBox1.TabIndex = 6;
            // 
            // AgentRemoteUI
            // 
            ui.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            ui.AutoScaleMode = AutoScaleMode.Font;
            ui.ClientSize = new System.Drawing.Size(451, 177);
            ui.Controls.Add(checkedListBox1);
            ui.Controls.Add(label1);
            ui.Controls.Add(button1);
            ui.Margin = new Padding(3, 2, 3, 2);
            ui.Name = "AgentRemoteUI";
            ui.Text = "Interfață AgentRemote";
            ui.ResumeLayout(false);
            ui.PerformLayout();

            button1.Click += new EventHandler((sender, e) => buttonSend_Click(sender, e, ui));
            if (ui.Controls.Count != 0)
            {
                var thread = new Thread(() =>
                {
                    Application.Run(ui);
                });
                thread.Start();
            }
        }
        public override string GetInfo()
        {
            throw new NotImplementedException();
        }
        #endregion Public Override Methods
    }
}
