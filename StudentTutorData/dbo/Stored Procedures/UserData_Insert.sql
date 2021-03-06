﻿CREATE PROCEDURE [dbo].[UserData_Insert]
	@Id nvarchar(255),
	@LastName nvarchar(255),
	@FirstName nvarchar(255),
	@Address nvarchar(MAX),
	--@SubjectInterest nvarchar(MAX),
	@Passport image,
	@EmailAddress nvarchar(255),
	@CreatedDate datetime2
AS
BEGIN
	SET NOCOUNT ON;
    INSERT INTO dbo.UserData(Id, LastName, FirstName, Address, Passport, EmailAddress, CreatedDate )
	VALUES (@Id, @LastName, @FirstName, @Address, @Passport, @EmailAddress, @CreatedDate);
END
