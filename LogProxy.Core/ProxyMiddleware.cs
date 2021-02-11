using LogProxy.Core.General;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace LogProxy.Core
{
    /// <summary>
    /// As midderware handels all proxy realated operations.
    /// </summary>
    public class ProxyMiddleware
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly RequestDelegate _nextMiddleware;
        private readonly ProxyOptions _options;

        public ProxyMiddleware(RequestDelegate nextMiddleware, IOptions<ProxyOptions> options)
        {
            _nextMiddleware = nextMiddleware;
            _options = options.Value;
        }

        /// <summary>
        /// Executes the whole proxy proccess.
        /// </summary>
        /// <param name="httpContext">Http Context</param>
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method.GetMethod().IsValid())
            {
                var requestHandler = new RequestHandler(context, _options);
                var responseHandler = new ResponseHandler(context);

                var targetRequestMessage = requestHandler.CreateTargetRequest();
                if (targetRequestMessage.IsValid())
                {
                    using var responseMessage = await _httpClient.SendAsync(targetRequestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted);
                    await responseHandler.CopyToCurrentResponseAsync(responseMessage);
                    return;
                }
            }
            await _nextMiddleware(context);
        }
    }
}
