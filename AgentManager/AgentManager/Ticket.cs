using System;

namespace MobileAgent.AgentManager
{
    public class Ticket 
    {
		#region Fields
		private String _file;
		private int _port;
		private readonly static int DEFAULT_HTTP_PORTNO = 80;
		public readonly static String DEFAULTSCHEME = "http";
		public readonly static String HTTP = "http";
		public readonly static String RMI = "rmi";
		#endegion Fields
		
		#region Constructors
		public Ticket(String host)//       throws java.net.MalformedURLException
		{
			
		}
		public Ticket(String destination)//, QoC qoc)   throws java.net.MalformedURLException
		{
			
		}
		public Ticket(String host,  int portNo)//      throws java.net.MalformedURLException
		{
			
		}
		public Ticket(java.net.URL address)
		{
			
		}
		#endegion Constructors
		
		#region Methods
		public static int GetDefaultPort()
		{
			return DEFAULT_HTTP_PORTNO;
		}
		public Uri GetDestination()
		{
			return null;
		}
		public Uri GetDestination()
		{
			return null;
		}
		public String GetFile()
		{
			return _file;
		}
		public String GetHost()
		{
			return "";
		}
		public int GetPort()
		{
			return _port;
		}
		public boolean IsDefaultPort()
		{
			return false;
		}
		private void Set(String address)// throws java.net.MalformedURLException
		{
			
		}
		private void Set(String address, String scheme, int portNo)// throws java.net.MalformedURLException
		{
			
		}
		private void Set(java.net.URL address)
		{
			
		}
		public static void SetDefaultPort(int portNo)
		{
			
		}
		public void SetDestination(String url)// throws java.net.MalformedURLException
		{
			
		}
		public void SetDestination(String address, String scheme, int portNo)// throws java.net.MalformedURLException
		{
			
		}
		public void SetDestination(Uri destination)
		{
			
		}
		public String ToString()
		{
			return "";
		}
		#endregion Methods
		
    }
}
