using Vostok.Hotline.Data.Repository.Core.Models.Responses;
using Vostok.Hotline.Gateway.Client.Travels.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Travels.Mapper
{
	public static class TravelCountMapperExtensions
	{
		public static TravelCountViewModel ToTravelCountViewModel(this TravelCountResponseModel response)
		{
			return new TravelCountViewModel
			{
				TotalRows = response.TotalRows
			};
		}
	}
}
