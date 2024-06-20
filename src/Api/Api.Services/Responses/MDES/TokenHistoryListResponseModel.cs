using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Api.Services.Responses.MDES
{
	public class TokenHistoryListResponseModel
	{
		public List<TokenHistoryResponseModel> Statuses { get; set; }
	}
}
