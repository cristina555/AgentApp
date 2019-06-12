using System.Configuration;

namespace AgentApp.AditionalClasses
{
    public class HostElement : ConfigurationElement
    {
        #region Properties
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }
        [ConfigurationProperty("ip", IsRequired = true)]
        public string Ip
        {
            get { return (string)base["ip"]; }
            set { base["ip"] = value; }
        }
        [ConfigurationProperty("port", IsRequired = true)]
        public string Port
        {
            get { return (string)base["port"]; }
            set { base["port"] = value; }
        }
        [ConfigurationProperty("neighbours", IsRequired = true)]
        public string Neighbours
        {
            get { return (string)base["neighbours"]; }
            set { base["neighbours"] = value; }
        }
        #endregion Properties
    }
}
