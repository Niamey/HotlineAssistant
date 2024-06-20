using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Gateway.Client.Counterpart.ViewModels;
using Vostok.Hotline.Gateway.Client.Counterpart.Mapper;
using System.Linq;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Api.References.Managers;
using Vostok.Hotline.Api.CRM.Managers;
using Vostok.Hotline.Api.CRM.SearchRequests;
using Vostok.Hotline.Api.CRM.Models;

namespace Vostok.Hotline.Gateway.Client.Counterpart.Services
{
	public class ClientService
	{
		private readonly ClientMapper _clientMapper;
		private readonly AddressApiManager _addressManager;
		private readonly CounterpartApiManager _counterpartApiManager;

		public ClientService(ClientMapper clientMapper,
			AddressApiManager addressManager,
			CounterpartApiManager counterpartApiManager)
		{
			_clientMapper = clientMapper;
			_addressManager = addressManager;
			_counterpartApiManager = counterpartApiManager;
		}

		private async Task<SearchPagedResponseRowModel<CounterpartApiModel>> ClientSearchAsync(CounterpartSearchRequest request, CancellationToken cancellationToken)
		{
			var counterparts = await _counterpartApiManager.SearchAsync(request, cancellationToken);
			if (counterparts?.Rows?.Count > 0)
			{
				var counterpatrsAddresses = counterparts.Rows.SelectMany(x => x.Addresses.Where(y => y.AddressId.HasValue)).ToList();
				var addressIds = counterpatrsAddresses.Select(x => x.AddressId.Value).Distinct().ToArray();
				var addresses = await _addressManager.GetAdresseCollectionAsync(addressIds, cancellationToken);

				foreach (var addr in counterpatrsAddresses)
				{
					if (addresses.ContainsKey(addr.AddressId.Value))
					{
						var toMapAddr = addresses[addr.AddressId.Value];
						addr.ToCounterpartAddressModel(toMapAddr);
					}
				}
			} 

			return counterparts;
		}

		public async Task<SearchPagedResponseRowModel<ClientViewModel>> ClientListAsync(CounterpartSearchRequest request, CancellationToken cancellationToken)
		{
			var result = await ClientSearchAsync(request, cancellationToken);
			
			return await _clientMapper.MapToClientViewModelAsync(result, cancellationToken);
		}

		public async Task<ClientViewModel> ClientDetailAsync(CounterpartSearchRequest request, CancellationToken cancellationToken)
		{
			var result = await ClientSearchAsync(request, cancellationToken);
			if (result?.Rows?.Count > 0)
			{
				var detail = result.Rows.First();
				return await _clientMapper.MapToClientViewModelAsync(detail, cancellationToken);
			}
			else
				throw new NotFoundRequestException();
		}		

		public async Task<ClientCountViewModel> ClientCountAsync(CounterpartCountSearchRequest request, CancellationToken cancellationToken)
		{
			var result = await _counterpartApiManager.GetTotalCountAsync(request, cancellationToken);

			return result.ToClientCountViewModel();
		}
	}
}
