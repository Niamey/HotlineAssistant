using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Accounts.Enums
{
	/// <summary>
	/// Статус счета
	/// </summary>
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	internal enum AccountStatusRetailEnum
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