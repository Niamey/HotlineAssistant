using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public static class SmsInfoMapperExtensions
	{
		public static SmsInfoViewModel ToSmsInfoViewModel(this SmsInfoApiModel response)
		{
			return new SmsInfoViewModel
			{
				Status = response.Status,
				Tariff = response.Tariff,
				Phone = response.Phone,
				IsFinancePhone = response.IsFinancePhone
			};
		}
	}
}
