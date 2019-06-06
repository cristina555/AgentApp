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
        public Dictionary<IPAddress, Tuple<string, int, string[]>> NetworkHosts { get; } = null;
        #endregion Properties

        #region Methods
        private void FormNetworkHosts()
        {
            Dictionary<IPAddress, Tuple<string, int, string[]>> hosts = new Dictionary<IPAddress, Tuple<string, int, string[]>>();

            var _config = (CustomConfig)ConfigurationManager.GetSection("networkConfig");

            foreach (HostElement instance in _config.Instances)
            {
                IPAddress ip = IPAddress.Parse(instance.Ip);
                string[] n = instance.Neighbours.Split(' ');
                int port = Convert.ToInt16(instance.Port);
                NetworkHosts.Add(ip, Tuple.Create(instance.Name, port, n));
            }
        }
        #endregion Methods
    }
}
