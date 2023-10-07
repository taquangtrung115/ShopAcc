using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HRM.Infrastructure.Utilities
{
    public class ApiControllerBase : ApiController
    {
        #region GetHeaderValue

        public string GetHeaderValue(this HttpHeaders headers, string headerName)
        {
            string result = string.Empty;

            if (headers != null && headers.Any(p => String.Equals(p.Key, headerName, StringComparison.OrdinalIgnoreCase)))
            {
                result = headers.GetValues(headerName).FirstOrDefault();
            }

            return result;
        }

        public string GetHeaderValue(this NameValueCollection headers, string headerName)
        {
            string result = string.Empty;

            if (headers != null && headers.AllKeys != null && headers.AllKeys.Any(p => String.Equals(p, headerName, StringComparison.OrdinalIgnoreCase)))
            {
                result = headers.GetValues(headerName).FirstOrDefault();
            }

            return result;
        }

        #endregion
        private string _userLogin;
        private Guid? _userID;
        private Guid? _profileID;
        private bool? _isSupperAdmin;
        protected virtual string LanguageCode
        {
            get
            {
                return Request.Headers.GetHeaderValue(HeaderObject.LanguageCode);
            }
        }

        protected virtual string UserLogin
        {
            get
            {
                if (!String.IsNullOrEmpty(this._userLogin))
                    return this._userLogin;

                this._userLogin = !Constant.UseRedisServer && AccessData == null
                    ? Request.Headers.GetHeaderValue(HeaderObject.UserLogin)
                    : AccessData?.UserName;

                return this._userLogin;
            }
        }

        protected virtual Guid UserID
        {
            get
            {
                if (this._userID.HasValue)
                    return this._userID.Value;

                this._userID = 
                    Request.Headers.GetHeaderValue(HeaderObject.UserID).TryGetValue<Guid>()
                    : (AccessData?.UserID as Guid?) ?? Guid.Empty;

                return this._userID.Value;
            }
        }

        protected virtual bool IsSupperAdmin
        {
            get
            {
                if (this._isSupperAdmin.HasValue)
                    return this._isSupperAdmin.Value;

                this._isSupperAdmin = !Constant.UseRedisServer && AccessData == null
                    ? Request.Headers.GetHeaderValue(HeaderObject.IsSupperAdmin).TryGetValue<bool>()
                    : (AccessData?.UserResource?.IsSupperAdmin as bool?) ?? false;

                return this._isSupperAdmin.Value;
            }
        }
    }
}
