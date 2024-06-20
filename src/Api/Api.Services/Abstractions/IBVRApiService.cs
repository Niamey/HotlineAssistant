using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;

namespace Vostok.Hotline.Api.Services.Abstractions
{
	public interface IBVRApiService
	{
		Task<StatusCommandApiViewModel> AddPermanentBlockingAsync(string phoneNumber, string description, CancellationToken cancellationToken);
	}
}
