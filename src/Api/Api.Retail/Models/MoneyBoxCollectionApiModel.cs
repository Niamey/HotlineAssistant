using System.Collections.Generic;
using Vostok.Hotline.Api.Retail.Models.MoneyBox;

namespace Vostok.Hotline.Api.Retail.Models
{
	public class MoneyBoxCollectionApiModel : List<MoneyBoxApiModel>
	{
		public MoneyBoxCollectionApiModel(IList<MoneyBoxApiModel> list)
			: base(list)
		{
		}

		public MoneyBoxCollectionApiModel(IEnumerable<MoneyBoxApiModel> list)
			: base(list)
		{
		}

		public MoneyBoxCollectionApiModel() : base()
		{
		}
	}
}
