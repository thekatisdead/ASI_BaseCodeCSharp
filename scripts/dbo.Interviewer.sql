USE [HRAutomationSystem]
GO

/****** Object: Table [dbo].[Interviewer] Script Date: 21/07/2023 12:13:15 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Interviewer] (
    [InterviewerId] INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]     VARCHAR (20)   NOT NULL,
    [LastName]      VARCHAR (20)   NOT NULL,
    [Email]         NVARCHAR (MAX) NOT NULL,
    [ContactNo]     VARCHAR (20)   NOT NULL,
    [CreatedTime]   DATETIME2 (7)  NULL,
    [CreatedBy]     NVARCHAR (50)  NULL,
    [UpdatedTime]   DATETIME2 (7)  NULL,
    [UpdatedBy]     NVARCHAR (50)  NULL
);


