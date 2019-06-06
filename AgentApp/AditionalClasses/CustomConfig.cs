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
        #endregion Properties
    }
}
