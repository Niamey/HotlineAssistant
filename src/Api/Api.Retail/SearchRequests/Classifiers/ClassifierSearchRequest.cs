using Vostok.Hotline.Api.Retail.Responses.Classifiers.Enums;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Api.Retail.SearchRequests.Classifiers
{
	internal class ClassifierSearchRequest : SearchRequestBaseModel
	{
		internal ClassifierSearchRequest(ClassifierRetailTypeEnum type, long entityId)
		{
			Type = type;
			EntityId = entityId;
		}
		public ClassifierRetailTypeEnum Type { get; set; }
		public long EntityId { get; set; }
		public override string GetUrlQuery()
		{
			return $"{Type}/{EntityId}";
		}
	}
}
