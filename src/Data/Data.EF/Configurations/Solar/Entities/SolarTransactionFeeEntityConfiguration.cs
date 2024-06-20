using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Data.EF.Entities.Solar;

namespace Vostok.Hotline.Data.EF.Configurations.Solar.Entities
{
	public static class SolarTransactionFeeEntityConfiguration
	{
		public static void ConfigureHotlineSolarTransactionFeeEntity(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SolarTransactionFee>(entity =>
			{
				entity.HasKey(x => x.Id);
				entity.ToView("vw_bo_fee_txn", "SOLAR_BO");

				entity.Property(e => e.Id)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ID");

				entity.Property(e => e.AccessorId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ACCESSOR_ID");

				entity.Property(e => e.AccessorNumber)
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("ACCESSOR_NUMBER");

				entity.Property(e => e.AccessorNumberHash)
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("ACCESSOR_NUMBER_HASH");

				entity.Property(e => e.AgreementId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("AGREEMENT_ID");

				entity.HasOne(d => d.Agreement)
					.WithMany(p => p.Fees)
					.HasForeignKey(m => m.AgreementId);

				entity.Property(e => e.Attributes)
					.HasMaxLength(4000)
					.IsUnicode(false)
					.HasColumnName("ATTRIBUTES");

				entity.Property(e => e.BankingDate)
					.HasColumnName("BANKING_DATE");

				entity.Property(e => e.BillingAmount)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("BILLING_AMOUNT");

				entity.Property(e => e.BillingCurrency)
					.HasMaxLength(3)
					.IsUnicode(false)
					.HasColumnName("BILLING_CURRENCY");

				entity.Property(e => e.ExportStatus)
					.IsRequired()
					.HasMaxLength(2)
					.IsUnicode(false)
					.HasColumnName("EXPORT_STATUS");

				entity.Property(e => e.FeeAmount)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("FEE_AMOUNT");

				entity.Property(e => e.FeeCurrency)
					.HasMaxLength(3)
					.IsUnicode(false)
					.HasColumnName("FEE_CURRENCY");

				entity.Property(e => e.FeeDetails)
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("FEE_DETAILS");

				entity.Property(e => e.FeeRuleId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("FEE_RULE_ID");

				entity.Property(e => e.NextStatus)
					.HasMaxLength(2)
					.IsUnicode(false)
					.HasColumnName("NEXT_STATUS");

				entity.Property(e => e.NonTxnFeeRuleActionId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("NON_TXN_FEE_RULE_ACTION_ID");

				entity.Property(e => e.NonTxnFeeRuleId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("NON_TXN_FEE_RULE_ID");

				entity.Property(e => e.PrevFeeTxnId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("PREV_FEE_TXN_ID");

				entity.Property(e => e.ResponseCode)
					.HasMaxLength(3)
					.IsUnicode(false)
					.HasColumnName("RESPONSE_CODE");

				entity.Property(e => e.RootFeeId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ROOT_FEE_ID");

				entity.Property(e => e.Status)
					.HasMaxLength(2)
					.IsUnicode(false)
					.HasColumnName("STATUS");

				entity.Property(e => e.SystemDate)
					.HasColumnName("SYSTEM_DATE");

				entity.Property(e => e.TrfTariffValueId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("TRF_TARIFF_VALUE_ID");

				entity.Property(e => e.TxnDirection)
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("TXN_DIRECTION");

				entity.Property(e => e.TxnId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("TXN_ID");

				entity.HasOne(d => d.Txn)
					.WithMany(p => p.Fees)
					.HasForeignKey(m => m.TxnId);

				entity.Property(e => e.TxnTypeId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("TXN_TYPE_ID");

				entity.HasOne(d => d.TxnType)
					.WithMany(p => p.Fees)
					.HasForeignKey(m => m.TxnTypeId);
			});
		}
	}
}
