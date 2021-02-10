using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LogProxy.Core
{
    /// <summary>
    /// Handel all operations related to preparing a response.
    /// </summary>
    public class ResponseHandler
    {
        private readonly HttpContext _context;
        public ResponseHandler(HttpContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Copy the response has gotten from destination or target to the response should be sent to the origin.
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        public async Task CopyToCurrentResponseAsync(HttpResponseMessage httpResponseMessage)
        {
            _context.Response.StatusCode = (int)httpResponseMessage.StatusCode;
            CopyFromTargetResponseHeaders(httpResponseMessage);
            await httpResponseMessage.Content.CopyToAsync(_context.Response.Body);
        }

        /// <summary>
        /// Copy from target response header.
        /// </summary>
        /// <param name="responseMessage">the origin Response Message</param>
        private void CopyFromTargetResponseHeaders(HttpResponseMessage responseMessage)
        {
            foreach (var header in responseMessage.Headers)
                _context.Response.Headers[header.Key] = header.Value.ToArray();

            foreach (var header in responseMessage.Content.Headers)
                _context.Response.Headers[header.Key] = header.Value.ToArray();

            _context.Response.Headers.Remove("transfer-encoding");
        }
    }
}
