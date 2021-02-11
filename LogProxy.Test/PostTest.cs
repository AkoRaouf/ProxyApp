﻿using LogProxy.Test.General;
using LogProxy.Test.Sample;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace LogProxy.Test
{
    public partial class MainTest : IClassFixture<ProxyWebAppFactory<App.Startup>>
    {
        [Fact]
        public async void Is_Post_Return_Currect()
        {
            //Arrange
            HttpContent c = new StringContent(JsonConvert.SerializeObject(SamplePostData.Get()));
            c.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var path = "/Logger";

            //Act
            var response = await _authenticatedClient.PostAsync(path, c);
            var postJsonResponse = JsonConvert.DeserializeObject<PostJsonResponse.Root>(await response.Content.ReadAsStringAsync());

            //Assert
            Assert.Equal(SamplePostData.Get().records.Count, postJsonResponse.records.Count);
        }
    }
}
