using Vostok.Hotline.Api.Services.Models.CardService;
using Vostok.Hotline.Api.Services.Responses.CardService;

namespace Vostok.Hotline.Api.Services.Mapper.CardService
{
	internal static class CardImageMapperExtensions
	{
		public static CardImageApiModel ToCardImageApiModel(this CardImageResponseModel responses)
		{
			return new CardImageApiModel()
			{
				FrontUrl = responses.FrontUrl,
				BackUrl = responses.BackUrl
			};
		}
	}
}
