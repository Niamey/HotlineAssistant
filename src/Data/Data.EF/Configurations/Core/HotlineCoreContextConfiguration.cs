using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Data.EF.Entities.Core;

namespace Vostok.Hotline.Data.EF.Configurations.Core
{
	public static class HotlineCoreContextConfiguration
	{
		public static void ConfigureHotlineCore(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<EnvironmentSetting>(entity =>
			{
				entity.HasKey(e => e.EnvironmentKey);

				entity.Property(e => e.EnvironmentKey)
					.HasColumnName("environmentKey")
					.HasMaxLength(128)
					.IsUnicode(false)
					.ValueGeneratedNever();

				entity.Property(e => e.EnvironmentValue)
					.HasColumnName("environmentValue")
					.IsUnicode(false);
			});

			modelBuilder.Entity<LoggingRequest>(entity =>
			{
				entity.HasKey(e => e.LoggingRequestId);

				entity.HasIndex(e => e.SessionId);

				entity.HasIndex(e => e.UniqueRequestId);

				entity.Property(e => e.LoggingRequestId)
					.HasColumnName("loggingRequestId");

				entity.Property(e => e.UniqueRequestId)
					.HasColumnName("uniqueRequestId")
					.HasMaxLength(32)
					.IsUnicode(false);

				entity.Property(e => e.LoggingSystemType)
					.HasColumnName("loggingSystemType");

				entity.Property(e => e.LoggingOperationType)
					.HasColumnName("loggingOperationType");

				entity.Property(e => e.HasError)
					.HasColumnName("hasError");

				entity.Property(e => e.RequestValue)
					.HasColumnName("requestValue")
					.IsUnicode(false);

				entity.Property(e => e.ResponseValue)
					.HasColumnName("responseValue")
					.IsUnicode(false);
			});

			modelBuilder.Entity<UserProfile>(entity =>
			{
				entity.HasKey(e => e.UserId);

				entity.HasIndex(e => e.Login).IsUnique();

				entity.HasIndex(e => e.SessionId);

				entity.Property(e => e.UserId)
					.HasColumnName("userId");

				entity.Property(e => e.Login)
					.HasColumnName("login")
					.HasMaxLength(32)
					.IsUnicode(false)
					.IsRequired();

				entity.Property(e => e.Email)
					.HasColumnName("email")
					.HasMaxLength(48)
					.IsUnicode(false)
					.IsRequired();

				entity.Property(e => e.FullName)
					.HasColumnName("fullName")
					.HasMaxLength(96)
					.IsUnicode(false);

				entity.Property(e => e.Position)
					.HasColumnName("position")
					.HasMaxLength(48)
					.IsUnicode(false);

				entity.Property(e => e.Avatar)
					.HasColumnName("avatar");

				entity.Property(e => e.SuccessfulDateLogin)
					.HasColumnName("successfulDateLogin");

				entity.Property(e => e.LastDateLogin)
					.HasColumnName("lastDateLogin");

				entity.Property(e => e.UnSuccessfulLogin)
					.HasColumnName("unSuccessfulLogin")
					.HasDefaultValue(0)
					.IsRequired();

				entity.Property(e => e.UserSessionId)
				.HasColumnName("userSessionId");
			});

			modelBuilder.Entity<Travel>(entity =>
			{
				entity.ToTable("Travels", "dbo");

				entity.HasKey(e => e.TravelId);

				entity.HasIndex(e => e.SessionId);

				entity.Property(e => e.TravelId)
					.HasColumnName("travelId");

				entity.Property(e => e.SolarId)
					.HasColumnName("solarId");

				entity.Property(e => e.TravelStatus)
					.HasColumnName("travelStatus")
					.HasConversion<int>()
					.IsRequired();

				entity.Property(e => e.StartTravel)
					.HasColumnName("startTravel")
					.IsRequired();

				entity.Property(e => e.EndTravel)
					.HasColumnName("endTravel")
					.IsRequired();

				entity.Property(e => e.ContactPhone)
					.HasColumnName("contactPhone");

				entity.Property(e => e.CashWithdrawalLimit)
					.HasColumnName("cashWithdrawalLimit");

				entity.Property(e => e.LimitCardTransfers)
					.HasColumnName("limitCardTransfers");

			});


			modelBuilder.Entity<TravelCard>(entity =>
			{
				entity.ToTable("TravelCards", "dbo");

				entity.HasOne(d => d.Travel)
					.WithMany(p => p.TravelCards)
					.HasForeignKey(m => m.TravelId)
					.Metadata.DeleteBehavior = DeleteBehavior.NoAction;

				entity.HasKey(e => e.TravelCardId);

				entity.HasIndex(e => e.SessionId);

				entity.Property(e => e.TravelCardId)
					.HasColumnName("travelCardId");

				entity.Property(e => e.TravelId)
					.HasColumnName("travelId");

				entity.Property(e => e.CardId)
					.HasColumnName("cardId");

				entity.Property(e => e.CardMaskNumber)
					.HasMaxLength(32)
					.IsUnicode(false)
					.HasColumnName("cardMaskNumber");
			});
	

			modelBuilder.Entity<TravelCountry>(entity =>
			{
				entity.ToTable("TravelCountries", "dbo");

				entity.HasOne(d => d.Travel)
					.WithMany(p => p.TravelCountries)
					.HasForeignKey(m => m.TravelId)
					.Metadata.DeleteBehavior = DeleteBehavior.NoAction;

				entity.HasKey(e => e.TravelCountryId);

				entity.HasIndex(e => e.SessionId);

				entity.Property(e => e.TravelCountryId)
					.HasColumnName("travelCountryId");

				entity.Property(e => e.TravelId)
					.HasColumnName("travelId");

				entity.Property(e => e.CountryCode)
					.HasColumnName("countryCode")
					.HasMaxLength(3)
					.IsUnicode(false)
					.IsRequired();

				entity.Property(e => e.RiskCountry)
					.HasColumnName("riskCountry")
					.HasDefaultValue(0);
			});
		}
	}
}
