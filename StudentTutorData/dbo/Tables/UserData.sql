﻿CREATE TABLE [dbo].[UserData]
(
	[Id] NVARCHAR(255) NOT NULL PRIMARY KEY, 
    [LastName] NVARCHAR(255) NOT NULL, 
    [FirstName] NVARCHAR(255) NOT NULL, 
    [Address] NVARCHAR(MAX) NOT NULL, 
    [Passport] IMAGE NULL, 
    [EmailAddress] NVARCHAR(255) NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate()
)
