using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.References.Managers
{
	public class DistrictApiManager
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
