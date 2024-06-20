using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Api.CRM.Responses.Models;
using Vostok.Hotline.Core.Common.Enums.Counterparts;

namespace Vostok.Hotline.Api.CRM.Mapper
{
	internal static class SegmentationMapperExtensions
	{
		public static SegmentTypeEnum? ToSegmentTypeEnum(this SegmentationTypeResponseModel response)
			  => response.SegmentationTypeId switch
			  {
				  1 => SegmentTypeEnum.Vip,
				  2 => SegmentTypeEnum.Priority,
				  3 => SegmentTypeEnum.Employee,
				  4 => SegmentTypeEnum.Individual,
				  5 => SegmentTypeEnum.Future,
				  6 => SegmentTypeEnum.Lost,
				  _ => null
			  };

		public static SegmentationApiModel ToSegmentType(this SegmentationTypeResponseModel response)
		{
			return new SegmentationApiModel
			{
				SegmentationType = response.ToSegmentTypeEnum(),
				Description = response.Description,
				Name = response.Name
			};
		}
	}
}