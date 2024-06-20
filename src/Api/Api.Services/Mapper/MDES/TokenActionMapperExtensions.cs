using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Services.Responses.MDES;

namespace Vostok.Hotline.Api.Services.Mapper.MDES
{
	public static class TokenActionMapperExtensions
	{
		public static StatusCommandApiViewModel ToStatusCommandApiViewModel (this TokenActionResponseModel response)
		{
			return new StatusCommandApiViewModel
			{
				Success = response.Success,
				Message = response.ErrorText
			};
		}
	}
}
