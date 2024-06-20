using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Data.EF.Configurations.Solar.Entities;
using Vostok.Hotline.Data.EF.Entities.Solar;

namespace Vostok.Hotline.Data.EF.Configurations.Solar
{
	public static class HotlineSolarContextConfiguration
	{
		public static void ConfigureHotlineSolar(this ModelBuilder modelBuilder)
		{
			modelBuilder.ConfigureHotlineSolarAgreementEntity();
			modelBuilder.ConfigureHotlineSolarTransactionEntity();
			modelBuilder.ConfigureHotlineSolarTransactionTypeEntity();
			modelBuilder.ConfigureHotlineSolarTransactionFeeEntity();
			modelBuilder.ConfigureHotlineSolarStmtEntryEntity();
			modelBuilder.ConfigureHotlineSolarStmtEntryAgreementEntity();

			modelBuilder.ConfigureHotlineSolarTransactionQueryEntity();
			modelBuilder.ConfigureHotlineSolarAccessorEntity();
			
			modelBuilder.ConfigureHotlineSolarTransactionReversalQueryEntity();
		}
	}
}
