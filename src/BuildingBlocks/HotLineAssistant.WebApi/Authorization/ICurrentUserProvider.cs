using System.Security.Claims;

namespace Vostok.HotLineAssistant.WebApi.Authorization
{
	public interface ICurrentUserProvider
	{
		ClaimsPrincipal GetUser();
	}
}