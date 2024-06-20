using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Vostok.Hotline.Core.Data.Contexts
{
	public abstract class ContextBaseFactory<TContext> : IDesignTimeDbContextFactory<TContext>
		where TContext : DbBaseContext
	{
		protected abstract string ProfileName { get; }
		protected abstract string Context { get; }
		protected abstract string ContextPassword { get; }
		protected abstract string GetConnectionString(IConfigurationRoot configuration);

		public TContext CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("launchSettings.json", true)
				.AddJsonFile("Properties\\launchSettings.json", true)
				.Build();

			Environment.SetEnvironmentVariable($"ConnectionStrings.{Context}", configuration.GetSection($"profiles:{ProfileName}:environmentVariables:ConnectionStrings.{Context}").Value, EnvironmentVariableTarget.Process);
			Environment.SetEnvironmentVariable($"ConnectionStrings.{ContextPassword}", configuration.GetSection($"profiles:{ProfileName}:environmentVariables:ConnectionStrings.{ContextPassword}").Value, EnvironmentVariableTarget.Process);

			var optionsBuilder = new DbContextOptionsBuilder<TContext>();
			optionsBuilder.UseSqlServer(GetConnectionString(configuration));

			return DbBaseContext.CreateMigrationContext<TContext>(optionsBuilder.Options);
		}
	}
}
