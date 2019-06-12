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
        private void CollectInfo()
        {
            String information = "";
            AgencyContext agencyContext = GetAgentCurrentContext();
            MobilityEventArgs args = new MobilityEventArgs();
            if (wayBack.Count != 0)
            {
                args.Source = "Agentul " + GetName() +" se duce la " + queue.Peek().Item1 + " si se intoarce prin: ";
                args.Information = agencyContext.GetName();

                string s = wayBack.Dequeue();
                if (wayBack.Count != 0)
                {
                    string back = wayBack.Peek();
                    IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(back);
                    int portNumber = AgencyForm.configParser.GetPort(back);
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
                    agencyContext.Dispatch(this, ipEndPoint);
                }
                else
                {
                    string cont = queue.Peek().Item1;
                    IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(cont);
                    int portNumber = AgencyForm.configParser.GetPort(cont);
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
                    //while (!agencyContext.Dispatch(this, ipEndPoint))
                    //{
                    //    string pop = queue.Dequeue().Item1;
                    //    if (queue.Count != 0)
                    //    {
                    //        cont = queue.Peek().Item1;
                    //        ipAddress = AgencyForm.configParser.GetIPAdress(cont);
                    //        portNumber = AgencyForm.configParser.GetPort(cont);
                    //        ipEndPoint = new IPEndPoint(ipAddress, portNumber);
                    //    }
                    //}
                }
            }
            else
            {

                if (queue.Count != 0)
                {

                    foreach (string par in Parameters)
                    {
                        AgentProxy agentStatic = agencyContext.GetStationaryAgent(GetInfo(par));
                        agentStatic.Run();
                        if (!_info.ContainsKey(agencyContext.GetName()))
                        {
                            _info.Add(agencyContext.GetName(), agentStatic.GetAgentCodebase());
                        }
                        else
                        {
                            _info[agencyContext.GetName()] += agentStatic.GetAgentCodebase();
                        }
                        information += agentStatic.GetAgentCodebase();

                    }
                    SetAgentCodebase(GetAgentCodebase() + agencyContext.GetName() + ": " + _info[agencyContext.GetName()] + Environment.NewLine);

                    args.Source = "Agentul  " + GetName() + " ruleaza...";
                    args.Information = agencyContext.GetName() + ": " + information;

                    Tuple<string, Queue<string>> s = queue.Dequeue();
                    foreach (string n in agencyContext.GetNeighbours())
                    {
                        if (!agenciesVisited.Contains(n))
                        {
                            Queue<string> stack = new Queue<string>();
                            stack.Enqueue(agencyContext.GetName());
                            foreach (Object obj in s.Item2)
                            {
                                stack.Enqueue(obj.ToString());
                            }
                            queue.Enqueue(Tuple.Create(n, stack));
                            agenciesVisited.Add(n);
                        }
                    }
                    if (queue.Count != 0)
                    {
                        string next = queue.Peek().Item1;
                        if (agencyContext.GetNeighbours().Contains(next))
                        {

                            IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(next);
                            int portNumber = AgencyForm.configParser.GetPort(next);
                            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
                            while(!agencyContext.Dispatch(this, ipEndPoint)&&(queue.Count!=0))
                            {
                                string pop = queue.Dequeue().Item1;
                                if (queue.Count != 0)
                                {
                                    next = queue.Peek().Item1;
                                    if(agencyContext.GetNeighbours().Contains(next))
                                    ipAddress = AgencyForm.configParser.GetIPAdress(next);
                                    portNumber = AgencyForm.configParser.GetPort(next);
                                    ipEndPoint = new IPEndPoint(ipAddress, portNumber);
                                }
                            }
                            
                        }
                        else
                        {
                            wayBack = s.Item2;
                            List<string> q = new List<string>(queue.Peek().Item2.ToArray());
                            q.Reverse();
                            q.RemoveAt(0);
                            foreach (string el in q)
                            {
                                wayBack.Enqueue(el);
                            }
                            string back = wayBack.Peek();
                            IPAddress ipAddress = AgencyForm.configParser.GetIPAdress(back);
                            int portNumber = AgencyForm.configParser.GetPort(back);
                            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
                            agencyContext.Dispatch(this, ipEndPoint);
                        }

                    }
                    else
                    {

                        agenciesVisited.Clear();
                        agencyContext.Dispatch(this, GetAgencyCreationContext());

                    }

                }
                else
                {
                    args.Source = "Agentul  " + GetName() + " a adunat informatiile: ";
                    args.Information = GetAgentCodebase();
                }
            }
            agencyContext.OnArrival(args);
        }
        #endregion Private Methods

        #region Public Methods
        public override void Run()
        {
            CollectInfo();
            

        }
        public override void GetUI()
        {
            XmlDocument xmlDoc = new XmlDocument();
        }
        #endregion Public Methods

    }
}
