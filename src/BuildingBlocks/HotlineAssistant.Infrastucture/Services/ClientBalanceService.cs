using Solar.Models.Models;
using Solar.Models.Models.Params;
using Solar.SDK.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vostok.HotLineAssistant.Infrastucture.Services
{
	public class ClientBalanceService : BaseService, IClientBalanceService
	{
		public ClientBalanceService(ISolarSdkFacade facade) : base(facade)
		{
		}

		public async Task<IEnumerable<Balance>> GetBalanceById(int id)
		{
			return await GetClientBalance(new ClientId { Criteria = id });
		}

		public async Task<IEnumerable<Balance>> GetBalanceByNumber(string number)
		{
			return await GetClientBalance(new ClientNumber { Criteria = number.Trim() });
		}

		public async Task<IEnumerable<Balance>> GetBalanceByCardNumber(string card)
		{
			return await GetClientBalance(new ClientCardNumber { Criteria = card.Trim() });
		}

		public async Task<IEnumerable<Balance>> GetBalanceByIBan(string ibanCode)
		{
			return await GetClientBalance(new ClientIBan { Criteria = ibanCode.Trim() });
		}
	}
}