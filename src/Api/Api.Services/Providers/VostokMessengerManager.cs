using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Abstractions;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Api.Services.Providers
{
	internal class VostokMessengerManager
	{
		private readonly EnvironmentManager _environmentManager;
		private readonly ISessionContext _sessionContext;
		public VostokMessengerManager(IServiceProvider serviceProvider)
		{
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
			_sessionContext = serviceProvider.GetRequiredService<ISessionContext>();
		}

		public async Task<VostokMessengerSettings> GetMessengerSettingsAsync()
		{
			return new VostokMessengerSettings(await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.VostokMessengerProvider.Url, _sessionContext.CancellationToken),
									await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.VostokMessengerProvider.Login, _sessionContext.CancellationToken),
									await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.VostokMessengerProvider.Password, _sessionContext.CancellationToken));
		}
	}
}