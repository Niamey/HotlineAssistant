using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Managers;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Data.EF.Enums;
using Vostok.Hotline.Data.Repository.Core.Managers;
using Vostok.Hotline.Data.Repository.Core.Models.Requests;
using Vostok.Hotline.Data.Repository.References.Managers;
using Vostok.Hotline.Gateway.Client.Travels.Mapper;
using Vostok.Hotline.Gateway.Client.Travels.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Travels.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Travels.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Travels.Services
{
	public class TravelService
	{
		private readonly TravelManager _travelManager;
		private readonly TravelMapper _travelMapper;
		private readonly TravelApiManager _travelApiManager;
		private readonly CountryManager _countryManager;

		private enum TemplatePath {
			ClientNotAbroadNotRisky,
			ClientNotAbroadNotChangeLimit,
			ClientNotAbroadChangeLimit,
			ClientAbroadNotRisky,
			ClientAbroadNotChangeLimit,
			ClientAbroadChangeLimit			
		}

		private readonly Dictionary<TemplatePath, string> _commentTemplates = new Dictionary<TemplatePath, string> {
			{ TemplatePath.ClientNotAbroadNotRisky, "Клієнт планує поїздку в {country}, період перебування з {dateFrom} до {dateTo}, контактний телефон {phone}, картка {pan}" },
			{ TemplatePath.ClientNotAbroadNotChangeLimit, "Клієнт планує поїздку в {country}, період перебування з {dateFrom} до {dateTo}, контактний телефон {phone}, картка {pan}, ліміти НЕ змінювати" },
			{ TemplatePath.ClientNotAbroadChangeLimit, " Клієнт планує поїздку в {country}, період перебування з {dateFrom} до {dateTo}, контактний телефон {phone}, картка {pan}. Прохання встановити ліміт на зняття готівки {withdrawal}, на перекази з картки {transfer}" },
			{ TemplatePath.ClientAbroadNotRisky, "Клієнт перебуває в {country}, період перебування з {dateFrom} до {dateTo}, контактний телефон {phone}, картка {pan}" },
			{ TemplatePath.ClientAbroadNotChangeLimit, "Клієнт перебуває в {country}, період перебування з {dateFrom} до {dateTo}, контактний телефон {phone}, картка {pan}, ліміти НЕ змінювати" },
			{ TemplatePath.ClientAbroadChangeLimit, " Клієнт перебуває в {country}, період перебування з {dateFrom} до {dateTo}, контактний телефон {phone}, картка {pan}. Прохання встановити ліміт на зняття готівки {withdrawal}, на перекази з картки {transfer}" },
		};

		public TravelService(TravelManager travelManager, TravelMapper travelMapper, TravelApiManager travelApiManager, CountryManager countryManager)
		{
			_travelManager = travelManager;
			_travelMapper = travelMapper;
			_travelApiManager = travelApiManager;
			_countryManager = countryManager;
		}

		public async Task<SearchPagedResponseRowModel<TravelViewModel>> GetTravelPagedAsync(TravelPagedQuery request, CancellationToken cancellationToken)
		{
			var searchRequest = new TravelSearchRequest()
			{
				SolarId = request.SolarId,
				Paging = new SearchPagingModel()
				{
					CurrentPage = request.PageIndex,
					PageSize = request.PageSize
				},
				Sorting = new SearchSortingModel()
				{
					Column = request.SortColumn,
					IsDescending = request.SortDirection == SortDirectionEnum.Descending
				}
			};
			var result = await _travelManager.GetPagedAsync(searchRequest, cancellationToken);

			return await _travelMapper.MapToPagedTravelViewModelAsync(result, searchRequest.Paging, cancellationToken);
		}

		public async Task<TravelViewModel> GetTravelAsync(TravelDetailQuery request, CancellationToken cancellationToken)
		{
			var result = await _travelManager.GetDetailAsync(request.SolarId.Value, request.TravelId.Value, cancellationToken);

			if (result != null)
				return await _travelMapper.MapToTravelViewModelAsync(result, cancellationToken);
			else
				throw new NotFoundRequestException();
		}

		public async Task<StatusCommandViewModel> DeleteTravelAsync(TravelDeleteCommand request, CancellationToken cancellationToken)
		{
			var result = await _travelManager.UpdateStatusAsync(request.SolarId.Value, request.TravelId.Value, TravelStatusEnum.Deleted, cancellationToken);

			return new StatusCommandViewModel()
			{
				Success = result,
				Message = result ? "Поїздка успішна видалена" : "Помилка при видаленні поїздки"
			};
		}


		public async Task<StatusCommandViewModel> NewTravelAsync(TravelNewCommand request, CancellationToken cancellationToken)
		{
			var travelId = await _travelManager.CreateAsync(
				request.SolarId.Value,
				request.TravelId.Value,
				request.StartTravel.Value,
				request.EndTravel.Value,
				request.ContactPhone.Value,
				request.Countries.ToListTravelCountry(),
				request.Cards.ToListTravelCard(),
				request.CashWithdrawalLimit.Value,
				request.LimitCardTransfers.Value,
				cancellationToken);

			string commentTemplate = null;
			bool isRisky = request.Countries.Any(x => x.IsRisky);
			bool isChangeLimit = request.CashWithdrawalLimit.Value > 0 || request.LimitCardTransfers.Value > 0;

			if (request.IsClientAbroad.Value)
			{
				if (isRisky)
				{
					commentTemplate = isChangeLimit ? _commentTemplates[TemplatePath.ClientAbroadChangeLimit]
						: _commentTemplates[TemplatePath.ClientAbroadNotChangeLimit];
				}
				else
				{
					commentTemplate = _commentTemplates[TemplatePath.ClientAbroadNotRisky];
				}
			}
			else
			{
				if (isRisky)
				{
					commentTemplate = isChangeLimit ? _commentTemplates[TemplatePath.ClientNotAbroadChangeLimit]
						: _commentTemplates[TemplatePath.ClientNotAbroadNotChangeLimit];
				}
				else
				{
					commentTemplate = _commentTemplates[TemplatePath.ClientNotAbroadNotRisky];
				}
			}

			try
			{
				if (String.IsNullOrEmpty(commentTemplate))
				{
					throw new NotFoundRequestException("Відсутній шаблон коментаря");
				}

				List<string> countryNames = new List<string>();
				foreach (var country in request.Countries)
				{
					var countryName = await _countryManager.GetCountryByCodeAsync(country.CountryCode, cancellationToken);
					if (countryName != null)
					{
						countryNames.Add(countryName.CountryName);
					}
				}

				commentTemplate = commentTemplate.Replace("{country}", String.Join(", ", countryNames));
				commentTemplate = commentTemplate.Replace("{dateFrom}", Converter.ToDateTimeToStringFormat(request.StartTravel, "dd.MM.yyyy"));
				commentTemplate = commentTemplate.Replace("{dateTo}", Converter.ToDateTimeToStringFormat(request.EndTravel, "dd.MM.yyyy"));
				commentTemplate = commentTemplate.Replace("{phone}", request.ContactPhone.Value.ToString());
				commentTemplate = commentTemplate.Replace("{withdrawal}", request.CashWithdrawalLimit.Value.ToString());
				commentTemplate = commentTemplate.Replace("{transfer}", request.LimitCardTransfers.Value.ToString());

				foreach (var card in request.Cards)
				{
					var comment = commentTemplate.Replace("{pan}", CardHelper.GetMaskedCardNumber(card.Number, 'X'));
					var resultComment = await _travelApiManager.AddCommentToCardAsync(card.CardId, comment, cancellationToken);
					if (resultComment == null)
					{
						throw new NotFoundRequestException($"Не вдалося додати коментаря до карти, id={card.CardId}");
					}
				}
			}
			catch (Exception)
			{
				await _travelManager.UpdateStatusAsync(request.SolarId.Value, travelId, TravelStatusEnum.Error, cancellationToken);
				return new StatusCommandViewModel()
				{
					Success = false,
					Message = "Помилка при створенні поїздки"
				};
			}


			return new StatusCommandViewModel()
			{
				Success = travelId > 0,
				Message = travelId > 0 ? "Поїздка успішно створена" : "Помилка при створенні поїздки"
			};
		}

		public async Task<TravelCountViewModel> TravelCountAsync(TravelCountQuery request, CancellationToken cancellationToken)
		{
			var result = await _travelManager.GetTotalCountAsync(request.SolarId.Value, cancellationToken);

			return result.ToTravelCountViewModel();
		}
	}
}
