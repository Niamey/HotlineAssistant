using System;
using Microsoft.Extensions.Configuration;
using Vostok.Hotline.Core.Common.Extensions;

namespace Vostok.Hotline.Core.Common.Configurations
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
			string result = Environment.GetEnvironmentVariable(key);
			if(string.IsNullOrEmpty(result) && key.Contains("."))
				result = Environment.GetEnvironmentVariable(key.Replace(".", "_"));

			if (string.IsNullOrEmpty(result))
				result = _configuration.GetValue<string>(key);

			return result;
		}

		public string GetEnvironmentPasswordVariable(string key)
		{			
			return GetEnvironmentVariable(key).GetSecretVariable();
		}

		public string ProjectName => "Hotline Assistance";
		public string EnvironmentName => GetEnvironmentVariable("HOTLINE.ENVIRONMENT"); 

		public bool IsLive => EnvironmentName == "Live";
	}
}