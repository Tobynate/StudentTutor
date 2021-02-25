CREATE PROCEDURE [dbo].[UserData_Passport_Update]
	@Id nvarchar(255),
	@Passport image
AS
BEGIN
	SET NOCOUNT ON
	UPDATE dbo.UserData
	SET Passport = @Passport
	WHERE Id = @Id;

END
