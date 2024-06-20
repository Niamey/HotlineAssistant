using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Api.Services.Models.MDES;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public static class CardTokenLastStatusMapperExtensions
	{
		public static CardTokenLastStatusViewModel ToCardTokenLastStatusViewModel(this TokenHistoryApiModel response)
		{
			return new CardTokenLastStatusViewModel { 
				CommentText = response.CommentText,	
				Initiator = response.Initiator,
				ReasonCode = response.ReasonCode,
				StatusCode = response.StatusCode,
				StatusDateTime = response.StatusDateTime,
				StatusDescription = response.StatusDescription
			};

		}
	}
}
