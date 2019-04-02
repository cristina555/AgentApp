using System;

namespace MobileAgent.AgentManager
{
    public class Agent : AgentProxy
    {
		#region Fields
		public readonly static short MAJOR_VERSION = 1;
		public readonly static short MINOR_VERSION = 0;
		public readonly static int ACTIVE = 1;
		public readonly static int INACTIVE = 0;
		private CloneListener cloneListener;
		private MobilityListener mobilityListener;
		private PersistencyListener persistencyListener;
        private int _id;
        private string _codebase;
		#endregion Fields
		
		#region Constructors
        protected Agent()
        {

        }
		#region Constructors

        public string GetSetCodebase { get; set; }

		#region Methods
		public void AddCloneListener(CloneListener listener)
		{
			
		}
		public void AddMobilityListener(MobilityListener listener)
		{
			
		}
		public void AddPersistencyListener(PersistencyListener listener)
		{
			
		}
        public AgletContext GetAgletContext()
		{
			
		}
		public int GetAgentId()
		{
			return _id;
		}
		public int GetAgentCodebase()
		{
			return _codebase;
		}
		public MessageManager GetMessageManager()
		{
			
		}
		public AgletProxy GetProxy()
		{
			
		}
		public String GetText()
		{
			
		}
		public bool HandleMessage(Message message)
		{
			
		}
		public void NotifyAllMessages()
		{
			
		}
		public void NotifyMessage()
		{
			
		}
		public void OnCreation(Object init)
		{
			
		}
		public void OnDisposing()
		{
			
		}
		protected void ProcessCloneEvent(CloneEvent ev)
		{
			
		}
		protected void ProcessMobilityEvent(MobilityEvent ev)
		{

		}
		protected void ProcessPersistencyEvent(PersistencyEvent ev)
		{
			
		}
		public void RemoveCloneListener(CloneListener l)
		{
			
		}
		public void RemoveMobilityListener(MobilityListener l)
		{
			
		}
		public void RemovePersistencyListener(PersistencyListener l)
		{
			
		}
		public void Run()
        {

        }
        public bool IsActive()
        {
            return false;
        }
        public bool IsRemote()
        {
            return false;
        }
		public bool IsState(int type)
        {
            return false;
        }
        public bool IsValid()
        {
            return false;
        }
		public void SetAgenttId(int id)
		{
			_id = id;
		}
		public void setAgentCodebase(String codebase)
		{
			_codebase = codebase;
		}
		public void SetText(String text)
		{
			
		}
        public Object SendMessage(Message msg)
        {
            return null;
        }
        public void Suspend()
        {

		}     
		#endregion Methods

    }
}
