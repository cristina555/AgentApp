using System;
using MobileAgent.EventAgent;

namespace MobileAgent.AgentManager
{
    public class Agent : AgentProxy
    {
		#region Fields
		public readonly static short MAJOR_VERSION = 1;
		public readonly static short MINOR_VERSION = 0;
		public readonly static int ACTIVE = 1;
		public readonly static int INACTIVE = 0;
        private int _id;
        private string _codebase;
		#endregion Fields
		
		#region Constructors
        protected Agent()
        {

        }
        #endregion Constructors

        #region Methods
        public void DelegateMessage(Message msg)//throws InvalidAgletException
        {
            //Not implemented
        }
        public AgentContext GetAgletContext()
		{
            //Not implemented
            AgentContext a = null;
            return a;
        }
		public int GetAgentId()
		{
			return _id;
		}
		public String GetAgentCodebase()
		{
			return _codebase;
		}
		public MessageManager GetMessageManager()
		{
            //Not implemented
            MessageManager m = null;
            return m;
        }
		public AgentProxy GetProxy()
		{
            //Not implemented
            AgentProxy a = null;
            return a;

        }
		public String GetText()
		{
            //Not implemented
            return "";
		}
        public String GetAgentInfo()
        {
            //Not implemented
            return "";
        }
        public bool HandleMessage(Message message)
		{
            //Not implemented
            return false;
		}
		public void NotifyAllMessages()
		{
            //Not implemented
        }
        public void NotifyMessage()
		{
            //Not implemented
        }
        public void OnCreation(Object init)
		{
            //Not implemented
        }
        public void OnDisposing()
		{
            //Not implemented
        }
		public void Run()
        {
            //Not implemented
        }
        public bool IsActive()
        {
            //Not implemented
            return false;
        }
        public bool IsRemote()
        {
            //Not implemented
            return false;
        }
		public bool IsState(int type)
        {
            //Not implemented
            return false;
        }
        public bool IsValid()
        {
            //Not implemented
            return false;
        }
		public void SetAgentId(int id)
		{
			_id = id;
		}
		public void SetAgentCodebase(String codebase)
		{
			_codebase = codebase;
		}
		public void SetText(String text)
		{
            //Not implemented
        }
        public Object SendMessage(Message msg)
        {
            //Not implemented
            return null;
        }
        public void Suspend()
        {
            //Not implemented
        }
        #endregion Methods

    }
}
