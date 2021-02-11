using LogProxy.Test.General;
using LogProxy.Test.Sample;
using Newtonsoft.Json;
using System.Net;
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

        [Fact]
        public async void Is_Post_Response_UnprocessableEntity()
        {
            //Arrange
            HttpContent c = new StringContent(string.Empty);
            c.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var path = "/Logger";

            //Act
            var response = await _authenticatedClient.PostAsync(path, c);

            //Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Fact]
        public async void Is_Methods_OtherThan_Get_And_Post_Rejected()
        {
            //Arrange
            HttpContent c = new StringContent(string.Empty);
            c.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var path = "/Logger";

            //Act
            var response = await _authenticatedClient.PatchAsync(path, c);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
