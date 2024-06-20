using Microsoft.Extensions.Logging;
using Solar.Models.Models;
using Solar.Models.Models.Params;
using Solar.SDK.Interfaces;
using System.Threading.Tasks;
using Vostok.HotlineAssistant.SolarCore;
using Vostok.HotLineAssistant.Common.Requests.Common;
using Vostok.HotLineAssistant.Common.Requests.Specific;
using Vostok.HotLineAssistant.Common.Response.Common;

namespace AgreementManager.Domain.Agreements
{
	public class AgreementBalanceService : BaseService, IAgreementBalanceService
	{
		private readonly ISolarSdkFacade _facade;
		private readonly ILogger<AgreementBalanceService> _logger;

		public AgreementBalanceService(ISolarSdkFacade facade, ILogger<AgreementBalanceService> logger) : base(logger)
		{
			_facade = facade;
			_logger = logger;
		}

		public async Task<ModelResponse<Balance>> GetBalanceById(ByIdRequest request)
		{
			return await ProcessRequest(request, async req =>
			{
				var response = GetResponse<ModelResponse<Balance>>();
				response.Item = await _facade.GetAgreementBalanceAsync(new AgreementId { Criteria = req.Id });
				return response;
			});
		}

		public async Task<ModelResponse<Balance>> GetBalanceByNumber(ByNumberRequest request)
		{
			return await ProcessRequest(request, async req =>
			{
				var response = GetResponse<ModelResponse<Balance>>();
				response.Item = await _facade.GetAgreementBalanceAsync(new AgreementNumber {Criteria = req.Number});
				return response;
			});
		}

		public async Task<ModelResponse<Balance>> GetBalanceByCardNumber(ByCardNumberRequest request)
		{
			return await ProcessRequest(request, async req =>
			{
				var response = GetResponse<ModelResponse<Balance>>();
				response.Item = await _facade.GetAgreementBalanceAsync(new AgreementCardNumber { Criteria = req.CardNumber });
				return response;
			});
		}

		public async Task<ModelResponse<Balance>> GetBalanceByIBan(ByIBanRequest request)
		{
			return await ProcessRequest(request, async req =>
			{
				var response = GetResponse<ModelResponse<Balance>>();
				response.Item = await _facade.GetAgreementBalanceAsync(new AgreementIBan{ Criteria = req.IBan });
				return response;
			});
		}
	}
}