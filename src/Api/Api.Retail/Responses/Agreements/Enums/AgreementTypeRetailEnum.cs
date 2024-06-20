using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Agreements.Enums
{
	/// <summary>
	/// Тип договора
	/// </summary>
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	internal enum AgreementTypeRetailEnum
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// Договор карточного счета
		/// </summary>
		CardAccount
	}
}
