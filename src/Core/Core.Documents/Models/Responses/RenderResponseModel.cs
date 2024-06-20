namespace Vostok.Hotline.Core.Documents.Models.Responses
{
	public class RenderResponseModel
	{
		public string Result { get; set; }
		public string ErrorMessage { get; set; }
		public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
	}
}
