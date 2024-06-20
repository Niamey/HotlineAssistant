using System;
using System.Collections.Generic;
using System.Linq;
using Vostok.Hotline.Api.Retail.Models.Agreements;
using Vostok.Hotline.Core.Common.Enums.Agreements;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Agreements.Mapper
{
	public static class AgreementStatusMapperExtensions
	{
		public static AgreementHistoryStatusViewModel ToAgreementHistoryStatusViewModel(this AgreementCollectionHistoryStatusApiModel collection)
		{
			if (!collection.Any())
				return new AgreementHistoryStatusViewModel();

			var currentHistory = collection.Where(x => x.ModifyDate < DateTime.Now).FirstOrDefault();

			var result = new AgreementHistoryStatusViewModel
			{
				CurrentStatus = currentHistory == null ? AgreementStatusEnum.Undefined : currentHistory.CurrentStatus,
				DateChangeStatus = currentHistory?.ModifyDate,
				Histories = new List<AgreementHistoryStatusAuditViewModel>()
			};
			result.Histories.AddRange(collection.Select(item => new AgreementHistoryStatusAuditViewModel()
			{
				Status = item.CurrentStatus,
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
