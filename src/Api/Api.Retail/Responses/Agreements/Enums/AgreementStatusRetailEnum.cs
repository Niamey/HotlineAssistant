using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Agreements.Enums
{
	/// <summary>
	/// Статус договора
	/// </summary>
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	internal enum AgreementStatusRetailEnum
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// Активный
		/// </summary>
		Active,
		/// <summary>
		/// Приостановленный
		/// </summary>
		Suspended,
		/// <summary>
		/// Замороженный
		/// </summary>
		Frozen,
		/// <summary>
		/// Закрытый
		/// </summary>
		Closed,
		/// <summary>
		/// Списан за счет резерва
		/// </summary>
		Reserve
	}
}
