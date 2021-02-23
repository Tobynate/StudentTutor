CREATE PROCEDURE [dbo].[UserData_GetUser]
	@Id nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM dbo.UserData 
	WHERE @Id = Id;
END
