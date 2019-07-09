using System;
using MobileAgent.EventAgent;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using MobileAgent.Exceptions;

namespace MobileAgent.AgentManager
{
    [Serializable]
    abstract public class Agent : IMobile, IStationary
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
        public readonly static int ONEWAY = 2;
        public readonly static int MASTER = 3;
        public readonly static int SLAVE = 4;
        public readonly static int LIFETIME = 3600; //seconds
        #endregion Public Fields

        #region Private Fields
        private string _name;
        private int _status = NOK;
        private int _workStatus = DONE;
        private int _state = INACTIVE;
        private int _mobility;
        private int _type;
        private int _workType;
        private int _id;
        private string _agentStateInfo;
        private string _creationTime;
        private IPEndPoint _agencyCreationContext;
        private string _agentInfo;
        private IAgencyContext _currentContext;
        private List<IMobile> _cloneList = new List<IMobile>();
        private int _lifetime = LIFETIME;
        private IMobile _parent = null;
        #endregion Private Fields

        #region Private Static Fields
        private static System.Timers.Timer _timer;
        #endregion #region Private Static Fields

        #region Constructors
        public Agent()
        {
            SetCreationTime();
            _state = ACTIVE;
           SetLifetime();
        }
        public Agent(int id)
        {
            _id = id;
            SetCreationTime();
            _state = ACTIVE;
            SetLifetime();
        }
        public IMobile Clone()
        {
            return (IMobile)this.MemberwiseClone();
        }
        #endregion Constructors

        #region Property Methods
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
        public IAgencyContext GetAgentCurrentContext()
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
        public int GetAgentType()
        {
            return _type;
        }
        public int GetWorkType()
        {
            return _workType;
        }
        public IMobile GetParent()
        {
            return _parent;
        }
        public List<IMobile> GetCloneList()
        {
            return _cloneList;
        }
        public IMobile GetClone(int id)
        {
           foreach(IMobile ap in _cloneList)
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
        
        public void SetAgentCurrentContext(IAgencyContext currentContext)
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
        public void SetWorkType(int status)
        {
            _workType = status;
        }
        public void SetMobility(int mobility)
        {
            _mobility = mobility;
        }
        public void SetType(int type)
        {
            _type = type;
        }
        public void SetClone(IMobile ap)
        {
            _cloneList.Add(ap);
        }
        public void SetParent(IMobile ap)
        {
            _parent = ap;
        }
        public void ResetLifetime()
        {
            _lifetime = LIFETIME;
        }
        public void SetState( int state)
        {
            _state = state;
        }
        public int GetLifetime()
        {
            return _lifetime;
        }
        #endregion Property Methods

        #region Private Methods
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            _lifetime -= 1;
            if(_lifetime == 60)
            {
                PersistencyEventArgs args = new PersistencyEventArgs();
                args.Source = "Agentul " + GetName();
                args.Information = "Mai are " + _lifetime + " secunde până să fie dezactivat";
                GetAgentCurrentContext().OnDeactivating(args);
            }

            if (_lifetime == 0)
            {
                this.SetState(INACTIVE);
                _timer.Stop();
                _timer.Dispose();
            }
        }
        #endregion Private Methods
        
        #region Public Methods
        public void SetLifetime()
        {
            Console.WriteLine("Lifetime-ul  a fost setat!");
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnTimedEvent;
            _timer.Enabled = true;
        }
        
        public void RetractAgent()
        {
            try
            {
                IAgencyContext agencyContext = GetAgentCurrentContext();


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
        public bool IsMaster()
        {
            return _workType == MASTER;
        }
        #endregion Public Methods

        #region Abstract Public Methods
        abstract public void Run();
        abstract public void GetUI();
        abstract public String GetInfo();
        #endregion Abstract Public Methods

    }
}
