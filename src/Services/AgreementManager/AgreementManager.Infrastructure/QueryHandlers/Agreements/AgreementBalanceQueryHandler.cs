using AgreementManager.Domain.Agreements;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos;
using Vostok.HotLineAssistant.AgreementManager.Application.AgreementBalances.Queries;
using Vostok.HotLineAssistant.Common.Requests.Common;
using Vostok.HotLineAssistant.Common.Requests.Specific;
using Vostok.HotLineAssistant.Common.Response.Common;

namespace Vostok.HotLineAssistant.AgreementManager.Infrastructure.QueryHandlers.Agreements
{
	public class AgreementBalanceQueryHandler : IRequestHandler<AgreementBalanceByIdQuery, ModelResponse<BalanceDto>>,
		IRequestHandler<AgreementBalanceByNumberQuery, ModelResponse<BalanceDto>>,
		IRequestHandler<AgreementBalanceByCardQuery, ModelResponse<BalanceDto>>,
		IRequestHandler<AgreementBalanceByIbanQuery, ModelResponse<BalanceDto>>
	{
		private readonly IAgreementBalanceService _agreementBalanceService;
		private readonly IMapper _mapper;
		private readonly ILogger<AgreementBalanceQueryHandler> _logger;

		public AgreementBalanceQueryHandler(IAgreementBalanceService agreementBalanceService, IMapper mapper, ILogger<AgreementBalanceQueryHandler> logger)
		{
			_agreementBalanceService = agreementBalanceService;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<ModelResponse<BalanceDto>> Handle(AgreementBalanceByNumberQuery request, CancellationToken cancellationToken)
		{
			var response = new ModelResponse<BalanceDto>();
			var item = await _agreementBalanceService.GetBalanceByNumber(new ByNumberRequest {Number = request.Number.Trim()});
			response.Item = _mapper.Map<BalanceDto>(item);
			
			return response;
		}

		public async Task<ModelResponse<BalanceDto>> Handle(AgreementBalanceByIdQuery request,
			CancellationToken cancellationToken)
		{
			var response = new ModelResponse<BalanceDto>();
			var item = await _agreementBalanceService.GetBalanceById(new ByIdRequest {Id = request.Id});
			response.Item = _mapper.Map<BalanceDto>(item);
			
			return response;
		}

		public async Task<ModelResponse<BalanceDto>> Handle(AgreementBalanceByCardQuery request, CancellationToken cancellationToken)
		{
			var response = new ModelResponse<BalanceDto>();
			var item = await _agreementBalanceService.GetBalanceByCardNumber(new ByCardNumberRequest
				{CardNumber = request.Card.Trim()});
				response.Item = _mapper.Map<BalanceDto>(item);
			
			return response;
		}

		public async Task<ModelResponse<BalanceDto>> Handle(AgreementBalanceByIbanQuery request,
			CancellationToken cancellationToken)
		{
			var response = new ModelResponse<BalanceDto>();
			var item = await _agreementBalanceService.GetBalanceByIBan(new ByIBanRequest {IBan = request.Iban.Trim()});
			response.Item = _mapper.Map<BalanceDto>(item);
			
			return response;
		}
	}
}