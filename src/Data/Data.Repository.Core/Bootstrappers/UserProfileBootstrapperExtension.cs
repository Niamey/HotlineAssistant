using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Data.Repository.Core.Bootstrappers
{
	public static class UserProfileBootstrapperExtension
	{
		public static void AddUserProfileSettingRules(this IServiceCollection services)
		{
			services.AddScoped<UserProfileManager>();
		}
	}
}
