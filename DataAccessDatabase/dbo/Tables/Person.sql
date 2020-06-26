﻿CREATE TABLE [dbo].[Person] (
    [Id]            NVARCHAR (128) NOT NULL,
    [DateCreated]   DATETIME2 (7)  NOT NULL,
    [DateModified]  DATETIME2 (7)  NOT NULL,
    [IsActive]      BIT            NOT NULL,
    [FileContentId] NVARCHAR (128) NULL,
    [BirthDate]     DATETIME2 (7)  NULL,
    [DeathDate]     DATETIME2 (7)  NULL,
    [Sex]           BIT            NULL,
    [Firstname]     NVARCHAR (255) NOT NULL,
    [Lastname]      NVARCHAR (255) NULL,
    [Patronym]      NVARCHAR (255) NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Person_FileContent] FOREIGN KEY ([FileContentId]) REFERENCES [dbo].[FileContent] ([Id])
);


GO
