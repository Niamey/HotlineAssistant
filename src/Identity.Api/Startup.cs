using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Vostok.HotLineAssistant.Identity.Api.Data;
using Vostok.HotLineAssistant.Identity.Api.Models;

namespace Vostok.HotLineAssistant.Identity.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddIdentityServer()
				.AddDeveloperSigningCredential()
				.AddInMemoryApiResources(Config.GetApiResources())
				.AddInMemoryClients(Config.GetClients());
			/*	.AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
			{
				var apiResource = options.ApiResources.First();
				apiResource.UserClaims = new[] { "hasUsersGroup" };

				var identityResource = new IdentityResource
				{
					Name = "customprofile",
					DisplayName = "Custom profile",
					UserClaims = new[] { "hasUsersGroup" },
				};
				identityResource.Properties.Add(ApplicationProfilesPropertyNames.Clients, "*");
				options.IdentityResources.Add(identityResource);
			});

			services.AddAuthentication().AddIdentityServerJwt();*/
			/*services.AddAuthentication()
				.AddOpenIdConnect("Google", "Google",
					o =>
					{
						IConfigurationSection googleAuthNSection =
							Configuration.GetSection("Authentication:Google");
						o.ClientId = googleAuthNSection["ClientId"];
						o.ClientSecret = googleAuthNSection["ClientSecret"];
						o.Authority = "https://accounts.google.com";
						o.ResponseType = OpenIdConnectResponseType.Code;
						o.CallbackPath = "/signin-google";
					}).AddIdentityServerJwt();*/

			services.AddControllers();
			services.AddRazorPages();

			services.AddAuthorization(options =>
			{
				options.AddPolicy("ShouldHasUsersGroup", policy => { policy.RequireClaim("hasUsersGroup"); });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			// this will do the initial DB population
			InitializeDatabase(app);
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();
			app.UseIdentityServer();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
			});
		}

		private void InitializeDatabase(IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
				var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
				context.Database.Migrate();
				if (!context.Clients.Any())
				{
					foreach (var client in Config.GetClients())
					{
						context.Clients.Add(client.ToEntity());
					}

					context.SaveChanges();
				}
				/*if (!context.IdentityResources.Any())
				{
					foreach (var resource in Config.GetIdentityResources())
					{
						context.IdentityResources.Add(resource.ToEntity());
					}
					context.SaveChanges();
				}*/

				if (!context.ApiResources.Any())
				{
					foreach (var resource in Config.GetApiResources())
					{
						context.ApiResources.Add(resource.ToEntity());
					}
					context.SaveChanges();
				}
			}
		}
	}
}
