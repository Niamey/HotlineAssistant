using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace HotLineAssistant.Testing.Functional.Auth
{
    public class TestAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        public ClaimsIdentity Identity { get; set; } = new ClaimsIdentity(new[]
        {
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", Guid.NewGuid().ToString()),
            new Claim("http://schemas.microsoft.com/identity/claims/tenantid", "test"),
            new Claim("http://schemas.microsoft.com/identity/claims/objectidentifier", Guid.NewGuid().ToString()),
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "test"),
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "test"),
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn", "test"),
        }, "test");
    }
}