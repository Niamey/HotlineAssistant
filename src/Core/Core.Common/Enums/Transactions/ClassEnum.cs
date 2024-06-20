namespace Vostok.Hotline.Core.Common.Enums.Transactions
{
	/// <summary>
	/// Класс операции
	/// </summary>
	public enum ClassEnum: byte
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// финансовая транзакция
		/// </summary>
		Financial = 1,

		/// <summary>
		/// авторизация
		/// </summary>
		Authorization = 2
	}
}
