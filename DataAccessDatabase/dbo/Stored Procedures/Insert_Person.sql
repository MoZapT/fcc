
CREATE PROCEDURE Insert_Person 
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
BEGIN
    DECLARE @tmp table (Id nvarchar(128))

    INSERT INTO [Person]
                ([Id]
                ,[Sex]
                ,[BirthDate]
                ,[DeathDate]
                ,[IsActive]
                ,[DateCreated]
                ,[DateModified]
                ,[Firstname]
                ,[Lastname]
                ,[Patronym])
	        OUTPUT INSERTED.Id INTO @tmp
            VALUES
                (@Id
                ,@Sex
                ,@BirthDate
                ,@DeathDate
                ,@IsActive
                ,@DateCreated
                ,@DateModified
                ,@Firstname
                ,@Lastname
                ,@Patronym)
END

RETURN SELECT * FROM @tmp