using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Transactions.Enums
{
	/// <summary>
	/// Класс операции
	/// </summary>
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	public enum ClassRetailEnum
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// финансовая транзакция
		/// </summary>
		FINANCIAL,

		/// <summary>
		/// авторизация
		/// </summary>
		AUTHORIZATION
	}
}
