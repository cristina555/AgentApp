using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using System.Net;

namespace AgentApp.AditionalClasses
{
    public class ConfigParser
    {
        #region Field
        List<IPEndPoint> _hostsList;
        #endregion Field

        #region Methods
        public Dictionary<IPAddress, Tuple<string, int, string[]>> GetNetworkHosts()
        {
            Dictionary<IPAddress, Tuple<string, int, string[]>> hosts = new Dictionary<IPAddress, Tuple<string, int, string[]>>();

            var _config = (CustomConfig)ConfigurationManager.GetSection("networkConfig");

            foreach (HostElement instance in _config.Instances)
            {
                IPAddress ip = IPAddress.Parse(instance.Ip);
                string[] n = instance.Neighbours.Split(' ');
                int port = Convert.ToInt16(instance.Port);
                hosts.Add(ip, Tuple.Create(instance.Name, port, n));
            }
            return hosts;
        }
    #endregion Methods
}
}
