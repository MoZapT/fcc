CREATE PROCEDURE [dbo].[Update_Person]
    @Id nvarchar(128)
    ,@Sex bit
    ,@BirthDate datetime2(7)
    ,@DeathDate datetime2(7)
    ,@IsActive bit
    ,@DateCreated datetime2(7)
    ,@DateModified datetime2(7)
    ,@Firstname nvarchar(255)
    ,@Lastname nvarchar(255)
    ,@Patronym nvarchar(255)
    ,@Name nvarchar(MAX)
    ,@HasBirthDate bit
    ,@HasDeathDate bit
    ,@IsMarried bit
    ,@IsInPartnership bit
    ,@FileContentId nvarchar(128)
AS

DECLARE @NameChanged bit = 
(SELECT
CASE WHEN NOT ([Firstname] = @Firstname 
	AND [Lastname] = @Lastname 
	AND [Patronym] = @Patronym) THEN 1
ELSE 0 END AS NameChanged
FROM [Person]
WHERE Id = @Id)

BEGIN TRAN

    IF @NameChanged = 1
    BEGIN
    INSERT INTO [PersonName]
            ([Id]
            ,[PersonId]
            ,[DateNameChanged]
            ,[DateCreated]
            ,[DateModified]
            ,[IsActive]
            ,[Firstname]
            ,[Lastname]
            ,[Patronym])
        VALUES
            (NEWID()
            ,@Id
            ,GETDATE()
            ,GETDATE()
            ,GETDATE()
            ,1
            ,(SELECT Firstname FROM [Person] WHERE Id = @Id)
            ,(SELECT Lastname FROM [Person] WHERE Id = @Id)
            ,(SELECT Patronym FROM [Person] WHERE Id = @Id))
    END

    UPDATE [Person]
        SET [Sex] = @Sex
            ,[BirthDate] = @BirthDate
            ,[DeathDate] = @DeathDate
            ,[IsActive] = @IsActive
            ,[DateCreated] = @DateCreated
            ,[DateModified] = @DateModified
            ,[Firstname] = @Firstname
            ,[Lastname] = @Lastname
            ,[Patronym] = @Patronym
            ,[FileContentId] = @FileContentId
        WHERE Id = @Id

COMMIT TRAN
