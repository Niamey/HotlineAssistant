using System.Threading.Tasks;
using Vostok.HotLineAssistant.AgreementManager.Api.Controllers;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Queries;
using Vostok.HotLineAssistant.Common.Response.Common;
using Xunit;

namespace AgreementManager.UnitTests.Api.Controllers
{
	public class AgreementControllerTest
	{
		[Fact]
		public void Ctor()
		{
			Assert.Injection.OfConstructor(typeof(AgreementController)).HasNullGuard();
		}

		[Fact]
		public Task GetByIdQuery()
		{
			//Arrange
			var action = Assert.Action<ModelResponse<AgreementDto>>(typeof(AgreementController), typeof(AgreementByIdQuery));

			//Assert
			return action.IsJson().Run();
		}

		[Fact]
		public Task GetByNumberQuery()
		{
			//Arrange
			var action = Assert.Action<ModelResponse<AgreementDto>>(typeof(AgreementController), typeof(AgreementByNumberQuery));

			//Assert
			return action.IsJson().Run();
		}

		[Fact]
		public Task GetByCardQuery()
		{
			//Arrange
			var action = Assert.Action<ModelResponse<AgreementDto>>(typeof(AgreementController), typeof(AgreementByCardQuery));

			//Assert
			return action.IsJson().Run();
		}

		[Fact]
		public Task GetByIbanQuery()
		{
			//Arrange
			var action = Assert.Action<ModelResponse<AgreementDto>>(typeof(AgreementController), typeof(AgreementByIbanQuery));

			//Assert
			return action.IsJson().Run();
		}
	}
}