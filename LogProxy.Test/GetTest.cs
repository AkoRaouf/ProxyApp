using LogProxy.Test.General;
using LogProxy.Test.GetJsonResponse;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Xunit;

namespace LogProxy.Test
{
    public partial class MainTest : IClassFixture<ProxyWebAppFactory<App.Startup>>
    {
        [Fact]
        public async void Is_Get_Returns_OK_Response()
        {
            //Arrange
            var client = _factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions() { HandleCookies = false });
            string username = "LoggerAPIUser";
            string password = "LoggerAPIPass";
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", svcCredentials);
            //Act
            var response = await client.GetAsync("/Logger?maxRecords=1&view=Grid view");

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Is_Get_ResponseType_Json()
        {
            //Arrange
            var client = _factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions() { HandleCookies = false });
            string username = "LoggerAPIUser";
            string password = "LoggerAPIPass";
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", svcCredentials);
            //Act
            var response = await client.GetAsync("/Logger?maxRecords=1&view=Grid view");

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(20)]
        public async void Is_Get_Response_ResultCount(int numberOfRecords)
        {
            //Arrange
            var client = _factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions() { HandleCookies = false });
            string username = "LoggerAPIUser";
            string password = "LoggerAPIPass";
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", svcCredentials);
            //Act
            
            var response = await client.GetAsync($"/Logger?maxRecords={numberOfRecords}&view=Grid view");
            var getJsonObject = JsonConvert.DeserializeObject<Root>(await response.Content.ReadAsStringAsync());

            //Assert
            Assert.Equal(numberOfRecords, getJsonObject.records.Count);
        }
    }
}
