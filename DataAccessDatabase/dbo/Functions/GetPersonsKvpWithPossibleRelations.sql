CREATE FUNCTION [dbo].[GetPersonsKvpWithPossibleRelations]
(
	@PersonId nvarchar(128)
)
RETURNS @ReturnTable TABLE
(
    [Key] nvarchar(MAX),
    [Value] nvarchar(MAX)
)
AS 
BEGIN

    DECLARE @table table (Id nvarchar(128))

    INSERT INTO @table
    SELECT InvitedId
    FROM [PersonRelation]
    WHERE InviterId = @PersonId

    INSERT INTO @ReturnTable
    SELECT 
	    pr.InviterId AS 'Key'
	    ,ISNULL(pe.Firstname, '') + ' ' + ISNULL(pe.Lastname, '') + ' ' + ISNULL(pe.Patronym, '') AS 'Value'
    FROM [PersonRelation] AS pr
    JOIN [Person] AS pe
	    ON pr.InviterId = pe.Id
    WHERE 
	    pr.InviterId IN (SELECT * FROM @table)
	    AND NOT pr.InvitedId = @PersonId
	    AND NOT pr.InvitedId IN (SELECT * FROM @table)
    GROUP BY
	    pr.InviterId
	    ,pe.Firstname
	    ,pe.Lastname
	    ,pe.Patronym

	RETURN

END

