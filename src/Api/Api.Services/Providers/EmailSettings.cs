namespace Vostok.Hotline.Api.Services.Providers
{
	public class EmailSettings
	{
		public EmailSettings()
		{
		}

		public EmailSettings(string from, string smtpHost, int smtpPort, string smtpLogin, string smtpPassword, bool checkCertRevocation)
		{
			From = from;
			SmtpHost = smtpHost;
			SmtpPort = smtpPort;
			SmtpLogin = smtpLogin;
			SmtpPassword = smtpPassword;
			CheckCertRevocation = checkCertRevocation;
		}

		public string From { get; private set; }
		public string SmtpHost { get; private set; }
		public int SmtpPort { get; private set; }
		public string SmtpLogin { get; private set; }
		public string SmtpPassword { get; private set; }
		public bool CheckCertRevocation { get; private set; }

	}
}
