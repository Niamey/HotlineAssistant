using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Core.Common.Base.Abstractions;
using Vostok.Hotline.Core.Data.Contexts;
using Vostok.Hotline.Data.EF.Configurations.Solar;
using Vostok.Hotline.Data.EF.Entities.Solar;

namespace Vostok.Hotline.Data.EF.DbStorage.Solar
{
	public partial class HotlineSolarContext : DbBaseContext
	{
		public HotlineSolarContext(DbContextOptions<HotlineSolarContext> options)
			: base(options)
		{
		}

		public virtual DbSet<SolarAgreement> SolarAgreements { get; set; }
		public virtual DbSet<SolarTransaction> SolarTransactions { get; set; }
		public virtual DbSet<SolarTransactionType> SolarTransactionTypes { get; set; }
		public virtual DbSet<SolarTransactionFee> SolarTransactionFees { get; set; }
		public virtual DbSet<SolarStmtEntry> SolarStmtEntries { get; set; }
		public virtual DbSet<SolarStmtEntryAgreement> SolarStmtEntryAgreements { get; set; }

		public virtual DbSet<SolarTransactionQuery> SolarTransactionQuery { get; set; }
		public virtual DbSet<SolarAccessor> SolarAccessors { get; set; }

		public virtual DbSet<SolarTransactionReversalQuery> SolarTransactionReversalQuery { get; set; }

		protected override void ApplyAudit(ISessionContext sessionContext, DbBaseContext dbBaseContext)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ConfigureHotlineSolar();
		}
	}
}