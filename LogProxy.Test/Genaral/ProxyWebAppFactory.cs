using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogProxy.Test.General
{
    public class ProxyWebAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
    }
}
