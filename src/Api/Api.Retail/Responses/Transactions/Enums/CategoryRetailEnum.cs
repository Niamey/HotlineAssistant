using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Transactions.Enums
{
	/// <summary>
	/// Категория операции
	/// </summary>
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	public enum CategoryRetailEnum
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// 
		/// </summary>
		ADVICE, 

		/// <summary>
		/// 
		/// </summary>
		REQUEST, 

		/// <summary>
		/// 
		/// </summary>
		NOTIFICATION, 

		/// <summary>
		/// 
		/// </summary>
		CHECK
	}
}
