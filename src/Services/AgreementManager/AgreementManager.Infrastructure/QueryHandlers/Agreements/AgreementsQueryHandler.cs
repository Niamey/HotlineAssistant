using AgreementManager.Domain.Agreements;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreements.Queries;
using Vostok.HotLineAssistant.Common.Helpers;
using Vostok.HotLineAssistant.Common.Requests.Common;
using Vostok.HotLineAssistant.Common.Requests.Specific;
using Vostok.HotLineAssistant.Common.Response.Common;

namespace Vostok.HotLineAssistant.AgreementManager.Infrastructure.QueryHandlers.Agreements
{
	public class AgreementsQueryHandler : IRequestHandler<AgreementsByIdQuery, ModelsResponse<AgreementDto>>,
		IRequestHandler<AgreementsByNumberQuery, ModelsResponse<AgreementDto>>,
		IRequestHandler<AgreementsByCardQuery, ModelsResponse<AgreementDto>>,
		IRequestHandler<AgreementsByIbanQuery, ModelsResponse<AgreementDto>>
	{
		private readonly IAgreementService _agreementService;
		private readonly IMapper _mapper;
		private readonly ILogger<AgreementsQueryHandler> _logger;
		public AgreementsQueryHandler(IAgreementService agreementService, IMapper mapper, ILogger<AgreementsQueryHandler> logger)
		{
			_agreementService = agreementService;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<ModelsResponse<AgreementDto>> Handle(AgreementsByIdQuery request,
			CancellationToken cancellationToken)
		{
			var response = new ModelsResponse<AgreementDto>();
			var items = await _agreementService.GetAgreementsById(new ByIdRequest{ Id = request.Id});
			response.Items = _mapper.Map<IEnumerable<AgreementDto>>(items).ToList();
			
			return response;
		}

		public async Task<ModelsResponse<AgreementDto>> Handle(AgreementsByNumberQuery request,
			CancellationToken cancellationToken)
		{
			var response = new ModelsResponse<AgreementDto>();
			var items = await _agreementService.GetAgreementsByNumber(new ByNumberRequest { Number = request.Number.Trim()});
			response.Items = _mapper.Map<IEnumerable<AgreementDto>>(items).ToList();
			
			return response;
		}

		public async Task<ModelsResponse<AgreementDto>> Handle(AgreementsByCardQuery request,
			CancellationToken cancellationToken)
		{
			var response = new ModelsResponse<AgreementDto>();
			var items = await _agreementService.GetAgreementsByCardNumber( new ByCardNumberRequest {CardNumber = request.Card.Trim()});
			response.Items = _mapper.Map<IEnumerable<AgreementDto>>(items).ToList();
			
			return response;
		}

		public async Task<ModelsResponse<AgreementDto>> Handle(AgreementsByIbanQuery request,
			CancellationToken cancellationToken)
		{
			var response = new ModelsResponse<AgreementDto>();
			var items = await _agreementService.GetAgreementsByIBan(new ByIBanRequest{ IBan = request.Iban.Trim()});
			response.Items = _mapper.Map<IEnumerable<AgreementDto>>(items).ToList();
			return response;
		}
	}
}