using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.EventAgent
{
    public class AgentEventListener : CloneListener, MobilityListener, PersistencyListener
    {
        #region Fields
        ArrayList _vectorEvents = new ArrayList();
        #endregion Fields

        #region Constructors
        public AgentEventListener()
        {
        }
        public AgentEventListener(CloneListener l1, CloneListener l2)
        {
            _vectorEvents.Add(l1);
            _vectorEvents.Add(l2);
        }
        public AgentEventListener(MobilityListener l1, MobilityListener l2)
        {
            _vectorEvents.Add(l1);
            _vectorEvents.Add(l2);
        }
        public AgentEventListener(PersistencyListener l1, PersistencyListener l2)
        {
            _vectorEvents.Add(l1);
            _vectorEvents.Add(l2);
        }
        #endregion Constructors

        #region Methods
        public void AddCloneListener(CloneListener listener)
        {
            if(_vectorEvents.Contains(listener))
            {
                return;
            }
            _vectorEvents.Add(listener);
        }
        public void AddMobilityListener(MobilityListener listener)
        {
            if (_vectorEvents.Contains(listener))
            {
                return;
            }
            _vectorEvents.Add(listener);
        }
        public void AddPersistencyListener(PersistencyListener listener)
        {
            if (_vectorEvents.Contains(listener))
            {
                return;
            }
            _vectorEvents.Add(listener);
        }
        public void OnClone(CloneEvent ev)
        {
            IEnumerator e = _vectorEvents.GetEnumerator();
            while(e.MoveNext())
            {
                ((CloneListener)e.Current).OnClone(ev);
            }
        }
        public void OnCloned(CloneEvent ev)
        {
            IEnumerator e = _vectorEvents.GetEnumerator();
            while (e.MoveNext())
            {
                ((CloneListener)e.Current).OnCloned(ev);
            }
        }
        public void OnCloning(CloneEvent ev)
        {
            IEnumerator e = _vectorEvents.GetEnumerator();
            while (e.MoveNext())
            {
                ((CloneListener)e.Current).OnCloning(ev);
            }
        }
        public void OnArrival(MobilityEvent ev)
        {
            IEnumerator e = _vectorEvents.GetEnumerator();
            while (e.MoveNext())
            {
                ((MobilityListener)e.Current).OnArrival(ev);
            }
        }
        public void OnDispatching(MobilityEvent ev)
        {
            IEnumerator e = _vectorEvents.GetEnumerator();
            while (e.MoveNext())
            {
                ((MobilityListener)e.Current).OnDispatching(ev);
            }
        }
        public void OnReverting(MobilityEvent ev)
        {
            IEnumerator e = _vectorEvents.GetEnumerator();
            while (e.MoveNext())
            {
                ((MobilityListener)e.Current).OnReverting(ev);
            }
        }
        public void OnDeactivating(PersistencyEvent ev)
        {
            IEnumerator e = _vectorEvents.GetEnumerator();
            while (e.MoveNext())
            {
                ((PersistencyListener)e.Current).OnDeactivating(ev);
            }
        }
        public void OnActivation(PersistencyEvent ev)
        {
            IEnumerator e = _vectorEvents.GetEnumerator();
            while (e.MoveNext())
            {
                ((PersistencyListener)e.Current).OnActivation(ev);
            }
        }
        public void RemoveCloneListener(CloneListener listener)
        {
            _vectorEvents.Remove(listener);
        }
        public void RemoveMobilityListener(MobilityListener listener)
        {
            _vectorEvents.Remove(listener);
        }
        public void RemovePersistencyListener(PersistencyListener listener)
        {
            _vectorEvents.Remove(listener);
        }
        public int Size()
        {
            return _vectorEvents.Count;
        }
        #endregion Methods
    }
}
