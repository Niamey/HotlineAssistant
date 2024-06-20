using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Authorization.ViewModels
{
	public class AuthenticationViewModel : ResponseBaseModel
	{		
		public string Token { get; set; }
		public DateTime TokenExpiresAt { get; set; }

		public UserProfileViewModel UserProfile { get; set; }
	}
}
