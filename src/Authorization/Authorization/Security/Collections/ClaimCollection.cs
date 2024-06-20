using System.Collections.Generic;
using System.Security.Claims;

namespace Vostok.Hotline.Authorization.Security.Collections
{
	public class ClaimCollection : List<Claim>
	{
		public void Add(string item, string value)
		{
			if (value == null)
				return;

			this.Add(new Claim(item, value));
		}
	}
}
