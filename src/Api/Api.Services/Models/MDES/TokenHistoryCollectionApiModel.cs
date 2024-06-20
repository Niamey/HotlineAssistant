using System.Collections.Generic;

namespace Vostok.Hotline.Api.Services.Models.MDES
{
	public class TokenHistoryCollectionApiModel: List<TokenHistoryApiModel>
	{
		public TokenHistoryCollectionApiModel(IList<TokenHistoryApiModel> list) : base(list)
		{
		}

		public TokenHistoryCollectionApiModel(IEnumerable<TokenHistoryApiModel> list) : base(list)
		{
		}

		public TokenHistoryCollectionApiModel() : base()
		{
		}
	}
}
