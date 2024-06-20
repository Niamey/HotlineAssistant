CREATE TABLE [dbo].[Travels] (
    [travelId] int NOT NULL IDENTITY,
    [version] rowversion NULL,
    [createdOn] datetime2 NOT NULL,
    [updatedOn] datetime2 NOT NULL,
    [sessionId] uniqueidentifier NULL,
    [solarId] bigint NOT NULL,
    [travelStatus] int NOT NULL,
    [startTravel] datetime2 NOT NULL,
    [endTravel] datetime2 NOT NULL,
    [contactPhone] bigint NOT NULL,
    [cashWithdrawalLimit] decimal(18,2) NOT NULL,
    [limitCardTransfers] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Travels] PRIMARY KEY ([travelId])
);

GO

CREATE TABLE [dbo].[TravelCards] (
    [travelCardId] int NOT NULL IDENTITY,
    [version] rowversion NULL,
    [createdOn] datetime2 NOT NULL,
    [updatedOn] datetime2 NOT NULL,
    [sessionId] uniqueidentifier NULL,
    [travelId] int NOT NULL,
    [cardId] bigint NOT NULL,
    [cardMaskNumber] varchar(32) NULL,
    CONSTRAINT [PK_TravelCards] PRIMARY KEY ([travelCardId]),
    CONSTRAINT [FK_TravelCards_Travels_travelId] FOREIGN KEY ([travelId]) REFERENCES [dbo].[Travels] ([travelId])
);

GO

CREATE TABLE [dbo].[TravelCountries] (
    [travelCountryId] int NOT NULL IDENTITY,
    [version] rowversion NULL,
    [createdOn] datetime2 NOT NULL,
    [updatedOn] datetime2 NOT NULL,
    [sessionId] uniqueidentifier NULL,
    [travelId] int NOT NULL,
    [countryCode] varchar(3) NOT NULL,
    [riskCountry] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_TravelCountries] PRIMARY KEY ([travelCountryId]),
    CONSTRAINT [FK_TravelCountries_Travels_travelId] FOREIGN KEY ([travelId]) REFERENCES [dbo].[Travels] ([travelId])
);

GO

CREATE INDEX [IX_TravelCards_sessionId] ON [dbo].[TravelCards] ([sessionId]);

GO

CREATE INDEX [IX_TravelCards_travelId] ON [dbo].[TravelCards] ([travelId]);

GO

CREATE INDEX [IX_TravelCountries_sessionId] ON [dbo].[TravelCountries] ([sessionId]);

GO

CREATE INDEX [IX_TravelCountries_travelId] ON [dbo].[TravelCountries] ([travelId]);

GO

CREATE INDEX [IX_Travels_sessionId] ON [dbo].[Travels] ([sessionId]);

GO

INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210611072747_AddTravelEntities', N'3.1.8');

GO

