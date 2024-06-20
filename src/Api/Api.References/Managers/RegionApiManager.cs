using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.References.Managers
{
	public class RegionApiManager
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
