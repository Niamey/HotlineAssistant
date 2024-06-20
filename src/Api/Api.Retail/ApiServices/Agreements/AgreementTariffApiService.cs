using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Api.Retail.Mapper.Agreements;
using Vostok.Hotline.Api.Retail.Models;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.Managers;

namespace Vostok.Hotline.Api.Retail.ApiServices.Agreements
{
	internal class AgreementTariffApiService : IAgreementTariffApiService
	{

		internal AgreementTariffApiService(IServiceProvider serviceProvider)
		{
		}

		public async Task<SearchRowsResponseRowModel<TariffApiModel>> GetAllTariffAsync(long? clientId, long agreementId, CancellationToken cancellationToken)
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

		public async Task<TariffApiModel> GetCurrentTariffAsync(long? clientId, long agreementId, CancellationToken cancellationToken)
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
