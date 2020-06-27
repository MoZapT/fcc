CREATE FUNCTION [dbo].[CheckIfSameRelationsAvaible]
(
	@PersonId nvarchar(128)
)
RETURNS BIT
AS
BEGIN

    DECLARE @table table (Id nvarchar(36))
    DECLARE @result table (Id nvarchar(36))

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

    RETURN (SELECT TOP 1
	    CASE 
		    WHEN COUNT(Id) > 0 THEN 1
		    ELSE 0
	    END AS Avaible
    FROM @result)

END
