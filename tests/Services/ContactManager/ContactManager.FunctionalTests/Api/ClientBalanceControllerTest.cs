using System.Net;
using System.Threading.Tasks;
using ContactManager.FunctionalTests.Fixtures;
using Xunit;

namespace ContactManager.FunctionalTests.Api
{
    public class ClientBalanceControllerTest : ApiControllerTestFixture
    {
        private const string BaseUrl = "/api/v1/client/balances";
        private const int ClientId = 99999999;

        public ClientBalanceControllerTest(ServiceFactory<TestStartup> factory) : base(factory)
        {
        }
        
        /*[Fact]
        public async Task Get()
        {
            //Act
            var url = $"{BaseUrl}/{ClientId}";
            var response = await Client.GetAsync(url);

            //Assert
            response.EnsureSuccessStatusCode();
        }*/

        //[Fact]
        //TODO: Move to card controller test
        public async Task Get_WhenIsNotFound_ReturnsNotFound()
        {
            //Act
            var url = $"{BaseUrl}/{ClientId}";
            var response = await Client.GetAsync(url);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}