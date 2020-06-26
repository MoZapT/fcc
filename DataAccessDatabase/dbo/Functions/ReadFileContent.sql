CREATE FUNCTION [dbo].[ReadFileContent]
(
)
RETURNS TABLE
AS
RETURN
	SELECT *
    FROM [FileContent]
	WHERE IsActive = 1
