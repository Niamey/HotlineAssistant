using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public static class CardBlockingMapperExtensions
	{
		public static CardBlockingViewModel ToCardBlockingViewModel(this CardBlockingApiModel response)
		{
			return new CardBlockingViewModel
			{
				Message = response.Message,
				Success = response.Success
			};
		}

		public static CardBlockingOperationStatusViewModel ToCardBlockingOperationStatusViewModel(this CardBlockingOperationStatusApiModel response)
		{
			return new CardBlockingOperationStatusViewModel
			{
				Message = response.Message,
				Success = response.Success
			};
		}

		public static CardBlockingOperationStatusCollectionViewModel ToCardBlockingOperationStatusCollectionViewModel(this CardBlockingOperationStatusCollectionApiModel response)
		{
			if (response == null) return null;

			var result = new CardBlockingOperationStatusCollectionViewModel();

			foreach (var item in response)
			{
				result.Add(item.ToCardBlockingOperationStatusViewModel());
			}

			return result;
		}

		public static CardBlockingResultViewModel ToCardBlockingResultViewModel(this CardBlockingResultApiModel response)
		{

			return new CardBlockingResultViewModel
			{
				Status = response.Status.MapToStatusCommandViewModel(),
				OperationStatuses = response.OperationStatuses.ToCardBlockingOperationStatusCollectionViewModel(),
				ResultForOperator = response.ResultForOperator
			};
		}
	}
}
