using Vostok.Hotline.Core.Common.Enums.Counterparts;

namespace Vostok.Hotline.Api.CRM.Models
{
	public class CounterpartSearchByPhoneApiModel
	{
		public long CounterpartId { get; set; }
		public long? SolarId { get; set; }
		public string FullName { get; set; }
		public SegmentTypeEnum? SegmentationType { get; set; }
	}
}
