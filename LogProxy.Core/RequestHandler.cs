using Microsoft.AspNetCore.Http;
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
        public RequestHandler(HttpContext context)
        {
            _context = context;
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
        }

        /// <summary>
        /// Buidl the target or final destination Uri.
        /// </summary>
        /// <param name="request">the origin Http request</param>
        /// <returns></returns>
        private Uri BuildTargetUri(HttpRequest request)
        {
            Uri targetUri = null;

            if (request.Path.StartsWithSegments("/Logger"))
            {
                targetUri = new Uri("https://api.airtable.com/v0/appD1b1YjWoXkUJwR/Messages" + request.QueryString);
            }

            return targetUri;
        }
    }
}
