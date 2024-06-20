using Microsoft.Extensions.Logging;
using Solar.Models.Models;
using Solar.Models.Models.Params;
using Solar.SDK.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Vostok.HotlineAssistant.SolarCore;
using Vostok.HotLineAssistant.Common.Requests.Common;
using Vostok.HotLineAssistant.Common.Requests.Specific;
using Vostok.HotLineAssistant.Common.Response.Common;

namespace AgreementManager.Domain.Agreements
{
	public class AgreementService : BaseService, IAgreementService
	{
		private readonly ISolarSdkFacade _facade;
		private readonly ILogger<AgreementService> _logger;

		public AgreementService(ISolarSdkFacade facade, ILogger<AgreementService> logger) : base(logger)
		{
			_facade = facade;
			_logger = logger;
		}

		public async Task<ModelResponse<Agreement>> GetAgreementById(ByIdRequest request)
		{
			return await ProcessRequest(request, async req =>
			{
				var response = GetResponse<ModelResponse<Agreement>>();
				response.Item = await _facade.GetAgreementAsync(new AgreementId {Criteria = req.Id});
				return response;
			});
		}

		public async Task<ModelResponse<Agreement>> GetAgreementByNumber(ByNumberRequest request)
		{
			return await ProcessRequest(request, async req =>
			{
				var response = GetResponse<ModelResponse<Agreement>>();
				response.Item = await _facade.GetAgreementAsync(new AgreementNumber {Criteria = req.Number});
				return response;
			});
		}

		public async Task<ModelResponse<Agreement>> GetAgreementByCardNumber(ByCardNumberRequest request)
		{
			return await ProcessRequest(request, async req =>
			{
				var response = GetResponse<ModelResponse<Agreement>>();
				_logger.LogInformation(req.CardNumber);
				response.Item = await _facade.GetAgreementAsync(new AgreementCardNumber {Criteria = req.CardNumber});
				return response;
			});
		}

		public async Task<ModelResponse<Agreement>> GetAgreementByIBan(ByIBanRequest request)
		{
			return await ProcessRequest(request, async req =>
			{
				var response = GetResponse<ModelResponse<Agreement>>();
				response.Item = await _facade.GetAgreementAsync(new AgreementIBan {Criteria = req.IBan});
				return response;
			});
		}

		public async Task<ModelsResponse<Agreement>> GetAgreementsById(ByIdRequest request)
		{
			return await ProcessRequest(request, async req =>
			{
				var response = GetResponse<ModelsResponse<Agreement>>();
				var items = await _facade.GetAgreementListAsync(new AgreementId {Criteria = req.Id});
				response.Items = items.ToList();
				return response;
			});
		}

		public async Task<ModelsResponse<Agreement>> GetAgreementsByNumber(ByNumberRequest request)
		{
			return await ProcessRequest(request, async req =>
			{
				var response = GetResponse<ModelsResponse<Agreement>>();
				var items = await _facade.GetAgreementListAsync(new AgreementNumber {Criteria = req.Number});
				response.Items = items.ToList();
				return response;
			});
		}

		public async Task<ModelsResponse<Agreement>> GetAgreementsByCardNumber(ByCardNumberRequest request)
		{
			return await ProcessRequest(request, async req =>
			{
				var response = GetResponse<ModelsResponse<Agreement>>();
				var items = await _facade.GetAgreementListAsync(new AgreementCardNumber
					{Criteria = req.CardNumber});
				response.Items = items.ToList();
				return response;
			});
		}

		public async Task<ModelsResponse<Agreement>> GetAgreementsByIBan(ByIBanRequest request)
		{
			return await ProcessRequest(request, async req =>
			{
				var response = GetResponse<ModelsResponse<Agreement>>();
				var items = await _facade.GetAgreementListAsync(new AgreementIBan
					{Criteria = req.IBan});
				response.Items = items.ToList();
				return response;
			});
		}
	}
}