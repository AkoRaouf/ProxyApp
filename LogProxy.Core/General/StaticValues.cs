using System;
using System.Collections.Generic;
using System.Text;

namespace LogProxy.Core.General
{
    /// <summary>
    /// Represents all constant values
    /// </summary>
    public struct StaticValues
    {
        public const string AUTHENTICATION_SCHEME = "Bearer";
        public const string LOGGER_PATH = "/Logger";
        public const string TRANSFER_ENCODING = "transfer-encoding";
        public const int UNAUTHORIZED_STATUS_CODE = 401;
    }
}
