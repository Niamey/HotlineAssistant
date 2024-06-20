IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF SCHEMA_ID(N'references') IS NULL EXEC(N'CREATE SCHEMA [references];');

GO

CREATE TABLE [references].[RefTransactionCode] (
    [code] varchar(2) NOT NULL,
    [rc] varchar(64) NULL,
    [description] varchar(256) NULL,
    CONSTRAINT [PK_RefTransactionCode] PRIMARY KEY ([code])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210519100253_AddReferencesSchema', N'3.1.8');

GO

