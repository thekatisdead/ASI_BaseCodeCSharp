CREATE TABLE [dbo].[Applicant] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (50) NOT NULL,
    [LastName]     NVARCHAR (50) NOT NULL,
    [EmailAddress] NVARCHAR (50) NOT NULL,
    [JobApplied]   INT NOT NULL,
    [Tracker]      NVARCHAR (50) NOT NULL,
	[Grading]      NVARCHAR (50) NOT NULL,
    [CreatedTime]  DATETIME2 (7) NULL,
    [CreatedBy]    NVARCHAR (50) NULL,
    [UpdatedTime]  DATETIME2 (7) NULL,
    [UpdatedBy]    NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

	