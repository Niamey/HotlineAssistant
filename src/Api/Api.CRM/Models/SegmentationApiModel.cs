using Vostok.Hotline.Core.Common.Enums.Counterparts;

namespace Vostok.Hotline.Api.CRM.Models
{
	public class SegmentationApiModel
	{
		public SegmentTypeEnum? SegmentationType { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
