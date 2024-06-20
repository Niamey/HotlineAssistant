using System;
using Newtonsoft.Json;

namespace Vostok.Hotline.Api.Retail.Responses.Cards
{
	internal class CardBalanceResponseModel
	{
		/// <summary>
		/// ИД договора в Солар
		/// </summary>
		public long AgreementId { get; set; }
		/// <summary>
		/// Дата, по состоянию на которую отображается информация
		/// </summary>
		public DateTime Date { get; set; }
		/// <summary>
		/// Числовой код валюты в соответствии со стандартом ISO 4217
		/// </summary>
		[JsonProperty("Currency")]
		public string CurrencyCode { get; set; }
		/// <summary>
		/// оступный остаток средств на балансе, вычисляемый при помощи следующих компонентов:
		/// собственные,
		/// заблокированные,
		/// овердрафт,
		/// доступный остаток кредитного лимита
		/// </summary>
		public decimal Available { get; set; }
		/// <summary>
		/// Сумма собственных средств на балансе
		/// </summary>
		public decimal Own { get; set; }
		/// <summary>
		/// Сумма заемных средств на балансе в пределах кредитного лимита
		/// </summary>
		public decimal Loan { get; set; }
		/// <summary>
		/// Неиспользованный остаток кредитного лимита 
		/// </summary>
		public decimal UnusedCreditLimit { get; set; }
		/// <summary>
		/// Размер кредитного лимита
		/// </summary>
		public decimal CreditLimit { get; set; }
		/// <summary>
		/// Максимальний кредитний ліміт
		/// </summary>
		public decimal MaxCreditLimit { get; set; }
		/// <summary>
		/// Мінімальний кредитний ліміт
		/// </summary>
		public decimal MinCreditLimit { get; set; }
		/// <summary>
		/// Сумма требований и частичных арестов.
		/// 0, если требований и частичных арестов нет
		/// </summary>
		public decimal FinBlocking { get; set; }
		/// <summary>
		/// Сумма средств, заблокированных на балансе
		/// </summary>
		public decimal Blocked { get; set; }
		/// <summary>
		/// Сумма заемных средств на балансе потраченных сверх кредитного лимита (технический овердрафт)
		/// </summary>
		public decimal Overlimit { get; set; }
		/// <summary>
		/// Финансовая деб задолженность + Просроченная фин. деб задолженность
		/// </summary>
		public decimal Overdraft { get; set; }
		/// <summary>
		/// Начисл проценты на просроч. заемные ср-ва Проценты по ссудной задолженности Требования по комиссии за конвертацию Просроченные требования по комиссии за конвертацию Требования по сервисным комиссиям Просроченные требования по сервисным комиссиям Требования по транзакционным комиссиям Просроченные требования по транз. комиссиям Просроченная фин. деб задолженность Финансовая деб задолженность
		/// </summary>
		public decimal Fee { get; set; }
		/// <summary>
		/// Сумма обязательного платежа по кредиту
		/// </summary>
		public decimal MandatoryPayment { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime MandatoryDate { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal PreferentialPayment { get; set; }
		/// <summary>
		/// Полная задолженность
		/// </summary>
		public decimal Debt { get; set; }
		/// <summary>
		/// Сумма просроченных средств на балансе
		/// </summary>
		public decimal Overdue { get; set; }
		/// <summary>
		/// Сумма процентов за пользование кредитом
		/// </summary>
		public decimal Interests { get; set; }
		/// <summary>
		/// Сумма компонентов баланса класса PENALTY (неустойка)
		/// </summary>
		public decimal Penalty { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int Period { get; set; }
		/// <summary>
		/// Собственные средства (общая сумма собственных средств с накопилками)
		/// </summary>
		public decimal FullOwn { get; set; }
		/// <summary>
		/// Неснижаемый остаток
		/// </summary>
		public decimal MinimumBalance { get; set; }
		/// <summary>
		/// Сумма минимального платежа по кредиту
		/// </summary>
		public decimal MinimalPayment { get; set; }
		/// <summary>
		/// Признак льготного периода
		/// </summary>
		public bool IsVacationPeriod { get; set; }
		/// <summary>
		/// Окончание льготного периода
		/// </summary>
		public DateTime VacationPeriodEnd { get; set; }
		/// <summary>
		/// Процентная ставка просрочки
		/// </summary>
		public decimal DebtPercent { get; set; }
	}
}
