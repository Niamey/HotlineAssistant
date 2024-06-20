using System;
using System.Security.Claims;
using Vostok.Hotline.Authorization.Security;
using Vostok.Hotline.Authorization.Security.Collections;
using Vostok.Hotline.Authorization.ViewModels;
using Vostok.Hotline.Core.Common.Security;

namespace Vostok.Hotline.Authorization.Mappers
{
	public static class AuthenticatedUserMapperExtensions
	{
		public static ClaimCollection ToClaim(this UserProfileModel user, Guid userSessionId)
		{
			var claims = new ClaimCollection();

			claims.Add(AppClaimsTypes.Login, user.Login);
			//claims.Add(AppClaimsTypes.FullName, user.FullName);
			//claims.Add(AppClaimsTypes.Position, user.Position);
			claims.Add(AppClaimsTypes.UserSessionId, userSessionId.ToString());
			//claims.Add(ClaimTypes.Email, user.Email);
			//claims.Add(AppClaimsTypes.AvatarBase64, user.AvatarBase64);

			return claims;
		}

		public static UserProfileViewModel ToUserProfileViewModel(this UserProfileModel userProfile)
		{
			return new UserProfileViewModel
			{
				AvatarBase64 = userProfile.AvatarBase64,
				Email = userProfile.Email,
				FullName = userProfile.FullName,
				Login = userProfile.Login,
				Position = userProfile.Position
			};
		}

		public static UserProfileModel ToUserProfileModel(this UserProfileViewModel userProfile)
		{
			return new UserProfileModel
			{
				AvatarBase64 = userProfile.AvatarBase64,
				Email = userProfile.Email,
				FullName = userProfile.FullName,
				Login = userProfile.Login,
				Position = userProfile.Position
			};
		}
	}
}