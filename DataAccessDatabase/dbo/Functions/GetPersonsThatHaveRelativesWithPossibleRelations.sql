CREATE FUNCTION [dbo].[GetPersonsThatHaveRelativesWithPossibleRelations]
(
)
RETURNS @ReturnTable TABLE
(
	[Key] nvarchar(MAX)
	,[Value] nvarchar(MAX)
)
AS 
BEGIN

	DECLARE @PersonId nvarchar(36)
	DECLARE @table table (Id nvarchar(36))
	DECLARE @result table (Id nvarchar(36))

	DECLARE db_cursor CURSOR FOR 
	SELECT InviterId
	FROM [PersonRelation]
	GROUP BY InviterId

	OPEN db_cursor
	FETCH NEXT FROM db_cursor
	INTO @PersonId

	WHILE @@FETCH_STATUS = 0   
	BEGIN
		DELETE FROM @table

		INSERT INTO @table
		SELECT InvitedId
		FROM [PersonRelation]
		WHERE InviterId = @PersonId

		INSERT INTO @result
		SELECT 
			@PersonId
		FROM [PersonRelation] AS pr
		JOIN [Person] AS pe
			ON pr.InviterId = pe.Id
		WHERE 
			pr.InviterId IN (SELECT * FROM @table)
			AND NOT pr.InvitedId = @PersonId
			AND NOT pr.InvitedId IN (SELECT * FROM @table)

		FETCH NEXT FROM db_cursor
		INTO @PersonId
	END

	CLOSE db_cursor
	DEALLOCATE db_cursor

	INSERT INTO @ReturnTable
	SELECT 
		re.Id AS 'Key'
		,ISNULL(pe.Firstname, '') + ' ' + ISNULL(pe.Lastname, '') + ' ' + ISNULL(pe.Patronym, '') AS 'Value'
	FROM @result AS re
	JOIN [Person] AS pe
		ON re.Id = pe.Id
	GROUP BY 
		re.Id
		,pe.Firstname
		,pe.Lastname
		,pe.Patronym

	RETURN

END

