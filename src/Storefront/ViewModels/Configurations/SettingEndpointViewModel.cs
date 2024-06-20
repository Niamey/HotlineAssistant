namespace Vostok.Hotline.Storefront.ViewModels.Configurations
{
	public class ApiSettingViewModel
	{
		public ApiSettingViewModel()
		{ 
		}

		public ApiSettingViewModel(string host)
		{
			Host = host;
		}

		public string Host { get; set; }
	}
}
