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
    public class AgentRemote : Agent, IMobile
    {
        #region Private Fields
        /// <summary>
        /// Variabile de stare pentru agent
        /// </summary>
        Queue<string> wayBack = new Queue<string>();
        Dictionary<string, String> _info = null;
        List<string> agenciesVisited = new List<string>();
        Queue<Tuple<string, Queue<string>>> queue = new Queue<Tuple<string, Queue<string>>>();
        #endregion Private Fields

        #region Constructors
        /// <summary>
        /// Constructorul care setează datele specifice ale agentului
        /// </summary>
        public AgentRemote() : base()
        {
            Parameters = new List<string>();
            SetType(Agent.WALKER);
            SetName("AgentRemote");
            SetAgentInfo("Colectează informații din rețea.");
            _info = new Dictionary<string, String>();
        }
        #endregion Constructors

        #region Properties
        public List<string> Parameters { get; set; }
        #endregion Properties

        #region Private Methods
        /// <summary>
        /// Metoda care face conversia intre datele interfetei grafice si numele agentului stationar
        /// </summary>
        /// <param name="parameter">resursa ceruta</param>
        /// <returns>numele agentului stationar care detine resursa</returns>
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
        /// Metoda care adaugă vecinii nevizitati in coada
        /// </summary>
        /// <param name="agencyContext">contextul agentiei</param>
        /// <param name="agencyQueue">coada care contine nodurile agentiei</param>
        private void AddNeighbours(IAgencyContext agencyContext, Queue<string> agencyQueue)
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
        /// <summary>
        /// Metoda de expediere a agentului
        /// </summary>
        /// <param name="agencyContext">contextul agentiei</param>
        /// <param name="destination">punctul destinatie</param>
        private void TryDispatch(IAgencyContext agencyContext, IPEndPoint destination)
        {
            MobilityEventArgs args = new MobilityEventArgs();
            if (!agencyContext.GetConnection(destination))
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
                            args.Information = "Agentul " + GetName() + " se duce către " + next;
                            agencyContext.OnDispatching(args);

                        }
                        else
                        {
                            CreateWayBack(t.Item2); // ceva probleme, trebuie sa depistez in ce caz
                            wayBack.Dequeue();
                            string back = wayBack.Peek();
                            IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(back);
                            int portNumber = AgencyForm.configParser.GetPort(back);
                            destination = new IPEndPoint(ipAddress, portNumber);

                            args.Source = "Punct de plecare: ";
                            args.Information = "Agentul " + GetName() + " se duce către " + back;
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
                            args.Information = "Agentul " + GetName() + " nu a adunat nicio informație !";
                            agencyContext.OnArrival(args);
                            Console.Beep(800, 1000);

                            return;
                        }

                        args.Source = "Punct de plecare: ";
                        args.Information = "Agentul " + GetName() + " se duce către sursă";
                        agencyContext.OnDispatching(args);
                    }

                }
                else
                {
                    return;

                }


                TryDispatch(agencyContext, destination);
            }
            else
            {
                agencyContext.Dispatch(this, destination);
            }
        }
        /// <summary>
        /// Metoda care creaza drumul de intorcere in retea
        /// </summary>
        /// <param name="b">coada care contine nodurile drumului de intoarcere</param>
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
        /// <summary>
        /// Metoda care creeaza drumul de intorcere la sursa
        /// </summary>
        /// <param name="agencyContext">contextul agentiei</param>
        /// <param name="t">coada care contine drumul de intrcere la sursa</param>
        /// <returns>urmatorul punct destinatar</returns>
        private IPEndPoint RetractAgent(IAgencyContext agencyContext, Queue<string> t)
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
        /// <summary>
        /// Metoda care parcurge intreaga topologie de retea
        /// </summary>
        /// <param name="agencyContext">contextul agentiei</param>
        private void RunNetwork(IAgencyContext agencyContext)
        {
            MobilityEventArgs args = new MobilityEventArgs();
            if (wayBack.Count != 0)
            {
               
                wayBack.Dequeue();
                if (queue.Count != 0)
                {
                    args.Source = "Punct de sosire: ";
                    args.Information = "Agentul " + GetName() + " se întoarce pentru a se duce la " + queue.Peek().Item1; 
                    agencyContext.OnArrival(args);
                }
                else
                {
                    args.Source = "Punct de sosire: ";
                    args.Information = "Agentul " + GetName() + " se întoarce la sursa";
                    agencyContext.OnArrival(args);
                }
                if (wayBack.Count != 0)
                {
                    string back = wayBack.Peek();
                    IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(back);
                    int portNumber = AgencyForm.configParser.GetPort(back);
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);

                    args.Source = "Punct de plecare: ";
                    args.Information = "Agentul " + GetName() + " se duce către " + back;
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
                        args.Information = "Agentul " + GetName() + " se duce către " + cont;
                        agencyContext.OnDispatching(args);

                        TryDispatch(agencyContext, ipEndPoint);
                    }
                    else
                    {
                        args.Source = "Punct de plecare: ";
                        args.Information = "Agentul " + GetName() + " se duce către sursă";
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
                            args.Information = "Agentul " + GetName() + " se duce către " + next;
                            agencyContext.OnDispatching(args);

                            TryDispatch(agencyContext, ipEndPoint);

                        }

                    }
                    else
                    {
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

                    }
                }
                else
                {
                    if (queue.Count != 0)
                    {
                        Tuple<string, Queue<string>> t = queue.Dequeue();
                        if (!agencyContext.IsBooked())
                        {
                            args.Source = "Punct de rulare: ";
                            args.Information = "Agentul  " + GetName() + " rulează..." + Environment.NewLine + agencyContext.GetName() + ": " + ColectInformation(agencyContext);
                            agencyContext.OnArrival(args);
                            Console.Beep();
                        }
                        else
                        {
                            args.Source = "Punct de rulare: ";
                            args.Information = "Agentul  " + GetName() + " nu poate rula: Agenție rezervată";
                            agencyContext.OnArrival(args);
                            Console.Beep();
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

                                args.Source = "Punct de plecare: ";
                                args.Information = "Agentul " + GetName() + " se duce către " + next;
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
                                args.Information = "Agentul " + GetName() + " se întoarce la " + back;
                                agencyContext.OnDispatching(args);

                                TryDispatch(agencyContext, ipEndPoint);
                            }
                        }
                        else
                        {
                            SetWorkStatus(Agent.DONE);

                            args.Source = "Punct de plecare: ";
                            args.Information = "Agentul " + GetName() + " se întoarce la sursă.";
                            agencyContext.OnDispatching(args);

                            TryDispatch(agencyContext, GetAgencyCreationContext());
                        }
                    }
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
                if (!_info.ContainsKey(agencyContext.GetName()))
                {
                    _info.Add(agencyContext.GetName(), i );
                }
                else
                {
                    _info[agencyContext.GetName()] += i ;
                }
                information += i;

            }
            SetAgentStateInfo(GetAgentStateInfo() + agencyContext.GetName() + ": " + _info[agencyContext.GetName()] + Environment.NewLine + Environment.NewLine);
            return information;
        }
        /// <summary>
        /// Controlorul care seteaza parametrii agentului
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
            ResetLifetime();
            IAgencyContext agencyContext = GetAgentCurrentContext();
            RunNetwork(agencyContext);
        }
        /// <summary>
        /// Metoda de afisare a interfetei grafice proprii a agentului
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
            label1.Text = "Informații";
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
        public override String GetInfo()
        {
            throw new NotImplementedException();
        }
        #endregion Public Override Methods

    }

}



