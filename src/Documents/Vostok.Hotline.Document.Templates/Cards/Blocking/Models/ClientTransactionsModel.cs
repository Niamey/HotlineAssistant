using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Document.Templates.Cards.Blocking.Models
{
	public class ClientTransactionsModel
	{
		public bool? AllTransaction { get; set; }
		public TransactionListModel? Transactions { get; set; }
		public override string ToString()
		{

			if (AllTransaction ?? false)
			{
				return "Усі транзакції";
			}
			else
			{
				if (Transactions?.Count > 0)
				{
					StringBuilder sb = new StringBuilder();
					sb.AppendLine("Cписок операцій:");

					foreach (var item in Transactions)
					{
						sb.AppendLine($"{item.OperationDate} | {item.MerchantName} | {item.OperationDetails} | {item.Status} | {item.Amount}{item.Currency}");
					}

					return sb.ToString();
				}
				return null;
			}
		}
	}
}
