CREATE TABLE [dbo].[JobPosting]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Position] NVARCHAR(50) NOT NULL, 
    [JobType] NVARCHAR(50) NOT NULL, 
    [Salary] FLOAT NOT NULL, 
    [Hours] INT NOT NULL, 
    [Shift] DATETIME2 NOT NULL, 
    [CreatedTime] DATETIME2 NULL, 
    [CreatedBy] NVARCHAR(50) NULL, 
    [UpdatedTime] DATETIME2 NULL, 
    [UpdatedBy] NVARCHAR(50) NULL
)
