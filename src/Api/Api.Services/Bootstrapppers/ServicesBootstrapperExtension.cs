using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.ApiServices;
using Vostok.Hotline.Api.Services.Managers;
using Vostok.Hotline.Api.Services.Providers;

namespace Vostok.Hotline.Api.Services.Bootstrappers
{
	public static class ServicesBootstrapperExtension
	{
		public static void AddApiServicesRules(this IServiceCollection services)
		{
			services.AddSingleton<ISharedLinkApiService, SharedLinkApiService>(serviceProvider =>
			{
				return new SharedLinkApiService(serviceProvider);
			});

			services.AddSingleton<IMessengerApiService, MessengerApiService>(serviceProvider =>
			{
				return new MessengerApiService(serviceProvider);
			});

			services.AddSingleton<VostokMessengerManager>();
			services.AddSingleton<VostokMessengerSettings>(serviceProvider =>
			{
				var vostokMessengerManager = serviceProvider.GetRequiredService<VostokMessengerManager>();
				VostokMessengerSettings settings = vostokMessengerManager.GetMessengerSettingsAsync().GetAwaiter().GetResult();
				
				return settings;
			});

			services.AddSingleton<EmailManager>();
			services.AddSingleton<EmailSettings>(serviceProvider =>
			{
				var emailManager = serviceProvider.GetRequiredService<EmailManager>();
				EmailSettings settings = emailManager.GetEmailSettingsAsync().GetAwaiter().GetResult();

				return settings;
			});

			services.AddSingleton<IMessengerProvider, VostokMessengerProvider>();
			services.AddSingleton<IMessengerProvider, EmailProvider>();
			services.AddSingleton<SharedLinkApiManager>();
			services.AddSingleton<MessengerApiManager>();

			services.AddSingleton<IMDESApiService, MDESApiService>(serviceProvider =>
			{
				return new MDESApiService(serviceProvider);
			});
			services.AddSingleton<MDESApiManager>();

			services.AddSingleton<ICardImageApiService, CardImageApiService>(serviceProvider =>
			{
				return new CardImageApiService(serviceProvider);
			});
			services.AddSingleton<CardImageApiManager>();

			services.AddSingleton<IBVRApiService, BVRApiService>(serviceProvider =>
			{
				return new BVRApiService(serviceProvider);
			});
			services.AddSingleton<BVRApiManager>();

			services.AddSingleton<IStatementApiService, StatementApiService>(serviceProvider =>
			{
				return new StatementApiService(serviceProvider);
			});
			services.AddSingleton<StatementApiManager>();
		}
	}
}
