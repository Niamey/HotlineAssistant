using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Cards.Enums
{
	/// <summary>
	/// Вид карты
	/// </summary>
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	internal enum CardKindRetailEnum
    {
		Undefined = UndefinedEnum.Undefined,
		
		DEBITWORLD_BVR,

		[EnumMember(Value = "516818_magstripe_PredPROD")]
		MAGSTRIPE_PREDPROD_516818,

		[EnumMember(Value = "mc_magstripe")]
		MC_MAGSTRIPE,

		[EnumMember(Value = "DebitWorld_NOmagstripe")]
		DEBITWORLD_NO_MAGSTRIPE,

		// Only in dev/test:

		visa_chip,
		visa_magstripe,
		DebitWorld_NOms_testK_liveC,
		mchip_advance_test
	}
}
