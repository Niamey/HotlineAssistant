using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Core.Common.Base.Abstractions;
using Vostok.Hotline.Core.Data.Contexts;
using Vostok.Hotline.Data.EF.Configurations.References;
using Vostok.Hotline.Data.EF.Entities.References;

namespace Vostok.Hotline.Data.EF.DbStorage.References
{
	public partial class HotlineReferencesContext : DbBaseContext
	{
		public HotlineReferencesContext(DbContextOptions<HotlineReferencesContext> options)
			: base(options)
		{
		}

		public virtual DbSet<RefTransactionCode> RefTransactionCodes { get; set; }
		public virtual DbSet<CountrySetting> CountrySettings { get; set; }

		protected override void ApplyAudit(ISessionContext sessionContext, DbBaseContext dbBaseContext)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ConfigureHotlineReferences();
		}
	}
}