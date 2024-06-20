using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Transactions.Enums
{
	/// <summary>
	/// Направление операции
	/// </summary>
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	public enum DirectionRetailEnum
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// оригинальная транзакция
		/// </summary>
		ORIGINAL,

		/// <summary>
		/// корректировка (частичная отмена)
		/// </summary>
		ADJUSTMENT,

		/// <summary>
		/// отмена
		/// </summary>
		REVERSE
	}
}
