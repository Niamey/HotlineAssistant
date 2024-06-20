namespace Vostok.Hotline.Core.Common.Enums.Transactions
{
	/// <summary>
	/// Направление операции
	/// </summary>
	public enum DirectionEnum: byte
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// оригинальная транзакция
		/// </summary>
		Original = 1,

		/// <summary>
		/// корректировка (частичная отмена)
		/// </summary>
		Adjustment = 2,

		/// <summary>
		/// отмена
		/// </summary>
		Reverse = 3
	}
}
