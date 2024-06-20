using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Data.EF.Entities.Solar;

namespace Vostok.Hotline.Data.EF.Configurations.Solar.Entities
{
	public static class SolarTransactionQueryEntityConfiguration
	{
		public static void ConfigureHotlineSolarTransactionQueryEntity(this ModelBuilder modelBuilder)
		{
			modelBuilder.Ignore<SolarTransactionQuery>();
			modelBuilder.Entity<SolarTransactionQuery>().HasNoKey();
		}
	}
}
