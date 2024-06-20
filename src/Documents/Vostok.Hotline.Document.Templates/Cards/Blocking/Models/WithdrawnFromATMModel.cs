using Vostok.Hotline.Document.Templates.Cards.Blocking.Abstractions;

namespace Vostok.Hotline.Document.Templates.Cards.Blocking.Models
{
	public class WithdrawnFromATMModel : ICardBlockingReason
	{
		public bool? IsClientCall { get; set; }
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

				result += $"причина: {Person.Comments}";
			}

			return result;
		}
	}
}
