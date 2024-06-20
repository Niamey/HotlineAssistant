using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Data.EF.Entities.Solar;

namespace Vostok.Hotline.Data.EF.Configurations.Solar.Entities
{
	public static class SolarTransactionReversalQueryEntityConfiguration
	{
		public static void ConfigureHotlineSolarTransactionReversalQueryEntity(this ModelBuilder modelBuilder)
		{
			modelBuilder.Ignore<SolarTransactionReversalQuery>();
			modelBuilder.Entity<SolarTransactionReversalQuery>().HasNoKey();
		}
	}
}
