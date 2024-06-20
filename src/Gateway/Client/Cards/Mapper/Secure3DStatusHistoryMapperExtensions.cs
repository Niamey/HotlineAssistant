using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public static class Secure3DStatusHistoryMapperExtensions
	{
		public static Secure3DStatusHistoryViewModel MapToSecure3DStatusHistoryViewModel(this Secure3DStatusHistoryApiModel response)
		{
			return new Secure3DStatusHistoryViewModel
			{
				CurrentStatus = response.CurrentStatus,
				DateChangeStatus = response.DateChangeStatus,
				Comment	= response.Comment
			};
		}
	}
}
