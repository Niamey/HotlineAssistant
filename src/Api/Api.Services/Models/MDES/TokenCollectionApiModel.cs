using System.Collections.Generic;
using System.Linq;

namespace Vostok.Hotline.Api.Services.Models.MDES
{
	public class TokenCollectionApiModel: List<TokenApiModel>
	{
		public TokenCollectionApiModel(IList<TokenApiModel> list) : base(list)
		{
		}

		public TokenCollectionApiModel(IEnumerable<TokenApiModel> list) : base(list)
		{
		}

		public TokenCollectionApiModel(IOrderedEnumerable<TokenApiModel> list) : base(list)
		{
		}

		public TokenCollectionApiModel() : base()
		{
		}
	}
}
