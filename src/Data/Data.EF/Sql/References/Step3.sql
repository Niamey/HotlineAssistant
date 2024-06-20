CREATE TABLE [dbo].[CountrySettings] (
    [countryCode] varchar(3) NOT NULL,
    [isCountryRisk] bit NOT NULL,
    CONSTRAINT [PK_CountrySettings] PRIMARY KEY ([countryCode])
);

GO

INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210610073236_AddCountrySetting', N'3.1.8');

GO

