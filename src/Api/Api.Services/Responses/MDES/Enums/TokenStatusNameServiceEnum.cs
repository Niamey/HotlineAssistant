namespace Vostok.Hotline.Api.Services.Responses.MDES.Enums
{
	public enum TokenStatusNameServiceEnum : byte
	{
		/// <summary>
		/// Токен создан, но ещё не активирован
		/// </summary>
		Inactive = 1,
		Unmapped = 2,

		/// <summary>
		/// Активный токен
		/// </summary>
		Active = 3,

		/// <summary>
		/// Действие токена приостановлено
		/// </summary>
		Suspended = 4,

		/// <summary>
		/// Токен удалён
		/// </summary>
		Deleted = 5
	}
}
