using LogProxy.Core.General;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;

namespace LogProxy.Core
{
    public static class ProxyController
    {
        /// <summary>
        /// Add the ProxyMiddleware to pipeline.
        /// </summary>
        public static void ExecuteProxy(this IApplicationBuilder app, Uri baseUri, string apiKey)
        {
            //Adding the configurable stuff.
            var options = new ProxyOptions
            {
                Scheme = baseUri.Scheme,
                Host = new HostString(baseUri.Authority),
                PathBase = baseUri.AbsolutePath,
                AppendQuery = new QueryString(baseUri.Query),
                ApiKey = apiKey
            };

            app.UseMiddleware<ProxyMiddleware>(Options.Create(options));
        }
    }
}
