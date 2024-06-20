using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Vostok.Hotline.Core.Common.Conversions;

namespace Vostok.Hotline.Core.Common.Enums.Cards
{
	/// <summary>
	/// Тип карты
	/// </summary>
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	public enum CardTypeEnum 
	{
		Undefined = UndefinedEnum.Undefined,
		MasterCardBVR = 1,
		MasterCardBVRVirt = 2,
	}
}
