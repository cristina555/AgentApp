using System;
using System.Collections.Generic;
using MobileAgent.Exceptions;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using MobileAgent.EventAgent;
using System.Net.NetworkInformation;

namespace MobileAgent.AgentManager
{   
    public class Agency : AgencyContext
    {
        #region Private Fields
       
        List<AgentProxy> _agentsMobileList = null;
        List<AgentProxy> _agentsStatList = null;
        Socket _agencySocket = null;
        IPEndPoint _ipEndPoint = null;
        int _agencyID;
        string _name;
        List<string> _neighbours = new List<string>();
        AgentProxy _agentDispatched = null;
        #endregion Private Fields

        #region Public Fields
        //public delegate void dgEventRaiser();
        //public event dgEventRaiser UpdateAgency;
        public event EventHandler<UnconnectedAgencyArgs> RefuseConnectionEvent;
        public event EventHandler<MobilityEventArgs> MobilityEventArr;
        public event EventHandler<MobilityEventArgs> MobilityEventDis;
        #endregion  Public Fields

        #region Constructors
        public Agency()
        {

        }
        public Agency(IPAddress ipAddress, int port)
        {
            try
            {
                Random random = new Random();
                _agencyID = random.Next(1000, 9999);
                _agentsMobileList = new List<AgentProxy>();
                _agentsStatList = new List<AgentProxy>();
                _agencySocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _ipEndPoint = new IPEndPoint(ipAddress, port);
                Console.WriteLine("Agentia creata la portul " + port + ".");
            }
            catch(SocketException e)
            {
                Console.WriteLine("SocketException caught! Message: " + e.Message + " --> Constructor Agency.");
            }
            catch (Exception e)
            { 
                Console.WriteLine("Exception caught! Message: " + e.Message + " --> Constructor Agency.");
            }
        }
        #endregion Constructors

        #region Properties
        
        public string GetName()
        {
            return _name;
        }
        public List<string> GetNeighbours()
        {
            return _neighbours;
        }
        public AgentProxy GetDispatchedAgent()
        {
            return _agentDispatched;
        }
        public List<AgentProxy> GetAgentProxies( int type)
        {
            if (type == Agent.MOBILE)
            {
                return _agentsMobileList;
            }
            else
            {
                return _agentsStatList;
            }
        }
        public int GetAgencyID()
        {
             return _agencyID;            
        }
        public IPEndPoint GetAgencyIPEndPoint()
        {
             return _ipEndPoint;
        }
        public IStationary GetStationaryAgent(string name)
        {
            foreach(IStationary ap in _agentsStatList)
            {
                if(ap.GetName() == name)
                {
                    return ap;
                }
            }
            return null;
        }
        public void SetName(string name)
        {
            _name = name;
        }
        public void SetNeighbours(List<string> neighbours)
        {
             _neighbours =  neighbours;
        }
        public void SetDispatchedAgent(AgentProxy agentProxy)
        {
            _agentDispatched = agentProxy;
        }
        #endregion Properties

        #region Methods
        public void Activate()
        {
            try
            {
                _agencySocket.Bind(_ipEndPoint);

            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException caught! Mesaj : " + se.Message+ " --> Activate Agency.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught! Mesaj : " + ex.Message + " --> Activate Agency.");
            }
        }
        public void Start()
        {
            try
            {
                _agencySocket.Listen(100);
                Console.WriteLine("Agentia a inceput sa asculte!");

                Thread mainThread = new Thread(new ThreadStart(StartListening));
                mainThread.IsBackground = true;
                mainThread.Start();

            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException caught! Mesaj : " + se.Message + " --> Start Agency.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught! Mesaj : " + ex.Message + " --> Start Agency.");
            }
        }
        private void StartListening()
        {

            while (true)
            {
                Socket mySocket = _agencySocket.Accept();
                Console.WriteLine(mySocket);
                Thread newThread = new Thread(new ParameterizedThreadStart(StartAccept))
                {
                    Name = GetName() + ": Accept agents",
                    IsBackground = true
                };
                newThread.Start(mySocket);
            }
        }
        private void StartAccept(object obj)
        {
            try
            {
                Socket s = (Socket)obj;
                Console.WriteLine(s.AddressFamily);
                NetworkStream networkStream = new NetworkStream(s);
                IFormatter formatter = new BinaryFormatter();
                AgentProxy agentProxy = (AgentProxy)formatter.Deserialize(networkStream);

                
                _agentsMobileList.Add(agentProxy);
                agentProxy.SetAgentCurrentContext(this);

                Thread agentThread = null;

                agentThread = new Thread(new ThreadStart(agentProxy.Run))
                {
                    Name = GetName() + ": " + agentProxy.GetName(),
                    IsBackground = true
                };
                agentThread.Start();
                
                
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException caught! Mesaj : " + se.Message +  " -->Agency Accept Agents.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught! Mesaj : " + ex.Message + ex.StackTrace +" --> Agency Accept Agents.");
            }
        }
        public void CreateAgent(AgentProxy agentProxy)
        {
            try
            {
                agentProxy.SetAgentCurrentContext(this);
                agentProxy.SetAgencyCreationContext(this.GetAgencyIPEndPoint());
                agentProxy.SetAgentStateInfo("");
                if (agentProxy.IsMobile())
                {
                    _agentsMobileList.Add(agentProxy);
                    agentProxy.GetUI();
                    //UpdateAgency();
                }
                else
                {
                    _agentsStatList.Add(agentProxy);
                }
            }
            catch(AgencyNotFoundException anfe)
            {
                Console.WriteLine("AgencyNotFoundException caught! Mesaj : " + anfe.Message + " --> Agency Create Agent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught! Mesaj : " + ex.Message + " --> Agency Create Agent.");
            }
        }
        
