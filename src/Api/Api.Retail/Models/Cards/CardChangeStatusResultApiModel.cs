using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;

namespace Vostok.Hotline.Api.Retail.Models.Cards
{
	public class CardChangeStatusResultApiModel
	{
		public StatusCommandApiViewModel Status { get; set; }

		public CardChangeStatusCommentApiModel Comment { get; set; }
	}
}
