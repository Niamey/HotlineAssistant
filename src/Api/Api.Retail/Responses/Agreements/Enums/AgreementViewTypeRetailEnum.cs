using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Agreements.Enums
{
	/// <summary>
	/// Тип продукта
	/// </summary>
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	internal enum AgreementViewTypeRetailEnum
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// Банк Власний Рахунок
		/// </summary>
		Bvr = 1
	}
}
