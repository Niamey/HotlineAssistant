using System.Collections;
using System.Collections.Generic;

namespace Vostok.Hotline.Data.Repository.Solar.Models.Responses
{
	public class TransactionFeeCollectionResponseModel : List<TransactionFeeResponseModel>
	{
		public TransactionFeeCollectionResponseModel() : base()
		{
		}

		public TransactionFeeCollectionResponseModel(IList<TransactionFeeResponseModel> list) : base(list) 
		{
		}

		public TransactionFeeCollectionResponseModel(IEnumerable<TransactionFeeResponseModel> list) : base(list)
		{
		}


	}
}
