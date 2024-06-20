using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Data.EF.Entities.Solar;

namespace Vostok.Hotline.Data.EF.Configurations.Solar.Entities
{
	public static class SolarStmtEntryAgreementEntityConfiguration
	{
		public static void ConfigureHotlineSolarStmtEntryAgreementEntity(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SolarStmtEntryAgreement> (entity =>
			{
				// entity.HasNoKey();
				entity.HasKey(x => new { x.AgreementId, x.StmtEntryId, x.StmtDate });
				entity.ToView("vw_bo_stmt_entry_agmt", "SOLAR_BO");

				entity.Property(e => e.AgreementId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("AGREEMENT_ID");

				entity.HasOne(d => d.Agreement)
					.WithMany(p => p.StmtEntryAgreements)
					.HasForeignKey(m => m.AgreementId);

				entity.Property(e => e.ShowInStatement)
					.IsRequired()
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("SHOW_IN_STATEMENT");

				entity.Property(e => e.StmtDate)
					.HasColumnName("STMT_DATE");

				entity.Property(e => e.StmtEntryId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("STMT_ENTRY_ID");

				entity.HasOne(d => d.StmtEntry)
					.WithMany(p => p.StmtEntryAgreements)
					.HasForeignKey(m => m.StmtEntryId);

			});
		}
	}
}
