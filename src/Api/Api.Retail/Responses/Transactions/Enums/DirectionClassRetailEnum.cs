using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Transactions.Enums
{
	/// <summary>
	/// Направление операции
	/// </summary>
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	public enum DirectionClassRetailEnum
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// дебетование счета
		/// </summary>
		DEBIT,

		/// <summary>
		/// кредитование счета
		/// </summary>
		CREDIT
	}
}
