using Vostok.Hotline.Api.Retail.Responses.MoneyBoxes.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.MoneyBoxes
{
	/// <summary>
	/// Договор
	/// </summary>
	internal class MoneyBoxResponseModel
	{
		/// <summary>
		/// ИД накопилки
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// ИД 
		/// </summary>
		public long SavingId { get; set; }
		/// <summary>
		/// Имя накопилки
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public MoneyResponseModel Amount { get; set; }
		/// <summary>
		/// Статус
		/// </summary>
		public MoneyBoxStatusRetailEnum Status { get; set; }
	}
}
