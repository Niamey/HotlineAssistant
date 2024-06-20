namespace Vostok.Hotline.Api.Retail.Models.Cards
{
	public class BranchApiModel
	{
		/// <summary>
		/// ИД отделения
		/// </summary>
		public long BranchId { get; set; }
		/// <summary>
		/// Наименование
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Адреса
		/// </summary>
		public string Address { get; set; }
		/// <summary>
		/// Телефон
		/// </summary>
		public string Phone { get; set; }
	}
}
