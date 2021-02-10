using Microsoft.AspNetCore.Builder;

namespace LogProxy.App.General
{
    public static class Extentions
    {
        /// <summary>
        /// Checks whether the string value is valid or not.
        /// </summary>
        public static bool IsValid(this string value) => !string.IsNullOrEmpty(value);

        /// <summary>
        /// Adds the BasicAuthMiddleware to application.
        /// </summary>
        public static IApplicationBuilder UseBasicAuthMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<BasicAuthMiddleware>();
    }
}
