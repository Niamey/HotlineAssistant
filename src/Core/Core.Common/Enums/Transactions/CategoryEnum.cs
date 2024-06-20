namespace Vostok.Hotline.Core.Common.Enums.Transactions
{
	/// <summary>
	/// Категория операции
	/// </summary>
	
	public enum CategoryEnum: byte
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// 
		/// </summary>
		Advice = 1, 

		/// <summary>
		/// 
		/// </summary>
		Request = 2, 

		/// <summary>
		/// 
		/// </summary>
		Notification = 3, 

		/// <summary>
		/// 
		/// </summary>
		Check = 4
	}
}
