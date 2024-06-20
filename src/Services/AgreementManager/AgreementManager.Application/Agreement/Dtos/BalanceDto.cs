using System;

namespace Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos
{
	public class BalanceDto
	{
		public string Date { get; set; }
		public string NextDueDate { get; set; }
		public string NextBillingDate { get; set; }
		public (int Id, string IdString, string Code, string Name) Currency { get; set; }
		public decimal Available { get; set; }
		public decimal Own { get; set; }
		public decimal Blocked { get; set; }
		public decimal Loan { get; set; }
		public decimal OverLimit { get; set; }
		public decimal OverDue { get; set; }
		public decimal CreditLimit { get; set; }
		public decimal FinBlocking { get; set; }
		public decimal Interests { get; set; }
		public decimal Penalty { get; set; }
		public decimal MinimalPayment { get; set; }
		public decimal MandatoryPayment { get; set; }
		public decimal BlockedByDemands { get; set; }
		public decimal DemandsLeftToCover { get; set; }
		public decimal UnusedCreditLimit { get; set; }
		public decimal OverDueLoan { get; set; }
		public decimal OverDueInterests { get; set; }
		public decimal CurrentDebt { get; set; }
		public decimal GracePaymentAmount { get; set; }
		public decimal FullDebtAmount { get; set; }
	}
}