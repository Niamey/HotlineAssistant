namespace Vostok.Hotline.Core.Common.Enums.MDES
{
	public enum TokenStatusNameMdesEnum : byte
	{
		Undefined = UndefinedEnum.Undefined,

		/// <summary>
		/// Токен создан, но ещё не активирован
		/// </summary>
		Inactive = 1,

		/// <summary>
		/// Активный токен
		/// </summary>
		Active = 2,

		/// <summary>
		/// Действие токена приостановлено
		/// </summary>
		Suspended = 3,

		/// <summary>
		/// Токен удалён
		/// </summary>
		Deleted = 4
	}
}
