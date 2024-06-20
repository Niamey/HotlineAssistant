using System.Threading.Tasks;
using Vostok.HotLineAssistant.AgreementManager.Api.Controllers;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreements.Queries;
using Vostok.HotLineAssistant.Common.Response.Common;
using Xunit;

namespace AgreementManager.UnitTests.Api.Controllers
{
	public class AgreementsControllerTest
	{
		[Fact]
		public void Ctor()
		{
			Assert.Injection.OfConstructor(typeof(AgreementsController)).HasNullGuard();
		}

		[Fact]
		public Task GetByIdQuery()
		{
			//Arrange
			var action = Assert.Action<ModelsResponse<AgreementDto>>(typeof(AgreementsController), typeof(AgreementsByIdQuery));

			//Assert
			return action.IsJson().Run();
		}

		[Fact]
		public Task GetByNumberQuery()
		{
			//Arrange
			var action = Assert.Action<ModelsResponse<AgreementDto>>(typeof(AgreementsController), typeof(AgreementsByNumberQuery));

			//Assert
			return action.IsJson().Run();
		}

		[Fact]
		public Task GetByCardQuery()
		{
			//Arrange
			var action = Assert.Action<ModelsResponse<AgreementDto>>(typeof(AgreementsController), typeof(AgreementsByCardQuery));

			//Assert
			return action.IsJson().Run();
		}

		[Fact]
		public Task GetByIbanQuery()
		{
			//Arrange
			var action = Assert.Action<ModelsResponse<AgreementDto>>(typeof(AgreementsController), typeof(AgreementsByIbanQuery));

			//Assert
			return action.IsJson().Run();
		}
	}
}