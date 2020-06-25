CREATE TABLE [dbo].[PersonRelation] (
    [Id]           NVARCHAR (128) NOT NULL,
    [InviterId]    NVARCHAR (128) NOT NULL,
    [InvitedId]    NVARCHAR (128) NOT NULL,
    [RelationType] INT            NOT NULL,
    [DateCreated]  DATETIME2 (7)  NOT NULL,
    [DateModified] DATETIME2 (7)  NOT NULL,
    [IsActive]     BIT            NOT NULL,
    CONSTRAINT [PK_PersonRelation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonRelation_Person] FOREIGN KEY ([InviterId]) REFERENCES [dbo].[Person] ([Id]),
    CONSTRAINT [FK_PersonRelation_Person1] FOREIGN KEY ([InvitedId]) REFERENCES [dbo].[Person] ([Id])
);

