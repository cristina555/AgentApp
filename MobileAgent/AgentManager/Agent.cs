using System;
using MobileAgent.EventAgent;
using System.Net.Sockets;
using System.Net;

namespace MobileAgent.AgentManager
{
    [Serializable]
    abstract public class Agent : AgentProxy
    {
        #region Fields
        public readonly static int ACTIVE = 1;
        public readonly static int INACTIVE = 0;
        public readonly static int REMOTE = 2;
        public int _state;
        private int _id;
        private string _codebase;
        private string _creationTime;
        private int _agencyHostID;
        private string _agentInfo;
        private IPEndPoint _currentContext;
        #endregion Fields

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
        public IPEndPoint GetAgentContext()
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
        public void SetAgencyHost(int id)
        {
            _agencyHostID = id;
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
        public void SetAgentContext(IPEndPoint currentContext)
        {
            _currentContext = currentContext;
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
            //Not implemented
            return false;
            throw new Exception("Aceasta metoda trebuie completata");
        }
        public bool IsValid()
        {
            //Not implemented
            return false;
            throw new Exception("Aceasta metoda trebuie completata");
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
