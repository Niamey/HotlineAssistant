using System.Collections.Generic;

namespace Vostok.Hotline.Gateway.Client.Transactions.ViewModels
{
	public class TransactionFeeCollectionViewModel : List<TransactionFeeViewModel>
	{
		public TransactionFeeCollectionViewModel() : base()
		{
		}

		public TransactionFeeCollectionViewModel(IList<TransactionFeeViewModel> list) : base(list)
		{
		}

		public TransactionFeeCollectionViewModel(IEnumerable<TransactionFeeViewModel> list) : base(list)
		{
		}
	}
}
