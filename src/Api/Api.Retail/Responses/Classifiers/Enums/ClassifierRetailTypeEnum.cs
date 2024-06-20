using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Classifiers.Enums
{
	/// <summary>
	/// Тип сущности
	/// </summary>
	[JsonConverter(typeof(HotlineStringEnumConverter))]
	public enum ClassifierRetailTypeEnum
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// клиент
		/// </summary>
		[EnumMember(Value = "CLIENT")]
		Client,
		/// <summary>
		/// договор
		/// </summary>
		[EnumMember(Value = "AGREEMENT")]
		Agreement,
		/// <summary>
		/// аксесор
		/// </summary>
		[EnumMember(Value = "ACCESSOR")]
		Accessor,
	}
}
