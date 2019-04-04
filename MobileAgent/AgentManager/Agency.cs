using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MobileAgent.EventAgent;
using MobileAgent.AdditionalClasses;

namespace MobileAgent.AgentManager
{
    public class Agency : AgentContext
    {
        #region Fields
        private CloneListener cloneListener;
        private MobilityListener mobilityListener;
        private PersistencyListener persistencyListener;
        #endregion Fields

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
		public void ClearCache(URL codebase)
		{
            //Not implemented
        }
        public  AgentProxy CreateAgent(URL codebase, String code, Object init)
        {
            //Not implemented
            AgentProxy a =null;
            return a;
        }
        public AgentProxy Clone(AgentProxy agent) // throws CloneNotSupportedException
        {
            //Not implemented
            AgentProxy agentCloned= null;
            return agentCloned;
        }
		public AgentProxy Dispatch(AgentProxy agentProxy, Ticket ticket)// throws IOException, AgletException
		{
            //Not implemented
            AgentProxy agentDispatched = null;
            return agentDispatched;
		}
		public AgentProxy Dispatch(URL destination)
		{
            //Not implemented
            AgentProxy agentDispatched = null;
            return agentDispatched;
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
            //Not implemented
            List<AgentProxy> Agent_List = new List<AgentProxy>();
            return Agent_List;

        }
        public AgentProxy GetAgentProxy(int id)
        {
            //Not implemented
            AgentProxy a = null;
            return a;
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
        [[MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveCloneListener(CloneListener l)
        {
            if (cloneListener == l)
            {
                cloneListener = null;
            }
            else if (cloneListener.GetType().IsInstanceOfType(typeof(AgentEventListener)) {
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
            else if (mobilityListener.GetType().IsInstanceOfType(typeof(AgentEventListener)) {
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
            else if (persistencyListener.GetType().IsInstanceOfType(typeof(AgentEventListener)) {
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
            //Not implemented
        }
        public void Start()
        {
            //Not implemented
        }
        public void Activate()
        {
            //Not implemented
        }
        public void Deactivate(long duration) // throws IOException
        {
            //Not implemented
        }
        public void ExitMonitor()
		{
			//Not implemented
		}
		#endregion Methods
    }
}
