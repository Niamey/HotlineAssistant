using Vostok.Hotline.Document.Templates.Cards.Blocking.Abstractions;

namespace Vostok.Hotline.Document.Templates.Cards.Blocking.Models
{
	public class ClientIsCheaterModel : ICardBlockingReason
	{
		public ClientTransactionsModel? ClientTransactions { get; set; }
		public PersonWhoCallsModel? Person { get; set; }

		public override string ToString()
		{
			string result = string.Empty;

			if (!string.IsNullOrEmpty(Person.FullName))
				result += $"ПІБ: {Person.FullName}";

			if (!string.IsNullOrEmpty(Person.Phone))
			{
				if (!string.IsNullOrEmpty(result))
					result += ", ";

				result += $"тел.: {Person.Phone}";
			}

			if (!string.IsNullOrEmpty(Person.Comments))
			{
				if (!string.IsNullOrEmpty(result))
					result += ", ";

				result += $"коментар: {Person.Comments}";
			}

			if (ClientTransactions != null) {
				if (!ClientTransactions.AllTransaction.Value && ClientTransactions.Transactions.Count > 0) {
					string operations = string.Empty;
					foreach (var transaction in ClientTransactions.Transactions) {
						if (!string.IsNullOrEmpty(operations))
							operations += "; ";

						operations += $"{transaction.OperationDate}, {transaction.MerchantName}, {transaction.Amount} {transaction.Currency}";
					}
					if (!string.IsNullOrEmpty(result))
						result += ". ";

					result += $"Операції: {operations}";
				}
			}

			return result;
		}
	}
}
