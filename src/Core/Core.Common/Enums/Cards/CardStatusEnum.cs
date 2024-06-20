namespace Vostok.Hotline.Core.Common.Enums.Cards
{   
	/// <summary>
	/// Статус карты
	/// </summary>
	public enum CardStatusEnum : byte
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// Заблокирована (All transactions prohibited)
		/// </summary>
		Blocked = 1,
		/// <summary>
		/// Активная
		/// </summary>
		Active = 2,
		/// <summary>
		/// Утеряна(Balance Inquury Only)
		/// </summary>
		Stolen = 3,
		/// <summary>
		/// Украдена(Balance Inquury Only)
		/// </summary>
		Lost = 4,
		/// <summary>
		/// Аннулирована(All transactions prohibited)
		/// </summary>
		Canceled = 5,
		/// <summary>
		/// Приостановлена
		/// </summary>
		Suspend = 6
	}
}
