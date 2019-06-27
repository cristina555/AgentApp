using MobileAgent.AgentManager;
using MobileAgent.EventAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgentApp.Agents.MobileAgents
{
    [Serializable]
    public class AgentClone : Agent, IMobile
    {
        List<string> agenciesVisited = new List<string>();
        public AgentClone():base()
        {
            SetType(WALKER);
            SetName("AgentClone");
            SetAgentInfo("Colecteaza informatii din retea.");
        }
        public List<string> Parameters { get; set; } = new List<string>();
        public int NumberOfSlaves { get; set; } = 0;
        private static readonly Object obj = new Object();
        public void RunNetwork(AgencyContext agencyContext)
        {
            MobilityEventArgs args = new MobilityEventArgs();
            if (agencyContext.GetAgencyIPEndPoint().Equals(GetAgencyCreationContext()))
            {
                if (IsMaster())
                {
                    //agenciesVisited.Clear();
                    SetAgentStateInfo("");
                    //agenciesVisited.Add(agencyContext.GetName());
                    foreach (String n  in agencyContext.GetNeighbours())
                    {
                        //if(!agenciesVisited.Contains(n))
                        //{
                           // agenciesVisited.Add(n);
                            IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(n);
                            int portNumber = AgencyForm.configParser.GetPort(n);
                            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
                            if(agencyContext.GetConnection(ipEndPoint))
                            {
                                IMobile agentCloned = Clone();
                                agentCloned.SetParent(this);
                                agentCloned.SetWorkType(SLAVE);
                                NumberOfSlaves++;
                                agencyContext.Dispatch(agentCloned, ipEndPoint);
                            }
                        //}
                    }
                    while (GetCloneList().Count() <= NumberOfSlaves) ;

                }
                else
                {
                    IMobile parent = agencyContext.GetMobileAgentProxy(GetParent().GetName());
                    lock (obj)
                    {
                        parent.SetAgentStateInfo(GetAgentStateInfo() + Environment.NewLine);
                        parent.SetClone(this);
                    }
                }
            }
            else
            {
               
                args.Source = "Punct de rulare: ";
                args.Information = "Agentul  " + GetName() + " ruleaza..." + Environment.NewLine + agencyContext.GetName() + ": " + ColectInformation(agencyContext);
                agencyContext.OnArrival(args);
                Console.Beep();
                
                agencyContext.Dispatch(this, GetAgencyCreationContext());
            }
            
        }
        private string ColectInformation(AgencyContext agencyContext)
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
                case "Arhitectura sistem de operare":
                    {
                        type = "AgentOSA";
                        break;
                    }
                case "Service Pack sistem de operare":
                    {
                        type = "AgentOSSP";
                        break;
                    }
                case "Informatii procesor":
                    {
                        type = "AgentP";
                        break;
                    }
                case "Informatii placa video":
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
        public override void Run()
        {
            AgencyContext agencyContext = GetAgentCurrentContext();
            RunNetwork(agencyContext);
        }
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
            "Arhitectura sistem de operare",
            "Service Pack sistem de operare",
            "Informatii procesor",
            "Informatii placa video"});
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
            ui.Text = "Interfata AgentRemote";
            ui.ResumeLayout(false);
            ui.PerformLayout();

            button1.Click += new System.EventHandler((sender, e) => buttonSend_Click(sender, e, ui));
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
    }
}
