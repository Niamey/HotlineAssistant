using Vostok.Hotline.Core.Documents.Abstractions;

namespace Vostok.Hotline.Document.Templates.FeedBack
{
	public class FeedBackModel : ITemplateViewModel
	{
		public string Login { get; set; }
		public string FullName { get; set; }
		public string Position { get; set; }
		public string Email { get; set; }
		public string Rating { get; set; }
		public string Message { get; set; }
	}
}
