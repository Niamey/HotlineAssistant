using System;
using Microsoft.Extensions.Configuration;
using Vostok.HotLineAssistant.WebApi.Extensions;

namespace Vostok.HotLineAssistant.WebApi
{
	public class HotlineEnvironment
	{
		private readonly IConfiguration _configuration;
		public HotlineEnvironment(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GetEnvironmentVariable(string key)
		{
			var result = Environment.GetEnvironmentVariable(key);
			if (string.IsNullOrEmpty(result))
				result = _configuration.GetValue<string>(key);

			return result;
		}

		public string GetEnvironmentPasswordVariable(string key)
		{
			return GetEnvironmentVariable(key).GetSecretVariable();
		}

		public string EnvironmentName => GetEnvironmentVariable("HOTLINE.ENVIRONMENT");
	}
}