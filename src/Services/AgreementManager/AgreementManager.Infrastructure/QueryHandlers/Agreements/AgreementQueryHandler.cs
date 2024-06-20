using AgreementManager.Domain.Agreements;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Queries;
using Vostok.HotLineAssistant.Common.Requests.Common;
using Vostok.HotLineAssistant.Common.Requests.Specific;
using Vostok.HotLineAssistant.Common.Response.Common;

namespace Vostok.HotLineAssistant.AgreementManager.Infrastructure.QueryHandlers.Agreements
{

	public class AgreementQueryHandler : IRequestHandler<AgreementByIdQuery, ModelResponse<AgreementDto>>,
		IRequestHandler<AgreementByNumberQuery, ModelResponse<AgreementDto>>,
		IRequestHandler<AgreementByCardQuery, ModelResponse<AgreementDto>>,
		IRequestHandler<AgreementByIbanQuery, ModelResponse<AgreementDto>>
	{
		private readonly IAgreementService _agreementService;
		private readonly IMapper _mapper;
		private ILogger<AgreementQueryHandler> _logger;
		public AgreementQueryHandler(IAgreementService agreementService, IMapper mapper, ILogger<AgreementQueryHandler> logger)
		{
			_agreementService = agreementService;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<ModelResponse<AgreementDto>> Handle(AgreementByNumberQuery request,
			CancellationToken cancellationToken)
		{
			var response = new ModelResponse<AgreementDto>();
			var item = await _agreementService.GetAgreementByNumber(new ByNumberRequest {Number = request.Number});
			response.Item = _mapper.Map<AgreementDto>(item);

			return response;
		}

		public async Task<ModelResponse<AgreementDto>> Handle(AgreementByIdQuery request,
			CancellationToken cancellationToken)
		{
			var response = new ModelResponse<AgreementDto>();
			var item = await _agreementService.GetAgreementById(new ByIdRequest{Id = request.Id});
			response.Item = _mapper.Map<AgreementDto>(item);

			return response;
		}

		public async Task<ModelResponse<AgreementDto>> Handle(AgreementByCardQuery request,
			CancellationToken cancellationToken)
		{
			var response = new ModelResponse<AgreementDto>();
			var item = await _agreementService.GetAgreementByCardNumber(new ByCardNumberRequest
				{CardNumber = request.Card});
			response.Item = _mapper.Map<AgreementDto>(item);

			return response;
		}

		public async Task<ModelResponse<AgreementDto>> Handle(AgreementByIbanQuery request,
			CancellationToken cancellationToken)
		{
			var response = new ModelResponse<AgreementDto>();
			var item = await _agreementService.GetAgreementByIBan(new ByIBanRequest{IBan = request.Iban});
			response.Item = _mapper.Map<AgreementDto>(item);
			
			return response;
		}
	}
}
