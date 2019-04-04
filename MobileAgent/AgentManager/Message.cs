using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.AgentManager
{
    public class Message 
    {
        #region Fields
        static readonly public String CLONE = "clone";
        static readonly public String DISPATCH = "dispatch";
        static readonly public String DISPOSE = "dispose";
        static readonly public String DEACTIVATE = "deactivate";
        static readonly public String REVERT = "revert";
        protected Object _arguments;
        protected String _kind;
        #endregion Fields

        #region Constructors
        public Message(String kind)
        {
            _kind = kind;
            _arguments = new Object();
        }
        public Message(String kind, char c)
        {
            _kind = kind;
            _arguments = new Char();
        }
        public Message(String kind, double d)
        {
            _kind = kind;
            _arguments = new Double();
        }
        public Message(String kind, Object arg)
        {
            _kind = kind;
            _arguments = arg;
        }
        #endregion Constructors

        #region Methods
        /*public void Destroy()
        {

        }
        public void ExitMonitor()
        {

        }
        public void NotifyAllMessages()
        {

        }
        public void NotifyMessage()
        {

        }
        public void SetPriority(String kind, int priority)
        {

        }

        public override bool Equals(Object obj)
        {
            if ( ((Message)obj).SameKind(_kind))
            {
                Object arg2 = ((Message)obj)._arguments;

                if (arg2 == _arguments || (_arguments != null && _arguments.Equals(arg2)))
                {
                    return true;
                }
            }
            return false;
        }*/
        public Object GetArg()
        {
            return _arguments;
        }
        public String GetKind()
        {
            return _kind;
        }
        public bool SameKind(Message m)
        {
            return (m != null && _kind.Equals(m._kind));
        }
        public bool SameKind(String k)
        {
            return _kind.Equals(k);
        }
        public void SendReply()
        {
            //Not implemeted
        }
        public void SendReply(char c)
        {
            //Not implemeted
        }
        public void SendReply(double c)
        {
            //Not implemeted
        }
        public void SendReply(Object arg)
        {
            //Not implemeted
        }
        public void SetArg(String name, char value)
        {
            //Not implemeted
        }
        public void SetArg(String name, Object a)
        {
            //Not implemeted
        }
        public override String ToString()
        {
            return "[kind = " + _kind + ": arg = " + _arguments + ']';
        }
        #endregion Methods
    }

}
