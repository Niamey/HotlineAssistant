using System;
using Vostok.Hotline.Core.Data.Entities;

namespace Vostok.Hotline.Data.EF.Entities.Core
{
	public class UserProfile : BusinessEntityBase
	{
		public UserProfile()
		{ 
		}

		public int UserId { get; set; }

		public string Login { get; set; }

		public string Email { get; set; }

		public string FullName { get; set; }

		public string Position { get; set; }

		public byte[] Avatar { get; set; }

		public DateTime? SuccessfulDateLogin { get; set; }

		public DateTime? LastDateLogin { get; set; }

		public byte UnSuccessfulLogin { get; set; }

		public Guid? UserSessionId { get; set; }
	}
}
