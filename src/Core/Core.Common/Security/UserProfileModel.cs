using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Core.Common.Security
{
	public class UserProfileModel
	{
		public string Login { get; set; }
		public string FullName { get; set; }
		public string Position { get; set; }
		public string Email { get; set; }

		public string IpAddress { get; set; }
		public string AvatarBase64 { get; set; }
		//public RoleAccessSettings Permissions { get; set; }
	}
}
