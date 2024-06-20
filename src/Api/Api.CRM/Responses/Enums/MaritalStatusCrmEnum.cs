using System.Runtime.Serialization;

namespace Vostok.Hotline.Api.CRM.Responses.Enums
{
	internal enum MaritalStatusCrmEnum : byte
	{
		[EnumMember(Value = "1")]
		Single = 1,
		[EnumMember(Value = "2")]
		Married = 2,
		[EnumMember(Value = "3")]
		Divorce = 3,
		[EnumMember(Value = "4")]
		Widow = 4,
		[EnumMember(Value = "5")]
		InCivilMarriage = 5
	}
}
