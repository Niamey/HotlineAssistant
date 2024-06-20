using System.Runtime.Serialization;

namespace Vostok.Hotline.Core.Common.Enums.Counterparts
{
	public enum MaritalStatusEnum : byte
	{
		Undefined = UndefinedEnum.Undefined,
		Single = 1,
		Married = 2,
		Divorce = 3,
		Widow = 4,
		InCivilMarriage = 5
	}
}
