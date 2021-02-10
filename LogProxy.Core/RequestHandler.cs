using LogProxy.Core.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Net.Http;

namespace LogProxy.Core
{
    /// <summary>
    /// Handel all operations related to preparing a request to send to the final destination.
    /// </summary>
    public class RequestHandler
    {
        private readonly HttpContext _context;
        private readonly ProxyOptions _options;

        public RequestHandler(HttpContext context, ProxyOptions proxyOptions)
        {
            _context = context;
            _options = proxyOptions;
        }

        /// <summary>
        /// Created request for send to destination.
        /// </summary>
        /// <returns>destination Http Request Message</returns>
        public HttpRequestMessage CreateTargetRequest()
        {
            var targetUri = BuildTargetUri(_context.Request);
            if (!targetUri.IsValid())
                return null;

            var requestMessage = new HttpRequestMessage();
            CopyFromOriginalRequestContentAndHeaders(_context, requestMessage);

            requestMessage.RequestUri = targetUri;
            requestMessage.Headers.Host = targetUri.Host;
            requestMessage.Method = _context.Request.Method.GetMethod();

            return requestMessage;
        }

        /// <summary>
        /// Copy the http headers and body content from origin request into destination request.
        /// </summary>
        /// <param name="context">The Http Context that contains the origin request</param>
        /// <param name="requestMessage">The destination Http Request Message</param>
        private void CopyFromOriginalRequestContentAndHeaders(HttpContext context, HttpRequestMessage requestMessage)
        {
            var requestMethod = context.Request.Method;

            if (!HttpMethods.IsGet(requestMethod) &&
              !HttpMethods.IsHead(requestMethod) &&
              !HttpMethods.IsDelete(requestMethod) &&
              !HttpMethods.IsTrace(requestMethod))
            {
                var streamContent = new StreamContent(context.Request.Body);
                requestMessage.Content = streamContent;
                requestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(context.Request.ContentType);
            }

            foreach (var header in context.Request.Headers)
                requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());

            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(StaticValues.AUTHENTICATION_SCHEME, _options.ApiKey);
        }

        /// <summary>
        /// Buidl the target or final destination Uri.
        /// </summary>
        /// <param name="request">the origin Http request</param>
        /// <returns></returns>
        private Uri BuildTargetUri(HttpRequest request)
        {
            Uri targetUri = null;

            if (request.Path.StartsWithSegments(StaticValues.LOGGER_PATH))
                targetUri = new Uri(UriHelper.BuildAbsolute(_options.Scheme, _options.Host, _options.PathBase, default, _context.Request.QueryString.Add(_options.AppendQuery)));
            
            return targetUri;
        }
    }
}
