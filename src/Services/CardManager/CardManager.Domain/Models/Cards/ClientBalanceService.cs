using Solar.Models.Models;
using Solar.Models.Models.Params;
using Solar.SDK.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vostok.HotLineAssistant.CardManager.Domain.Models.Cards
{
	public class ClientBalanceService : IClientBalanceService
	{
		private readonly ISolarSdkFacade _facade;
		public ClientBalanceService(ISolarSdkFacade facade)
		{
			_facade = facade;
		}

		public async Task<IEnumerable<Balance>> GetBalanceById(int id)
		{
			var req = new ClientId {Criteria = id};
			return await _facade.GetClientBalanceAsync(req);
		}

		public async Task<IEnumerable<Balance>> GetBalanceByNumber(string number)
		{
			var req = new ClientNumber {Criteria = number.Trim()};
			return await _facade.GetClientBalanceAsync(req);
		}

		public async Task<IEnumerable<Balance>> GetBalanceByCardNumber(string card)
		{
			var req = new ClientCardNumber {Criteria = card.Trim()};
			return await _facade.GetClientBalanceAsync(req);
		}

		public async Task<IEnumerable<Balance>> GetBalanceByIBan(string ibanCode)
		{
			var req = new ClientIBan {Criteria = ibanCode.Trim()};
			return await _facade.GetClientBalanceAsync(req);
		}
	}
}