CREATE TABLE [dbo].[CurrentHires] (
    [Id]          INT Identity (1,1) NOT NULL,
    [ApplicantID] INT           NULL,
    [JobID]       INT           NULL,
    [CreatedTime] DATETIME      NULL,
    [CreatedBy]   NVARCHAR (50) NULL,
    [UpdatedTime] DATETIME      NULL,
    [UpdatedBy]   NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

