using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
            var targetUri = BuildTargetUri(_context.Request) 
                ?? throw new Exception("There is an error in specifying the target URI"); 

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
            }

            foreach (var header in context.Request.Headers)
            {
                requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            }
        }

        private Uri BuildTargetUri(HttpRequest request)
        {
            Uri targetUri = null;

            if (request.Path.StartsWithSegments("/googleforms", out var remainingPath))
            {
                targetUri = new Uri("https://docs.google.com/forms" + remainingPath);
            }

            return targetUri;
        }
    }
}
