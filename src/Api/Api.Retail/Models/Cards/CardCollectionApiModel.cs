using System.Collections.Generic;

namespace Vostok.Hotline.Api.Retail.Models.Cards
{
	public class CardCollectionApiModel : List<CardApiModel>
	{
		public CardCollectionApiModel(IList<CardApiModel> list)
				  : base(list)
		{
		}

		public CardCollectionApiModel(IEnumerable<CardApiModel> list)
			: base(list)
		{
		}

		public CardCollectionApiModel() : base()
		{
		}
	}
}
