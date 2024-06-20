using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;

namespace Vostok.Hotline.Api.Retail.Abstractions
{
	public interface ICardBlockingApiService
	{
		Task<StatusCommandApiViewModel> SendEmailAsync(string subject, string body, CancellationToken cancellationToken);		
	}
}