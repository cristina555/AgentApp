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
        private List<IMobile> _agentsMobileList = null;
        private List<IStationary> _agentsStatList = null;
        private Socket _agencySocket = null;
        private IPEndPoint _ipEndPoint = null;
        private int _agencyID;
        private string _name;
        private List<string> _neighbours = new List<string>();
        private AgentProxy _agentDispatched = null;
        #endregion Private Fields

        #region Private Static Fields
        private static int _rezervationTime;
        private static System.Timers.Timer _timer;
        #endregion Private Static Fields

        #region Public Fields
        public delegate void dgEventRaiser();
        public event dgEventRaiser UpdateAgency;
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
                _agentsMobileList = new List<IMobile>();
                _agentsStatList = new List<IStationary>();
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

        #region Property Methods
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
                List<AgentProxy> agentsMobileList = new List<AgentProxy>(_agentsMobileList);
                return agentsMobileList;
            }
            else
            {
                List<AgentProxy> agentsStatList = new List<AgentProxy>(_agentsStatList);
                return agentsStatList;
            }
        }
        public List<AgentProxy> GetActiveAgentProxies()
        {
            List<AgentProxy> activeAP = new List<AgentProxy>();
            foreach (IMobile ap in _agentsMobileList)
            {
                if (ap.IsActive())
                {
                    activeAP.Add(ap);
                }
            }
            return activeAP;
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
        #endregion Property Methods

        #region Private Methods
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
                IMobile agentProxy = (IMobile)formatter.Deserialize(networkStream);


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
                Console.WriteLine("SocketException caught! Mesaj : " + se.Message + " -->Agency Accept Agents.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught! Mesaj : " + ex.Message + ex.StackTrace + " --> Agency Accept Agents.");
            }
        }
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            _rezervationTime -= 1000;
            if (_rezervationTime == 0)
            {
                Console.WriteLine("Time is over!!");
                UpdateAgency();
                _timer.Stop();
                _timer.Dispose();
            }
        }
        #endregion Private Methods

        #region Public Methods
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
        public void CreateAgent(AgentProxy agentProxy)
        {
            try
            {
                agentProxy.SetAgentCurrentContext(this);
                agentProxy.SetAgencyCreationContext(this.GetAgencyIPEndPoint());
                agentProxy.SetAgentStateInfo("");
                if (agentProxy.IsMobile())
                {
                    IMobile agentProxyM = (IMobile)agentProxy;
                    _agentsMobileList.Add(agentProxyM);                    
                    agentProxyM.GetUI();
                }
                else
                {
                    IStationary agentProxyS = (IStationary)agentProxy;
                    _agentsStatList.Add(agentProxyS);
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
        public void Clone(IMobile agentCloned) 
        {
            try
            {
                agentCloned.SetAgentCurrentContext(this);
                agentCloned.SetAgencyCreationContext(this.GetAgencyIPEndPoint());
                agentCloned.SetName(agentCloned.GetName() + " cloned");
                agentCloned.SetAgentInfo(agentCloned.GetAgentInfo() + " cloned");
                _agentsMobileList.Add(agentCloned);                
                agentCloned.GetUI();
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
        public bool Dispatch(IMobile agentProxy, IPEndPoint destination)
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
        public void Deactivate(AgentProxy agentProxy) 
		{
            agentProxy.SetState(Agent.INACTIVE);
        }
        public void ActivateAgent(AgentProxy agentProxy)
        {
            agentProxy.SetState(Agent.ACTIVE);
            agentProxy.ResetLifetime();
        }
        public void RemoveAgent(IMobile agentProxy)
        {
            _agentsMobileList.Remove(agentProxy);
        }
        public IMobile GetMobileAgentProxy(string name)
        {
            int index = name.IndexOf(":");
            string newName = name.Substring(index + 2);
            foreach (IMobile aP in _agentsMobileList)
            {
                if (aP.GetName().Equals(newName)) 
                {
                    return  aP;
                }
            }
            return null;
        }
        public IMobile GetMobileAgentProxy(int id)
        {
            foreach (IMobile aP in _agentsMobileList)
            {
                if (aP.GetAgentId().Equals(id))
                {
                    return aP;
                }
            }
            return null;
        }
        public IStationary GetStatAgentProxy(string name)
        {
            int index = name.IndexOf(":");
            string newName = name.Substring(index + 2);
            foreach (IStationary aP in _agentsStatList)
            {
                if (aP.GetName().Equals(newName))
                {
                    return aP;
                }
            }
            return null;
        }
        //TODO
        public void RetractAgent(IMobile agentProxy) 
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
        public void RunAgent(IMobile agentProxy)
        {
            agentProxy.SetWorkStatus(Agent.READY);
            agentProxy.SetAgentStateInfo(null);
            Thread agentThread = new Thread(new ThreadStart(agentProxy.Run));
            agentThread.Name = GetName() + ": " +agentProxy.GetName();
            agentThread.IsBackground = true;
            agentThread.Start();
            //UpdateAgency();
        }
        public void SetBookedTime(int milli)
        {
            _rezervationTime = milli;
            Console.WriteLine("Timer-ul a fost setat!");
            _timer = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer. 
            _timer.Elapsed +=  OnTimedEvent;
            _timer.Enabled = true;
        }
        #endregion Public Methods

        #region Public Event Methods
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
        #endregion Public Event Methods

    }
}
