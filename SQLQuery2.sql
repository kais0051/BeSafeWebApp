USE [BeSafeDB]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 15-01-2021 20:31:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](250) NULL,
	[ParentCategoryId] [bigint] NULL,
	[Remarks] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MasterItemsSet]    Script Date: 15-01-2021 20:31:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterItemsSet](
	[ItemId] [bigint] IDENTITY(1,1) NOT NULL,
	[CategoryId] [bigint] NOT NULL,
	[ItemType] [nvarchar](250) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ItemLink] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_MasterItemsSet_CreatedDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_MasterItemsSet] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 15-01-2021 20:31:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](250) NOT NULL,
	[Password] [nvarchar](250) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[ContactNumber] [nvarchar](250) NULL,
	[Phone] [nvarchar](250) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_User_CreatedDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [ParentCategoryId], [Remarks]) VALUES (1, N'test3', NULL, NULL)
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [ParentCategoryId], [Remarks]) VALUES (2, N'SubTest', 1, NULL)
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [ParentCategoryId], [Remarks]) VALUES (3, N'Sub-SubTest', 2, NULL)
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [ParentCategoryId], [Remarks]) VALUES (4, N'Sub-Sub-SubTest', 3, NULL)
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [ParentCategoryId], [Remarks]) VALUES (5, N'Test1', NULL, NULL)
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [ParentCategoryId], [Remarks]) VALUES (6, N'SubTest1', 5, NULL)
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [ParentCategoryId], [Remarks]) VALUES (7, N'Sub-SubTest12', 6, NULL)
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [ParentCategoryId], [Remarks]) VALUES (8, N'Test2', NULL, N'test2')
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [ParentCategoryId], [Remarks]) VALUES (10, N'SubTest1edit', 1, N'SubTest1edit')
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [ParentCategoryId], [Remarks]) VALUES (11, N'Testgg', 1, N'Test')
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[MasterItemsSet] ON 

GO
INSERT [dbo].[MasterItemsSet] ([ItemId], [CategoryId], [ItemType], [Name], [Description], [ItemLink], [CreatedDate]) VALUES (3, 1, N'Image', N'Doc1', NULL, N'H:\Upwork\i need help in .net mvc app_14903055\BeSafeWebApp\BeSafeWebApp\wwwroot\UploadedMasterItem\cancel cheque_c9b6.jpg', CAST(N'2021-01-15 11:48:59.413' AS DateTime))
GO
INSERT [dbo].[MasterItemsSet] ([ItemId], [CategoryId], [ItemType], [Name], [Description], [ItemLink], [CreatedDate]) VALUES (4, 1, N'Image', N'Doc1', NULL, N'PanCard_d819.jpg', CAST(N'2021-01-15 12:12:30.883' AS DateTime))
GO
INSERT [dbo].[MasterItemsSet] ([ItemId], [CategoryId], [ItemType], [Name], [Description], [ItemLink], [CreatedDate]) VALUES (5, 1, N'Image', N'Pan Card', NULL, N'PanCard_ad76.jpg', CAST(N'2021-01-15 19:47:35.173' AS DateTime))
GO
INSERT [dbo].[MasterItemsSet] ([ItemId], [CategoryId], [ItemType], [Name], [Description], [ItemLink], [CreatedDate]) VALUES (6, 2, N'Document', N'Aadhar Card', NULL, N'adhar_78d5.pdf', CAST(N'2021-01-15 19:48:14.580' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[MasterItemsSet] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

GO
INSERT [dbo].[User] ([ID], [UserName], [Password], [Name], [Email], [ContactNumber], [Phone], [IsActive], [CreatedDate]) VALUES (1, N'admin', N'admin', N'Admin', N'admin@abc.com', N'123456789', N'123456789', 1, CAST(N'2021-01-12 08:24:48.337' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[MasterItemsSet]  WITH CHECK ADD  CONSTRAINT [FK_MasterItemsSet_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MasterItemsSet] CHECK CONSTRAINT [FK_MasterItemsSet_Categories]
GO
