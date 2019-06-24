using MobileAgent.AgentManager;
using MobileAgent.EventAgent;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Windows.Forms;


namespace AgentApp.Agents
{
    [Serializable]
    public class AgentR : Agent, IMobile
    {
        #region Private Fields
        Queue<string> wayBack = new Queue<string>();
        List<string> agenciesVisited = new List<string>();
        Queue<Tuple<string, Queue<string>>> queue = new Queue<Tuple<string, Queue<string>>>();
        #endregion Private Fields

        #region Constructor
        public AgentR() : base()
        {
            Parameters = new List<Tuple<string, string>>();
            SetType(Agent.WALKER);
            SetName("AgentR");
            SetAgentInfo("Collect information from network");
        }
        #endregion Constructor

        #region Properties
        public List<Tuple<string, string>> Parameters { get; set; }
        public int Threshold { get; set; }
        public int NumberOfValidAgencies { get; set; } = 0;
        #endregion Properties

        #region Private Methods

        private void AddNeighbours(AgencyContext agencyContext, Queue<string> agencyQueue)
        {
            foreach (string n in agencyContext.GetNeighbours())
            {
                if (!agenciesVisited.Contains(n))
                {
                    Queue<string> q = new Queue<string>();
                    q.Enqueue(agencyContext.GetName());
                    foreach (Object obj in agencyQueue)
                    {
                        q.Enqueue(obj.ToString());
                    }
                    queue.Enqueue(Tuple.Create(n, q));
                    agenciesVisited.Add(n);
                }
            }
        }
        private void TryDispatch(AgencyContext agencyContext, IPEndPoint destination)
        {
            MobilityEventArgs args = new MobilityEventArgs();
            while (!agencyContext.Dispatch(this, destination))
            {
                if (queue.Count != 0)
                {
                    Tuple<string, Queue<string>> t = queue.Dequeue();
                    if (queue.Count != 0)
                    {
                        string next = queue.Peek().Item1;
                        if (agencyContext.GetNeighbours().Contains(next))
                        {
                            IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(next);
                            int portNumber = AgencyForm.configParser.GetPort(next);
                            destination = new IPEndPoint(ipAddress, portNumber);

                            args.Source = "Punct de plecare:  ";
                            args.Information = "Agentul " + GetName() + " se duce catre " + next;
                            agencyContext.OnDispatching(args);

                        }
                        else
                        {
                            CreateWayBack(t.Item2);
                            wayBack.Dequeue();
                            string back = wayBack.Peek();
                            IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(back);
                            int portNumber = AgencyForm.configParser.GetPort(back);
                            destination = new IPEndPoint(ipAddress, portNumber);

                            args.Source = "Punct de plecare: ";
                            args.Information = "Agentul " + GetName() + " se duce catre " + back;
                            agencyContext.OnDispatching(args);
                        }
                    }
                    else
                    {
                        t.Item2.Dequeue();
                        destination = RetractAgent(agencyContext, t.Item2);
                        if (destination == null)
                        {
                            args.Source = "Punct de stop: ";
                            args.Information = "Agentul " + GetName() + " nu a adunat nicio informatie !";
                            agencyContext.OnArrival(args);
                            Console.Beep(800, 1000);

                            break;
                        }
                        args.Source = "Punct de plecare: ";
                        args.Information = "Agentul " + GetName() + " se duce catre sursa";
                        agencyContext.OnDispatching(args);
                    }

                }
                else
                {
                    break;
                }

            }
        }
        private void CreateWayBack(Queue<string> b)
        {
            wayBack = b;
            List<string> q = new List<string>(queue.Peek().Item2.ToArray());
            q.Reverse();
            q.RemoveAt(0);
            foreach (string el in q)
            {
                wayBack.Enqueue(el);
            }
        }
        private IPEndPoint RetractAgent(AgencyContext agencyContext, Queue<string> t)
        {
            IPEndPoint destination = null;
            SetWorkStatus(Agent.DONE);
            wayBack = t;
            if (wayBack.Count != 0)
            {
                string back = wayBack.Dequeue();
                IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(back);
                int portNumber = AgencyForm.configParser.GetPort(back);
                destination = new IPEndPoint(ipAddress, portNumber);
            }
            else
            {
                if (!agencyContext.GetAgencyIPEndPoint().Equals(GetAgencyCreationContext()))
                {
                    destination = GetAgencyCreationContext();
                }

            }
            return destination;
        }
        private void RunNetwork(AgencyContext agencyContext)
        {
            MobilityEventArgs args = new MobilityEventArgs();
            if (wayBack.Count != 0)
            {
                if (queue.Count != 0)
                {
                    args.Source = "Punct de sosire: ";
                    args.Information = "Agentul " + GetName() + " se intoarce pentru a se duce la " + queue.Peek().Item1;
                    agencyContext.OnArrival(args);
                }
                else
                {
                    args.Source = "Punct de sosire: ";
                    args.Information = "Agentul " + GetName() + " se intoarce la sursa";
                    agencyContext.OnArrival(args);
                }
                wayBack.Dequeue();
                if (wayBack.Count != 0)
                {
                    string back = wayBack.Peek();
                    IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(back);
                    int portNumber = AgencyForm.configParser.GetPort(back);
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);

                    args.Source = "Punct de plecare: ";
                    args.Information = "Agentul " + GetName() + " se duce catre " + back;
                    agencyContext.OnDispatching(args);

                    TryDispatch(agencyContext, ipEndPoint);
                }
                else
                {
                    if (queue.Count != 0)
                    {
                        string cont = queue.Peek().Item1;
                        IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(cont);
                        int portNumber = AgencyForm.configParser.GetPort(cont);
                        IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);

                        args.Source = "Punct de plecare: ";
                        args.Information = "Agentul " + GetName() + " se duce catre " + cont;
                        agencyContext.OnDispatching(args);

                        TryDispatch(agencyContext, ipEndPoint);
                    }
                    else
                    {
                        args.Source = "Punct de plecare: ";
                        args.Information = "Agentul " + GetName() + " se duce catre sursa";
                        agencyContext.OnDispatching(args);

                        TryDispatch(agencyContext, GetAgencyCreationContext());
                    }
                }
            }
            else
            {

                if (agencyContext.GetAgencyIPEndPoint().Equals(GetAgencyCreationContext()))
                {
                    if (IsReady())
                    {
                        agenciesVisited.Clear();
                        SetAgentStateInfo("");
                        NumberOfValidAgencies = 0;
                        agenciesVisited.Add(agencyContext.GetName());
                        AddNeighbours(agencyContext, new Queue<string>());
                        if (queue.Count != 0)
                        {
                            string next = queue.Peek().Item1;
                            IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(next);
                            int portNumber = AgencyForm.configParser.GetPort(next);
                            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);

                            args.Source = "Punct de plecare: ";
                            args.Information = "Agentul " + GetName() + " se duce catre " + next;
                            agencyContext.OnDispatching(args);

                            TryDispatch(agencyContext, ipEndPoint);
                        }

                    }
                    else
                    {
                        if (NumberOfValidAgencies != 0)
                        {
                            args.Source = "Punct de stop: ";
                            args.Information = "Agentul  " + GetName() + " a gasit " + NumberOfValidAgencies + " agentii valide!";
                            agencyContext.OnArrival(args);
                            Console.Beep(800, 1000);
                        }
                        else
                        {
                            args.Source = "Punct de stop: ";
                            args.Information = "Agentul " + GetName() + " nu a vizitat nicio agentie !";
                            agencyContext.OnArrival(args);
                            Console.Beep(800, 1000);
                        }
                    }
                }
                else
                {
                    if (queue.Count != 0)
                    {
                        Tuple<string, Queue<string>> t = queue.Dequeue();

                        args.Source = "Punct de rulare: ";
                        args.Information = "Agentul  " + GetName() + " ruleaza..." + Environment.NewLine + agencyContext.GetName() + Environment.NewLine + ShowInformation(agencyContext);
                        agencyContext.OnArrival(args);
                        Console.Beep();

                        if(IsValidAgency(agencyContext))
                        {
                            NumberOfValidAgencies++;
                            agencyContext.SetBookedTime(10000);
                        }

                        AddNeighbours(agencyContext, t.Item2);
                        if (queue.Count != 0)
                        {
                            string next = queue.Peek().Item1;
                            if (agencyContext.GetNeighbours().Contains(next))
                            {
                                IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(next);
                                int portNumber = AgencyForm.configParser.GetPort(next);
                                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);

                                args.Source = "Agentul se deplaseaza ";
                                args.Information = "Agentul " + GetName() + " se duce catre " + next;
                                agencyContext.OnDispatching(args);

                                TryDispatch(agencyContext, ipEndPoint);
                            }
                            else
                            {
                                CreateWayBack(t.Item2);
                                string back = wayBack.Peek();
                                IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(back);
                                int portNumber = AgencyForm.configParser.GetPort(back);
                                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);

                                args.Source = "Punct de plecare: ";
                                args.Information = "Agentul " + GetName() + " se duce catre " + next;
                                agencyContext.OnDispatching(args);

                                TryDispatch(agencyContext, ipEndPoint);
                            }
                        }
                        else
                        {
                            SetWorkStatus(Agent.DONE);

                            args.Source = "Punct de plecare: ";
                            args.Information = "Agentul " + GetName() + " se intoarce la sursa .";
                            agencyContext.OnDispatching(args);

                            TryDispatch(agencyContext, GetAgencyCreationContext());
                        }
                    }
                }
            }
        }
        private bool IsValidAgency(AgencyContext agencyContext)
        {
            foreach (Tuple<string, string> par in Parameters)
            {
                IStationary agentStatic = agencyContext.GetStationaryAgent(par.Item1);
                String i = agentStatic.GetInfo();
                if(!i.Contains(par.Item2))
                {
                    return false;
                }
            }
            return true;
        }
        private string ShowInformation(AgencyContext agencyContext)
        {
            string information = "";
            foreach (Tuple<string, string> par in Parameters)
            {
                IStationary agentStatic = agencyContext.GetStationaryAgent(par.Item1);
                String i = agentStatic.GetInfo();
                information += agentStatic.GetAgentInfo()  + Environment.NewLine + i ;
            }
            return information;
        }
        private void buttonSend_Click(object sender, EventArgs e, Form ui)
        {
            try
            {
                List<Tuple<string, string>> parameters = new List<Tuple<string, string>>();
                foreach (Control c in ui.Controls)
                {
                    if (c is ComboBox)
                    {
                        ComboBox control = (ComboBox)c;
                        switch (control.Name)
                        {
                            case "comboBoxOS":
                                {
                                    parameters.Add(Tuple.Create("AgentOS", control.GetItemText(control.SelectedItem)));
                                    break;
                                }
                            case "comboBoxP":
                                {
                                    parameters.Add(Tuple.Create("AgentP", control.GetItemText(control.SelectedItem)));
                                    break;
                                }
                            case "comboBoxVC":
                                {
                                    parameters.Add(Tuple.Create("AgentVC", control.GetItemText(control.SelectedItem)));
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    }
                }
                foreach (Control c in ui.Controls)
                {
                    if (c is TextBox)
                    {
                        TextBox t = (TextBox)c;
                        Threshold = int.Parse(t.Text);
                        break;
                    }
                }
                Parameters = parameters;

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
        public override void Run()
        {
            ResetLifetime();
            AgencyContext agencyContext = GetAgentCurrentContext();
            RunNetwork(agencyContext);
        }
        public override void GetUI()
        {
            Form ui = new Form();

            Button button1;
            ComboBox comboBox1;
            Label label1;
            Label label2;
            Label label3;
            Label label4;
            Label label5;
            ComboBox comboBox2;
            ComboBox comboBox3;
            TextBox textBox1;

            button1 = new Button();
            comboBox1 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            comboBox2 = new ComboBox();
            comboBox3 = new ComboBox();
            textBox1 = new TextBox();
            ui.SuspendLayout();

            // button1
            // 
            button1.Location = new System.Drawing.Point(285, 198);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(107, 38);
            button1.TabIndex = 4;
            button1.Text = "Trmite";
            button1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] {
            "Windows 7",
            "Windows 8",
            "Windows 10",
            "Windows XP"});
            comboBox1.Location = new System.Drawing.Point(28, 75);
            comboBox1.Name = "comboBoxOS";
            comboBox1.Size = new System.Drawing.Size(219, 24);
            comboBox1.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(25, 55);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(124, 17);
            label1.TabIndex = 6;
            label1.Text = "Sistem de operare";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(24, 9);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(256, 20);
            label2.TabIndex = 7;
            label2.Text = "Selecteaza informatiile dorite";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(28, 121);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(65, 17);
            label3.TabIndex = 8;
            label3.Text = "Procesor";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(28, 192);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(81, 17);
            label4.TabIndex = 9;
            label4.Text = "Placa video";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] {
            "AMD",
            "Intel"});
            comboBox2.Location = new System.Drawing.Point(28, 141);
            comboBox2.Name = "comboBoxP";
            comboBox2.Size = new System.Drawing.Size(219, 24);
            comboBox2.TabIndex = 10;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] {
            "Intel",
            "NVidia"});
            comboBox3.Location = new System.Drawing.Point(31, 212);
            comboBox3.Name = "comboBoxVC";
            comboBox3.Size = new System.Drawing.Size(216, 24);
            comboBox3.TabIndex = 11;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(285, 135);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(61, 30);
            textBox1.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.Location = new System.Drawing.Point(285, 98);
            label5.Name = "label5";
            label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            label5.Size = new System.Drawing.Size(148, 18);
            label5.TabIndex = 13;
            label5.Text = "Numarul de agentii";
            label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AgentRemoteUI
            // 
            ui.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            ui.AutoScaleMode = AutoScaleMode.Font;
            ui.ClientSize = new System.Drawing.Size(465, 259);
            ui.Controls.Add(textBox1);
            ui.Controls.Add(comboBox3);
            ui.Controls.Add(comboBox2);
            ui.Controls.Add(label4);
            ui.Controls.Add(label4);
            ui.Controls.Add(label3);
            ui.Controls.Add(label2);
            ui.Controls.Add(label1);
            ui.Controls.Add(comboBox1);
            ui.Controls.Add(button1);
            ui.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            ui.Name = "AgentRemoteUI";
            ui.Text = "Interfata AgentRemote";
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
        public override String GetInfo()
        {
            throw new NotImplementedException();
        }
        #endregion Public Override Methods

    }

}



