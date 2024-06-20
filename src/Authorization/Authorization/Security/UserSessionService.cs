using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Vostok.Hotline.Authorization.Security.Extensions;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Security;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Authorization.Security
{
	public class UserSessionService
	{
		private readonly HttpContext _httpContext;
		private readonly UserProfileManager _userProfileManager;

		public UserSessionService(IHttpContextAccessor httpContextAccessor, UserProfileManager userProfileManager)
		{
			_httpContext = httpContextAccessor.HttpContext;
			_userProfileManager = userProfileManager;
		}

		public UserSession GetCurrentSession()
		{
			if (_httpContext?.User?.Identity?.IsAuthenticated != true)
				return null;

			var userSession = FromClaimsPrincipal(_httpContext.User);
			if (userSession != null)
			{
				userSession.CurrentUser.IpAddress = _httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
				var userProfile = _userProfileManager.GetUserProfile(userSession.UserSessionId);
				if (userProfile == null || userProfile.Login != userSession.CurrentUser.Login)
					return null;

				userSession.CurrentUser.AvatarBase64 = userProfile.AvatarBase64;
				userSession.CurrentUser.Email = userProfile.Email;
				userSession.CurrentUser.FullName = userProfile.FullName;
				userSession.CurrentUser.Position = userProfile.Position;
			}

			return userSession;
		}

		public UserSession FromClaimsPrincipal(ClaimsPrincipal principal)
		{
			return principal != null
			  ? new UserSession
			  {
				  UserSessionId = Converter.ToGuid(principal.GetClaimNullableValue<string>(AppClaimsTypes.UserSessionId)),
				  CurrentUser = new UserProfileModel
				  {
					  Login = principal.GetClaimValue<string>(AppClaimsTypes.Login)
				  }
			  }
			  : null;
		}
	}
}