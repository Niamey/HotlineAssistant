using AgreementManager.Domain.Agreements;
using AutoMapper;
using Moq;
using Solar.SDK.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreements.Queries;
using Vostok.HotLineAssistant.AgreementManager.Infrastructure.QueryHandlers.Agreements;
using Xunit;

namespace AgreementManager.UnitTests.Infrastructure.QueryHandlers
{
	public class AgreementsQueryHandlerTest
	{
		private readonly AgreementsQueryHandler _handler;

		public AgreementsQueryHandlerTest()
		{
			_handler = new AgreementsQueryHandler(new Mock<AgreementService>(new Mock<ISolarSdkFacade>().Object).Object,
				new Mock<IMapper>().Object,
				new Mock<ILogger<AgreementsQueryHandler>>().Object);
		}

		[Fact]
		public void Ctor()
		{
			Assert.Injection.OfConstructor(typeof(AgreementQueryHandler)).HasNullGuard();
		}

		[Fact]
		public Task Handle_GivenQueryById_IsNull_Throws()
		{
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null as AgreementsByIdQuery, CancellationToken.None));
		}
		[Fact]
		public Task Handle_GivenQueryByNumber_IsNull_Throws()
		{
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null as AgreementsByNumberQuery, CancellationToken.None));
		}
		[Fact]
		public Task Handle_GivenQueryByCard_IsNull_Throws()
		{
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null as AgreementsByCardQuery, CancellationToken.None));
		}
		[Fact]
		public Task Handle_GivenQueryByIban_IsNull_Throws()
		{
			return Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null as AgreementsByIbanQuery, CancellationToken.None));
		}
	}
}