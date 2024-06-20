namespace Vostok.Hotline.Core.Common.Enums.Transactions
{
	/// <summary>
	/// Статус транзакции
	/// </summary>
	public enum StmtStatusEnum: byte
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// загружена и готова дляо бработки
		/// </summary>
		Loaded = 1,

		/// <summary>
		/// прошла предобработку
		/// </summary>
		Prepared = 2,

		/// <summary>
		/// отправлена во внешнюю систему, и ответ еще не получен
		/// </summary>
		Suspended = 3,

		/// <summary>
		/// обработана
		/// </summary>
		Finished = 4,

		/// <summary>
		/// авторизация закрыта, блокировка средств отсутствует
		/// </summary>
		Closed = 5,

		/// <summary>
		/// отклонена
		/// </summary>
		Rejected = 6,

		/// <summary>
		/// отменена системой или оператором
		/// </summary>
		Cancelled = 7
	}
}
