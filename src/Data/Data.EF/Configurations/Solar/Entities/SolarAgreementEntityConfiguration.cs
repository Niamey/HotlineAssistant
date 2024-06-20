using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Data.EF.Entities.Solar;

namespace Vostok.Hotline.Data.EF.Configurations.Solar.Entities
{
	public static class SolarAgreementEntityConfiguration
	{
		public static void ConfigureHotlineSolarAgreementEntity(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SolarAgreement>(entity =>
			{
				entity.HasKey(x => x.Id);
				entity.ToView("vw_bo_agreement", "SOLAR_BO");

				entity.Property(e => e.Id)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ID");

				entity.Property(e => e.AgrNumber)
					.IsRequired()
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("AGR_NUMBER");

				entity.Property(e => e.AgreementClass)
					.HasMaxLength(2)
					.IsUnicode(false)
					.HasColumnName("AGREEMENT_CLASS");

				entity.Property(e => e.Attributes)
					.HasMaxLength(4000)
					.IsUnicode(false)
					.HasColumnName("ATTRIBUTES");

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

				entity.Property(e => e.BankingDate)
					.HasColumnName("BANKING_DATE");

				entity.Property(e => e.BranchId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("BRANCH_ID");

				entity.Property(e => e.ClientId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("CLIENT_ID");

				entity.Property(e => e.ClosingDate)
					.HasColumnName("CLOSING_DATE");

				entity.Property(e => e.Configuration)
					.HasMaxLength(4000)
					.IsUnicode(false)
					.HasColumnName("CONFIGURATION");

				entity.Property(e => e.Currency)
					.HasMaxLength(3)
					.IsUnicode(false)
					.HasColumnName("CURRENCY");

				entity.Property(e => e.DefaultMcc)
					.HasMaxLength(4)
					.IsUnicode(false)
					.HasColumnName("DEFAULT_MCC");

				entity.Property(e => e.Description)
					.HasMaxLength(4000)
					.IsUnicode(false)
					.HasColumnName("DESCRIPTION");

				entity.Property(e => e.EffectiveDate)
					.HasColumnName("EFFECTIVE_DATE");

				entity.Property(e => e.ExpirationDate)
					.HasColumnName("EXPIRATION_DATE");

				entity.Property(e => e.ExtraCurrencyList)
					.HasMaxLength(4000)
					.IsUnicode(false)
					.HasColumnName("EXTRA_CURRENCY_LIST");

				entity.Property(e => e.FinInstitutionId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("FIN_INSTITUTION_ID");

				entity.Property(e => e.GdsCodingSchemeId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("GDS_CODING_SCHEME_ID");

				entity.Property(e => e.IsOverridingMulticurrency)
					.IsRequired()
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("IS_OVERRIDING_MULTICURRENCY");

				entity.Property(e => e.Isolated)
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("ISOLATED");

				entity.Property(e => e.LastBillingDate)
					.HasColumnName("LAST_BILLING_DATE");

				entity.Property(e => e.Name)
					.HasMaxLength(4000)
					.IsUnicode(false)
					.HasColumnName("NAME");

				entity.Property(e => e.ProductId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("PRODUCT_ID");

				entity.Property(e => e.ServiceType)
					.HasMaxLength(2)
					.IsUnicode(false)
					.HasColumnName("SERVICE_TYPE");

				entity.Property(e => e.SignatureDate)
					.HasColumnName("SIGNATURE_DATE");
			});
		}
	}
}
