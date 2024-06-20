namespace Vostok.Hotline.Document.Templates.Cards.Blocking.Models
{
	public class CheaterModel
	{
		public string CardNumber { get; set; }
		public string FullName { get; set; }
		public string Phone { get; set; }
		public string DateCall { get; set; }
		public string Comments { get; set; }

		public override string ToString()
		{
			string result = string.Empty;

			if (!string.IsNullOrEmpty(FullName))
				result += $"Ким представився: {FullName}";

			if (!string.IsNullOrEmpty(Phone))
			{
				if (!string.IsNullOrEmpty(result))
					result += ", ";

				result += $"тел.: {Phone}";
			}

			if (!string.IsNullOrEmpty(DateCall))
			{
				if (!string.IsNullOrEmpty(result))
					result += ", ";

				result += $"дата дзвінка: {DateCall}";
			}

			if (!string.IsNullOrEmpty(Comments))
			{
				if (!string.IsNullOrEmpty(result))
					result += ", ";

				result += $"повідомив інформацію: {Comments}";
			}

			return result;
		}
	}
}
