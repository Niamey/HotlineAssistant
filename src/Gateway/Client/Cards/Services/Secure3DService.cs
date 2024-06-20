using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Managers;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Gateway.Client.Cards.Mapper;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Services
{
	public class Secure3DService
	{
		private readonly Secure3DApiManager _secure3DApiManager;

		public Secure3DService(Secure3DApiManager secure3DApiManager)
		{
			_secure3DApiManager = secure3DApiManager;
		}

		public async Task<Secure3DInfoViewModel> GetStatusAsync(Secure3DInfoQuery request, CancellationToken cancellationToken)
		{
			var result = await _secure3DApiManager.GetStatusAsync(request.SolarId, request.CardId.Value, cancellationToken);
			if (result != null)
				return result.ToSecure3DInfoViewModel();
			else
				throw new NotFoundRequestException();
		}

		public async Task<Secure3DStatusHistoryViewModel> GetStatusHistoryAsync(Secure3DStatusHistoryQuery request, CancellationToken cancellationToken)
		{
			var result = await _secure3DApiManager.GetStatusHistoryAsync(request.SolarId, request.CardId.Value, cancellationToken);
			if (result != null)
				return result.MapToSecure3DStatusHistoryViewModel();
			else
				throw new NotFoundRequestException();
		}
	}
}
