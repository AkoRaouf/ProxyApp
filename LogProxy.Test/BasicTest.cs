using LogProxy.Test.General;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xunit;

namespace LogProxy.Test
{
    public partial class MainTest : IClassFixture<ProxyWebAppFactory<App.Startup>>
    {
        private readonly ProxyWebAppFactory<App.Startup> _factory;
        private readonly HttpClient _authenticatedClient;

        public MainTest(ProxyWebAppFactory<App.Startup> factory)
        {
            _factory = factory;
            var _authenticatedClient = _factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions() { HandleCookies = false });
            string username = "LoggerAPIUser";
            string password = "LoggerAPIPass";
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            _authenticatedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", svcCredentials);
        }

        [Fact]
        public async void Is_Unauthorized_User_Rejected()
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync("/Logger");

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
