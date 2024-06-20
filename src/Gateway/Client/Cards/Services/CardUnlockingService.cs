using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Managers;
using Vostok.Hotline.Core.Common.Enums.Cards;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Data.Repository.Core.Managers;
using Vostok.Hotline.Gateway.Client.Cards.Mapper;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Commands;

namespace Vostok.Hotline.Gateway.Client.Cards.Services
{
	public class CardUnlockingService
	{
		private readonly LoggingRequestManager _loggingRequest;
		private readonly CardUnlockingApiManager _unlockingApiManager;

		public CardUnlockingService(LoggingRequestManager loggingRequest, CardUnlockingApiManager unlockingApiManager)
		{
			_loggingRequest = loggingRequest;
			_unlockingApiManager = unlockingApiManager;
		}

		public async Task<StatusCommandViewModel> CardUnlockingFailedAsync(CardUnlockingFailedCommand request, CancellationToken cancellationToken)
		{
			await _loggingRequest.AddAsync(Data.EF.Enums.LoggingSystemTypeEnum.Hotline, Data.EF.Enums.LoggingOperationTypeEnum.UnlockingInsufficientData, request, null, cancellationToken);
			return new StatusCommandViewModel()
			{
				Success = true,
				Message = "Дані успішно залоговані"
			};
		}

		public async Task<StatusCommandViewModel> CardUnlockingAsync(CardUnlockingCommand request, CancellationToken cancellationToken)
		{
			var result = await _unlockingApiManager.UnlockingAsync(request.CardId.Value, request.Comment, cancellationToken);
			return result.MapToStatusCommandViewModel();
		}

		public async Task<StatusCommandViewModel> CardUnlockingLockAsync(CardUnlockingLockCommand request, CancellationToken cancellationToken)
		{
			var result = await _unlockingApiManager.LockingAsync(request.CardId.Value, CardStatusEnum.Suspend, request.Comment, cancellationToken);
			return result.MapToStatusCommandViewModel();
		}
	}
}
