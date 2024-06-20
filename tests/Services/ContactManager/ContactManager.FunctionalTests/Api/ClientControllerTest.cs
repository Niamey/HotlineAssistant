using System.Net;
using System.Threading.Tasks;
using ContactManager.FunctionalTests.Fixtures;
using Xunit;

namespace ContactManager.FunctionalTests.Api
{
	public class ClientControllerTest : ApiControllerTestFixture
	{
		private const string BaseUrl = "api/v1/Clients";

		public ClientControllerTest(ServiceFactory<TestStartup> factory) : base(factory)
		{
		}

		//[Fact]
		//TODO: Fix request params
		public async Task Get_WhenIsNotFound_ReturnsNotFound()
		{
			//Act
			var url = $"{BaseUrl}";
			var response = await Client.GetAsync(url);

			//Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}
	}
}