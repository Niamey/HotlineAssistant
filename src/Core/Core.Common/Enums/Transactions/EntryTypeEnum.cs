namespace Vostok.Hotline.Core.Common.Enums.Transactions
{
	/// <summary>
	/// Тип операции
	/// </summary>
	public enum EntryTypeEnum: byte
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// транзакция
		/// </summary>
		Transaction = 1,

		/// <summary>
		/// комиссия
		/// </summary>
		Fee = 2,

		/// <summary>
		/// установка или изменение кредитного лимита
		/// </summary>
		Credit_limit = 3
	}
}
