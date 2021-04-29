CREATE PROCEDURE [dbo].[SubjectOfInterest_GetAll]
	
AS
BEGIN
	SET NOCOUNT ON;
	SELECT dbo.SubjectOfInterest.Id, dbo.SubjectOfInterest.Note, dbo.SubjectOfInterest.SubjectTopics FROM dbo.SubjectOfInterest;
END
