using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ContactManager.UnitTests.Api.Controllers
{
    public class ClientBalanceControllerTest
    {
        
        [Fact]
        public void Ctor()
        {
            Assert.Injection.OfConstructor(typeof(ClientBalancesController)).HasNullGuard();
        }

        [Fact]
        public Task Get()
        {
	        //Arrange
	        var action = Assert.Action<IEnumerable<Balance>>(typeof(ClientBalancesController), typeof(ClientBalancesByIdQuery));

	        //Assert
	        return action.IsJson().Run();
        }

        [Fact]
        public Task Get_ByNumber()
        {
	        //Arrange
	        var action = Assert.Action<IEnumerable<Balance>>(typeof(ClientBalancesController), typeof(ClientBalancesByNumberQuery));

	        //Assert
	        return action.IsJson().Run();
        }

        [Fact]
        public Task Get_ByCard()
        {
	        //Arrange
	        var action = Assert.Action<IEnumerable<Balance>>(typeof(ClientBalancesController), typeof(ClientBalancesByCardQuery));

	        //Assert
	        return action.IsJson().Run();
        }

        [Fact]
        public Task Get_ByIban()
        {
	        //Arrange
	        var action = Assert.Action<IEnumerable<Balance>>(typeof(ClientBalancesController), typeof(ClientBalancesByIbanQuery));

	        //Assert
	        return action.IsJson().Run();
        }
    }
}