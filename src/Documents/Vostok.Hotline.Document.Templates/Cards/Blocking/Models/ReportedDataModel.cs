using System.Text;
using Vostok.Hotline.Document.Templates.Cards.Blocking.Abstractions;

namespace Vostok.Hotline.Document.Templates.Cards.Blocking.Models
{
	public class ReportedDataModel : ICardBlockingReason
	{
		public string ContactPhone { get; set; }
		public ClientTransactionsModel ClientTransactions { get; set; }
		public PropertyListModel Properties { get; set; }
		public PersonWhoCallsModel Person { get; set; }
		public CheaterModel Cheater { get; set; }
		public TokenListModel DeletedTokens { get; set; }
		public bool? IsClientCall { get; set; }
		public bool? IsNeedDeleteToken { get; set; }
		public bool? IsCancelAccessCode { get; set; }


		public override string ToString()
		{
			string result = string.Empty;

			if (!(IsClientCall ?? false)) {

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
			else 
			{
				StringBuilder sb = new StringBuilder();
				if (Properties.Count > 0) {
					sb.AppendLine("Список даних переданих шахраєві:");

					foreach (var item in Properties)
					{
						sb.AppendLine($"{item.PersonDataTitle}");
					};
				}

				if (!string.IsNullOrEmpty(Cheater.ToString()))
					sb.AppendLine($"Інфо про шахрая: {Cheater}");

				if (!string.IsNullOrEmpty(ContactPhone))
					sb.AppendLine($"Конт. тел. клієнта: {ContactPhone}");
				
				if (!string.IsNullOrEmpty(ClientTransactions.ToString()))
					sb.AppendLine($"{ClientTransactions}");

				if (IsNeedDeleteToken ?? false) {
					sb.AppendLine("Вилучені токени:");

					if (DeletedTokens.Count > 0)
						sb.AppendLine($"{DeletedTokens}");
					else
						sb.AppendLine("Операції з токенами не здійснювалося");
				}

				return sb.ToString();
			}
		}
	}
}
