using Microsoft.Extensions.Logging;
using Solar.Models.Enums;
using Solar.Models.Models;
using Solar.Models.Models.Params;
using Solar.SDK.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Vostok.HotlineAssistant.SolarCore;
using Vostok.HotLineAssistant.Common.Requests.Common;
using Vostok.HotLineAssistant.Common.Requests.Specific;
using Vostok.HotLineAssistant.Common.Response.Common;

namespace Vostok.HotLineAssistant.AccessorManager.Domain.Accessors
{
	public class AccessorService : BaseService, IAccessorService
	{
		private readonly ISolarSdkFacade _facade;
		private readonly ILogger<AccessorService> _logger;

		public AccessorService(ISolarSdkFacade facade, ILogger<AccessorService> logger) : base(logger)
		{
			_facade = facade;
			_logger = logger;
		}

		public async Task<ModelsResponse<Accessor>> GetAccessorById(ByIdRequest request)
		{
			return await ProcessRequest(request, async req =>
			{
				var response = GetResponse<ModelsResponse<Accessor>>();
				var items = await _facade.GetAccessorListAsync(new AgreementId
					{Criteria = req.Id, Filter = AccessorTypes.CARD});
				response.Items = items.ToList();
				return response;
			});
		}

		public async Task<ModelsResponse<Accessor>> GetAccessorByNumber(ByNumberRequest request)
		{
			return await ProcessRequest(request, async req =>
			{
				var response = GetResponse<ModelsResponse<Accessor>>();
				var items = await _facade.GetAccessorListAsync(new AgreementNumber
				{
					Criteria = req.Number
				});
				response.Items = items.ToList();
				return response;
			});
		}
	

		public async Task<ModelsResponse<Accessor>> GetAccessorByCardNumber(ByCardNumberRequest request)
		{
			return await ProcessRequest(request, async req =>
			{
				var response = GetResponse<ModelsResponse<Accessor>>();
				var items = await _facade.GetAccessorListAsync(new AgreementCardNumber
				{
					Criteria = req.CardNumber
				});
				response.Items = items.ToList();
				return response;
			});
		}

		public async Task<ModelsResponse<Accessor>> GetAccessorByIBan(ByIBanRequest request)
		{
			return await ProcessRequest(request, async req =>
			{
				var response = GetResponse<ModelsResponse<Accessor>>();
				var items = await _facade.GetAccessorListAsync(new AgreementIBan
				{
					Criteria = req.IBan
				});
				response.Items = items.ToList();
				return response;
			});
		}
	} }