CREATE PROCEDURE [dbo].[Insert_PersonDocument]
    @Id NVARCHAR(128),
    @DateCreated DATETIME2(7),
    @DateModified DATETIME2(7),
    @IsActive BIT,
    @BinaryContent VARBINARY(MAX),
    @FileType NVARCHAR(250),
    @Name NVARCHAR(250),
    @PersonId nvarchar(128),
    @Category nvarchar(500),
    @ActivityId nvarchar(128),
    @RetVal nvarchar(128) OUTPUT
AS

DECLARE @FileContentId nvarchar(128) = NEWID()
DECLARE @tmp table (Id nvarchar(128))

BEGIN TRAN

INSERT INTO [dbo].[FileContent]
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

INSERT INTO [dbo].[PersonDocument]
    ([Id]
    ,[FileContentId]
	,[PersonActivityId])
OUTPUT INSERTED.Id INTO @tmp
VALUES
    (@RetVal
    ,@Id
	,@ActivityId)

COMMIT TRAN

RETURN
