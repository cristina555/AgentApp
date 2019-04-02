using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.AgentManager
{
    public class Message : MessageManager
    {
		#region Fields
		protected Object arg;
		protected String kind;
		#endregion Fields
		
		#region Constructors
		public Message(String kind)
		{
			
		}
		public Message(String kind, char c)
		{
			
		}
		public Message(String kind, double d)
		{
			
		}
		public Message(String kind, Object arg)
		{
			
		}
		#endregion Constructors
		
		#region Methods
		public bool Equals(Object obj)
		{
			return false;
		}
		public Object GetArg()
		{
			return null;
		}
		public Object GetArg(String name)
		{
			return null;
		}
		public String GetKind()
		{
			
		}
		public int GetMessageType()
		{
			return 0;
		}
		public long GetTimeStamp()
		{
			
		}
		public bool SameKind(Message m)
		{
			
		}
		public bool SameKind(String k)
		{
			
		}
		public void SendReply()
		{
			
		}
		public void SendReply(char c)
		{
			
		}
		public void SendReply(Object arg)
		{
			
		}
		public void SetArg(String name, char value)
		{
			
		}
		public void SetArg(String name, Object a)
		{
			
		}
		public String ToString()
		{
			
		}
		#region Methods
    }
	
	
}
