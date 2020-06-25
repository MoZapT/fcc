CREATE TABLE [dbo].[PersonFileContent] (
    [Id]            NVARCHAR (128) NOT NULL,
    [PersonId]      NVARCHAR (128) NOT NULL,
    [FileContentId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_PersonFileContent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonFileContent_FileContent] FOREIGN KEY ([FileContentId]) REFERENCES [dbo].[FileContent] ([Id]),
    CONSTRAINT [FK_PersonFileContent_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
);

