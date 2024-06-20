using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.MoneyBoxes.Enums
{
	/// <summary>
	/// Статус счета
	/// </summary>
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	internal enum MoneyBoxStatusRetailEnum
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// Активный
		/// </summary>
		Active,
		/// <summary>
		/// 
		/// </summary>
		Inactive
	}
}