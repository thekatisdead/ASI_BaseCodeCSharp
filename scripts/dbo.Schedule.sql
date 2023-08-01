CREATE TABLE [dbo].[Schedule] (
    [ScheduleId]    INT            IDENTITY (1, 1) NOT NULL,
    [InterviewerId] INT            NOT NULL,
    [JobId]         INT            NOT NULL,
    [StartTime]     TIME (7)       NOT NULL,
    [EndTime]       TIME (7)       NOT NULL,
    [Date]          DATE           NOT NULL,
    [Instruction]   NVARCHAR (MAX) NULL,
    [CreatedTime]   DATETIME2 (7)  NULL,
    [CreatedBy]     NVARCHAR (50)  NULL,
    [UpdatedTime]   DATETIME2 (7)  NULL,
    [UpdatedBy]     NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([ScheduleId] ASC),
    FOREIGN KEY ([JobId]) REFERENCES [dbo].[JobOpening] ([Id]),
    FOREIGN KEY ([InterviewerId]) REFERENCES [dbo].[Interviewer] ([InterviewerId])
);


