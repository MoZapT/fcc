﻿CREATE TABLE [dbo].[PersonDocument] (
    [Id]               NVARCHAR (128) NOT NULL,
    [PersonId]         NVARCHAR (128) NOT NULL,
    [FileContentId]    NVARCHAR (128) NOT NULL,
    [PersonActivityId] NVARCHAR (128) NULL,
    [CategoryName]     NVARCHAR (500) NOT NULL,
    CONSTRAINT [PK_PersonDocument] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonDocument_FileContent] FOREIGN KEY ([FileContentId]) REFERENCES [dbo].[FileContent] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PersonDocument_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PersonDocument_PersonActivity] FOREIGN KEY ([PersonActivityId]) REFERENCES [dbo].[PersonActivity] ([Id]) ON DELETE CASCADE
);

