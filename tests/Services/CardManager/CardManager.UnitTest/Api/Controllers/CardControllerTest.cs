using System.Threading.Tasks;
using Vostok.HotLineAssistant.CardManager.Api.Controllers;
using Vostok.HotLineAssistant.CardManager.Application.Cards.Dtos;
using Vostok.HotLineAssistant.CardManager.Application.Cards.Queries;
using Xunit;

namespace CardManager.UnitTests.Api.Controllers
{
	public class CardControllerTest
	{
		[Fact]
		public void Ctor()
		{
			Assert.Injection.OfConstructor(typeof(CardsController)).HasNullGuard();
		}

		[Fact]
		public Task Get()
		{
			//Arrange
			var action = Assert.Action<CardInfoDto>(typeof(CardsController), typeof(CardInfoByIdQuery));

			//Assert
			return action.IsJson().Run();
		}
	}
}
