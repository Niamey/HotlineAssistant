using System.Runtime.Serialization;
using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Cards.Enums
{
	/// <summary>
	/// Тип карты
	/// </summary>
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	internal enum CardTypeRetailEnum
	{
		Undefined = UndefinedEnum.Undefined,
		MASTERCARD_BVR,
		MASTERCARD_BVR_VIRT,

		// Only in dev/test:

		MASTERCARD_1,
		MASTERCARD_4,
		MASTERCARD_BVR_BARCODE,
		[EnumMember(Value = "Visa Classic MS")]
		VisaClassicMS,
		[EnumMember(Value = "Visa Classic CHIP")]
		VisaClassicCHIP
	}
}
