CREATE TABLE [dbo].[FileContent] (
    [Id]            NVARCHAR (128)  NOT NULL,
    [DateCreated]   DATETIME2 (7)   NOT NULL,
    [DateModified]  DATETIME2 (7)   NOT NULL,
    [IsActive]      BIT             NOT NULL,
    [BinaryContent] VARBINARY (MAX) NOT NULL,
    [FileType]      NVARCHAR (250)  NOT NULL,
    [Name]          NVARCHAR (250)  NOT NULL,
    CONSTRAINT [PK_FileContent] PRIMARY KEY CLUSTERED ([Id] ASC)
);