CREATE TABLE [dbo].[PersonBiography] (
    [Id]            NVARCHAR (128) NOT NULL,
    [PersonId]      NVARCHAR (128) NOT NULL,
    [BiographyText] NVARCHAR (MAX) NULL,
    [DateCreated]   DATETIME2 (7)  NOT NULL,
    [DateModified]  DATETIME2 (7)  NOT NULL,
    [IsActive]      BIT            NOT NULL,
    CONSTRAINT [PK_PersonBiography] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonBiography_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
);

