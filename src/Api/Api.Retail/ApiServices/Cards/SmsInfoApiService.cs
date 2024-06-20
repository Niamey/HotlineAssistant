using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.ApiServices.Cards
{
	internal class SmsInfoApiService : ISmsInfoApiService
	{
		internal SmsInfoApiService(IServiceProvider serviceProvider)
		{
		}
				
		public async Task<SmsInfoApiModel> GetStatusAsync(long? clientId, long cardId, CancellationToken cancellationToken)
		{
			return new SmsInfoApiModel
			{
				Status = SmsInfoStatusEnum.Active,
				Tariff = "Безкоштовно",
				Phone = "(050) 364 21 04",
				IsFinancePhone = false
			};
		}

		public async Task<SmsInfoChangeStatusApiModel> ChangeStatusAsync(long? clientId, long cardId, SmsInfoChangeStatusEnum status, CancellationToken cancellationToken)
		{
			return new SmsInfoChangeStatusApiModel
			{
				Status = status,
				Message = "message"
			};
		}

		public async Task<SmsInfoHistoryStatusApiModel> HistoryStatusAsync(long? clientId, long cardId, CancellationToken cancellationToken)
		{
			return new SmsInfoHistoryStatusApiModel
			{
				CurrentStatus = SmsInfoHistoryStatusEnum.Active,
				DateChangeStatus = DateTime.Now,
				Comment = "Клієнт"
			};
		}
	}
}