using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Abstractions;
using Vostok.Hotline.Api.References.Models;

namespace Vostok.Hotline.Api.References.Managers
{
	public class DivisionApiManager
	{
		private readonly IDivisionApiService _divisionApiService;
		public DivisionApiManager(IDivisionApiService divisionApiService)
		{
			_divisionApiService = divisionApiService;
		}

		public async Task<DivisionApiModel> GetDivisionAsync(int divisionId, CancellationToken cancellationToken)
		{
			return await _divisionApiService.GetDivisionAsync(divisionId, cancellationToken);
		}
		
		public async Task<string> GetDivisionNameAsync(int divisionId, CancellationToken cancellationToken)
		{
			var result = await GetDivisionAsync(divisionId, cancellationToken);

			return result?.Name;
		}
	}
}
