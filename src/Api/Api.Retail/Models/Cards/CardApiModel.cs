using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Cards;
using Vostok.Hotline.Core.Common.Helpers;

namespace Vostok.Hotline.Api.Retail.Models.Cards
{
	public class CardApiModel : ResponseBaseModel
	{
		/// <summary>
		/// ИД карты
		/// </summary>
		public long CardId { get; set; }
		/// <summary>
		/// ИД клиента в Солар
		/// </summary>
		public long SolarId { get; set; }
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
		public CardStatusEnum Status { get; set; }
		/// <summary>
		/// Штрих код
		/// </summary>
		public string Barcode { get; set; }
		/// <summary>
		/// Вид карты
		/// </summary>
		public CardKindEnum Kinde { get; set; }
		/// <summary>
		/// Тип карты
		/// </summary>
		public CardTypeEnum Type { get; set; }
		/// <summary>
		/// Категория карты
		/// </summary>
		public CardCategoryEnum Category { get; set; }
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
		public BranchApiModel Branch { get; set; }
		/// <summary>
		/// Тарифный пакет
		/// </summary>
		public TariffApiModel Tariff { get; set; }
		/// <summary>
		/// Срок окончания действия
		/// </summary>
		public DateTime Expired { get; set; }

		public string GetMaskedCardNumber()
		{
			return CardHelper.GetMaskedCardNumber(Number);
		}
	}
}
