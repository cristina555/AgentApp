using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MobileAgent.EventAgent;
using MobileAgent.AdditionalClasses;
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
        private FileStream _fileStream;
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

        #region Methods
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
            else if (cloneListener.GetType().IsInstanceOfType(typeof(AgentEventListener))) {
                ((AgentEventListener)cloneListener).AddCloneListener(listener);
            } else if (cloneListener.GetType().IsInstanceOfType(typeof(CloneListener)) ){
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
        public void AddContextListener()
		{
            //Not implemented
        }
        public void CreateAgent(AgentProxy agentProxy)
        {
            try
            {
                _agentList.Add(agentProxy);
            }
            catch (AgencyNotFoundException anfe)
            {
                Console.WriteLine("AgencyNotFoundException caught!!!");
                Console.WriteLine("Source : " + anfe.Source);
                Console.WriteLine("Message : " + anfe.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
        }
        public AgentProxy Clone(AgentProxy agent) // throws CloneNotSupportedException
        {
            
            //AgentProxy agentCloned= null;
            //agentCloned = agent;
            //return agentCloned;
            throw new Exception("Aceasta metoda trebuie completata");

        }
        public void Dispatch(AgentProxy agentProxy, Ticket destination)// throws IOException, AgletException
		{            
            throw new Exception("Aceasta metoda trebuie completata");
        }
		public AgentProxy Dispatch(URL destination)
		{
            //Not implemented
            //AgentProxy agentDispatched = null;
            //return agentDispatched;
            throw new Exception("Aceasta metoda trebuie completata");
        }
        public void Dispatch(AgentProxy agentProxy, int destination)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                _fileStream = File.Create("agent.data");
                formatter.Serialize(_fileStream, agentProxy);

                byte[] dataArray = readBytes(_fileStream);

                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Connect(ipAddress, destination);

                if(listener.Connected == true)
                {
                    _agencySocket.Send(dataArray);
                    listener.Receive(dataArray); 
                }

            }
            catch(AgentNotFoundException anfe)
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
            //throw new Exception("Aceasta metoda trebuie completata");
        }
        private byte[] readBytes(FileStream stream)
        {
            byte[] dataArray = new byte[16];
            try
            {
                for (int i = 0; i < stream.Length; i++)
                {
                    stream.Read(dataArray, 0, dataArray.Length);
                }
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
            return dataArray;
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
            //Not implemented
        }
        public List<AgentProxy> GetAgentProxies()
        {
            return _agentList;

        }
        public AgentProxy GetAgentProxy(int id)
        {
            //Not implemented
            AgentProxy a = null;
            return a;
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
        public int GetAgencyID()
        {
            //Not implemented
            return _agencyID ;
        }
        public AgentProxy GetAgentProxy(URL contextAddress, int id)
        {
            //Not implemented
            AgentProxy a = null;
            return a;
        }
        public String GetName()
        {
            String Name = "";
            return Name;
        }
		public Object GetProperty(String key)
		{
            //Not implemented
            return null;
		}
        public Object GetProperty(String key, Object defaultValue)
		{
            //Not implemented
            return null;
		}
        public URL GetHostingURL()
        {
            //Not implemented
            return null;
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveCloneListener(CloneListener l)
        {
            if (cloneListener == l)
            {
                cloneListener = null;
            }
            else if (cloneListener.GetType().IsInstanceOfType(typeof(AgentEventListener))) {
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
            else if (mobilityListener.GetType().IsInstanceOfType(typeof(AgentEventListener))) {
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
            else if (persistencyListener.GetType().IsInstanceOfType(typeof(AgentEventListener))){
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
        }
        public AgentProxy RetractAglet(URL url) //throws IOException, AgletException
		{
            //Not implemented
            AgentProxy a = null;
            return a;
        }
		public AgentProxy RetractAglet(URL url, int agentId)//  throws IOException, AgletException
		{
            //Not implemented
            AgentProxy a = null;
            return a;
        }
		public void SetProperty(String key, Object value)
		{
            //Not implemented
        }
        public void ShutDown()
        {
            _agencySocket.Close();
        }
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
                Thread t1 = new Thread(new ParameterizedThreadStart(startListening));
                t1.IsBackground = true;
                t1.Start();

           }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
        }
        private void startListening(object obj)
        {
            
            Socket s = (Socket)obj;

            while (true)
            {
                if (File.Exists("agent.data"))
                {
                    _fileStream = File.OpenRead("agent.data");
                    IFormatter formatter = new BinaryFormatter();
                    AgentProxy agentProxy = (AgentProxy)formatter.Deserialize(_fileStream);
                }
            }
            //s.Receive()
            //Socket agentSocket = _agencySocket.Accept();                
           
        }
        public void Deactivate(long duration) // throws IOException
        {
            //Not implemented
        }

		#endregion Methods
    }
}
