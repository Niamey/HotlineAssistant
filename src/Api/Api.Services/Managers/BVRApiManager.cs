using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Services.Abstractions;

namespace Vostok.Hotline.Api.Services.Managers
{
	public class BVRApiManager
	{
		private readonly IBVRApiService _bvrApiService;

		public BVRApiManager(IBVRApiService bvrApiService) {
			_bvrApiService = bvrApiService;
		}	

		public async Task<StatusCommandApiViewModel> AddPermanentBlockingAsync(string phoneNumber, string description, CancellationToken cancellationToken)
		{
			return await _bvrApiService.AddPermanentBlockingAsync(phoneNumber, description, cancellationToken);
		}
	}
}
