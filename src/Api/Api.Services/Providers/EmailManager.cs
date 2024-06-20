using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Abstractions;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Api.Services.Providers
{
	internal class EmailManager
	{
		private readonly EnvironmentManager _environmentManager;
		private readonly ISessionContext _sessionContext;
		public EmailManager(IServiceProvider serviceProvider)
		{
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
			_sessionContext = serviceProvider.GetRequiredService<ISessionContext>();
		}

		public async Task<EmailSettings> GetEmailSettingsAsync()
		{
			return new EmailSettings(await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.EmailProvider.From, _sessionContext.CancellationToken),
									await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.EmailProvider.Smtp.Host, _sessionContext.CancellationToken),
									Converter.ToInt32(await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.EmailProvider.Smtp.Port, _sessionContext.CancellationToken)),
									await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.EmailProvider.Smtp.Login, _sessionContext.CancellationToken),
									await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.EmailProvider.Smtp.Password, _sessionContext.CancellationToken),
									Converter.ToBoolean(await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.EmailProvider.Smtp.CheckCertRevocation, _sessionContext.CancellationToken)));			
		}
	}
}