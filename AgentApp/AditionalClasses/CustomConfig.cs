using System.Configuration;

namespace AgentApp.AditionalClasses
{
    public class CustomConfig : ConfigurationSection
    {
        #region Properties
        [ConfigurationProperty("instances")]
        [ConfigurationCollection(typeof(NetworkCollection))]
        public NetworkCollection Instances
        {
            get
            {
                return (NetworkCollection)this["instances"];
            }
        }
        [ConfigurationProperty("instances2")]
        [ConfigurationCollection(typeof(NetworkCollection))]
        public NetworkCollection Instances2
        {
            get
            {
                return (NetworkCollection)this["instances2"];
            }
        }
        [ConfigurationProperty("instances3")]
        [ConfigurationCollection(typeof(NetworkCollection))]
        public NetworkCollection Instances3
        {
            get
            {
                return (NetworkCollection)this["instances3"];
            }
        }
        #endregion Properties
    }
}
