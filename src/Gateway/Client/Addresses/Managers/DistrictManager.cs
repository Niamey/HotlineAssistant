using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Gateway.Client.Addresses.Managers
{
	public class DistrictManager
	{
		public string GetName(LocalizationEnum localization)
		{
			return localization switch
			{
				LocalizationEnum.Ukraine => "район",
				LocalizationEnum.Russian => "район",
				_ => ""
			};
		}
	}
}
