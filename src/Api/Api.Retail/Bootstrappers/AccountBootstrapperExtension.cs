using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Retail.ApiServices;
using Vostok.Hotline.Api.Retail.Managers;

namespace Vostok.Hotline.Api.Retail.Bootstrappers
{
	public static class AccountBootstrapperExtension
	{
		public static void AddApiAccountRules(this IServiceCollection services)
		{
			services.AddSingleton<IAccountApiService, AccountApiService>(serviceProvider => 
			{
				return new AccountApiService(serviceProvider); 
			});
			services.AddSingleton<AccountApiManager>();
		}
	}
}
