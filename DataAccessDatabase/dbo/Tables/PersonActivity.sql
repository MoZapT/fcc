CREATE TABLE [dbo].[PersonActivity] (
    [Id]           NVARCHAR (128) NOT NULL,
    [BiographyId]  NVARCHAR (128) NOT NULL,
    [DateCreated]  DATETIME2 (7)  NOT NULL,
    [DateModified] DATETIME2 (7)  NOT NULL,
    [IsActive]     BIT            NOT NULL,
    [Activity]     NVARCHAR (500) NOT NULL,
    [ActivityType] INT            NOT NULL,
    [HasBegun]     BIT            NOT NULL,
    [HasEnded]     BIT            NOT NULL,
    [DateBegin]    DATETIME2 (7)  NULL,
    [DateEnd]      DATETIME2 (7)  NULL,
    CONSTRAINT [PK_PersonActivity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonActivity_PersonBiography] FOREIGN KEY ([BiographyId]) REFERENCES [dbo].[PersonBiography] ([Id])
);

