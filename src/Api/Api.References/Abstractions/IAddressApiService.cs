using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Models;

namespace Vostok.Hotline.Api.References.Abstractions
{
	public interface IAddressApiService
	{
		Task<AddressApiModel> GetAddressAsync(int addressId, CancellationToken cancellationToken);
	}
}