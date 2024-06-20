using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Data.EF.Entities.Solar;

namespace Vostok.Hotline.Data.EF.Configurations.Solar.Entities
{
	public static class SolarTransactionTypeEntityConfiguration
	{
		public static void ConfigureHotlineSolarTransactionTypeEntity(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SolarTransactionType>(entity =>
			{
				entity.HasKey(x => x.Id);
				entity.ToView("vw_bo_txn_type", "SOLAR_BO");

				entity.Property(e => e.Id)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ID");

				entity.Property(e => e.AgreementClass)
					.HasMaxLength(2)
					.IsUnicode(false)
					.HasColumnName("AGREEMENT_CLASS");

				entity.Property(e => e.AuditDate)
					.HasColumnName("AUDIT_DATE");

				entity.Property(e => e.AuditState)
					.IsRequired()
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("AUDIT_STATE");

				entity.Property(e => e.AuditUserId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("AUDIT_USER_ID");

				entity.Property(e => e.ChargebackStage)
					.HasMaxLength(2)
					.IsUnicode(false)
					.HasColumnName("CHARGEBACK_STAGE");

				entity.Property(e => e.Code)
					.IsRequired()
					.HasMaxLength(32)
					.IsUnicode(false)
					.HasColumnName("CODE");

				entity.Property(e => e.Description)
					.HasMaxLength(4000)
					.IsUnicode(false)
					.HasColumnName("DESCRIPTION");

				entity.Property(e => e.Direction)
					.IsRequired()
					.HasMaxLength(2)
					.IsUnicode(false)
					.HasColumnName("DIRECTION");

				entity.Property(e => e.IsStandard)
					.IsRequired()
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("IS_STANDARD");

				entity.Property(e => e.MayBePartial)
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("MAY_BE_PARTIAL");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("NAME");

				entity.Property(e => e.OperationClass)
					.IsRequired()
					.HasMaxLength(2)
					.IsUnicode(false)
					.HasColumnName("OPERATION_CLASS");
			});
		}
	}
}
