using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using ContactManager.FunctionalTests.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using Xunit;

namespace ContactManager.FunctionalTests.Fixtures
{
    public abstract class ApiControllerTestFixture : IClassFixture<ServiceFactory<TestStartup>>, IDisposable
    {
        protected readonly HttpClient Client;

        private readonly IServiceScope _scope;

        protected ApiControllerTestFixture(ServiceFactory<TestStartup> factory)
        {
            Client = factory.CreateClient();
            _scope = factory.CreateScope();
        }
        
        public void Dispose()
        {
            _scope.Dispose();
        }

		// TODO: Refact this
       /* public class DefaultControllerTests
        {
	        [Fact]
	        public async Task Get_Should_Return_OK_String()
	        {
		        // Arrange  
		        var httpClientFactory = new Mock<IHttpClientFactory>();
		        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
		        var fixture = new Fixture();

		        mockHttpMessageHandler.Protected()
			        .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
			        .ReturnsAsync(new HttpResponseMessage
			        {
				        StatusCode = HttpStatusCode.OK,
				        Content = new StringContent(fixture.Create<String>()),
			        });

		        var client = new HttpClient(mockHttpMessageHandler.Object) {BaseAddress = fixture.Create<Uri>()};
		        httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

		        // Act  
		        var controller = new DefaultController(httpClientFactory.Object);
		        var result = await controller.Get();

		        // Assert  
		        httpClientFactory.Verify(f => f.CreateClient(It.IsAny<String>()), Times.Once);

		        Assert.NotNull(result);
		        Assert.IsAssignableFrom<OkObjectResult>(result);
		        Assert.IsAssignableFrom<String>((result as OkObjectResult)?.Value);
		        Assert.False(String.IsNullOrWhiteSpace((result as OkObjectResult)?.Value as String));
	        }
        }*/
    }
}