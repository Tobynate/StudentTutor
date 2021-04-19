CREATE TABLE [dbo].[User_SubjectOfInterest]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [UserId] NVARCHAR(255) NOT NULL, 
    [SubjectTopicId] INT NOT NULL, 
    CONSTRAINT [FK_User_SubjectOfInterest_UserData] FOREIGN KEY (UserId) REFERENCES UserData(Id), 
    CONSTRAINT [FK_User_SubjectOfInterest_SubjectOfInterest] FOREIGN KEY (SubjectTopicId) REFERENCES SubjectOfInterest(id)
)
