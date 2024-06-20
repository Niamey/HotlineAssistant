using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Accounts.Enums
{
	/// <summary>
	/// Тип счета
	/// </summary>
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	internal enum AccountTypeRetailEnum
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// Основной
		/// </summary>
		// Main,

		[EnumMember(Value = "1004_DEVICE")]
		DEVICE_1004,
		[EnumMember(Value = "2608_INT")]
		INT_2608,
		[EnumMember(Value = "2920_TRANSIT")]
		TRANSIT_2920,
		[EnumMember(Value = "2924_DISPUTE")]
		DISPUTE_2924,
		[EnumMember(Value = "2924_SHORTAGE")]
		SHORTAGE_2924,
		[EnumMember(Value = "2924_SURPLUS")]
		SURPLUS_2924,
		[EnumMember(Value = "2924_TRANSIT")]
		TRANSIT_2924,
		[EnumMember(Value = "3570_FEE")]
		FEE_3570,
		[EnumMember(Value = "3570_FEE_OVD")]
		FEE_OVD_3570,
		[EnumMember(Value = "3578_FEE")]
		FEE_3578,
		[EnumMember(Value = "3578_FEE_OVD")]
		FEE_OVD_3578,
		[EnumMember(Value = "3678_CASHBACK")]
		CASHBACK_3678,
		[EnumMember(Value = "9129_LIMIT")]
		LIMIT_9129,
		[EnumMember(Value = "9601/9618")]
		DAMPING_9601_9618,
		[EnumMember(Value = "9601_RESERVE")]
		RESERVE_9601,
		ACC_2924_ADVICE_OFFUS,
		ACC_3622_NDFL,
		ACC_6050,
		ACC_6052,
		ACC_2924_P2P_VB,
		[EnumMember(Value = "ACC_2924_TRANSIT (29)")]
		ACC_2924_TRANSIT_29,
		ACC_3622_MILITARY_FEE,
		ACC_3800,
		ACC_3801_EUR,
		ACC_3801_USD,
		ACC_6397,
		ACC_6510,
		ACC_6510_CASH,
		ACC_6514,
		ACC_7020,
		[EnumMember(Value = "ACC_7020_$$$")]
		ACC_7020_VAL,
		ACC_7040,
		[EnumMember(Value = "ACC_7040_$$$")]
		ACC_7040_VAL,
		ACC_9900,
		ACC_9910,
		OVER,
		OVER_EXPIRE_2809BPK,
		OVER_INT,
		OVER_INT_OVD,
		OVER_OVD,
		OVER_OVD_INT,
		OVER_OVD_INT_OVD,
		OVER_REST_2809BPK,
		SKS,
		SKS_INT,
		АСС_2924_P2P
	}
}