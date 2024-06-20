using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Vostok.Hotline.Api.Retail.Responses.Cards.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Cards
{
	/// <summary>
	/// Карта
	/// </summary>
	internal class CardResponseModel 
    {
        /// <summary>
        /// ИД карты
        /// </summary>
        public long Id { get; set; }
		/// <summary>
		/// ИД клиента в Солар
		/// </summary>
		public long ClientId { get; set; }
		/// <summary>
		/// ИД договора в Солар
		/// </summary>
		public long AgreementId { get; set; }
        /// <summary>
        /// Картинка карты
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Номер карты
        /// </summary>
        public string Number { get; set; }
		/// <summary>
		/// Имя на карте
		/// </summary>
		public string EmbossedName { get; set; }
		/// <summary>
		/// smsInfo номер телефона
		/// </summary>
		public string SmsInfoPhone { get; set; }
        /// <summary>
        /// 3d secure номер телефона
        /// </summary>
        public string SecurePhone { get; set; }
		/// <summary>
		/// Финансовый номер телефона
		/// </summary>
		public string FinancePhone { get; set; }
		/// <summary>
		/// Наличие в стоп листе
		/// </summary>
		public bool InStopList { get; set; }
        /// <summary>
        /// Статус карты
        /// </summary>
        public CardStatusRetailEnum Status { get; set; }
        /// <summary>
        /// Штрих код
        /// </summary>
        public string Barcode { get; set; }
		/// <summary>
		/// Вид карты
		/// </summary>
		 
		public CardKindRetailEnum Kind { get; set; }
		/// <summary>
		/// Тип карты
		/// </summary>

		public CardTypeRetailEnum Type { get; set; }
		/// <summary>
		/// Категория карты
		/// </summary>
		public CardCategoryRetailEnum Category { get; set; }
		/// <summary>
		/// Чип
		/// </summary>
		public bool HasChip { get; set; }
		/// <summary>
		/// Виртуальная
		/// </summary>
		public bool IsVirtual { get; set; }
		/// <summary>
		/// Отделение оформления
		/// </summary>
		public BranchResponseModel Branch { get; set; }
        /// <summary>
        /// Тарифный пакет
        /// </summary>
        public TariffResponseModel Tariff { get; set; }
        /// <summary>
        /// Срок окончания действия
        /// </summary>
        public DateTime Expired { get; set; }
	}
}
