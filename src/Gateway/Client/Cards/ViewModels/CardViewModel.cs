using System;
using Vostok.Hotline.Api.Retail.Models;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class CardViewModel : ResponseBaseModel
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
		/// Номер договора в Солар
		/// </summary>
		public string AgreementNumber { get; set; }
		/// <summary>
		/// Код продукта
		/// </summary>
		public string CardCode { get; set; }
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
		/// Push-Info статус
		/// </summary>
		public PushInfoStatusEnum PushInfo { get; set; }
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
		public BranchViewModel Branch { get; set; }
		/// <summary>
		/// Тарифный пакет
		/// </summary>
		public TariffApiModel Tariff { get; set; }
		/// <summary>
		/// Срок окончания действия
		/// </summary>
		public DateTime Expired { get; set; }

		public ViewTypeEnum ViewType { get; set; }
	}
}
