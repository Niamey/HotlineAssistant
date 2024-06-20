using System;
using System.Collections.Generic;
using System.Linq;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Core.Common.Enums.Cards;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public static class CardHistoryStatusMapperExtensions
	{
		public static CardHistoryStatusViewModel ToCardHistoryStatusViewModel(this CardCollectionHistoryStatusApiModel collection)
		{
			if (!collection.Any())
				return new CardHistoryStatusViewModel();

			var currentHistory = collection.Where(x => x.ModifyDate < DateTime.Now).FirstOrDefault();

			var result = new CardHistoryStatusViewModel
				{
					CurrentStatus = currentHistory == null ? CardStatusEnum.Undefined : currentHistory.Status,
					DateChangeStatus = currentHistory?.ModifyDate,
					Histories = new List<CardHistoryStatusAuditViewModel>()
				};
				result.Histories.AddRange(collection.Select(item => new CardHistoryStatusAuditViewModel()
				{
					Status = item.Status,
					Comment = item.Comment,
					SystemName = item.SystemName,
					UserLogin = item.UserLogin,
					ModifyDate = item.ModifyDate,
					IsEmployee = item.IsEmployee
				}));
				return result;
		}
	}
}