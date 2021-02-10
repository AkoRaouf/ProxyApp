using LogProxy.Test.General;
using LogProxy.Test.Sample;
using Newtonsoft.Json;
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
        [Fact]
        public async void Is_Post_Return_Currect()
        {
            //Arrange
            var client = _factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions() { HandleCookies = false });
            string username = "LoggerAPIUser";
            string password = "LoggerAPIPass";
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", svcCredentials);
            //Act
            HttpContent c = new StringContent(JsonConvert.SerializeObject(SamplePostData.Get()));
            c.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync("/Logger", c);
            var postJsonResponse = JsonConvert.DeserializeObject<PostJsonResponse.Root>(await response.Content.ReadAsStringAsync());

            //Assert
            Assert.Equal(SamplePostData.Get().records.Count, postJsonResponse.records.Count);
        }
    }
}
