using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Gateway.Client.Addresses.Managers
{
	public class RegionManager
	{
		public string GetName(LocalizationEnum localization)
		{
			return localization switch
			{
				LocalizationEnum.Ukraine => "область",
				LocalizationEnum.Russian => "область",
				_ => ""
			};
		}
	}
}
