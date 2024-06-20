using Vostok.Hotline.Api.CRM.Models;

namespace Vostok.Hotline.Api.CRM.Mapper
{
	public static class CounterpartSearchByPhoneMapperExtensions
	{
		public static CounterpartSearchByPhoneApiModel ToCounterpartSearchByPhoneApiModel(this CounterpartApiModel response)
		{
			return new CounterpartSearchByPhoneApiModel { 
				CounterpartId = response.CounterpartId,
				SolarId = response.SolarId,
				FullName = response.FullName,
				SegmentationType = response.SegmentationType?.SegmentationType
			};
		}
	}
}
