using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace HotLineAssistant.Testing.Functional.Auth
{
    public static class ApplicationBuilderExtensions
    {
        public static AuthenticationBuilder AddTestAuthentication(this AuthenticationBuilder builder,
            string authenticationScheme)
        {
            return builder.AddScheme<TestAuthenticationSchemeOptions, TestAuthenticationHandler>(authenticationScheme,
                options =>
                {
                    options.Identity.AddClaim(new Claim(options.Identity.RoleClaimType, "Administrator"));
                });
        }
    }
}