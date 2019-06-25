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
    public class AgentSelfRemote : Agent, IMobile
    {
        #region Private Fields
        Queue<string> wayBack = new Queue<string>();
        Dictionary<string, String> _info = null;
        List<string> agenciesVisited = new List<string>();
        Queue<Tuple<string, Queue<string>>> queue = new Queue<Tuple<string, Queue<string>>>();
        #endregion Private Fields

        #region Constructors
        public AgentSelfRemote() : base()
        {
            Parameters = new List<string>();
            SetType(Agent.WALKER);
            SetName("AgentSelfRemote");
            SetAgentInfo("Collect information from network");
            _info = new Dictionary<string, String>();
        }
        #endregion Constructors

        #region Properties
        public List<string> Parameters { get; set; }
        #endregion Properties

        #region Private Methods
        private string GetInfo(string parameter)
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
            while (!Dispatch(destination))
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

                            args.Source = "Punct de plecare: ";
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
            //Thread.CurrentThread.Abort();
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
                        _info.Clear();
                        SetAgentStateInfo("");
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
                        if (!GetAgentStateInfo().Equals(""))
                        {
                            args.Source = "Punct de stop: ";
                            args.Information = "Agentul  " + GetName() + " a adunat informatiile: " + Environment.NewLine + GetAgentStateInfo();
                            agencyContext.OnArrival(args);
                            Console.Beep(800, 1000);
                        }
                        else
                        {
                            args.Source = "Punct de stop: ";
                            args.Information = "Agentul " + GetName() + " nu a adunat nicio informatie !";
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
                        args.Information = "Agentul  " + GetName() + " ruleaza..." + Environment.NewLine + agencyContext.GetName() + ": " + ColectInformation(agencyContext);
                        agencyContext.OnArrival(args);
                        Console.Beep();

                        AddNeighbours(agencyContext, t.Item2);
                        if (queue.Count != 0)
                        {
                            string next = queue.Peek().Item1;
                            if (agencyContext.GetNeighbours().Contains(next))
                            {
                                IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(next);
                                int portNumber = AgencyForm.configParser.GetPort(next);
                                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);

                                args.Source = "Punct de plecare: ";
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
                                args.Information = "Agentul " + GetName() + " se intoarce la " + next;
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
        private string ColectInformation(AgencyContext agencyContext)
        {
            string information = "";
            foreach (string par in Parameters)
            {
                IStationary agentStatic = agencyContext.GetStationaryAgent(GetInfo(par));
                String i = agentStatic.GetInfo();
                if (!_info.ContainsKey(agencyContext.GetName()))
                {
                    _info.Add(agencyContext.GetName(), i);
                }
                else
                {
                    _info[agencyContext.GetName()] += i;
                }
                information += i;

            }
            SetAgentStateInfo(GetAgentStateInfo() + agencyContext.GetName() + ": " + _info[agencyContext.GetName()] + Environment.NewLine);
            return information;
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
        public override void Run()
        {
            ResetLifetime();
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
        public override String GetInfo()
        {
            throw new NotImplementedException();
        }
        #endregion Public Override Methods

    }

}



