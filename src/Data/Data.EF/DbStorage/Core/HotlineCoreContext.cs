using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Core.Common.Base.Abstractions;
using Vostok.Hotline.Core.Data.Contexts;
using Vostok.Hotline.Data.EF.Configurations.Core;
using Vostok.Hotline.Data.EF.Entities.Core;

namespace Vostok.Hotline.Data.EF.DbStorage.Core
{
	public partial class HotlineCoreContext : DbBaseContext
	{
		public HotlineCoreContext(DbContextOptions<HotlineCoreContext> options)
			: base(options)
		{
		}

		public virtual DbSet<EnvironmentSetting> EnvironmentSettings { get; set; }
		public virtual DbSet<LoggingRequest> LoggingRequests { get; set; }
		public virtual DbSet<UserProfile> UserProfiles { get; set; }
		public virtual DbSet<Travel> Travels { get; set; }
		public virtual DbSet<TravelCard> TravelCards { get; set; }
		public virtual DbSet<TravelCountry> TravelCountries { get; set; }

		protected override void ApplyAudit(ISessionContext sessionContext, DbBaseContext dbBaseContext)
		{
			EntitySelfAuditor.ApplyAudit(sessionContext, this);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ConfigureHotlineCore();
		}
	}
}