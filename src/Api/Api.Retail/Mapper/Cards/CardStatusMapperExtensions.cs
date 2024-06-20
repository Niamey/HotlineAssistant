using System;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Api.Retail.Requests.Commands.Cards;
using Vostok.Hotline.Api.Retail.Responses.Cards.Enums;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.Mapper.Cards
{
	internal static class CardStatusMapperExtensions
	{
		public static CardStatusEnum ToCardStatusEnum(this CardStatusRetailEnum response)
		   => response switch
		   {
			   CardStatusRetailEnum.Active => CardStatusEnum.Active,
			   CardStatusRetailEnum.Blocked => CardStatusEnum.Blocked,
			   CardStatusRetailEnum.Canceled => CardStatusEnum.Canceled,
			   CardStatusRetailEnum.Lost => CardStatusEnum.Lost,
			   CardStatusRetailEnum.Stolen => CardStatusEnum.Stolen,
			   CardStatusRetailEnum.Suspend => CardStatusEnum.Suspend,
			   _ => CardStatusEnum.Undefined
		   };

		public static CardStatusRetailEnum ToCardStatusRetailEnum(this CardStatusEnum response)
		   => response switch
		   {
			   CardStatusEnum.Active => CardStatusRetailEnum.Active,
			   CardStatusEnum.Blocked => CardStatusRetailEnum.Blocked,
			   CardStatusEnum.Canceled => CardStatusRetailEnum.Canceled,
			   CardStatusEnum.Lost => CardStatusRetailEnum.Lost,
			   CardStatusEnum.Stolen => CardStatusRetailEnum.Stolen,
			   CardStatusEnum.Suspend => CardStatusRetailEnum.Suspend,
			   _ => CardStatusRetailEnum.Undefined
		   };

		public static CardChangeStatusCommentApiModel ToChangeStatusComment(this CardChangeStatusCommentCommand response)
			=> new CardChangeStatusCommentApiModel()
			{
				UserLogin = response?.UserLogin,
				SystemName = response?.SystemName,
				Comment = response?.Comment,
				IsEmployee = response.IsEmployee,
				ModifyDate = response.ModifyDate
			};
	}
}
