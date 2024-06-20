using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Data.Repository.References.Models.Responses
{
	public class RefTransactionCodeResponseModel : ResponseBaseModel
	{
		public string Code { get; set; }
		public string Rc { get; set; }
		public string Description { get; set; }
	}
}
