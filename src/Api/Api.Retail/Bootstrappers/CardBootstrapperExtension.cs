using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Api.Retail.ApiServices.Cards;
using Vostok.Hotline.Api.Retail.Managers;

namespace Vostok.Hotline.Api.Retail.Bootstrappers
{
	public static class CardBootstrapperExtension
	{
		public static void AddApiCardRules(this IServiceCollection services)
		{
			services.AddSingleton<ICardApiService, CardApiService>(serviceProvider =>
			{
				return new CardApiService(serviceProvider);
			});
			services.AddSingleton<CardApiManager>();

			services.AddSingleton<ICardHistoryApiService, CardHistoryApiService>(serviceProvider =>
			{
				return new CardHistoryApiService(serviceProvider);
			});
			services.AddSingleton<CardHistoryApiManager>();

			services.AddSingleton<ISmsInfoApiService, SmsInfoApiService>(serviceProvider =>
			{
				return new SmsInfoApiService(serviceProvider);
			});
			services.AddSingleton<SmsInfoApiManager>();

			services.AddSingleton<ISecure3DApiService, Secure3DApiService>(serviceProvider =>
			{
				return new Secure3DApiService(serviceProvider);
			});
			services.AddSingleton<Secure3DApiManager>();

			services.AddSingleton<ICardTariffApiService, CardTariffApiService>(serviceProvider =>
			{
				return new CardTariffApiService(serviceProvider);
			});
			services.AddSingleton<CardTariffApiManager>();

			services.AddSingleton<ICardLimitApiService, CardLimitApiService>(serviceProvider =>
			{
				return new CardLimitApiService(serviceProvider);
			});
			services.AddSingleton<CardLimitApiManager>();

			services.AddSingleton<ICardBlockingApiService, CardBlockingApiService>(serviceProvider =>
			{
				return new CardBlockingApiService(serviceProvider);
			});
			services.AddSingleton<CardBlockingApiManager>();

			services.AddSingleton<CardUnlockingApiManager>();
		}
	}
}
