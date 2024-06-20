using Microsoft.AspNetCore.Http;
using Vostok.Hotline.Authorization.FzBank.Models;
using Vostok.Hotline.Core.Common.Security;

namespace Vostok.Hotline.Authorization.Mappers
{
	public class FzBankAuthorizationMapper
	{
		private readonly IHttpContextAccessor _httpContext;
		public FzBankAuthorizationMapper(IHttpContextAccessor httpContext)
		{
			_httpContext = httpContext;
		}

		public UserProfileModel ToUserProfile(FzBankAuthorizationDataApiModel user)
		{
			var userProfile = new UserProfileModel
			{
				FullName = user.FIO,
				Login = user.Login,
				Position = user.Position,	
				AvatarBase64 = user.AvatarBase64,
				Email = $"{user.Login}@bankvostok.com.ua"
			};

			userProfile.IpAddress = _httpContext.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

			return userProfile;
		}
	}
}
