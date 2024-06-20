using AgreementManager.Domain.Agreements;
using AutoMapper;
using Moq;
using Solar.SDK.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Vostok.HotLineAssistant.AgreementManager.Application.AgreementBalances.Queries;
using Vostok.HotLineAssistant.AgreementManager.Infrastructure.QueryHandlers.Agreements;
using Xunit;

namespace AgreementManager.UnitTests.Infrastructure.QueryHandlers
{
	public class AgreementBalanceQueryHandlerTest
	{
		private readonly AgreementBalanceQueryHandler _handler;

		public AgreementBalanceQueryHandlerTest()
		{
			_handler = new AgreementBalanceQueryHandler(
				new Mock<AgreementBalanceService>(new Mock<ISolarSdkFacade>().Object).Object,
				new Mock<IMapper>().Object,
				new Mock<ILogger<AgreementBalanceQueryHandler>>().Object);
		}

		[Fact]
		public void Ctor()
		{
			Assert.Injection.OfConstructor(typeof(AgreementQueryHandler)).HasNullGuard();
		}

		[Fact]
		public Task Handle_GivenQueryById_IsNull_Throws()
		{
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null as AgreementBalanceByIdQuery, CancellationToken.None));
		}

		[Fact]
		public Task Handle_GivenQueryByNumber_IsNull_Throws()
		{
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null as AgreementBalanceByNumberQuery, CancellationToken.None));
		}

		[Fact]
		public Task Handle_GivenQueryByCard_IsNull_Throws()
		{
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null as AgreementBalanceByCardQuery, CancellationToken.None));
		}

		[Fact]
		public Task Handle_GivenQueryByIban_IsNull_Throws()
		{
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null as AgreementBalanceByIbanQuery, CancellationToken.None));
		}
	}
}