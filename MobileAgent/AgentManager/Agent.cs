using System;
using MobileAgent.EventAgent;
using System.Net.Sockets;
using System.Net;

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
        #endregion Public Fields

        #region Private Fields
        private string _name;
        private int _status = NOK;
        private int _state = INACTIVE;
        private int _mobility;
        private int _id;
        private string _codebase;
        private string _creationTime;
        private IPEndPoint _agencyCreationContext;
        private string _agentInfo;
        private AgencyContext _currentContext;
        #endregion Private Fields

        #region Constructors
        public Agent()
        {

        }
        public Agent(int id)
        {
            _id = id;
            SetCreationTime();
            _state = ACTIVE;
        }
        #endregion Constructors

        #region Properties
        public string GetName()
        {
            return _name;
        }
        public int GetStatus()
        {
            return _status;
        }
        public int GetMobility()
        {
            return _mobility;
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
		public String GetAgentCodebase()
		{  
             return _codebase;
        }
        public String GetAgentInfo()
        {
            return _agentInfo;
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
        public void SetAgentCodebase(string codebase)
        {
            _codebase = codebase;
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
        public void SetMobility(int mobility)
        {
            _mobility = mobility;
        }
        #endregion Properties

        #region Methods
        public void OnCreation(Object init)
		{
            //Not implemented
            throw new Exception("Aceasta metoda trebuie completata");
        }
        public void OnDisposing()
		{
            //Not implemented
            throw new Exception("Aceasta metoda trebuie completata");
        }        
        public bool IsActive()
        {
            //Not implemented
            return _state == ACTIVE;
        }
        public bool IsRemote()
        {
            return _state == REMOTE;
        }
        public bool IsMobile()
        {
            return _mobility == MOBILE;
        }
        public void Suspend()
        {
            //Not implemented
            throw new Exception("Aceasta metoda trebuie completata");
        }
        abstract public void Run();
        #endregion Methods

    }
}
