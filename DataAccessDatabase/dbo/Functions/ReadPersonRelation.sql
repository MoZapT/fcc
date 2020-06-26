CREATE FUNCTION [dbo].[ReadPersonRelation]
(
	@InviterId nvarchar(128)
	,@InvitedId nvarchar(128)
	,@RelationType int
)
RETURNS TABLE
AS
RETURN 
	SELECT *
	FROM [PersonRelation]
	WHERE 
		IsActive = 1
		AND (InviterId = @InviterId OR @InviterId IS NULL)
		AND (InvitedId = @InvitedId OR @InvitedId IS NULL)
		AND (RelationType = @RelationType OR @RelationType IS NULL)