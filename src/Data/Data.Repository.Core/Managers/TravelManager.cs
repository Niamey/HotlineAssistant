using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Data.Repository.Helpers;
using Vostok.Hotline.Data.Repository.Core.Managers.Base;
using Vostok.Hotline.Data.Repository.Core.Models.Requests;
using Vostok.Hotline.Data.Repository.Core.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Data.Repository.Core.Mapper;
using Vostok.Hotline.Data.EF.Entities.Core;
using Vostok.Hotline.Data.EF.Enums;
using System.Collections.Generic;

namespace Vostok.Hotline.Data.Repository.Core.Managers
{
	public class TravelManager : BaseCoreManager
	{
		public TravelManager(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{

		}

		public async Task<SearchPagedResponseRowModel<TravelResponseModel>> GetPagedAsync(TravelSearchRequest request, CancellationToken cancellationToken)
		{
			SearchPagedResponseRowModel<TravelResponseModel> response = new SearchPagedResponseRowModel<TravelResponseModel>();

			var query = DbContext.Travels
				.Include(x => x.TravelCards)
				.Include(x => x.TravelCountries)
				.Where(x => x.SolarId == request.SolarId);

			var sorted = SearchBuildHelper.ApplySorting(query, x => x.TravelId, request);

			var projection = sorted.Select(s => new Travel()
			{
				SessionId = s.SessionId,
				SolarId = s.SolarId,
				TravelId = s.TravelId,
				StartTravel = s.StartTravel,
				EndTravel = s.EndTravel,
				TravelCountries = s.TravelCountries,
				TravelCards = s.TravelCards,
				Version = s.Version,
				CashWithdrawalLimit = s.CashWithdrawalLimit,
				LimitCardTransfers = s.LimitCardTransfers,
				ContactPhone = s.ContactPhone,
				CreatedOn = s.CreatedOn,
				TravelStatus = s.TravelStatus,
				UpdatedOn = s.UpdatedOn
			}.ToTravelResponseModel());

			await SearchBuildHelper.ReadRecordsAsync(response, projection, request, cancellationToken);

			return response;
		}

		public async Task<TravelResponseModel> GetDetailAsync(long solarId, int travelId, CancellationToken cancellationToken)
		{
			var response = await DbContext.Travels
				.Include(x => x.TravelCards)
				.Include(x => x.TravelCountries)
				.Where(x => x.SolarId == solarId && x.TravelId == travelId).Select(query => 
				query.ToTravelResponseModel()
			).FirstOrDefaultAsync(cancellationToken);
			
			return response;
		}

		/*
		public async Task<bool> DeleteAsync(long solarId, int travelId, CancellationToken cancellationToken)
		{
			var travel = await DbContext.Travels		
				.Where(t => t.SolarId == solarId && t.TravelId == travelId)
				.FirstOrDefaultAsync(cancellationToken);

			if (travel == null)
				return false;

			travel.TravelStatus = TravelStatusEnum.Deleted;

			using (var transaction = TransactionFactory.Create())
			{
				var result = await DbContext.SaveChangesAsync(SessionContext, cancellationToken);
				transaction.Complete();
			}
			return true;
		}*/

		public async Task<bool> UpdateStatusAsync(long solarId, int travelId, TravelStatusEnum status, CancellationToken cancellationToken)
		{
			var travel = await DbContext.Travels
				.Where(t => t.SolarId == solarId && t.TravelId == travelId)
				.FirstOrDefaultAsync(cancellationToken);

			if (travel == null)
				return false;

			travel.TravelStatus = status;

			using (var transaction = TransactionFactory.Create())
			{
				var result = await DbContext.SaveChangesAsync(SessionContext, cancellationToken);
				transaction.Complete();
			}
			return true;
		}

		public async Task<int> CreateAsync(long solarId, int travelId, DateTime startTravel, DateTime endTravel, long contactPhone,
			List<TravelCountry> countries, List<TravelCard> cards, decimal cashWithdrawalLimit, decimal limitCardTransfers, CancellationToken cancellationToken)
		{

		    var entity = new Travel();
		    DbContext.Travels.Add(entity);
		
			entity.SolarId = solarId;
			entity.TravelStatus = TravelStatusEnum.Active;
			entity.StartTravel = startTravel;
			entity.EndTravel = endTravel;
			entity.ContactPhone = contactPhone;
			entity.CashWithdrawalLimit = cashWithdrawalLimit;
			entity.LimitCardTransfers = limitCardTransfers;

			entity.TravelCountries = new List<TravelCountry>();
			foreach (var country in countries)
			{
				entity.TravelCountries.Add(country);
			}


			entity.TravelCards = new List<TravelCard>();
			foreach (var card in cards)
			{
				entity.TravelCards.Add(card);
			}

			using (var transaction = TransactionFactory.Create())
			{
				var res = await DbContext.SaveChangesAsync(SessionContext, cancellationToken);
				transaction.Complete();
			}

			return entity.TravelId;
		}

		public async Task<TravelCountResponseModel> GetTotalCountAsync(long solarId, CancellationToken cancellationToken)
		{
			var query = DbContext.Travels.Where(x => x.SolarId == solarId);

			return new TravelCountResponseModel() { TotalRows = await query.CountAsync(cancellationToken) };
		}
	}
}
