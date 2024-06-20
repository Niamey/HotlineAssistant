using Solar.Models.Models;
using System.Threading.Tasks;
using Vostok.HotLineAssistant.Common.Requests.Common;
using Vostok.HotLineAssistant.Common.Requests.Specific;
using Vostok.HotLineAssistant.Common.Response.Common;
using Vostok.HotlineAssistant.SolarCore;

namespace AgreementManager.Domain.Agreements
{
	public interface IAgreementBalanceService : IService
	{
		Task<ModelResponse<Balance>> GetBalanceById(ByIdRequest request);
		Task<ModelResponse<Balance>> GetBalanceByNumber(ByNumberRequest request);
		Task<ModelResponse<Balance>> GetBalanceByCardNumber(ByCardNumberRequest request);
		Task<ModelResponse<Balance>> GetBalanceByIBan(ByIBanRequest request);
	}
}