using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Services.Responses.BVR;

namespace Vostok.Hotline.Api.Services.Mapper.BVR
{
	public static class AddPermanentBlockingMapperExtensions
	{
		public static StatusCommandApiViewModel ToStatusCommandApiViewModel (this AddPermanentBlockingResponseModel response)
		{
			return new StatusCommandApiViewModel
			{
				Success = true,
				Message = $"{response.Description}. {response.Message}. {response.Source}"
			};
		}
	}
}
