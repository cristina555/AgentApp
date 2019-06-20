using System;
using MobileAgent.EventAgent;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MobileAgent.AgentManager
{
    [Serializable]
    abstract public class Agent : AgentProxy
    {
        #region Public Fields
        public readonly static int ACTIVE = 1;
        public readonly static int INACTIVE = 0;
        public readonly static int REMOTE = 2;
        public readonly static int LOCAL = 3;
        public readonly static int OK = 0;
        public readonly static int NOK = 1;
        public readonly static int STATIC = 0;
        public readonly static int MOBILE = 1;
        public readonly static int READY = 0;
        public readonly static int DONE = 1;
        public readonly static int BOOMERANG = 0;
        public readonly static int WALKER = 1;

        //public delegate void dgEventRaiser();
        //public event dgEventRaiser UpdateAgency;
        #endregion Public Fields

        #region Private Fields
        private string _name;
        private int _status = NOK;
        private int _workStatus = DONE;
        private int _state = INACTIVE;
        private int _mobility;
        private int _type;
        private int _id;
        private string _agentStateInfo;
        private string _creationTime;
        private IPEndPoint _agencyCreationContext;
        private string _agentInfo;
        private AgencyContext _currentContext;
        private List<AgentProxy> _cloneList = null;
        private DateTime _lifetime;
        #endregion Private Fields

        #region Constructors
        public Agent()
        {
            _cloneList = new List<AgentProxy>();
            SetCreationTime();
            _state = ACTIVE;
        }
        public Agent(int id)
        {
            _id = id;
            _cloneList = new List<AgentProxy>();
            SetCreationTime();
            _state = ACTIVE;
        }
        #endregion Constructors

        #region Properties
        public string GetName()
        {
            return _name;
        }
        public string GetCreationTime()
        {
            return _creationTime;
        }
        public IPEndPoint GetAgencyCreationContext()
        {
            return _agencyCreationContext;
        }
        public AgencyContext GetAgentCurrentContext()
        {
            return _currentContext;
        }
		public int GetAgentId()
		{
             return _id;
  		}
		public String GetAgentStateInfo()
		{  
             return _agentStateInfo;
        }
        public String GetAgentInfo()
        {
            return _agentInfo;
        }
        public List<AgentProxy> GetCloneList()
        {
            return _cloneList;
        }
        public AgentProxy GetClone(int id)
        {
           foreach(AgentProxy ap in _cloneList)
           {
                if(ap.GetAgentId() == id )
                {
                    return ap;
                }
           }
            return null;
        }
        public void SetAgentId(int id)
        {
            _id = id;
        }
        public void SetAgencyCreationContext(IPEndPoint context)
        {
            _agencyCreationContext = context;
        }
        public void SetCreationTime()
        {
            _creationTime = DateTime.Now.ToString();
        }
        public void SetAgentStateInfo(string agentStateInfo)
        {
            _agentStateInfo = agentStateInfo;
        }
        public void SetAgentInfo(string info)
        {
            _agentInfo = info;
        }
        public void SetAgentCurrentContext(AgencyContext currentContext)
        {
            _currentContext = currentContext;
        }
        public void SetName(string name)
        {
            _name = name;
        }
        public void SetStatus(int status)
        {
            _status = status;
        }
        public void SetWorkStatus(int status)
        {
            _workStatus = status;
        }
        public void SetMobility(int mobility)
        {
            _mobility = mobility;
        }
        public void SetType(int type)
        {
            _type = type;
        }
        public void SetClone(AgentProxy ap)
        {
            _cloneList.Add(ap);
        }
        #endregion Properties

        #region Methods
        public void Clone()
        {
            //Not implemented yet
        }
        public bool Dispatch(IPEndPoint destination)
        {
            try
            {
                AgencyContext agencyContext = GetAgentCurrentContext();

                IFormatter formatter = new BinaryFormatter();
                NetworkStream networkStream = null;

                Socket connectSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    connectSocket.Connect(destination);
                }
                catch (Exception)
                {

                }
                if (connectSocket.Connected)
                {
                    
                    agencyContext.RemoveAgent(this);
                    networkStream = new NetworkStream(connectSocket);
                    SetAgentCurrentContext(null);
                    formatter.Serialize(networkStream, this);
                    return true;

                }
                else
                {
                    UnconnectedAgencyArgs args = new UnconnectedAgencyArgs();
                    args.Date = DateTime.Now;
                    args.Name = destination;
                    agencyContext.OnRefuseConnection(args);
                    Console.WriteLine("Refuse connection");

                }
                return false;
            }
            catch (NullReferenceException nfe)
            {
                Console.WriteLine("NullReferenceException caught! Mesaj : " + nfe.Message + " " + nfe.StackTrace + " --> Agency Dispach Agent.");
            }
            catch (SocketException io)
            {
                Console.WriteLine("SocketException caught! Mesaj : " + io.Message + io.StackTrace + " --> Agency Dispach Agent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught! Mesaj : " + ex.Message + " --> Agency Dispach Agent.");
            }
            return false;
        }
        public void RetractAgent()
        {
            try
            {
                AgencyContext agencyContext = GetAgentCurrentContext();


                NetworkStream networkStream;
                Socket connectSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    connectSocket.Connect(GetAgencyCreationContext());
                }
                catch (Exception)
                {

                }
                if (connectSocket.Connected)
                {
                    agencyContext.RemoveAgent(this);
                    SetAgentCurrentContext(null);
                    networkStream = new NetworkStream(connectSocket);
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(networkStream, this);
                }
                else
                {
                    UnconnectedAgencyArgs args = new UnconnectedAgencyArgs();
                    args.Date = DateTime.Now;
                    args.Name = GetAgencyCreationContext();
                }
            }
            catch (NullReferenceException nfe)
            {
                Console.WriteLine("NullReferenceException caught! Mesaj : " + nfe.Message + " " + nfe.StackTrace + " --> Agency Dispach Agent.");
            }
            catch (SocketException io)
            {
                Console.WriteLine("SocketException caught! Mesaj : " + io.Message + io.StackTrace + " --> Agency Dispach Agent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught! Mesaj : " + ex.Message + " --> Agency Dispach Agent.");
            }
            //UpdateAgency();
        }
        public bool IsActive()
        {
            return _state == ACTIVE;
        }
        public bool IsRemote()
        {
            return _state == REMOTE;
        }
        public bool IsReady()
        {
            return _workStatus == READY;
        }
        public bool IsMobile()
        {
            return _mobility == MOBILE;
        }
        public bool IsBoomerang()
        {
            return _type == BOOMERANG;
        }
        public bool IsStatusOK()
        {
            return _status == OK;
        }
        abstract public void Run();
        abstract public void GetUI();
        #endregion Methods

    }
}
