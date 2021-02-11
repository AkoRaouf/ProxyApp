using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LogProxy.Core.General
{
    public static class Extention
    {
        /// <summary>
        /// Checks whether the HttpRequestMessage value is valid or not.
        /// </summary>
        public static bool IsValid(this HttpRequestMessage httpRequestMessage)
        {
            return httpRequestMessage != null;
        }

        /// <summary>
        /// Checks whether the Uri value is valid or not.
        /// </summary>
        public static bool IsValid(this Uri uri)
        {
            return uri != null;
        }

        public static bool IsValid(this HttpMethod httpMethod)
        {
            if (httpMethod == HttpMethod.Get || httpMethod == HttpMethod.Post)
                return true;

            return false;
        }

        /// <summary>
        /// Represnts the HttpMethod based on string value.
        /// </summary>
        /// <param name="method">string method name</param>
        /// <returns>http method</returns>
        public static HttpMethod GetMethod(this string method)
        {
            if (HttpMethods.IsDelete(method)) return HttpMethod.Delete;
            if (HttpMethods.IsGet(method)) return HttpMethod.Get;
            if (HttpMethods.IsHead(method)) return HttpMethod.Head;
            if (HttpMethods.IsOptions(method)) return HttpMethod.Options;
            if (HttpMethods.IsPost(method)) return HttpMethod.Post;
            if (HttpMethods.IsPut(method)) return HttpMethod.Put;
            if (HttpMethods.IsTrace(method)) return HttpMethod.Trace;
            return new HttpMethod(method);
        }
    }
}
