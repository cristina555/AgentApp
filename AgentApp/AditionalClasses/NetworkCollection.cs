using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentApp.AditionalClasses
{
    public class NetworkCollection : ConfigurationElementCollection
    {
        #region Properties
        public HostElement this[int index]
        {
            get
            {
                 return (HostElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }
        public new HostElement this[string key]
        {
            get
            { 
                return (HostElement)BaseGet(key);
            }
            set
            {
                if (BaseGet(key) != null)
                    BaseRemoveAt(BaseIndexOf(BaseGet(key)));
                BaseAdd(value);
            }
        }
        #endregion Properties

        #region Methods
        protected override ConfigurationElement CreateNewElement()
        {
            return new HostElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((HostElement)element).Name;
        }
        #endregion Methods
    }
}
