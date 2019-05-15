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
        Dictionary<IPAddress, bool> _hostsList;
        #endregion Field
        #region Constructor
        public ConfigParser()
        {
            _hostsList = new Dictionary<IPAddress, bool>();
        }
        #endregion Constructor

        #region Methods
        private void CreateHostsList()
        {
            NameValueCollection ipAdresses;
            ipAdresses = ConfigurationManager.AppSettings;
            foreach (string ipAdress in ipAdresses.AllKeys)
            {
                _hostsList.Add(IPAddress.Parse(ipAdress), Convert.ToBoolean(ipAdresses.Get(ipAdress)));
            }
        }
        private void SetSettings(IPAddress key, string value)
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
        }
        public Dictionary<IPAddress, bool> GetHosts()
        {
            CreateHostsList();
            return _hostsList;
        }
        public void UpdateHosts(IPAddress key, string value)
        {
            SetSettings(key, value);
        }
        public void Reset(IPAddress key)
        {
            ResetSettings(key);
        }
        #endregion Methods
    }
}
