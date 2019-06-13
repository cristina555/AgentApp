using MobileAgent.AgentManager;
using MobileAgent.EventAgent;
using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;

namespace AgentApp.Agents
{
    [Serializable]
    public class AgentRemote : Agent
    {
        #region Private Fields
        Queue<string> wayBack = new Queue<string>();
        Dictionary<string, String> _info = null;
        
        #endregion Private Fields

        #region Public Fields
        public List<string> agenciesVisited =  new List<string>();
        public Queue<Tuple<string, Queue<string>>> queue = new Queue<Tuple<string, Queue<string>>>();
        #endregion Public Fields

        #region Constructor
        public AgentRemote()
        {
            Parameters = new List<string>();
            this.SetName("AgentRemote");
            this.SetAgentInfo("Collect information from network");
            _info = new Dictionary<string, String>();
        }
        #endregion Constructor

        #region Properties
        public List<string> Parameters { get;  set; }
        #endregion Properties

        #region Private Methods
        private string GetInfo(string parameter)
        {
            string type = "";
            switch(parameter)
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
                default:
                    {
                        break;
                    }
            }
            return type;
        }
        
        private void AddNeighbours(AgencyContext agencyContext, Queue<string> agencyQueue)
        {
            foreach(string n in agencyContext.GetNeighbours())
            {
                if(!agenciesVisited.Contains(n))
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
                        }
                        else
                        {
                            CreateWayBack(t.Item2);
                            wayBack.Dequeue();
                            string back = wayBack.Peek();
                            IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(back);
                            int portNumber = AgencyForm.configParser.GetPort(back);
                            destination = new IPEndPoint(ipAddress, portNumber);
                        }
                    }
                    else
                    {
                        t.Item2.Dequeue();                       
                        destination = RetractAgent(agencyContext, t.Item2);
                        if(destination == null)
                        {
                            break;
                        }
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
            if (wayBack.Count !=0)
            {
                if (queue.Count != 0)
                {
                    args.Source = "Agentul " + GetName() + "se intoarce pentru a se duce la " + queue.Peek().Item1;
                    args.Information = "";
                }
                else
                {
                    args.Source = "Agentul " + GetName() + "se intoarce la sursa";
                    args.Information = "";
                }
                string s = wayBack.Dequeue();
                if (wayBack.Count != 0)
                {
                    string back = wayBack.Peek();
                    IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(back);
                    int portNumber = AgencyForm.configParser.GetPort(back);
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
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
                        TryDispatch(agencyContext, ipEndPoint);
                    }
                    else
                    {
                        TryDispatch(agencyContext, GetAgencyCreationContext());
                    }
                }
            }
            else
            {
                
                if (agencyContext.GetAgencyIPEndPoint().Equals(GetAgencyCreationContext()))
                {
                    if (GetWorkStatus() == Agent.READY)
                    {
                        agenciesVisited.Clear();
                        _info.Clear();
                        SetAgentCodebase("");
                        agenciesVisited.Add(agencyContext.GetName());
                        AddNeighbours(agencyContext, new Queue<string>());
                        if (queue.Count != 0)
                        {
                            string next = queue.Peek().Item1;
                            IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(next);
                            int portNumber = AgencyForm.configParser.GetPort(next);
                            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
                            TryDispatch(agencyContext, ipEndPoint);
                        }
                        
                    }
                    else
                    {
                        if (!GetAgentCodebase().Equals(""))
                        {
                            args.Source = "Agentul  " + GetName() + " a adunat informatiile: ";
                            args.Information = GetAgentCodebase();
                        }
                        else
                        {
                            args.Source = "Agentul " + GetName() + " nu a adunat nicio informatie !";
                            args.Information = "";
                        }

                    }
                }
                else
                {
                    if (queue.Count != 0)
                    {
                        Tuple<string, Queue<string>> t = queue.Dequeue();
                        args.Source = "Agentul  " + GetName() + " ruleaza...";
                        args.Information = agencyContext.GetName() + ": " + ColectInformation(agencyContext);

                        AddNeighbours(agencyContext, t.Item2);
                        if (queue.Count != 0)
                        {
                            string next = queue.Peek().Item1;
                            if (agencyContext.GetNeighbours().Contains(next))
                            {
                                IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(next);
                                int portNumber = AgencyForm.configParser.GetPort(next);
                                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
                                TryDispatch(agencyContext, ipEndPoint);
                            }
                            else
                            {
                                CreateWayBack(t.Item2);
                                string back = wayBack.Peek();
                                IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(back);
                                int portNumber = AgencyForm.configParser.GetPort(back);
                                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
                                TryDispatch(agencyContext, ipEndPoint);
                            }
                        }
                        else
                        {
                            SetWorkStatus(Agent.DONE);
                            TryDispatch(agencyContext, GetAgencyCreationContext());
                        }

                    }
                    
                    
                }
            }
            agencyContext.OnArrival(args);
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
            SetAgentCodebase(GetAgentCodebase() + agencyContext.GetName() + ": " + _info[agencyContext.GetName()] + Environment.NewLine);
            return information;
        }
        #endregion Private Methods

        #region Public Methods
        public override void Run()
        {
            
            AgencyContext agencyContext = GetAgentCurrentContext();
            RunNetwork(agencyContext);
        }
        public override void GetUI()
        {
            XmlDocument xmlDoc = new XmlDocument();
        }
        #endregion Public Methods

    }
}
