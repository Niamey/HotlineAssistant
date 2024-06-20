using System.Runtime.Serialization;
using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Cards.Enums
{
	/// <summary>
	/// Статус карты
	/// </summary>
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	public enum CardStatusRetailEnum
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// Заблокирована (All transactions prohibited)
		/// </summary>
		[EnumMember(Value = "BLOCKED")]
		Blocked,
		/// <summary>
		/// Активная
		/// </summary>
		[EnumMember(Value = "ACTIVE")]
		Active,
		/// <summary>
		/// Утеряна(Balance Inquury Only)
		/// </summary>
		[EnumMember(Value = "STOLEN")]
		Stolen,
		/// <summary>
		/// Украдена(Balance Inquury Only)
		/// </summary>
		[EnumMember(Value = "LOST")]
		Lost,
		/// <summary>
		/// Аннулирована(All transactions prohibited)
		/// </summary>
		[EnumMember(Value = "CANCELED")]
		Canceled,
		/// <summary>
		/// Приостановлена
		/// </summary>
		[EnumMember(Value = "SUSPEND")]
		Suspend
	}
}
