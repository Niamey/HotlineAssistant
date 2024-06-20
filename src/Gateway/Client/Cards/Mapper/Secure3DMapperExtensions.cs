using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public static class Secure3DMapperExtensions
	{
		public static Secure3DInfoViewModel ToSecure3DInfoViewModel(this Secure3DInfoApiModel response)
		{
			return new Secure3DInfoViewModel
			{
				Status = response.Status,
				Tariff = response.Tariff,
				Phone = response.Phone,
				IsFinancePhone = response.IsFinancePhone
			};
		}
	}
}
