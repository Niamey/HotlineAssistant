using System;

namespace Vostok.Hotline.Core.Common.Security
{
	public class UserSession
	{
		public Guid UserSessionId { get; set; }
		public UserProfileModel CurrentUser { get; set; }

		public bool Authorized => !string.IsNullOrEmpty(CurrentUser?.Login);
	}
}
