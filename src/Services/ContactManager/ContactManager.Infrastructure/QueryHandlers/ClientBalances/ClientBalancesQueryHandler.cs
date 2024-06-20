using MediatR;
using Solar.Models.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vostok.HotLineAssistant.ContactManager.Application.ClientBalances.Queries;
using Vostok.HotLineAssistant.Infrastucture.Services;

namespace Vostok.HotLineAssistant.ContactManager.Infrastructure.QueryHandlers.ClientBalances
{
	public class ClientBalancesQueryHandler : IRequestHandler<ClientBalancesByIdQuery, IEnumerable<Balance>>,
		IRequestHandler<ClientBalancesByNumberQuery, IEnumerable<Balance>>, 
		IRequestHandler<ClientBalancesByCardQuery, IEnumerable<Balance>>, 
		IRequestHandler<ClientBalancesByIbanQuery, IEnumerable<Balance>>
	{
		private readonly IClientBalanceService _clientBalanceService;

		public ClientBalancesQueryHandler(IClientBalanceService clientBalanceService)
		{
			_clientBalanceService =
				clientBalanceService ?? throw new ArgumentNullException(nameof(clientBalanceService));
		}

		public async Task<IEnumerable<Balance>> Handle(ClientBalancesByIdQuery request, CancellationToken cancellationToken)
		{
			if(request == null)
				throw new ArgumentNullException(nameof(request));
			return await _clientBalanceService.GetBalanceById(request.Id);
		}

		public async Task<IEnumerable<Balance>> Handle(ClientBalancesByNumberQuery request, CancellationToken cancellationToken)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));
			return await _clientBalanceService.GetBalanceByNumber(request.Number);
		}

		public async Task<IEnumerable<Balance>> Handle(ClientBalancesByCardQuery request, CancellationToken cancellationToken)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));
			return await _clientBalanceService.GetBalanceByCardNumber(request.Card);
		}

		public async Task<IEnumerable<Balance>> Handle(ClientBalancesByIbanQuery request, CancellationToken cancellationToken)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));
			return await _clientBalanceService.GetBalanceByIBan(request.Iban);
		}
	}
}