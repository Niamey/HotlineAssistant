using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Travels;
using Vostok.Hotline.Data.Repository.Core.Models.Responses;
using Vostok.Hotline.Data.Repository.Core.Models.Responses.Enums;
using Vostok.Hotline.Data.Repository.References.Managers;
using Vostok.Hotline.Gateway.Client.Travels.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Travels.Mapper
{
	public class TravelMapper
	{
		private readonly TravelCountryMapper _travelCountryMapper;

		public TravelMapper(TravelCountryMapper travelCountryMapper) {
			_travelCountryMapper = travelCountryMapper;
		}

		public async Task<TravelViewModel> MapToTravelViewModelAsync(TravelResponseModel response, CancellationToken cancellationToken)
		{
			return new TravelViewModel
			{ 
				TravelId = response.TravelId,
				SolarId = response.SolarId,
				Status = MapToTravelStatusEnum(response.TravelStatus),
				Countries = await _travelCountryMapper.MapToListTravelCountryViewModelAsync(response.TravelCountries, cancellationToken),
				StartTravel = response.StartTravel,
				EndTravel = response.EndTravel,
				ContactPhone = response.ContactPhone,
				Cards = response.TravelCards?.ToListTravelCardViewModel(),
				CashWithdrawalLimit = response.CashWithdrawalLimit,
				LimitCardTransfers = response.LimitCardTransfers
			};
		}

		public TravelStatusEnum MapToTravelStatusEnum(TravelStatusResponseEnum response)
			 => response switch
			 {
				 TravelStatusResponseEnum.Active => TravelStatusEnum.Active,
				 TravelStatusResponseEnum.Deleted => TravelStatusEnum.Deleted,
				 TravelStatusResponseEnum.Planned => TravelStatusEnum.Planned,
				 TravelStatusResponseEnum.Finished => TravelStatusEnum.Finished,
				 TravelStatusResponseEnum.Error => TravelStatusEnum.Error,
				 _ => TravelStatusEnum.Undefined,
			 };


		public async Task<SearchPagedResponseRowModel<TravelViewModel>> MapToPagedTravelViewModelAsync(SearchPagedResponseRowModel<TravelResponseModel> response, SearchPagingModel request, CancellationToken cancellationToken)
		{
			var result = new SearchPagedResponseRowModel<TravelViewModel>();
			result.IsNextPageAvailable = response.IsNextPageAvailable;

			var currentPage = request.CurrentPage.GetValueOrDefault(0);

			int i = 1;
			foreach (var item in response.Rows)
			{
				var travel = await MapToTravelViewModelAsync(item, cancellationToken);
				travel.RowNum = request.PageSize * currentPage  + i;
				result.Rows.Add(travel);
				i++;
			}
			return result;
		}
	}
}
