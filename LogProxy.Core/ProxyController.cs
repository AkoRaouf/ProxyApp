using Microsoft.AspNetCore.Builder;

namespace LogProxy.Core
{
    public static class ProxyController
    {
        public static void ExecuteProxy(this IApplicationBuilder app)
        {
            app.UseMiddleware<ProxyMiddleware>();
        }
    }
}
