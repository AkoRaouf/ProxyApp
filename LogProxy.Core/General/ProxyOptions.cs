﻿using Microsoft.AspNetCore.Http;

namespace LogProxy.Core.General
{
    /// <summary>
    /// Proxy Options.
    /// </summary>
    public class ProxyOptions
    {
        /// <summary>
        /// Destination uri scheme
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Destination uri host
        /// </summary>
        public HostString Host { get; set; }

        /// <summary>
        /// Destination uri path base to which current Path will be appended
        /// </summary>
        public PathString PathBase { get; set; }

        /// <summary>
        /// Query string parameters to append to each request
        /// </summary>
        public QueryString AppendQuery { get; set; }

        /// <summary>
        /// API Key for target api
        /// </summary>
        public string ApiKey { get; set; }
    }
}
