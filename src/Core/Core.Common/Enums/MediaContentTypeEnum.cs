using System.ComponentModel;

namespace Vostok.Hotline.Core.Common.Enums
{
	public enum MediaContentTypeEnum
	{
		[Description("application/json")]
		ApplicationJson = 1,

		[Description("application/x-www-form-urlencoded")]
		ApplicationFormUrlEncoded = 2
	}
}
