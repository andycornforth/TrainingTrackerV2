﻿CREATE TABLE [dbo].[Log]
(
	[LogId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[UserId] INT NOT NULL,
    [Title] NVARCHAR(20) NOT NULL, 
    [DateAdded] DATETIME NOT NULL, 
    CONSTRAINT [FK_Log_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId])
)
