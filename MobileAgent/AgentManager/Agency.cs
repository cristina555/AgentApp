using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MobileAgent.EventAgent;
using MobileAgent.Exceptions;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace MobileAgent.AgentManager
{   
    public class Agency : AgentContext
    {
        #region Fields
        private CloneListener cloneListener = null;
        private MobilityListener mobilityListener = null;
        private PersistencyListener persistencyListener =  null;
        private List<AgentProxy> _agentList = null;
        Socket _agencySocket = null;
        IPEndPoint _ipEndPoint = null;
        private int _agencyID;
        Dictionary<IPEndPoint, Socket> _connectionMap = new Dictionary<IPEndPoint, Socket>();
        #endregion Fields

        #region Constructors
        public Agency(IPAddress ipAddress, int port)
        {
            try
            {
                Random random = new Random();
                _agencyID = random.Next(1000, 9999);
                _agentList = new List<AgentProxy>();
                _agencySocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _ipEndPoint = new IPEndPoint(ipAddress, port);
                Console.WriteLine("Agentia creata la portul " + port + ".");
            }
            catch(SocketException e)
            {
                Console.WriteLine("SocketException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
        }
        #endregion Constructors

        #region Properties
        public List<AgentProxy> GetAgentProxies()
        {
             return _agentList;            
        }
        public int GetAgencyID()
        {
             return _agencyID;            
        }
        public IPEndPoint GetAgencyContext()
        {
             return _ipEndPoint;
        }
        #endregion Properties

        #region Methods
        public void Activate()
        {
            try
            {
                _agencySocket.Bind(_ipEndPoint);

            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
        }
        public void Start()
        {
            try
            {
                _agencySocket.Listen(100);
                Console.WriteLine("Agentia a inceput sa asculte!");

                Thread mainThread = new Thread(new ThreadStart(startListening));
                mainThread.IsBackground = true;
                mainThread.Start();

            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
        }
        private void startListening()
        {

            while (true)
            {
                Socket mySocket = _agencySocket.Accept();
                Thread newThread = new Thread(new ParameterizedThreadStart(startAccept));
                newThread.IsBackground = true;
                newThread.Start(mySocket);
            }
        }
        private void startAccept(object obj)
        {
            try
            {
                Socket s = (Socket)obj;
                NetworkStream networkStream = new NetworkStream(s);
                IFormatter formatter = new BinaryFormatter();
                AgentProxy agentProxy = (AgentProxy)formatter.Deserialize(networkStream);
                _agentList.Add(agentProxy);
                agentProxy.SetAgentContext(_ipEndPoint);  
                              
                Thread agentThread = new Thread(new ThreadStart(agentProxy.Run));
                agentThread.IsBackground = true;
                agentThread.Start();

            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
        }
        public void CreateAgent(AgentProxy agentProxy)
        {
            try
            {
                _agentList.Add(agentProxy);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
        }
        public AgentProxy Clone(AgentProxy agent) 
        {            
            AgentProxy agentCloned= null;
            agentCloned.SetAgentCodebase(agentCloned.GetAgentCodebase() + " cloned");
            return agentCloned;
        }
        public void Dispatch(AgentProxy agentProxy, IPEndPoint destination)
        {
            try
            {
                if (_connectionMap.ContainsKey(destination))
                {
                    NetworkStream networkStream = new NetworkStream(_connectionMap[destination]);
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(networkStream, agentProxy);
                    _agentList.Remove(agentProxy);
                }
                else
                {
                    Socket connectSocket = new Socket(_ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    connectSocket.Connect(destination);
                    _connectionMap.Add(destination, connectSocket);
                    NetworkStream networkStream = new NetworkStream(connectSocket);
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(networkStream, agentProxy);
                    _agentList.Remove(agentProxy);
                }
            }
            catch (AgentNotFoundException anfe)
            {
                Console.WriteLine("AgentNotFoundException caught!!!");
                Console.WriteLine("Source : " + anfe.Source);
                Console.WriteLine("Message : " + anfe.Message);
            }
            catch (IOException io)
            {
                Console.WriteLine("IOException caught!!!");
                Console.WriteLine("Source : " + io.Source);
                Console.WriteLine("Message : " + io.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
        }
        public void DispatchEvent(AgentEvent ev)
        {
            switch (ev.GetId())
            {
                case CloneEvent.CLONING:
                case CloneEvent.CLONE:
                case CloneEvent.CLONED:
                    ProcessCloneEvent((CloneEvent)ev);
                    break;
                case MobilityEvent.DISPATCHING:
                case MobilityEvent.REVERTING:
                case MobilityEvent.ARRIVAL:
                    ProcessMobilityEvent((MobilityEvent)ev);
                    break;
                case PersistencyEvent.DEACTIVATING:
                case PersistencyEvent.ACTIVATION:
                    ProcessPersistencyEvent((PersistencyEvent)ev);
                    break;
            }
        }
        public void Dispose(AgentProxy agentProxy) //throws InvalidAgletException
		{
            throw new Exception("Aceasta metoda trebuie completata");
        }

       
        public AgentProxy GetAgentProxy(string codebase)
        {
            AgentProxy agentProxy = null;
            foreach (AgentProxy aP in _agentList)
            {
                if (aP.GetAgentCodebase().Equals(codebase)) 
                {
                    agentProxy = aP;
                    break;
                }
            }
            return agentProxy;
        }
       
        public AgentProxy RetractAglet(IPEndPoint location) //throws IOException, AgletException
		{
            //Not implemented
            AgentProxy a = null;
            return a;
            throw new Exception("Aceasta metoda trebuie completata");
        }
        public void ShutDown()
        {
           
        }
        
        public void Deactivate(long duration) // throws IOException
        {
            throw new Exception("Aceasta metoda trebuie completata");
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCloneListener(CloneListener listener)
        {
            if (cloneListener == null)
            {
                cloneListener = listener;
            }
            else if (cloneListener == listener)
            {
                return;
            }
            else if (cloneListener.GetType().IsInstanceOfType(typeof(AgentEventListener)))
            {
                ((AgentEventListener)cloneListener).AddCloneListener(listener);
            }
            else if (cloneListener.GetType().IsInstanceOfType(typeof(CloneListener)))
            {
                cloneListener = new AgentEventListener(cloneListener, listener);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddMobilityListener(MobilityListener listener)
        {
            if (mobilityListener == null)
            {
                mobilityListener = listener;
            }
            else if (mobilityListener == listener)
            {
                return;
            }
            else if (mobilityListener.GetType().IsInstanceOfType(typeof(AgentEventListener)))
            {
                ((AgentEventListener)mobilityListener).AddMobilityListener(listener);
            }
            else if (mobilityListener.GetType().IsInstanceOfType(typeof(MobilityListener)))
            {
                mobilityListener = new AgentEventListener(mobilityListener, listener);
            }
        }
        public void AddPersistencyListener(PersistencyListener listener)
        {
            if (persistencyListener == null)
            {
                persistencyListener = listener;
            }
            else if (persistencyListener == listener)
            {
                return;
            }
            else if (persistencyListener.GetType().IsInstanceOfType(typeof(AgentEventListener)))
            {
                ((AgentEventListener)persistencyListener).AddPersistencyListener(listener);
            }
            else if (persistencyListener.GetType().IsInstanceOfType(typeof(PersistencyListener)))
            {
                persistencyListener = new AgentEventListener(persistencyListener, listener);
            }
        }
        public void AddContextListener(ContextListener listener)
        {
            throw new Exception("Aceasta metoda trebuie completata");
        }
        protected void ProcessCloneEvent(CloneEvent ev)
        {
            if (cloneListener != null)
            {
                switch (ev.GetId())
                {
                    case CloneEvent.CLONING:
                        cloneListener.OnCloning(ev);
                        break;
                    case CloneEvent.CLONE:
                        cloneListener.OnClone(ev);
                        break;
                    case CloneEvent.CLONED:
                        cloneListener.OnCloned(ev);
                        break;
                }
            }
        }
        protected void ProcessMobilityEvent(MobilityEvent ev)
        {
            if (mobilityListener != null)
            {
                switch (ev.GetId())
                {
                    case MobilityEvent.DISPATCHING:
                        mobilityListener.OnDispatching(ev);
                        break;
                    case MobilityEvent.REVERTING:
                        mobilityListener.OnReverting(ev);
                        break;
                    case MobilityEvent.ARRIVAL:
                        mobilityListener.OnArrival(ev);
                        break;
                }
            }
        }
        protected void ProcessPersistencyEvent(PersistencyEvent ev)
        {
            if (persistencyListener != null)
            {
                switch (ev.GetId())
                {
                    case PersistencyEvent.DEACTIVATING:
                        persistencyListener.OnDeactivating(ev);
                        break;
                    case PersistencyEvent.ACTIVATION:
                        persistencyListener.OnActivation(ev);
                        break;
                }
            }
        }
        protected void ProcessContextEvent(ContextEvent ev)
        {
            throw new Exception("Aceasta metoda trebuie completata");
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveCloneListener(CloneListener l)
        {
            if (cloneListener == l)
            {
                cloneListener = null;
            }
            else if (cloneListener.GetType().IsInstanceOfType(typeof(AgentEventListener)))
            {
                ((AgentEventListener)cloneListener).RemoveCloneListener(l);
                if (((AgentEventListener)cloneListener).Size() == 0)
                {
                    cloneListener = null;
                }
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveMobilityListener(MobilityListener l)
        {
            if (mobilityListener == l)
            {
                mobilityListener = null;
            }
            else if (mobilityListener.GetType().IsInstanceOfType(typeof(AgentEventListener)))
            {
                ((AgentEventListener)mobilityListener).RemoveMobilityListener(l);
                if (((AgentEventListener)mobilityListener).Size() == 0)
                {
                    mobilityListener = null;
                }
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemovePersistencyListener(PersistencyListener l)
        {
            if (persistencyListener == l)
            {
                persistencyListener = null;
            }
            else if (persistencyListener.GetType().IsInstanceOfType(typeof(AgentEventListener)))
            {
                ((AgentEventListener)persistencyListener).AddPersistencyListener(l);
                if (((AgentEventListener)persistencyListener).Size() == 0)
                {
                    persistencyListener = null;
                }
            }
        }
        public void RemoveContextListener(ContextListener listener)
        {
            //Not implemented
            throw new Exception("Aceasta metoda trebuie completata");
        }
        #endregion Methods
    }
}
