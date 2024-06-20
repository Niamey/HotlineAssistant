namespace Vostok.Hotline.Document.Templates.Cards.Blocking.Models
{
	public class PersonWhoCallsModel
	{
		public string FullName { get; set; }
		public string Phone { get; set; }
		public string Comments { get; set; }
		public bool? IsRefusedToProvideInfo { get; set; }
	}
}
