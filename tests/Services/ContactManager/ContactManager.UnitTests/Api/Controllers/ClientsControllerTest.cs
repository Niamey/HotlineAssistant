using System.Threading.Tasks;
using Vostok.HotLineAssistant.ContactManager.Api.Controllers;
using Vostok.HotLineAssistant.ContactManager.Application.Client.Dtos;
using Vostok.HotLineAssistant.ContactManager.Application.Client.Queries;
using Xunit;

namespace ContactManager.UnitTests.Api.Controllers
{
	public class ClientsControllerTest
	{

		[Fact]
		public void Ctor()
		{
			Assert.Injection.OfConstructor(typeof(ClientsController)).HasNullGuard();
		}

		//[Fact]
		public Task Get()
		{
			//Arrange
			var action =
				Assert.Action<ClientDto>(typeof(ClientsController), typeof(ClientSearchQuery));

			//Assert
			return action.IsJson().Run();
		}
	}
}