using System;
using Vostok.Hotline.Document.Templates.Cards.Blocking.Abstractions;

namespace Vostok.Hotline.Document.Templates.Cards.Blocking.Models
{
	public class LostModel : ICardBlockingReason
	{
		public ClientTransactionsModel ClientTransactions { get; set; }
		public bool? IsClientCall { get; set; }
		public bool? IsClientLostPhone { get; set; }
		public PersonWhoCallsModel? Person { get; set; }

		public override string ToString()
		{
			string result = string.Empty;

			if (!(IsClientCall ?? false))
			{
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
			} else
			{
				if ((ClientTransactions?.AllTransaction ?? false) && (IsClientLostPhone ?? false))
				{
					result += "Всі операції клієнта, код доступа в МП видалено, обліковий запис МП заблокировано";
				}
				if ((ClientTransactions?.AllTransaction ?? false) && !(IsClientLostPhone ?? false))
				{
					result += "Всі операції клієнта";
				}
				if (!(ClientTransactions?.AllTransaction ?? false) && (IsClientLostPhone ?? false))
				{
					result += $"{ClientTransactions}";
					result += "код доступа в МП видалено, обліковий запис МП заблокировано";
				}
				if (!(ClientTransactions?.AllTransaction ?? false) && !(IsClientLostPhone ?? false))
				{
					result += $"{ClientTransactions}";
				}
			}

			return result;
		}
	}
}
