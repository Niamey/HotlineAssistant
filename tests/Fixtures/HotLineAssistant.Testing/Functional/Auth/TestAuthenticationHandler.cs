﻿using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HotLineAssistant.Testing.Functional.Auth
{
    public class TestAuthenticationHandler : AuthenticationHandler<TestAuthenticationSchemeOptions>
    {
        public TestAuthenticationHandler(
            IOptionsMonitor<TestAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return Task.FromResult(
                AuthenticateResult.Success(
                    new AuthenticationTicket(
                        new ClaimsPrincipal(Options.Identity),
                        new AuthenticationProperties(),
                        Scheme.Name)));
        }
    }
}