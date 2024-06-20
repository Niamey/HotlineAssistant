using System.Collections.Generic;
using Vostok.Hotline.Core.Documents.Abstractions;

namespace Vostok.Hotline.Core.Documents.Requests
{
	public class RenderQuery <T>
		where T: ITemplateViewModel
	{
		public string TemplatePath { get; set; }
		public T TokenModel { get; set; }
		public Dictionary<string, object> ViewData { get; set; }
	}
}
