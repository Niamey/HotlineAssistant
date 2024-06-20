using Microsoft.AspNetCore.Http;
using Vostok.Hotline.Authorization.FzBank.Models;
using Vostok.Hotline.Authorization.FzBank.Responses;

namespace Vostok.Hotline.Authorization.FzBank.Mappers
{
	public static class AuthMapperExtensions
	{
		public static FzBankAuthorizationApiModel ToFzBankAuthorization(this AuthSuccessResponseModel response)
		{
			if (response?.Session != null && response.ErrorCode == 0)
			{
				return new FzBankAuthorizationApiModel
				{
					Data = new FzBankAuthorizationDataApiModel
					{
						AvatarBase64 = response.Session.AvatarBase64,
						FIO = response.Session.FIO,
						Login = response.Session.Login,
						Position = response.Session.Position,
						UserSessionId = response.Session.SessionId
					},
					StatusCode = StatusCodes.Status200OK
				};
			}

			if (response != null)
			{
				return new FzBankAuthorizationApiModel
				{
					StatusCode = response.ErrorCode,
					ErrorMessage = response.ErrorString
				};
			}

			return new FzBankAuthorizationApiModel
			{
				StatusCode = StatusCodes.Status401Unauthorized,
				ErrorMessage = "Unauthorized"
			};
		}
	}
}
