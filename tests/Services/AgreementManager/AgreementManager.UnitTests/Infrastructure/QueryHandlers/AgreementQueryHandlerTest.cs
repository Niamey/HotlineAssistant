using AgreementManager.Domain.Agreements;
using AutoMapper;
using Moq;
using Solar.SDK.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Queries;
using Vostok.HotLineAssistant.AgreementManager.Infrastructure.QueryHandlers.Agreements;
using Xunit;

namespace AgreementManager.UnitTests.Infrastructure.QueryHandlers
{
	public class AgreementQueryHandlerTest
	{
		private readonly AgreementQueryHandler _handler;

		public AgreementQueryHandlerTest()
		{
			_handler = new AgreementQueryHandler(new Mock<AgreementService>(new Mock<ISolarSdkFacade>().Object).Object,
				new Mock<IMapper>().Object,
				new Mock<ILogger<AgreementQueryHandler>>().Object);
		}

		[Fact]
		public void Ctor()
		{
			Assert.Injection.OfConstructor(typeof(AgreementQueryHandler)).HasNullGuard();
		}

		[Fact]
		public Task Handle_GivenQueryById_IsNull_Throws()
		{
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null as AgreementByIdQuery, CancellationToken.None));
		}
		[Fact]
		public Task Handle_GivenQueryByNumber_IsNull_Throws()
		{
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null as AgreementByNumberQuery, CancellationToken.None));
		}
		[Fact]
		public Task Handle_GivenQueryByCard_IsNull_Throws()
		{
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null as AgreementByCardQuery, CancellationToken.None));
		}
		[Fact]
		public Task Handle_GivenQueryByIban_IsNull_Throws()
		{
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null as AgreementByIbanQuery, CancellationToken.None));
		}
	}
}

