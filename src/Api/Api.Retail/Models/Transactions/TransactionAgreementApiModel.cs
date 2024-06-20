namespace Vostok.Hotline.Api.Retail.Models.Transactions
{
	public class TransactionAgreementApiModel
	{
		public long Id { get; set; }
		public string Number { get; set; }
		public TransactionCardApiModel Card { get; set; }
	}
}
