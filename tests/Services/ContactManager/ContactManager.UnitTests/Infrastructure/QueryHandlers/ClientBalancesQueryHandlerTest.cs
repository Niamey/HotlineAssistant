using AutoFixture;
using Moq;
using Solar.Models.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.HotLineAssistant.ContactManager.Application.ClientBalances.Queries;
using Vostok.HotLineAssistant.ContactManager.Infrastructure.QueryHandlers.ClientBalances;
using Vostok.HotLineAssistant.Infrastucture.Services;
using Xunit;

namespace ContactManager.UnitTests.Infrastructure.QueryHandlers
{
	public class ClientBalancesQueryHandlerTest
	{
		private readonly ClientBalancesQueryHandler _handler;
		private readonly Mock<IClientBalanceService> _serviceMock;

		public ClientBalancesQueryHandlerTest()
		{
			_serviceMock = new Mock<IClientBalanceService>();
			_handler = new ClientBalancesQueryHandler(_serviceMock.Object);
		}

		[Fact]
		public void Ctor()
		{
			Assert.Injection.OfConstructor(typeof(ClientBalancesQueryHandler)).HasNullGuard();
		}

		[Fact]
		public async Task Handle_ById()
		{
			//Arrange
			var fixture = new Fixture();
			var query = fixture.Create<ClientBalancesByIdQuery>();
			var expected = fixture.CreateMany<Balance>().ToList();
			_serviceMock.Setup(p => p.GetBalanceById(query.Id))
				.ReturnsAsync(expected);
			//Act
			var actual = await _handler.Handle(query, CancellationToken.None);

			//Assert
			Assert.Equal(expected, actual.ToList());
		}

		[Fact]
		public async Task Handle_ByNumber()
		{
			//Arrange
			var fixture = new Fixture();
			var query = fixture.Create<ClientBalancesByNumberQuery>();
			var expected = fixture.CreateMany<Balance>().ToList();
			_serviceMock.Setup(p => p.GetBalanceByNumber(query.Number))
				.ReturnsAsync(expected);
			//Act
			var actual = await _handler.Handle(query, CancellationToken.None);

			//Assert
			Assert.Equal(expected, actual.ToList());
		}

		[Fact]
		public async Task Handle_ByCardNumber()
		{
			//Arrange
			var fixture = new Fixture();
			var query = fixture.Create<ClientBalancesByCardQuery>();
			var expected = fixture.CreateMany<Balance>().ToList();
			_serviceMock.Setup(p => p.GetBalanceByCardNumber(query.Card))
				.ReturnsAsync(expected);
			//Act
			var actual = await _handler.Handle(query, CancellationToken.None);

			//Assert
			Assert.Equal(expected, actual.ToList());
		}

		[Fact]
		public async Task Handle_ByIban()
		{
			//Arrange
			var fixture = new Fixture();
			var query = fixture.Create<ClientBalancesByIbanQuery>();
			var expected = fixture.CreateMany<Balance>().ToList();
			_serviceMock.Setup(p => p.GetBalanceByIBan(query.Iban))
				.ReturnsAsync(expected);
			//Act
			var actual = await _handler.Handle(query, CancellationToken.None);

			//Assert
			Assert.Equal(expected, actual.ToList());
		}

		[Fact]
		public Task Handle_GivenQueryById_IsNull_Throws()
		{
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle((ClientBalancesByIdQuery) null, CancellationToken.None));
		}

		[Fact]
		public Task Handle_GivenQueryByIBan_IsNull_Throws()
		{
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle((ClientBalancesByIbanQuery) null, CancellationToken.None));
		}

		[Fact]
		public Task Handle_GivenQueryByNumber_IsNull_Throws()
		{
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle((ClientBalancesByNumberQuery)null, CancellationToken.None));
		}

		[Fact]
		public Task Handle_GivenQueryByCardNumber_IsNull_Throws()
		{
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle((ClientBalancesByCardQuery)null, CancellationToken.None));
		}
	}
}