using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Api.References.Models
{
	public class DivisionApiModel : ResponseBaseModel
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
	}
}
