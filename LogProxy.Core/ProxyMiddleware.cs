using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace LogProxy.Core
{
    public class ProxyMiddleware
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly RequestDelegate _nextMiddleware;

        public ProxyMiddleware(RequestDelegate nextMiddleware)
        {
            _nextMiddleware = nextMiddleware;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestHandler = new RequestHandler(context);
            var responseHandler = new ResponseHandler(context);

            var targetRequestMessage = requestHandler.CreateTargetRequest();
            if (targetRequestMessage.IsValid())
            {
                using var responseMessage = await _httpClient.SendAsync(targetRequestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted);
                await responseHandler.CopyToCurrentResponseAsync(responseMessage);
                return;
            }
            await _nextMiddleware(context);
        }
    }
}
