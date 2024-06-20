using System;
using Vostok.Hotline.Authorization.ViewModels;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Data.EF.Entities.Core;

namespace Vostok.Hotline.Data.Repository.Core.Mapper
{
	internal static class UserProfileMapperExtensions
	{
		public static UserProfileViewModel ToUserProfileModel(this UserProfile response)
		{
			var result = new UserProfileViewModel()
			{
				Login = response.Login,
				Email = response.Email,
				FullName = response.FullName,
				Position = response.Position,
				AvatarBase64 = Converter.ToBase64String(response.Avatar)				 
			};

			return result;
		}
	}
}
