using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Vostok.HotLineAssistant.CardManager.Domain.Models.Cards;
using Vostok.HotLineAssistant.CardManager.Infrastructure.QueryHandlers;
using Xunit;

namespace CardManager.UnitTests.Infrastructure.QueryHandlers
{
	public class CardInfoQueryHandlerTest
	{
		private readonly CardInfoQueryHandler _handler;
		private readonly Mock<IClientBalanceService> _mock;

		public CardInfoQueryHandlerTest()
		{
			_mock = new Mock<IClientBalanceService>();
			_handler = new CardInfoQueryHandler(_mock.Object);
		}

		[Fact]
		public void Ctor()
		{
			Assert.Injection.OfConstructor(typeof(CardInfoQueryHandler)).HasNullGuard();
		}

		[Fact]
		public Task Handle_GivenQuery_IsNull_Throws()
		{
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
		}
	}
}