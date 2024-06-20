using System.Collections.Generic;
using System.Threading.Tasks;
using Solar.Models.Models;

namespace Vostok.HotLineAssistant.CardManager.Domain.Models.Cards
{
	public interface IClientBalanceService
	{
		Task<IEnumerable<Balance>> GetBalanceById(int id);
		Task<IEnumerable<Balance>> GetBalanceByNumber(string number);
		Task<IEnumerable<Balance>> GetBalanceByCardNumber(string card);
		Task<IEnumerable<Balance>> GetBalanceByIBan(string ibanCode);
	}
}