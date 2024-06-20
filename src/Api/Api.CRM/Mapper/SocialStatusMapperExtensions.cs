using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Api.CRM.Responses.Models;

namespace Vostok.Hotline.Api.CRM.Mapper
{
	internal static class SocialStatusMapperExtensions
	{
		public static SocialStatusApiModel MapToSocialStatusApiModel(this SocialStatusResponseModel response)
		{
			var result = new SocialStatusApiModel
			{
				Description = response.Description,
				Name = response.Name,
				SocialStatusId = response.SocialStatusId
			};

			return result;
		}
	}
}
