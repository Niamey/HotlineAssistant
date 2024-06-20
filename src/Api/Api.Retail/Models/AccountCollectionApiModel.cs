using System.Collections.Generic;

namespace Vostok.Hotline.Api.Retail.Models
{
	public class AccountCollectionApiModel : List<AccountApiModel>
	{
		public AccountCollectionApiModel(IList<AccountApiModel> list) 
			: base(list)
		{ 
		}

		public AccountCollectionApiModel(IEnumerable<AccountApiModel> list)
			: base(list)
		{
		}

		public AccountCollectionApiModel() : base()
		{ 
		}
	}
}
