using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LogProxy.Core
{
    public class ResponseHandler
    {
        private readonly HttpContext _context;
        public ResponseHandler(HttpContext context)
        {
            _context = context;
        }

        public async Task CopyToCurrentResponseAsync(HttpResponseMessage httpResponseMessage)
        {
            _context.Response.StatusCode = (int)httpResponseMessage.StatusCode;
            CopyFromTargetResponseHeaders(httpResponseMessage);
            await httpResponseMessage.Content.CopyToAsync(_context.Response.Body);
        }

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
