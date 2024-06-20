using Vostok.Hotline.Api.Services.Responses.MDES.Enums;
using Vostok.Hotline.Core.Common.Enums.MDES;

namespace Vostok.Hotline.Api.Services.Mapper.MDES
{
	public static class ReasonTypeMapperExtensions
	{
		public static ReasonTypeServiceEnum ToReasonTypeServiceEnum(this ReasonTypeMdesEnum response)
			=> response switch {
				ReasonTypeMdesEnum.Auth => ReasonTypeServiceEnum.A,
				ReasonTypeMdesEnum.Lost => ReasonTypeServiceEnum.L,
				ReasonTypeMdesEnum.Stolen => ReasonTypeServiceEnum.S,
				ReasonTypeMdesEnum.Closed => ReasonTypeServiceEnum.C,
				ReasonTypeMdesEnum.Found => ReasonTypeServiceEnum.F,
				ReasonTypeMdesEnum.Transaction => ReasonTypeServiceEnum.T,
				ReasonTypeMdesEnum.Other => ReasonTypeServiceEnum.Z,
				_ => ReasonTypeServiceEnum.Z
			};

		public static ReasonTypeMdesEnum ToReasonTypeMdesEnum(this ReasonTypeServiceEnum response)
			=> response switch
			{
				ReasonTypeServiceEnum.A => ReasonTypeMdesEnum.Auth,
				ReasonTypeServiceEnum.L => ReasonTypeMdesEnum.Lost,
				ReasonTypeServiceEnum.S => ReasonTypeMdesEnum.Stolen,
				ReasonTypeServiceEnum.C => ReasonTypeMdesEnum.Closed,
				ReasonTypeServiceEnum.F => ReasonTypeMdesEnum.Found,
				ReasonTypeServiceEnum.T => ReasonTypeMdesEnum.Transaction,
				ReasonTypeServiceEnum.Z => ReasonTypeMdesEnum.Other,
				_ => ReasonTypeMdesEnum.Undefined
			};
	}
}
