using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Vostok.HotLineAssistant.AccessorManager.Application.Accessors.Dtos;
using Vostok.HotLineAssistant.AccessorManager.Application.Accessors.Queries;
using Vostok.HotLineAssistant.AccessorManager.Domain.Accessors;
using Vostok.HotLineAssistant.Common.Requests.Common;
using Vostok.HotLineAssistant.Common.Requests.Specific;
using Vostok.HotLineAssistant.Common.Response.Common;

namespace Vostok.HotLineAssistant.AccessorManager.Infrastructure.QueryHandlers
{
	public class AccessorQueryHandler : IRequestHandler<AccessorByIdQuery, ModelsResponse<AccessorDto>>,
		IRequestHandler<AccessorByNumberQuery, ModelsResponse<AccessorDto>>,
		IRequestHandler<AccessorByCardQuery, ModelsResponse<AccessorDto>>,
		IRequestHandler<AccessorByIbanQuery, ModelsResponse<AccessorDto>>
	{
		private readonly IAccessorService _accessorService;
		private readonly IMapper _mapper;
		private readonly ILogger<AccessorQueryHandler> _logger;

		public AccessorQueryHandler(IAccessorService accessorService, IMapper mapper, ILogger<AccessorQueryHandler> logger)
		{
			_accessorService = accessorService;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<ModelsResponse<AccessorDto>> Handle(AccessorByIdQuery request,
			CancellationToken cancellationToken)
		{
			var response = new ModelsResponse<AccessorDto>();
			var items = await _accessorService.GetAccessorById(new ByIdRequest {Id = request.Id});
			//response.Items = _mapper.Map<IEnumerable<AccessorDto>>(items).ToList();

			return response;
		}

		public async Task<ModelsResponse<AccessorDto>> Handle(AccessorByNumberQuery request,
			CancellationToken cancellationToken)
		{
			var response = new ModelsResponse<AccessorDto>();
			var items = await _accessorService.GetAccessorByNumber(new ByNumberRequest {Number = request.Number});
			//response.Items = _mapper.Map<IEnumerable<AccessorDto>>(items).ToList();

			return response;
		}

		public async Task<ModelsResponse<AccessorDto>> Handle(AccessorByCardQuery request,
			CancellationToken cancellationToken)
		{
			var response = new ModelsResponse<AccessorDto>();
			var items = await _accessorService.GetAccessorByCardNumber(new ByCardNumberRequest {CardNumber = request.Card});
			//response.Items = _mapper.Map<IEnumerable<AccessorDto>>(items).ToList();

			return response;
		}

		public async Task<ModelsResponse<AccessorDto>> Handle(AccessorByIbanQuery request,
			CancellationToken cancellationToken)
		{
			var response = new ModelsResponse<AccessorDto>();
			var items = await _accessorService.GetAccessorByIBan(new ByIBanRequest {IBan = request.Iban});
			//response.Items = _mapper.Map<IEnumerable<AccessorDto>>(items).ToList();

			return response;
		}
	}
}
