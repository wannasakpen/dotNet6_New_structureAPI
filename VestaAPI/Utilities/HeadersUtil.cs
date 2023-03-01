using VestaAPI.Extensions;
using VestaAPI.Utilities.IHeadersUtil;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
 

namespace VestaAPI.Utilities
{
    public class HeadersUtil : IHeadersUtils
    {
        private readonly IHttpContextAccessor _httpContextAccessor; 
        public string RoleCode { get; set; }
        public string ControllerName { get; set; }


        public HeadersUtil(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        
        public Guid? GetUserID()
        {
            Guid? CurrentUserID;

            Guid parsedUserID;
            if (Guid.TryParse(_httpContextAccessor?.HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
            {
                CurrentUserID = parsedUserID;
            }
            else
                CurrentUserID = null;

            return CurrentUserID;
        }
        public string GetRoleCode()
        {
            string RoleCode;
            RoleCode = _httpContextAccessor?.HttpContext?.User?.Claims.Where(x => x.Type == ClaimTypes.Role).Select(o => o.Value).SingleOrDefault();
            this.RoleCode = RoleCode;
            return this.RoleCode;
        } 
        public string GetMethod()
        {
            return _httpContextAccessor.HttpContext.Request.Method.ToString();
            //return this._httpContextAccessor.HttpContext.Request.Headers["Method"].ToString(); ;
        }

        public string SetMethod(string SetMethod)
        {
            _httpContextAccessor.HttpContext.Request.Headers.Add("Method", SetMethod);
            return null;
        }

         

        public string GetQueryString()
        {
            return _httpContextAccessor.HttpContext.Request.QueryString.ToString(); ;
        }

        public string GetBodyRequest()
        {
            var bodyStream = new StreamReader(_httpContextAccessor.HttpContext.Request.Body);
            //bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            string paramRequest = bodyStream.ReadToEnd();
            if (Extension.IsValidJson(paramRequest) && !string.IsNullOrWhiteSpace(paramRequest))
            {
                paramRequest = JValue.Parse(paramRequest).ToString(Formatting.Indented);
            }

            return paramRequest;
        }

        public HttpRequest GetHttpRequest()
        {
            return this._httpContextAccessor.HttpContext.Request;
        }

    }
}
