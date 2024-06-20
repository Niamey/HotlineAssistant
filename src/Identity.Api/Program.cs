using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Vostok.HotLineAssistant.Identity.Api
{
	public class Program
	{
		private const string SeedArgs = "/seed";
		public static void Main(string[] args)
		{
			try
			{
				var seed = args.Any(x => x == SeedArgs);
				if (seed) args = args.Except(new[] { SeedArgs }).ToArray();
				var host = CreateHostBuilder(args).Build();
				if (seed)
				{
					/*await DbMigrationHelpers
						.EnsureSeedData<IdentityServerConfigurationDbContext, AdminIdentityDbContext,
							IdentityServerPersistedGrantDbContext, AdminLogDbContext, AdminAuditLogDbContext,
							UserIdentity, UserIdentityRole>(host);*/
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
