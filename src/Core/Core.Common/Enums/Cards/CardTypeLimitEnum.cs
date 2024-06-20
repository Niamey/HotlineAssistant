using System.Text.Json.Serialization;
using Vostok.Hotline.Core.Common.Conversions;

namespace Vostok.Hotline.Core.Common.Enums.Cards
{
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	public enum CardTypeLimitEnum
	{
		Undefined = UndefinedEnum.Undefined,
		Operation,
		Money
	}
}
