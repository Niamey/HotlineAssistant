using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Agreements;

namespace Vostok.Hotline.Gateway.Client.Agreements.ViewModels
{
	public class AgreementMoneyBoxViewModel : ResponseBaseModel
	{
		/// <summary>
		/// ИД накопилки
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// ИД 
		/// </summary>
		public long SavingId { get; set; }
		/// <summary>
		/// Имя накопилки
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public MoneyViewModel Amount { get; set; }
		/// <summary>
		/// Статус
		/// </summary>
		public AgreementMoneyBoxStatusEnum Status { get; set; }
	}
}
