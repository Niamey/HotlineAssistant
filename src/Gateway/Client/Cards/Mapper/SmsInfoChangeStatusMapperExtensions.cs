using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public static class SmsInfoChangeStatusMapperExtensions
	{
		public static SmsInfoChangeStatusViewModel ToSmsInfoChangeStatusViewModel(this SmsInfoChangeStatusApiModel response)
		{
			return new SmsInfoChangeStatusViewModel
			{
				Status = response.Status,
				Message = response.Message
			};
		}
	}
}
