CREATE FUNCTION [dbo].[ReadAllPersonRelationsThatArePossible]
(
	@PersonId nvarchar(128),
	@InviterId nvarchar(128)
)
RETURNS @ReturnTable TABLE
(
    [Id] nvarchar(128),
    [InviterId] nvarchar(128),
    [InvitedId] nvarchar(128),
    [RelationType] int,
    [DateCreated] datetime2(7),
    [DateModified] datetime2(7),
    [IsActive] bit
)
AS 
BEGIN

    DECLARE @table table (Id nvarchar(36))

    INSERT INTO @table
    SELECT InvitedId
    FROM [PersonRelation]
    WHERE 
	    InviterId = @PersonId
	    AND NOT InvitedId = @InviterId

	INSERT INTO @ReturnTable
    SELECT *
    FROM [PersonRelation]
    WHERE 
	    InviterId = @InviterId
	    AND NOT InvitedId IN (SELECT * FROM @table)
        AND NOT InvitedId = @PersonId

	RETURN

END

