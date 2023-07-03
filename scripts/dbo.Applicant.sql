CREATE TABLE [dbo].[Applicant] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (50) NOT NULL,
    [LastName]     NVARCHAR (50) NOT NULL,
    [EmailAddress] NVARCHAR (50) NOT NULL,
    [JobApplied]   NVARCHAR (50) NOT NULL,
    [Tracker]      NVARCHAR (50) NOT NULL,
	[CreatedTime] DATETIME2 NULL, 
    [CreatedBy] NVARCHAR(50) NULL, 
    [UpdatedTime] DATETIME2 NULL, 
    [UpdatedBy] NVARCHAR(50) NULL
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

