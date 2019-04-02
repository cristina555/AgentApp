using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAgent.AdditionalClasses
{
    public class URL
    {
        #region Fields
        private String _protocol;
        private String _host;
        private int _port = -1;
        private String _file;
        [NonSerialized]
        private String _query;
        private String _authority;
        [NonSerialized]
        private String _path;
        [NonSerialized]
        private String _userInfo;
        private String _reference;
        #endregion Fields

        #region Constructors
        public URL(String protocol, String host, int port, String file)
        {
            _protocol = protocol;
            _host = host;
            _port = port;
            _file = file;
        }
        public URL(String protocol, String host, String file)
        {
            _protocol = protocol;
            _host = host;
            _port = -1;
            _file = file;
        }
        #endregion Constructors

        #region Methods
        public String GetProtocol()
        {
            return _protocol;
        }
        public String GetAuthority()
        {
            return _authority;
        }
        public String GetPath()
        {
            return _path;
        }
        public String GetQuery()
        {
            return _query;
        }
        public String GetReference()
        {
            return _reference;
        }
        public String GetHost()
        {
            return _host;
        }
        public String GetFile()
        {
            return _file;
        }
        public int GetPort()
        {
            return _port;
        }
        protected String ToExternalForm(URL u)
        {
            int len = u.GetProtocol().Length + 1;
            if (u.GetAuthority() != null && u.GetAuthority().Length > 0)
            {
                len += 2 + u.GetAuthority().Length;
            }
            if (u.GetPath() != null)
            {
                len += u.GetPath().Length;
            }
            if (u.GetQuery() != null)
            {
                len += 1 + u.GetQuery().Length;
            }
            if (u.GetReference() != null)
                len += 1 + u.GetReference().Length;

            StringBuilder result = new StringBuilder(len);
            result.Append(u.GetProtocol());
            result.Append(":");
            if (u.GetAuthority() != null && u.GetAuthority().Length > 0)
            {
                result.Append("//");
                result.Append(u.GetAuthority());
            }
            if (u.GetPath() != null)
            {
                result.Append(u.GetPath());
            }
            if (u.GetQuery() != null)
            {
                result.Append('?');
                result.Append(u.GetQuery());
            }
            if (u.GetReference() != null)
            {
                result.Append("#");
                result.Append(u.GetReference());
            }
            return result.ToString();
        }
        public override String ToString()
        {
            return ToExternalForm(this);
        }
        #endregion Methods
    }
}
