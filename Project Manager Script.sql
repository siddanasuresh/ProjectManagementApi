USE [ProjectManagement]
GO
/****** Object:  Table [dbo].[User]    Script Date: 05/12/2019 18:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[User_Id] [int] IDENTITY(1,1) NOT NULL,
	[Employee_Id] [int] NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([User_Id], [Employee_Id], [FirstName], [LastName]) VALUES (1, 415511, N'Suresh', N'Siddana')
INSERT [dbo].[User] ([User_Id], [Employee_Id], [FirstName], [LastName]) VALUES (2, 1224326, N'Lyft', N'INC')
INSERT [dbo].[User] ([User_Id], [Employee_Id], [FirstName], [LastName]) VALUES (3, 589698, N'CTS', N'CORP')
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[ParentTaskDetails]    Script Date: 05/12/2019 18:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ParentTaskDetails](
	[Parent_ID] [int] NOT NULL,
	[Parent_Task] [varchar](200) NOT NULL,
 CONSTRAINT [PK_ParentTask] PRIMARY KEY CLUSTERED 
(
	[Parent_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Project]    Script Date: 05/12/2019 18:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Project_Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [bit] NOT NULL,
	[End_Date] [datetime2](7) NULL,
	[Priority] [int] NOT NULL,
	[Project] [nvarchar](100) NOT NULL,
	[Start_Date] [datetime2](7) NULL,
	[User_Id] [int] NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[Project_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Project] ON
INSERT [dbo].[Project] ([Project_Id], [Status], [End_Date], [Priority], [Project], [Start_Date], [User_Id]) VALUES (4, 1, CAST(0x070000000000EB3F0B AS DateTime2), 9, N'Health care', CAST(0x070000000000923F0B AS DateTime2), 1)
INSERT [dbo].[Project] ([Project_Id], [Status], [End_Date], [Priority], [Project], [Start_Date], [User_Id]) VALUES (5, 1, CAST(0x070000000000CD3F0B AS DateTime2), 9, N'Insurance', CAST(0x070000000000933F0B AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Project] OFF
/****** Object:  Table [dbo].[Task]    Script Date: 05/12/2019 18:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Task_Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [bit] NOT NULL,
	[End_Date] [datetime2](7) NULL,
	[Task] [nvarchar](100) NOT NULL,
	[ParentId] [int] NULL,
	[Priority] [int] NOT NULL,
	[Project_Id] [int] NOT NULL,
	[Start_Date] [datetime2](7) NULL,
	[User_Id] [int] NOT NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Task_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Task] ON
INSERT [dbo].[Task] ([Task_Id], [Status], [End_Date], [Task], [ParentId], [Priority], [Project_Id], [Start_Date], [User_Id]) VALUES (1, 1, CAST(0x070000000000963F0B AS DateTime2), N'Create Project', NULL, 1, 4, CAST(0x070000000000923F0B AS DateTime2), 1)
INSERT [dbo].[Task] ([Task_Id], [Status], [End_Date], [Task], [ParentId], [Priority], [Project_Id], [Start_Date], [User_Id]) VALUES (2, 1, CAST(0x070000000000973F0B AS DateTime2), N'nunit tests', NULL, 2, 5, CAST(0x070000000000933F0B AS DateTime2), 2)
SET IDENTITY_INSERT [dbo].[Task] OFF
/****** Object:  ForeignKey [FK_Project_User_User_Id]    Script Date: 05/12/2019 18:27:36 ******/
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_User_User_Id] FOREIGN KEY([User_Id])
REFERENCES [dbo].[User] ([User_Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_User_User_Id]
GO
/****** Object:  ForeignKey [FK_Task_Project_Project_Id]    Script Date: 05/12/2019 18:27:36 ******/
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Project_Project_Id] FOREIGN KEY([Project_Id])
REFERENCES [dbo].[Project] ([Project_Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Project_Project_Id]
GO
/****** Object:  ForeignKey [FK_Task_User_User_Id]    Script Date: 05/12/2019 18:27:36 ******/
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_User_User_Id] FOREIGN KEY([User_Id])
REFERENCES [dbo].[User] ([User_Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_User_User_Id]
GO
