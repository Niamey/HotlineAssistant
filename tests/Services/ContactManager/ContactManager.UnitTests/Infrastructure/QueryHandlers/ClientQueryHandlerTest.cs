using AutoFixture;
using AutoMapper;
using Moq;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Vostok.HotLineAssistant.ContactManager.Application.Client.Queries;
using Vostok.HotLineAssistant.ContactManager.Domain.Clients;
using Vostok.HotLineAssistant.ContactManager.Infrastructure.QueryHandlers.Clients;
using Xunit;

namespace ContactManager.UnitTests.Infrastructure.QueryHandlers
{
	public class ClientQueryHandlerTest
	{
		private readonly ClientSearchQueryHandler _handler;

		public ClientQueryHandlerTest()
		{
			_handler = new ClientSearchQueryHandler(new Mock<ClientService>(new Mock<IHttpClientFactory>().Object).Object, new Mock<IMapper>().Object);
		}

		[Fact]
		public void Ctor()
		{
			Assert.Injection.OfConstructor(typeof(ClientSearchQueryHandler)).HasNullGuard();
		}

		//[Fact]
		public async Task Handle_GivenHttpClient_IsNull_Throws()
		{
			//Arrange
			var fixture = new Fixture();
			var query = fixture.Create<ClientSearchQuery>();

			//Assert
			await Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(query, CancellationToken.None));
		}

		[Fact]
		public Task Handle_GivenSearchQuery_IsNull_Throws()
		{
			//Assert
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle((ClientSearchQuery)null, CancellationToken.None));
		}
	}
}