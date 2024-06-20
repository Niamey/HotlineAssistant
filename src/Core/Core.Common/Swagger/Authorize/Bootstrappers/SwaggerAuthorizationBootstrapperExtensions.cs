using Microsoft.AspNetCore.Builder;

namespace Vostok.Hotline.Core.Common.Swagger.Authorize.Bootstrappers
{
	public static class SwaggerAuthorizationBootstrapperExtensions
	{
		public static void UseHotlineAuthorizationSwaggerRules(this IApplicationBuilder applicationBuilder)
		{
			applicationBuilder.UseSwaggerAuthorization();
		}
	}
}
