using System.Text.Json.Serialization;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Cards.Enums
{
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	internal enum CardPeriodTypeLimitRetailEnum
	{
		Undefined = UndefinedEnum.Undefined,
		Day, 
		Week,
		Month,
		Year
	}
}
