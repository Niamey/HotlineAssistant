using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Gateway.Client.Counterpart.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Counterpart.Mapper
{
	public static class ClientSegmentationMapperExtensions
	{
		public static ClientSegmentViewModel ToSegmentType(this SegmentationApiModel response)
		{
			return new ClientSegmentViewModel
			{
				SegmentType = response.SegmentationType,
				Name = response.Name
			};
		}
	}
}