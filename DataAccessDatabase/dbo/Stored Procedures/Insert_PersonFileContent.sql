CREATE PROCEDURE [dbo].[Insert_PersonFileContent]
	@PersonId NVARCHAR(128),
    @Id NVARCHAR(128),
    @DateCreated DATETIME2(7),
    @DateModified DATETIME2(7),
    @IsActive BIT,
    @BinaryContent VARBINARY(MAX),
    @FileType NVARCHAR(250),
    @Name NVARCHAR(250)
AS

SET @Id = NEWID()
DECLARE @tmp table (Id nvarchar(128))

BEGIN TRAN

INSERT INTO [FileContent]
    ([Id]
    ,[DateCreated]
    ,[DateModified]
    ,[IsActive]
    ,[BinaryContent]
    ,[FileType]
    ,[Name])
VALUES
    (@Id
    ,@DateCreated
    ,@DateModified
    ,@IsActive
    ,@BinaryContent
    ,@FileType
    ,@Name)

INSERT INTO [dbo].[PersonFileContent]
    ([Id]
    ,[PersonId]
    ,[FileContentId])
OUTPUT INSERTED.Id INTO @tmp
VALUES
    (NEWID()
    ,@PersonId
    ,@Id)

COMMIT TRAN

RETURN SELECT * FROM @tmp
