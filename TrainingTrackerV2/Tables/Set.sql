CREATE TABLE [dbo].[Set]
(
	[SetId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LogId] INT NOT NULL, 
    [ExerciseId] INT NOT NULL, 
    [Weight] FLOAT NULL, 
    [Reps] INT NOT NULL, 
    [DateAdded] DATETIME NOT NULL, 
    [Position] INT NOT NULL, 
    CONSTRAINT [FK_Set_Log] FOREIGN KEY ([LogId]) REFERENCES [Log]([LogId]),
	CONSTRAINT [FK_Set_Exercise] FOREIGN KEY ([ExerciseId]) REFERENCES [Exercise]([ExerciseId])
)
