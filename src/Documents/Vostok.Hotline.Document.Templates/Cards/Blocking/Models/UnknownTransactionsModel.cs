using System.Text;
using Vostok.Hotline.Core.Common.Enums.Cards;
using Vostok.Hotline.Document.Templates.Cards.Blocking.Abstractions;

namespace Vostok.Hotline.Document.Templates.Cards.Blocking.Models
{
	public class UnknownTransactionsModel : ICardBlockingReason
	{
		public ClientTransactionsModel? ClientTransactions { get; set; }
		public string ContactPhone { get; set; }
		public bool? IsFromUserDevice { get; set; }
		public CardBlockingTransactionTypeEnum? TransactionType { get; set; }
		public override string ToString()
		{
			StringBuilder result = new StringBuilder();

			if (!string.IsNullOrEmpty(ClientTransactions.ToString()))
			{
				result.AppendLine("Cписок операцій:");

				result.AppendLine($"{ClientTransactions}");
				

				if (TransactionType == CardBlockingTransactionTypeEnum.MobileApp)
				{
					if (IsFromUserDevice != null)
					{
						result.AppendLine((bool)IsFromUserDevice ? "Пристрій клієнта" : "Пристрій НЕ клієнта");
					}
				}

				if (!string.IsNullOrEmpty(ContactPhone))
				{
					result.AppendLine($"Контактний телефон {ContactPhone}");
				}
			}

			return result.ToString();
		}
	}
}
