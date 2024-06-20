namespace Vostok.Hotline.Api.Retail.Responses.Cards
{
	internal class BranchResponseModel
	{
		/// <summary>
		/// ИД отделения
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// Наименование
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// ИД адреса
		/// </summary>
		public long AddressId { get; set; }

		/// <summary>
		/// Адрес отделения
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// Телефон отделения
		/// </summary>
		public string Phone { get; set; }
	}
}