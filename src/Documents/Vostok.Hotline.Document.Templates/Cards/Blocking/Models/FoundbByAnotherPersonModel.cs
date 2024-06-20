using Vostok.Hotline.Document.Templates.Cards.Blocking.Abstractions;

namespace Vostok.Hotline.Document.Templates.Cards.Blocking.Models
{
	public class FoundbByAnotherPersonModel : ICardBlockingReason
	{
		public string FullName { get; set; }
		public string Phone { get; set; }
		public bool FoundThings { get; set; }
		public string Comments { get; set; }

		public override string ToString() {
			string result = string.Empty;
			if (!string.IsNullOrEmpty(FullName)) 
				result += $" ПІБ: {FullName} ";

			if (!string.IsNullOrEmpty(Phone))
				result += $" Тел.: {Phone} ";

			if (!string.IsNullOrEmpty(Comments))
				result += $" Знайдено: {Comments} ";
				
			return result;
		}
	}
}
