using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Transactions.Enums
{
	/// <summary>
	/// Тип операции
	/// </summary>
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	public enum EntryTypeRetailEnum
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// транзакция
		/// </summary>
		TRANSACTION,

		/// <summary>
		/// комиссия
		/// </summary>
		FEE,

		/// <summary>
		/// установка или изменение кредитного лимита
		/// </summary>
		CREDIT_LIMIT
	}
}
