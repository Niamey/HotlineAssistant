using Vostok.Hotline.Api.Services.Responses.MDES.Enums;
using Vostok.Hotline.Core.Common.Enums.MDES;

namespace Vostok.Hotline.Api.Services.Mapper.MDES
{
	public static class StatusCodeMapperExtensions
	{
		public static StatusCodeMdesEnum ToStatusCodeMdesEnum(this StatusCodeServiceEnum responses) =>
			responses switch
			{
				StatusCodeServiceEnum.U => StatusCodeMdesEnum.Unmapped,
				StatusCodeServiceEnum.A => StatusCodeMdesEnum.Active,
				StatusCodeServiceEnum.S => StatusCodeMdesEnum.Suspended,
				StatusCodeServiceEnum.D => StatusCodeMdesEnum.Deleted,
				StatusCodeServiceEnum.R => StatusCodeMdesEnum.Remapped,
				_ => StatusCodeMdesEnum.Undefined
			};
	}
}
