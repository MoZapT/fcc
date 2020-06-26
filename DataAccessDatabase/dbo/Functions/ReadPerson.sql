CREATE FUNCTION ReadPerson
(
	@PersonId nvarchar(128)
	,@MarriedType int
	,@InRelationType int
	,@Search nvarchar(MAX)
)
RETURNS @PersonTable TABLE
(
    [Id]            NVARCHAR (128),
    [DateCreated]   DATETIME2 (7),
    [DateModified]  DATETIME2 (7),
    [IsActive]      BIT,
    [FileContentId] NVARCHAR (128),
    [BirthDate]     DATETIME2 (7),
    [DeathDate]     DATETIME2 (7),
    [Sex]           BIT,
    [Firstname]     NVARCHAR (255),
    [Lastname]      NVARCHAR (255),
    [Patronym]      NVARCHAR (255),
	[IsMarried] BIT,
	[IsInPartnership] BIT
) 
AS
BEGIN

    IF (@MarriedType IS NULL OR @InRelationType IS NULL)
	BEGIN
         INSERT INTO @PersonTable SELECT
		 	per.*
			,IsMarried = 0
			,IsInPartnership = 0
		FROM [Person] AS per
		WHERE 
			IsActive = 1
			AND (Id = @PersonId OR ISNULL(@PersonId, '') = '')
			AND ((Firstname LIKE '%'+@Search+'%' OR LastName LIKE '%'+@Search+'%' OR Patronym LIKE '%'+@Search+'%') OR ISNULL(@Search, '') = '')
	END

    ELSE
	BEGIN
		INSERT INTO @PersonTable SELECT 
			per.*
			,IsMarried = (SELECT COUNT(Id)
							FROM [PersonRelation]
							WHERE 
								InviterId = per.Id
							AND RelationType = @MarriedType)
			,IsInPartnership = (SELECT COUNT(Id)
								FROM [PersonRelation]
								WHERE 
									InviterId = per.Id
								AND RelationType = @InRelationType)
		FROM [Person] AS per
		WHERE 
			IsActive = 1
			AND (Id = @PersonId OR ISNULL(@PersonId, '') = '')
			AND ((Firstname LIKE '%'+@Search+'%' OR LastName LIKE '%'+@Search+'%' OR Patronym LIKE '%'+@Search+'%') OR ISNULL(@Search, '') = '')
	END

    RETURN

END