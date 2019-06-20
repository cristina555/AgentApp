using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;

namespace AgentApp.AditionalClasses
{
    public class ConfigParser
    {

        #region Constructor
        public ConfigParser()
        {
            NetworkHosts = new Dictionary<IPAddress, Tuple<string, int, string[]>>();
            FormNetworkHosts();
        }
        #endregion Constructor

        #region Properties
        public Dictionary<IPAddress, Tuple<string, int, string[]>> NetworkHosts { get; } = new Dictionary<IPAddress, Tuple<string, int, string[]>>();
        #endregion Properties

        #region Private Methods
        private void FormNetworkHosts()
        {
            var _config = (CustomConfig)ConfigurationManager.GetSection("networkConfig");

            foreach (HostElement instance in _config.Instances)
            {
                IPAddress ip = IPAddress.Parse(instance.Ip);
                string[] n = instance.Neighbours.Split(' ');
                int port = Convert.ToInt16(instance.Port);
                NetworkHosts.Add(ip, Tuple.Create(instance.Name, port, n));
            }
        }
        #endregion Private Methods

        #region Public Methods
        public List<string> GetNeighbours(string name)
        {
            foreach (Tuple<string, int, string[]> t in NetworkHosts.Values)
            {
                if(t.Item1.Equals(name))
                {
                    return new List<string>(t.Item3);
                }
            }
            return null;
        }
        public IPAddress GetIPAdress(string name)
        {
            foreach(IPAddress ip in NetworkHosts.Keys)
            {
                if(NetworkHosts[ip].Item1.Equals(name))
                {
                    return ip;
                }
            }
            return null;
        }
        public int GetPort(string name)
        {
            foreach (Tuple<string, int, string[]> t in NetworkHosts.Values)
            {
                if (t.Item1.Equals(name))
                {
                    return t.Item2;
                }
            }
            return 0;
        }
        public string GetName(IPEndPoint ipEndPoint)
        {
            foreach(IPAddress ipAddress in NetworkHosts.Keys)
            {
                if(ipAddress == ipEndPoint.Address && NetworkHosts[ipAddress].Item2 == ipEndPoint.Port )
                {
                    return NetworkHosts[ipAddress].Item1;
                }
            }
            return null;
        }
        #endregion Public Methods
    }
}
