using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Data.EF.Entities.References;

namespace Vostok.Hotline.Data.EF.Configurations.References
{
	public static class HotlineReferencesContextConfiguration
	{
		public static void ConfigureHotlineReferences(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<RefTransactionCode>(entity =>
			{
				entity.HasKey(x => x.Code);
				entity.ToTable("RefTransactionCode", "references");

				entity.Property(e => e.Code)
					.HasMaxLength(2)
					.IsUnicode(false)
					.HasColumnName("code");

				entity.Property(e => e.Rc)
					.HasMaxLength(64)
					.IsUnicode(false)
					.HasColumnName("rc");

				entity.Property(e => e.Description)
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("description");
			});
			modelBuilder.Entity<CountrySetting>(entity =>
			{
				entity.HasKey(x => x.CountryCode);

				entity.Property(e => e.CountryCode)
					.HasMaxLength(3)
					.IsUnicode(false)
					.HasColumnName("countryCode");

				entity.Property(e => e.IsCountryRisk)
					.HasColumnName("isCountryRisk");
			});
		}
	}
}
