using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Data.EF.Entities.Solar;

namespace Vostok.Hotline.Data.EF.Configurations.Solar.Entities
{
	public static class SolarAccessorEntityConfiguration
	{
		public static void ConfigureHotlineSolarAccessorEntity(this ModelBuilder modelBuilder)
		{
			/*
			modelBuilder.Ignore<SolarAccessor>();
			modelBuilder.Entity<SolarAccessor>().HasNoKey();
			*/

			modelBuilder.Entity<SolarAccessor>(entity =>
			{
				entity.HasKey(x => x.AccessorId);
				entity.ToView("vw_bo_accessor", "SOLAR_BO");
				
				entity.Property(e => e.AccessorId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("AccessorId");

				entity.Property(e => e.AccessorTypeId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("AccessorTypeId");

				entity.Property(e => e.AgreementId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("AgreementId");

				entity.HasOne(d => d.Agreement)
					.WithMany(p => p.Accessors)
					.HasForeignKey(m => m.AgreementId);

				entity.Property(e => e.AccessNumber)
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("AccesNumber");

				entity.Property(e => e.IsDelete)
					.HasColumnType("int")
					.HasColumnName("IsDelete");

				entity.Property(e => e.ClientId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ClientId");

				entity.Property(e => e.ParentAccessorId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ParentAccessorId");

			});
		}
	}
}
