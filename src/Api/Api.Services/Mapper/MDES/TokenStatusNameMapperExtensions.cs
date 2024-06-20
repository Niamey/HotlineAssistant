using Vostok.Hotline.Api.Services.Responses.MDES.Enums;
using Vostok.Hotline.Core.Common.Enums.MDES;

namespace Vostok.Hotline.Api.Services.Mapper.MDES
{
	public static class TokenStatusNameMapperExtensions
	{
		public static TokenStatusNameMdesEnum ToTokenStatusNameMdesEnum(this TokenStatusNameServiceEnum response)
		=> response switch
		{
			TokenStatusNameServiceEnum.Inactive => TokenStatusNameMdesEnum.Inactive,
			TokenStatusNameServiceEnum.Unmapped => TokenStatusNameMdesEnum.Inactive,
			TokenStatusNameServiceEnum.Active => TokenStatusNameMdesEnum.Active,
			TokenStatusNameServiceEnum.Suspended => TokenStatusNameMdesEnum.Suspended,
			TokenStatusNameServiceEnum.Deleted => TokenStatusNameMdesEnum.Deleted,
			_ => TokenStatusNameMdesEnum.Undefined
		};

	}
}
