using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Addresses.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Addresses.Services;
using Vostok.Hotline.Gateway.Client.Addresses.Services.SearchRequests;
using Vostok.Hotline.Gateway.Client.Addresses.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Addresses.Requests.Handlers
{
	public class AddressDetailQueryHandler : IRequestHandler<AddressDetailQuery, AddressViewModel>
	{
		private readonly AddressService _addressService;

		public AddressDetailQueryHandler(AddressService addressService)
		{
			_addressService = addressService;
		}

		public async Task<AddressViewModel> Handle(AddressDetailQuery request, CancellationToken cancellationToken)
		{
			var searchRequest = new AddressDetailRequest
			{
				AddressId = request.AddressId
			};

			return await _addressService.GetAddressDetailAsync(searchRequest, cancellationToken);
		}
	}
}
