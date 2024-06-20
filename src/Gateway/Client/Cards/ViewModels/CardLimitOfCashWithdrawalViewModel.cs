using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class CardLimitOfCashWithdrawalViewModel : ResponseBaseModel
	{
		/// <summary>
		///доступно ли снятие налички в банкомате
		/// </summary>
		public bool LimitOfCashWithdrawal { get; set; }
		/// <summary>
		///лимиты операции между своими счетами
		/// </summary>
		public CardLimitViewModel InternalTransactions { get; set; }
		/// <summary>
		///операции по переводу средств в пределах и за пределы банка
		/// </summary>
		public CardLimitViewModel TransferTransactions { get; set; }
		/// <summary>
		///Доступно в риск. странах в экв. на снятие наличных и перевода карты
		/// </summary>
		public CardLimitViewModel SystemLimit { get; set; }
		/// <summary>
		///продуктовые лимиты  
		/// </summary>
		public CardLimitViewModel Products { get; set; }
	}
}
