using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Transactions.Enums
{
	/// <summary>
	/// Статус транзакции
	/// </summary>
	/// 
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	public enum TxnStatusRetailEnum
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// загружена и готова для обработки
		/// </summary>
		LOADED,

		/// <summary>
		/// прошла предобработку
		/// </summary>
		PREPARED,

		/// <summary>
		/// отправлена во внешнюю систему, и ответ еще не получен
		/// </summary>
		SUSPENDED,

		/// <summary>
		/// обработана
		/// </summary>
		FINISHED,

		/// <summary>
		/// авторизация закрыта, блокировка средств отсутствует
		/// </summary>
		CLOSED,

		/// <summary>
		/// отклонена
		/// </summary>
		REJECTED,

		/// <summary>
		/// отменена системой или оператором
		/// </summary>
		CANCELLED
	}
}
