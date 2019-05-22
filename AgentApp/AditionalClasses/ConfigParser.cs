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

        #region Constructor
        public ConfigParser()
        {
            _hostsList = new List<IPEndPoint>();
        }
        #endregion Constructor

        #region Methods
        private void CreateHostsList()
        {
            NameValueCollection ipAddresses;
            ipAddresses = ConfigurationManager.AppSettings;
            foreach (string ipAddress in ipAddresses.AllKeys)
            {
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), Convert.ToInt16(ipAddresses.Get(ipAddress)));
                _hostsList.Add(ipEndPoint);
            }
        }
        /*private void SetSettings(IPAddress key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key.ToString()].Value = value;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void ResetSettings(IPAddress key)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key.ToString()].Value = "false";
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }*/
        public List<IPEndPoint> GetHosts()
        {
            CreateHostsList();
            return _hostsList;
        }
        public void DeleteHost(IPEndPoint host)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings.Remove(host.Address.ToString());
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }
        /*public void UpdateHosts(IPAddress key, string value)
        {
            SetSettings(key, value);
        }
        public void Reset(IPAddress key)
        {
            ResetSettings(key);
        }*/
        public List<Tuple<String, IPEndPoint, string[]>> GetNetworkHosts()
        {
            List<Tuple<string, IPEndPoint, string[]>> hosts = null;

            var _config = (CustomConfig)ConfigurationManager.GetSection("networkConfig");

            foreach (HostElement instance in _config.Instances)
            {
                
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse(instance.Ip), Convert.ToInt16(instance.Port));
                string[] n = instance.Neighbours.Split(' ');
                hosts.Add(Tuple.Create(instance.Name, ip, n));
            }
            return hosts;
        }
    #endregion Methods
}
}
