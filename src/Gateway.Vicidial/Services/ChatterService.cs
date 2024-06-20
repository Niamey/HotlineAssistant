using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Gateway.Vicidial.Requests.Commands;
using Vostok.Hotline.Gateway.Vicidial.ViewModels;
using Vostok.Hotline.Api.CRM.Managers;
using Vostok.Hotline.Api.CRM.SearchRequests;
using Vostok.Hotline.Gateway.Vicidial.Mapper;
using Vostok.Hotline.Data.Repository.Core.Managers;
using Vostok.Hotline.Data.EF.Enums;
using Vostok.Hotline.Gateway.Vicidial.Requests.Queries;

namespace Vostok.Hotline.Gateway.Vicidial.Services
{
	public class ChatterService
	{
		private readonly CounterpartApiManager _counterpartApiManager;
		private readonly ChatterNewCallMapper _chatterNewCallMapper;
		private readonly LoggingRequestManager _loggingRequestManager;

		public ChatterService(CounterpartApiManager counterpartApiManager, ChatterNewCallMapper chatterNewCallMapper, LoggingRequestManager loggingRequestManager)
		{
			_counterpartApiManager = counterpartApiManager;
			_chatterNewCallMapper = chatterNewCallMapper;
			_loggingRequestManager = loggingRequestManager;
		}

		public async Task<ChatterNewCallResponseViewModel> OpenNewCallAsync(ChatterNewCallQuery request, CancellationToken cancellationToken)
		{
			var searchRequest = new CounterpartSearchByPhoneRequest
			{
				SearchFilter = $"+{request.FinancePhone}"
			};

			var result = await _counterpartApiManager.SearchFirstByPhoneAsync(searchRequest, cancellationToken);
			if (result != null)
			{
				await _loggingRequestManager.AddAsync(LoggingSystemTypeEnum.Chatter, LoggingOperationTypeEnum.IncomingDial, request, result, cancellationToken);
				return await _chatterNewCallMapper.MapToChatterNewCallResponseViewModelAsync(result, cancellationToken);
			}
			else
			{
				var exception = new NotFoundRequestException();
				await _loggingRequestManager.AddAsync(LoggingSystemTypeEnum.Chatter, LoggingOperationTypeEnum.IncomingDial, request, exception, cancellationToken);
				throw exception;
			}				
		}
	}
}