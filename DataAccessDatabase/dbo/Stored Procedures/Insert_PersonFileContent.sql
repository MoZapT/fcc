CREATE PROCEDURE [dbo].[Insert_PersonFileContent]
	@PersonId NVARCHAR(128),
    @Id NVARCHAR(128),
    @DateCreated DATETIME2(7),
    @DateModified DATETIME2(7),
    @IsActive BIT,
    @BinaryContent VARBINARY(MAX),
    @FileType NVARCHAR(250),
    @Name NVARCHAR(250),
    @RetVal nvarchar(128) OUTPUT
AS

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

SET @RetVal = NEWID()

INSERT INTO [PersonPhoto]
    ([Id]
    ,[PersonId]
    ,[FileContentId])
OUTPUT inserted.Id
VALUES
    (@RetVal
    ,@PersonId
    ,@Id)

UPDATE [Person]
SET FileContentId = @Id
WHERE Id = @PersonId
    AND FileContentId IS NULL

COMMIT TRAN

RETURN