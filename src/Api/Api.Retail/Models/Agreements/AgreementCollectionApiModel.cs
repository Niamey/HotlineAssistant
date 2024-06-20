using System.Collections.Generic;

namespace Vostok.Hotline.Api.Retail.Models.Agreements
{
	public class AgreementCollectionApiModel : List<AgreementApiModel>
	{
		public AgreementCollectionApiModel(IList<AgreementApiModel> list)
			   : base(list)
		{
		}

		public AgreementCollectionApiModel(IEnumerable<AgreementApiModel> list)
			: base(list)
		{
		}

		public AgreementCollectionApiModel() : base()
		{
		}
	}
}
