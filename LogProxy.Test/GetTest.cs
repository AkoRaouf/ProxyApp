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
            
            //Act
            var response = await _authenticatedClient.GetAsync("/Logger?maxRecords=1&view=Grid view");

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Is_Get_ResponseType_Json()
        {
            //Arrange
           
            //Act
            var response = await _authenticatedClient.GetAsync("/Logger?maxRecords=1&view=Grid view");

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
           
            //Act
            
            var response = await _authenticatedClient.GetAsync($"/Logger?maxRecords={numberOfRecords}&view=Grid view");
            var getJsonObject = JsonConvert.DeserializeObject<Root>(await response.Content.ReadAsStringAsync());

            //Assert
            Assert.Equal(numberOfRecords, getJsonObject.records.Count);
        }
    }
}
