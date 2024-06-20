using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public static class CardLimitStatusMapperExtensions
	{
		public static StatusCommandViewModel MapToStatusCommandViewModel(this StatusCommandApiViewModel response)
		{
			return new StatusCommandViewModel
			{
				Success = response.Success,
				Message = response.Message
			};
		}
	}
}
