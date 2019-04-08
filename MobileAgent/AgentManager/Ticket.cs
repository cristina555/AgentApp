using System;
using MobileAgent.AdditionalClasses;

namespace MobileAgent.AgentManager
{
    [Serializable]
    public class Ticket 
    {
        #region Fields
        private String _protocol = null;
        private String _host = null;
        private String _file = null;
        private int _port = -1;
        public readonly static String HTTP = "http";
        public readonly static String DEFAULTSCHEME = HTTP;
		public readonly static String RMI = "rmi";
        private readonly static int DEFAULT_HTTP_PORTNO = 80;
        private static int _defaultPortNo = DEFAULT_HTTP_PORTNO;
        #endregion Fields

        #region Constructors
        public Ticket(String host) //       throws java.net.MalformedURLException
		{
            _host = host;
		}
		public Ticket(String host, String scheme, int portNo)//      throws java.net.MalformedURLException
		{
            Set(host, scheme, portNo);
		}
		public Ticket(URL address)
		{
            Set(address);
		}
		#endregion Constructors
		
		#region Methods
		public static int GetDefaultPort()
		{
			return DEFAULT_HTTP_PORTNO;
		}
		public URL GetDestination()
		{
            URL url = null;
            String file = null;

            if (_file != null && !_file.StartsWith("/"))
            {
                file = "/" + _file;     // It may be a bug of java.net.URL
            }
            else
            {
                file = _file;
            }
            try
            {
                if (IsDefaultPort())
                {
                    url = new URL(_protocol, _host, _file);
                }
                else
                {
                    url = new URL(_protocol, _host, _port, _file);
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
            return url;
        }
        private String GetDestinationString()
        {
            String file = null;

            if (_file != null && !_file.StartsWith("/"))
            {
                file = "/" + _file;
            }
            if (_protocol != null && _protocol.Equals("file", StringComparison.OrdinalIgnoreCase))
            {
                return _protocol + ":" + file;
            }
            else
            {
                if (IsDefaultPort())
                {
                    return _protocol + "://" + _host + file;
                }
                else
                {
                    return _protocol + "://" + _host + ":" + _port + file;
                }
            }
        }
        public String GetProtocol()
        {
            return _protocol;
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
		public bool IsDefaultPort()
		{
            if (_port == -1)
            {
                return true;
            }
            if (_port == _defaultPortNo)
            {
                return true;
            }
            return false;
        }
		private void Set(String address)// throws java.net.MalformedURLException
		{
            SetDestination(address);
        }
		private void Set(String address, String scheme, int portNo)// throws java.net.MalformedURLException
		{
            SetDestination(address, scheme, portNo);
        }
		private void Set(URL address)
		{
            SetDestination(address);
        }
		public static void SetDefaultPort(int portNo)
		{
            _defaultPortNo = portNo;
        }
		public void SetDestination(String url)// throws java.net.MalformedURLException
		{
            Uri destination = new Uri(url);

            if (destination != null)
            {
                _protocol = destination.Scheme;
                _host = destination.Host;
                _file = destination.AbsolutePath;
                _port = destination.Port;
            }
        }
		public void SetDestination(String address, String scheme, int portNo)// throws java.net.MalformedURLException
		{
            String protocol = HTTP;
            URL url = null;

            try
            {
                url = new URL(protocol, address, portNo, "");
            }
            catch (Exception e)
            {
                url = null;
                Console.WriteLine("Exception caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
            SetDestination(url);
        }
		public void SetDestination(URL destination)
		{
            if (destination != null)
            {
                _protocol = destination.GetProtocol().ToLower();
                _host = destination.GetHost();
                _file = destination.GetFile();
                _port = destination.GetPort();
            }
        }
		public override String ToString()
		{
            URL url = GetDestination();
            String destination = null;

            if (url != null)
            {
                destination = url.ToString();
            }
            else
            {
                destination = GetDestinationString();
            }
            String str = destination;

            return str;
        }
		#endregion Methods
		
    }
}
