using System;
using Vostok.Hotline.Api.Retail.Responses.Agreements.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Agreements
{
	internal class AgreementCreditParamsResponseModel
	{
		/// <summary>
		/// Нахождение в льготном периоде
		/// -1-нет (есть просрочка),
		/// 0-нет(нет просрочки)
		/// 1-й месяц в льготе,
		/// 2-й месяц в льготе)
		/// </summary>
		public AgreementCreditPeriodTypeRetailEnum? Period { get; set; }
		/// <summary>
		/// Сумма минимального платежа
		/// </summary>
		public decimal MandatoryPayment { get; set; }
		/// <summary>
		/// Крайняя дата внесения платежа
		/// </summary>
		public DateTime MandatoryDate { get; set; }
		/// <summary>
		/// Сумма платежа, чтобы оставаться в льготном периоде
		/// </summary>
		public decimal PreferentialPayment { get; set; }
		/// <summary>
		/// Погасить задолженность полностью
		/// </summary>
		public decimal Debt { get; set; }
		/// <summary>
		/// Просрочка
		/// </summary>
		public decimal OverdueLoan { get; set; }
		/// <summary>
		/// %% по кредиту
		/// </summary>
		public decimal Interests { get; set; }
		/// <summary>
		/// Н/о + %%
		/// </summary>
		public decimal Overdraft { get; set; }
		/// <summary>
		/// Пени/штрафы
		/// </summary>
		public decimal Penalty { get; set; }
		/// <summary>
		/// %% + другие платежи
		/// </summary>
		public decimal Fee { get; set; }
		/// <summary>
		/// Кредитные каникулы
		/// </summary>
		public bool IsVacationPeriod { get; set; }
		/// <summary>
		/// Дата окончания кредитных каникул
		/// </summary>
		public DateTime? VacationPeriodEnd { get; set; }
		/// <summary>
		/// Процент на задолжность
		/// </summary>
		public decimal DebtPercent { get; set; }
	}
}
