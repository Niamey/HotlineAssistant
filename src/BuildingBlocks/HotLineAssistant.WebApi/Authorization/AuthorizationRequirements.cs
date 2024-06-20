using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Vostok.HotLineAssistant.WebApi.Authorization
{
	public static class AuthorizationRequirements
	{
		public static readonly IAuthorizationRequirement None = new AssertionRequirement(c => true);
	}
}