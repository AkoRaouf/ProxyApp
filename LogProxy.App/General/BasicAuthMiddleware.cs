using LogProxy.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LogProxy.App.General
{
    /// <summary>
    /// Handels the basic authentication operations.
    /// </summary>
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Executes the whole Authentication proccess.
        /// </summary>
        /// <param name="httpContext">Http Context</param>
        public async Task Invoke(HttpContext httpContext)
        {
            #region Check the existence of basic authentication in the request header.
            string authorizationHeader = httpContext.Request.Headers[StaticValues.AUTHORIZATION];
            if (!authorizationHeader.IsValid())
            {
                httpContext.Response.StatusCode = StaticValues.UNAUTHORIZED_STATUS_CODE;
                return;
            }
            #endregion

            #region Check basic authentication
            (string Username, string Password) = GetCredential(authorizationHeader);

            //The username and password retrieved from conventional data store.
            if (Username == "LoggerAPIUser" && Password == "LoggerAPIPass")
                await _next(httpContext);
            else
            {
                httpContext.Response.StatusCode = StaticValues.UNAUTHORIZED_STATUS_CODE;
                return;
            } 
            #endregion
        }

        /// <summary>
        /// Returns the username and password presented in the request header.
        /// </summary>
        /// <param name="authorizationHeader">authorization header attribute</param>
        /// <returns></returns>
        private (string, string) GetCredential(string authorizationHeader)
        {
            string auth = authorizationHeader.Split(new char[] { ' ' })[1];
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            var usernameAndPassword = encoding.GetString(Convert.FromBase64String(auth));
            string username = usernameAndPassword.Split(new char[] { ':' })[0];
            string password = usernameAndPassword.Split(new char[] { ':' })[1];
            var result = (Username: username, Password: password);
            return result;
        }
    }
}
