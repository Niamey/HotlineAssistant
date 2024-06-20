using System;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Configurations;

namespace Vostok.Hotline.Api.Services.Providers
{
	public class VostokMessengerSettings
	{
		public VostokMessengerSettings()
		{
		}
		public VostokMessengerSettings(string url, string login, string password)
		{
			Url = url;
			Login = login;
			Password = password;
		}

		public string Url { get; private set; }
  		public string Login { get; private set; }
		public string Password { get; private set; }

	}
}