        //TODO
        public void Clone(AgentProxy agentCloned) 
        {
            try
            {
                agentCloned.SetAgentCurrentContext(this);
                agentCloned.SetAgencyCreationContext(this.GetAgencyIPEndPoint());
                agentCloned.SetName(agentCloned.GetName() + " cloned");
                agentCloned.SetAgentInfo(agentCloned.GetAgentInfo() + " cloned");
                _agentsMobileList.Add(agentCloned);
                agentCloned.GetUI();
                //UpdateAgency();
            }
            catch (CloneNotSupportedException cnse)
            {
                Console.WriteLine("CloneNotSupportedException caught! Mesaj : " + cnse.Message + " --> Agency Clone Agent.");
            }
            catch (AgencyNotFoundException anfe)
            {
                Console.WriteLine("AgencyNotFoundException caught! Mesaj : " + anfe.Message + " --> Agency Clone Agent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught! Mesaj : " + ex.Message + " --> Agency Clone Agent.");
            }
        }
        
        //More exceptions
        public bool Dispatch(AgentProxy agentProxy, IPEndPoint destination)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                NetworkStream networkStream =  null;
               
                Socket connectSocket = new Socket(_ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    connectSocket.Connect(destination);
                }
                catch (Exception)
                {
                    
                }
                if (connectSocket.Connected)                   
                {
                    networkStream = new NetworkStream(connectSocket);
                    agentProxy.SetAgentCurrentContext(null);
                    formatter.Serialize(networkStream, agentProxy);
                    _agentsMobileList.Remove(agentProxy);
                    //UpdateAgency();
                    return true;

                }
                else
                {
                    UnconnectedAgencyArgs args = new UnconnectedAgencyArgs();
                    args.Date = DateTime.Now;
                    args.Name = destination;
                    OnRefuseConnection(args);
                    Console.WriteLine("Refuse connection");
                    
                }
                //UpdateAgency();
                return false;
            }
            catch (NullReferenceException nfe)
            {
                Console.WriteLine("NullReferenceException caught! Mesaj : " + nfe.Message + " " + nfe.StackTrace +" --> Agency Dispach Agent.");
            }
            catch (SocketException io)
            {
                Console.WriteLine("SocketException caught! Mesaj : " + io.Message + io.StackTrace +" --> Agency Dispach Agent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught! Mesaj : " + ex.Message + " --> Agency Dispach Agent.");
            }
            return false;
        }
        public void OnRefuseConnection(UnconnectedAgencyArgs e)
        {
            RefuseConnectionEvent?.Invoke(this, e);
        }
        public void OnArrival(MobilityEventArgs e)
        {
            MobilityEventArr?.Invoke(this, e);
        }
        public void OnDispatching(MobilityEventArgs e)
        {
            MobilityEventDis?.Invoke(this, e);
        }
        public void Dispose(AgentProxy agentProxy) 
		{
            agentProxy.SetStatus(Agent.INACTIVE);
            //UpdateAgency();
        }
        public void RemoveAgent(AgentProxy agentProxy)
        {
            _agentsMobileList.Remove(agentProxy);
        }
       
        public AgentProxy GetMobileAgentProxy(string name)
        {
            int index = name.IndexOf(":");
            string newName = name.Substring(index + 2);
            foreach (AgentProxy aP in _agentsMobileList)
            {
                if (aP.GetName().Equals(newName)) 
                {
                    return  aP;
                }
            }
            return null;
        }
        public AgentProxy GetMobileAgentProxy(int id)
        {
            foreach (AgentProxy aP in _agentsMobileList)
            {
                if (aP.GetAgentId().Equals(id))
                {
                    return aP;
                }
            }
            return null;
        }
        //TODO
        public void RetractAgent(AgentProxy agentProxy) 
		{
            agentProxy.SetAgentCurrentContext(null);
            NetworkStream networkStream;
            Socket connectSocket = new Socket(_ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            connectSocket.Connect(agentProxy.GetAgencyCreationContext());
            if (connectSocket.Connected)
            {
                networkStream = new NetworkStream(connectSocket);
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(networkStream, agentProxy);
                _agentsMobileList.Remove(agentProxy);
            }
            else
            {
                UnconnectedAgencyArgs args = new UnconnectedAgencyArgs();
                args.Date = DateTime.Now;
                args.Name = agentProxy.GetAgencyCreationContext();
            }
            //UpdateAgency();
        }
        public void ShutDown()
        {
           
        }
        public void Deactivate(AgentProxy agentProxy)
        {
            agentProxy.SetStatus(Agent.INACTIVE);
        }
        public void RunAgent(AgentProxy agentProxy)
        {
            agentProxy.SetWorkStatus(Agent.READY);
            agentProxy.SetAgentStateInfo(null);
            Thread agentThread = new Thread(new ThreadStart(agentProxy.Run));
            agentThread.Name = GetName() + ": " +agentProxy.GetName();
            agentThread.IsBackground = true;
            agentThread.Start();
            //UpdateAgency();
        }
        
        #endregion Methods
    }
}
