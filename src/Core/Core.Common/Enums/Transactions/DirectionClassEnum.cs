namespace Vostok.Hotline.Core.Common.Enums.Transactions
{
	/// <summary>
	/// Направление операции
	/// </summary>
	public enum DirectionClassEnum: byte
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// дебетование счета
		/// </summary>
		Debit = 1,

		/// <summary>
		/// кредитование счета
		/// </summary>
		Credit = 2
	}
}
