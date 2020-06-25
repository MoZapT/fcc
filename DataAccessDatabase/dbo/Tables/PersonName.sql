CREATE TABLE [dbo].[PersonName] (
    [Id]              NVARCHAR (128) NOT NULL,
    [PersonId]        NVARCHAR (128) NULL,
    [DateCreated]     DATETIME2 (7)  NOT NULL,
    [DateModified]    DATETIME2 (7)  NOT NULL,
    [DateNameChanged] DATETIME2 (7)  NOT NULL,
    [IsActive]        BIT            NOT NULL,
    [Firstname]       NVARCHAR (255) NULL,
    [Lastname]        NVARCHAR (255) NULL,
    [Patronym]        NVARCHAR (255) NULL,
    CONSTRAINT [PK_PersonName] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonName_PersonName] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
);

