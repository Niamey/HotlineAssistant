using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Models;

namespace Vostok.Hotline.Api.References.Abstractions
{
	public interface IDivisionApiService
	{
		Task<DivisionApiModel> GetDivisionAsync(int divisionId, CancellationToken cancellationToken);
	}
}