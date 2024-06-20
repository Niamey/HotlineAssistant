using System.Text;
using Vostok.Hotline.Document.Templates.Cards.Blocking.Abstractions;

namespace Vostok.Hotline.Document.Templates.Cards.Blocking.Models
{
	public class ForgottenInATMModel : ICardBlockingReason
	{
		public ClientTransactionsModel ClientTransactions { get; set; }
		public bool? IsClientCall { get; set; }
		public PersonWhoCallsModel Person { get; set; }
		public string ContactPhone { get; set; }

		public override string ToString()
		{
			string result = string.Empty;

			if ((IsClientCall ?? false) && (ClientTransactions.AllTransaction ?? false))
			{
				return "Всі операції клієнта";
			}
			else if ((IsClientCall ?? false) && !(ClientTransactions.AllTransaction ?? false))
			{
				StringBuilder sb = new StringBuilder();

				if (ClientTransactions.Transactions?.Count > 0)
				{
					sb.AppendLine("Cписок операцій які здійснив не клієнт:");
					foreach (var item in ClientTransactions.Transactions)
					{
						sb.AppendLine($"{item.OperationDate} | {item.MerchantName} | {item.OperationDetails} | {item.Status} | {item.Amount}{item.Currency}");
					};
				}

				if (!string.IsNullOrEmpty(ContactPhone))
					sb.AppendLine($"Контактний телефон: {ContactPhone}");
				
				return sb.ToString();
			}

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

				result += $"причина: {Person.Comments}";
			}

			return result;
		}
	}
}
