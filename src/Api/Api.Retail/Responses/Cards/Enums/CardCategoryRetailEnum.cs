using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Cards.Enums
{
	/// <summary>
	/// Категория карты
	/// </summary>
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	internal enum CardCategoryRetailEnum
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// Основная
		/// </summary>
		Primary,
		/// <summary>
		/// Дополнительная
		/// </summary>
		Supplementary
	}
}
