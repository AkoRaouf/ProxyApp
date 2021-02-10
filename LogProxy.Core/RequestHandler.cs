using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace LogProxy.Core
{
    public class RequestHandler
    {
        private readonly HttpContext _context;
        public RequestHandler(HttpContext context)
        {
            _context = context;
        }

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
