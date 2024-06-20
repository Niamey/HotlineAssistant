namespace Vostok.Hotline.Core.Common.Enums.MDES
{
	public enum ReasonTypeMdesEnum: byte
	{
		Undefined = UndefinedEnum.Undefined,

		/// <summary>
		/// A - Владелец карты успешно прошел аутентификацию до активации
		/// </summary>
		Auth = 1,

		/// <summary>
		/// L - Владелец карты сообщил о потере устройства / Владелец карты подтвердил потерю устройства
		/// </summary>
		Lost = 2,

		/// <summary>
		/// S - Владелец карты сообщил о краже устройства / Владелец карты подтвердил кражу устройства
		/// </summary>
		Stolen = 3,

		/// <summary>
		/// C - Счёт или карта закрыты
		/// </summary>
		Closed = 4,

		/// <summary>
		/// F - Эмитент или владелец сообщил о возобновлении контроля над устройством
		/// </summary>
		Found = 5,

		/// <summary>
		/// T - Эмитент или владелец карты подтвердил отсутствие мошеннических транзакций с токеном / Эмитент или владелец карты подтвердил мошеннические транзакции с токеном
		/// </summary>
		Transaction = 6,

		/// <summary>
		/// Z - Другое
		/// </summary>
		Other = 7
	}
}
