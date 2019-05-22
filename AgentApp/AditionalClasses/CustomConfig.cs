using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentApp.AditionalClasses
{
    public class CustomConfig : ConfigurationSection
    {
        [ConfigurationProperty("instances")]
        [ConfigurationCollection(typeof(NetworkCollection))]
        public NetworkCollection Instances
        {
            get
            {
                // Get the collection and parse it
                return (NetworkCollection)this["instances"];
            }
        }
    }
}
