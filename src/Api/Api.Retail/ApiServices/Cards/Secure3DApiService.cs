using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.ApiServices.Cards
{
	internal class Secure3DApiService : ISecure3DApiService
	{
		internal Secure3DApiService(IServiceProvider serviceProvider)
		{
		}
				
		public async Task<Secure3DInfoApiModel> GetStatusAsync(long? clientId, long cardId, CancellationToken cancellationToken)
		{
			return new Secure3DInfoApiModel
			{
				Status = Secure3DStatusEnum.Active,
				Tariff = "Безкоштовно",
				Phone = "(044) 364 21 04",
				IsFinancePhone = true
			};
		}

		public async Task<Secure3DStatusHistoryApiModel> GetStatusHistoryAsync(long? clientId, long cardId, CancellationToken cancellationToken)
		{
			return new Secure3DStatusHistoryApiModel
			{
				CurrentStatus = Secure3DStatusHistoryEnum.Active,
				DateChangeStatus = DateTime.Now,
				Comment = "Оператор"
			};
		}
	}
}