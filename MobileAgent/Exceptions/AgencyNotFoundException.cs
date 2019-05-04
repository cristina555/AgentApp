using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.Exceptions
{
    public class AgencyNotFoundException : Exception
    {
        #region Constructors
        public AgencyNotFoundException()
        {

        }
        public AgencyNotFoundException(String m) : base(m)
        {
        }
        #endregion Constructors
    }
}
