using LogProxy.Test.General;
using System.Net;
using Xunit;

namespace LogProxy.Test
{
    public partial class MainTest : IClassFixture<ProxyWebAppFactory<App.Startup>>
    {
        private readonly ProxyWebAppFactory<App.Startup> _factory;

        public MainTest(ProxyWebAppFactory<App.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Is_Reject_Unauthorized_User()
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
