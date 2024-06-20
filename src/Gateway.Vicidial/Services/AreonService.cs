using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Gateway.Vicidial.Requests.Commands;
using Vostok.Hotline.Gateway.Vicidial.ViewModels;
using Vostok.Hotline.Api.CRM.Managers;
using Vostok.Hotline.Api.CRM.SearchRequests;
using Vostok.Hotline.Gateway.Vicidial.Mapper;
using Vostok.Hotline.Data.Repository.Core.Managers;
using Vostok.Hotline.Data.EF.Enums;

namespace Vostok.Hotline.Gateway.Vicidial.Services
{
	public class AreonService
	{
		private readonly CounterpartApiManager _counterpartApiManager;
		private readonly AreonNewCallMapper _areonNewCallMapper;
		private readonly LoggingRequestManager _loggingRequestManager;

		public AreonService(CounterpartApiManager counterpartApiManager, AreonNewCallMapper areonNewCallMapper, LoggingRequestManager loggingRequestManager)
		{
			_counterpartApiManager = counterpartApiManager;
			_areonNewCallMapper = areonNewCallMapper;
			_loggingRequestManager = loggingRequestManager;
		}

		public async Task<AreonNewCallResponseViewModel> OpenNewCallAsync(AreonNewCallCommand request, CancellationToken cancellationToken)
		{
			var searchRequest = new CounterpartSearchByPhoneRequest
			{
				SearchFilter = $"+{request.Number_a}"
			};

			var result = await _counterpartApiManager.SearchFirstByPhoneAsync(searchRequest, cancellationToken);

			await _loggingRequestManager.AddAsync(LoggingSystemTypeEnum.Vicidile, LoggingOperationTypeEnum.IncomingDial, request, result, cancellationToken);

			return await _areonNewCallMapper.MapToAreonNewCallResponseViewModelAsync(result, cancellationToken);
		}

		public async Task<AreonNewDetailCallResponseViewModel> OpenNewDetailCallAsync(AreonNewDetailCallCommand request, CancellationToken cancellationToken)
		{
			var searchRequest = new CounterpartSearchByPhoneRequest
			{
				SearchFilter = $"+{request.Number_a}"
			};

			var result = await _counterpartApiManager.SearchFirstByPhoneAsync(searchRequest, cancellationToken);

			await _loggingRequestManager.AddAsync(LoggingSystemTypeEnum.Vicidile, LoggingOperationTypeEnum.IncomingDial, request, result, cancellationToken);

			return await _areonNewCallMapper.MapToAreonNewDetailCallResponseViewModelAsync(result, cancellationToken);
		}
	}
}