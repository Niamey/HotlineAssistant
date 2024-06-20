using Solar.Models.Models;
using System.Threading.Tasks;
using Vostok.HotlineAssistant.SolarCore;
using Vostok.HotLineAssistant.Common.Requests.Common;
using Vostok.HotLineAssistant.Common.Requests.Specific;
using Vostok.HotLineAssistant.Common.Response.Common;

namespace AgreementManager.Domain.Agreements
{
	public interface IAgreementService : IService
	{
		Task<ModelResponse<Agreement>> GetAgreementById(ByIdRequest request);
		Task<ModelResponse<Agreement>> GetAgreementByNumber(ByNumberRequest request);
		Task<ModelResponse<Agreement>> GetAgreementByCardNumber(ByCardNumberRequest request);
		Task<ModelResponse<Agreement>> GetAgreementByIBan(ByIBanRequest request);
		Task<ModelsResponse<Agreement>> GetAgreementsById(ByIdRequest request);
		Task<ModelsResponse<Agreement>> GetAgreementsByNumber(ByNumberRequest request);
		Task<ModelsResponse<Agreement>> GetAgreementsByCardNumber(ByCardNumberRequest request);
		Task<ModelsResponse<Agreement>> GetAgreementsByIBan(ByIBanRequest request);
	}
}