using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Models.MoneyBox
{
	public class MoneyBoxApiModel : ResponseBaseModel
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
		public MoneyApiModel Amount { get; set; }
		/// <summary>
		/// Статус
		/// </summary>
		public MoneyBoxStatusEnum Status { get; set; }
	}
}