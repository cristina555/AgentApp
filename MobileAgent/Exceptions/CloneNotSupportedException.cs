using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.Exceptions
{
    public class CloneNotSupportedException : Exception
    {
        #region Constructors
        public CloneNotSupportedException() : base()
        {

        }
        public CloneNotSupportedException(String message) : base(message)        
        {

        }
        #endregion Constructors
    }
}
