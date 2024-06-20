using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;

namespace Vostok.Hotline.Api.Retail.Mapper.Cards
{
	public static class CardBlockingMapperExtensions
	{
		public static CardBlockingApiModel ToCardBlockingApiModel(this StatusCommandViewModel response)
		{
			return new CardBlockingApiModel
			{
				Success = response.Success,
				Message = response.Message
			};
		}

		public static CardBlockingOperationStatusApiModel ToCardBlockingOperationStatusApiModel(this StatusCommandApiViewModel response)
		{
			return new CardBlockingOperationStatusApiModel
			{
				Success = response.Success,
				Message = response.Message
			};
		}
	}
}
