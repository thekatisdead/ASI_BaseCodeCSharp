CREATE TABLE [dbo].[Interviewer] (
    [InterviewerId] INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]     VARCHAR (20)   NOT NULL,
    [LastName]      VARCHAR (20)   NOT NULL,
    [Email]         NVARCHAR (MAX) NOT NULL,
    [ContactNo]     VARCHAR (20)   NOT NULL,
    [CreatedTime]   DATETIME2 (7)  NULL,
    [CreatedBy]     NVARCHAR (50)  NULL,
    [UpdatedTime]   DATETIME2 (7)  NULL,
    [UpdatedBy]     NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([InterviewerId] ASC)
);


