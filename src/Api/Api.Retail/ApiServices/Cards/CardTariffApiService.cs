using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Api.Retail.Mapper.Cards;
using Vostok.Hotline.Api.Retail.Models;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Api.Retail.ApiServices.Cards
{
	internal class CardTariffApiService : ICardTariffApiService
	{

		internal CardTariffApiService(IServiceProvider serviceProvider)
		{
		}

		public async Task<SearchRowsResponseRowModel<TariffApiModel>> GetAllTariffAsync(long? clientId, long cardId, CancellationToken cancellationToken)
		{
			var result = new TariffCollectionApiModel {
				new TariffApiModel {
					TariffName = "Тариф",
					TariffId = 2,
					TariffStart = "26.11.2020",
					TariffUrl = "tarif-BVR.pdf"
				}
			};

			return result.ToTariffListApiModel();
		}

		public async Task<TariffApiModel> GetCurrentTariffAsync(long? clientId, long cardId, CancellationToken cancellationToken)
		{
			var result = new TariffApiModel
			{
				TariffName = "Тариф",
				TariffId = 2,
				TariffStart = "26.11.2020",
				TariffUrl = "tarif-BVR.pdf"
			};

			return result;
		}
	}
}
