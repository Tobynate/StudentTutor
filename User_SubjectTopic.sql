CREATE TABLE [dbo].[User_SubjectOfInterest] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [UserId]         NVARCHAR (255) NOT NULL,
    [SubjectTopicId] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_SubjectOfInterest_UserData] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserData] ([Id]),
    CONSTRAINT [FK_User_SubjectOfInterest_SubjectOfInterest] FOREIGN KEY ([SubjectTopicId]) REFERENCES [dbo].[SubjectOfInterest] ([Id])
);