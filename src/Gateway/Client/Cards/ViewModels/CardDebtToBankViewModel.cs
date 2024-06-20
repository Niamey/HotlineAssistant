using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Agreements;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class CardDebtToBankViewModel : ResponseBaseModel
	{
		public AgreementCreditPeriodTypeEnum Period { get; set; }
		public decimal MandatoryPayment { get; set; }
		public DateTime MandatoryDate { get; set; }
		public decimal PreferentialPayment { get; set; }
		public decimal Debt { get; set; }
		public decimal OverdueLoan { get; set; }
		public decimal Interests { get; set; }
		public decimal Overdraft { get; set; }
		public decimal Penalty { get; set; }
		public decimal Fee { get; set; }
		public string CurrencyName { get; set; }
		public bool IsVacationPeriod { get; set; }
		public DateTime? VacationPeriodEnd { get; set; }
		public decimal DebtPercent { get; set; }
	}
}
