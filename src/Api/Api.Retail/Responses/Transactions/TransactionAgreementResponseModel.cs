namespace Vostok.Hotline.Api.Retail.Responses.Transactions
{
	public class TransactionAgreementResponseModel
	{
		public long Id { get; set; }
		public string Number { get; set; }

		public TransactionCardResponseModel Card { get; set; }
	}
}
