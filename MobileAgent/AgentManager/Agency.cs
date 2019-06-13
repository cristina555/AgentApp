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

namespace MobileAgent.AgentManager
{   
    public class Agency : AgencyContext
    {
        #region Private Fields
       // CloneListener cloneListener = null;
        //MobilityListener mobilityListener = null;
       // PersistencyListener persistencyListener =  null;

        List<AgentProxy> _agentsMobileList = null;
        List<AgentProxy> _agentsStatList = null;
        Socket _agencySocket = null;
        IPEndPoint _ipEndPoint = null;
        int _agencyID;
        string _name;
        List<string> _neighbours = new List<string>();
        AgentProxy _lastRunnableAgent = null;
        Dictionary<IPEndPoint, NetworkStream> _connectionMap = new Dictionary<IPEndPoint, NetworkStream>();
        #endregion Private Fields

        #region Public Fields
        public delegate void dgEventRaiser();
        public delegate void dgEventRaiser1();
       // public event dgEventRaiser OnArrival;
        public bool isConnected = false;

        //public event dgEventRaiser1 OnDispatching ;
        //public event dgEventRaiser OnReverting;
        //public event dgEventRaiser1 OnRefuseConnection;
        public event EventHandler<UnconnectedAgencyArgs> RefuseConnectionEvent;
        public event EventHandler<MobilityEventArgs> MobilityEvent;

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
        public AgentProxy GetLastRunnableAgent()
        {
            return _lastRunnableAgent;
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
        public void SetLastRunnableAgent(AgentProxy agentProxy)
        {
            _lastRunnableAgent = agentProxy;
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
                Thread newThread = new Thread(new ParameterizedThreadStart(StartAccept));
                newThread.Name = GetName() + ": Accept agents";
                newThread.IsBackground = true;
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
                SetLastRunnableAgent(agentProxy);
                IPEndPoint ip = agentProxy.GetAgencyCreationContext();

                Thread agentThread = new Thread(new ThreadStart(agentProxy.Run));
                agentThread.Name = GetName() + ": " + agentProxy.GetName();
                agentThread.IsBackground = true;
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
                agentProxy.SetAgentCodebase("");
                if (agentProxy.GetMobility().Equals(Agent.MOBILE))
                {
                    _agentsMobileList.Add(agentProxy);
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
                    isConnected = true;
                    networkStream = new NetworkStream(connectSocket);
                    agentProxy.SetAgentCurrentContext(null);
                    formatter.Serialize(networkStream, agentProxy);
                    _agentsMobileList.Remove(agentProxy);
                    return true;

                }
                else
                {
                    UnconnectedAgencyArgs args = new UnconnectedAgencyArgs();
                    args.Name = destination.ToString();
                    OnRefuseConnection(args);
                    Console.WriteLine("Refuse connection");
                    
                }
                return false;
                //OnDispatching();
            }
            catch (AgencyNotFoundException anfe)
            {
                Console.WriteLine("AgencyNotFoundException caught! Mesaj : " + anfe.Message + " --> Agency Dispach Agent.");
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
                //OnRefuseConnection();
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
            MobilityEvent?.Invoke(this, e);
        }
        //public void DispatchEvent(AgentEvent ev)
        //{
        //    switch (ev.GetId())
        //    {
        //        case CloneEvent.CLONING:
        //        case CloneEvent.CLONE:
        //        case CloneEvent.CLONED:
        //            ProcessCloneEvent((CloneEvent)ev);
        //            break;
        //        case MobilityEvent.DISPATCHING:
        //        case MobilityEvent.REVERTING:
        //        case MobilityEvent.ARRIVAL:
        //            ProcessMobilityEvent((MobilityEvent)ev);
        //            break;
        //        case PersistencyEvent.DEACTIVATING:
        //        case PersistencyEvent.ACTIVATION:
        //            ProcessPersistencyEvent((PersistencyEvent)ev);
        //            break;
        //    }
        //}
        public void Dispose(AgentProxy agentProxy) 
		{
            throw new Exception("Aceasta metoda trebuie completata");
        }

       
        public AgentProxy GetMobileAgentProxy(string name)
        {
            foreach (AgentProxy aP in _agentsMobileList)
            {
                if (aP.GetName().Equals(name)) 
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
        public void RetractAgent(AgentProxy agentProxy, IPEndPoint location) 
		{
            NetworkStream networkStream;
            Socket connectSocket = new Socket(_ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            connectSocket.Connect(location);
            networkStream = new NetworkStream(connectSocket);                
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(networkStream, agentProxy);
            _agentsMobileList.Remove(agentProxy);
            //OnReverting();
        }
        public void ShutDown()
        {
           
        }
        public void Deactivate(long duration)
        {
            throw new Exception("Aceasta metoda trebuie completata");
        }
        public void RunAgent(AgentProxy agentProxy)
        {
            agentProxy.SetWorkStatus(Agent.READY);
            Thread agentThread = new Thread(new ThreadStart(agentProxy.Run));
            agentThread.Name = GetName() + ": " +agentProxy.GetName();
            agentThread.IsBackground = true;
            agentThread.Start();
        }

        //[MethodImpl(MethodImplOptions.Synchronized)]
        //public void AddCloneListener(CloneListener listener)
        //{
        //    if (cloneListener == null)
        //    {
        //        cloneListener = listener;
        //    }
        //    else if (cloneListener == listener)
        //    {
        //        return;
        //    }
        //    else if (cloneListener.GetType().IsInstanceOfType(typeof(AgentEventListener)))
        //    {
        //        ((AgentEventListener)cloneListener).AddCloneListener(listener);
        //    }
        //    else if (cloneListener.GetType().IsInstanceOfType(typeof(CloneListener)))
        //    {
        //        cloneListener = new AgentEventListener(cloneListener, listener);
        //    }
        //}
        //[MethodImpl(MethodImplOptions.Synchronized)]
        //public void AddMobilityListener(MobilityListener listener)
        //{
        //    if (mobilityListener == null)
        //    {
        //        mobilityListener = listener;
        //    }
        //    else if (mobilityListener == listener)
        //    {
        //        return;
        //    }
        //    else if (mobilityListener.GetType().IsInstanceOfType(typeof(AgentEventListener)))
        //    {
        //        ((AgentEventListener)mobilityListener).AddMobilityListener(listener);
        //    }
        //    else if (mobilityListener.GetType().IsInstanceOfType(typeof(MobilityListener)))
        //    {
        //        mobilityListener = new AgentEventListener(mobilityListener, listener);
        //    }
        //}
        //public void AddPersistencyListener(PersistencyListener listener)
        //{
        //    if (persistencyListener == null)
        //    {
        //        persistencyListener = listener;
        //    }
        //    else if (persistencyListener == listener)
        //    {
        //        return;
        //    }
        //    else if (persistencyListener.GetType().IsInstanceOfType(typeof(AgentEventListener)))
        //    {
        //        ((AgentEventListener)persistencyListener).AddPersistencyListener(listener);
        //    }
        //    else if (persistencyListener.GetType().IsInstanceOfType(typeof(PersistencyListener)))
        //    {
        //        persistencyListener = new AgentEventListener(persistencyListener, listener);
        //    }
        //}
        //public void AddContextListener(ContextListener listener)
        //{
        //    throw new Exception("Aceasta metoda trebuie completata");
        //}
        //protected void ProcessCloneEvent(CloneEvent ev)
        //{
        //    if (cloneListener != null)
        //    {
        //        switch (ev.GetId())
        //        {
        //            case CloneEvent.CLONING:
        //                cloneListener.OnCloning(ev);
        //                break;
        //            case CloneEvent.CLONE:
        //                cloneListener.OnClone(ev);
        //                break;
        //            case CloneEvent.CLONED:
        //                cloneListener.OnCloned(ev);
        //                break;
        //        }
        //    }
        //}
        //protected void ProcessMobilityEvent(MobilityEvent ev)
        //{
        //    if (mobilityListener != null)
        //    {
        //        switch (ev.GetId())
        //        {
        //            case MobilityEvent.DISPATCHING:
        //                mobilityListener.OnDispatching(ev);
        //                break;
        //            case MobilityEvent.REVERTING:
        //                mobilityListener.OnReverting(ev);
        //                break;
        //            case MobilityEvent.ARRIVAL:
        //                mobilityListener.OnArrival(ev);
        //                break;
        //        }
        //    }
        //}
        //protected void ProcessPersistencyEvent(PersistencyEvent ev)
        //{
        //    if (persistencyListener != null)
        //    {
        //        switch (ev.GetId())
        //        {
        //            case PersistencyEvent.DEACTIVATING:
        //                persistencyListener.OnDeactivating(ev);
        //                break;
        //            case PersistencyEvent.ACTIVATION:
        //                persistencyListener.OnActivation(ev);
        //                break;
        //        }
        //    }
        //}
        //protected void ProcessContextEvent(ContextEvent ev)
        //{
        //    throw new Exception("Aceasta metoda trebuie completata");
        //}
        //[MethodImpl(MethodImplOptions.Synchronized)]
        //public void RemoveCloneListener(CloneListener l)
        //{
        //    if (cloneListener == l)
        //    {
        //        cloneListener = null;
        //    }
        //    else if (cloneListener.GetType().IsInstanceOfType(typeof(AgentEventListener)))
        //    {
        //        ((AgentEventListener)cloneListener).RemoveCloneListener(l);
        //        if (((AgentEventListener)cloneListener).Size() == 0)
        //        {
        //            cloneListener = null;
        //        }
        //    }
        //}
        //[MethodImpl(MethodImplOptions.Synchronized)]
        //public void RemoveMobilityListener(MobilityListener l)
        //{
        //    if (mobilityListener == l)
        //    {
        //        mobilityListener = null;
        //    }
        //    else if (mobilityListener.GetType().IsInstanceOfType(typeof(AgentEventListener)))
        //    {
        //        ((AgentEventListener)mobilityListener).RemoveMobilityListener(l);
        //        if (((AgentEventListener)mobilityListener).Size() == 0)
        //        {
        //            mobilityListener = null;
        //        }
        //    }
        //}
        //[MethodImpl(MethodImplOptions.Synchronized)]
        //public void RemovePersistencyListener(PersistencyListener l)
        //{
        //    if (persistencyListener == l)
        //    {
        //        persistencyListener = null;
        //    }
        //    else if (persistencyListener.GetType().IsInstanceOfType(typeof(AgentEventListener)))
        //    {
        //        ((AgentEventListener)persistencyListener).AddPersistencyListener(l);
        //        if (((AgentEventListener)persistencyListener).Size() == 0)
        //        {
        //            persistencyListener = null;
        //        }
        //    }
        //}
        //public void RemoveContextListener(ContextListener listener)
        //{
        //    //Not implemented
        //    throw new Exception("Aceasta metoda trebuie completata");
        //}
        #endregion Methods
    }
}
