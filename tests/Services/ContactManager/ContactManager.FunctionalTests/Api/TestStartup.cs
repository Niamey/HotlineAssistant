using HotLineAssistant.Testing.Functional.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vostok.HotLineAssistant.ContactManager.Api;

namespace ContactManager.FunctionalTests.Api
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        /*protected override void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication("Test")
                .AddTestAuthentication("Test");
            
            services.AddAuthorization(o =>
            {
                o.AddPolicy("admin", policy => { policy.RequireAssertion(c => true); });
            });
        }*/
    }
}