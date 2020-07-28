CREATE TABLE [dbo].[PersonDocument] (
    [Id]               NVARCHAR (128) NOT NULL,
    [BiographyId]      NVARCHAR (128) NOT NULL,
    [PersonActivityId] NVARCHAR (128) NULL,
    [FileContentId]    NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_PersonDocument] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonDocument_FileContent] FOREIGN KEY ([FileContentId]) REFERENCES [dbo].[FileContent] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PersonDocument_Person] FOREIGN KEY ([BiographyId]) REFERENCES [dbo].[Person] ([Id]) ON DELETE CASCADE
);



