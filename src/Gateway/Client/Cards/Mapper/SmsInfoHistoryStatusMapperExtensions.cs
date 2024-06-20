using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public static class SmsInfoHistoryStatusMapperExtensions
	{
		public static SmsInfoHistoryStatusViewModel MapToSmsInfoHistoryStatusViewModel(this SmsInfoHistoryStatusApiModel response)
		{
			return new SmsInfoHistoryStatusViewModel
			{
				Comment = response.Comment,
				CurrentStatus = response.CurrentStatus,
				DateChangeStatus = response.DateChangeStatus
			};
		}
	}
}
